using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NHamcrest.Extensions;

namespace NHamcrest.Core
{
    internal class StructuralComparisonMatcher<T> : IMatcher<T>
    {
        private readonly IMatcher<T> _createdMatcher;


        public StructuralComparisonMatcher(T exampleValue)
        {
            _createdMatcher = CreateMatcherForExampleValue(exampleValue);
        }

        private object CreateMatcherForExampleValueUntyped(object value, Type valueType)
        {
            var methodToCall = GetType().GetTypeInfo().GetMethod("CreateMatcherForExampleValue", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(valueType);

            return methodToCall.Invoke(this, new[] { value });
        }

        private bool IsIList(Type t)
        {
            return t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(IList<>);
        }

        private bool ImplementsIList(Type t)
        {
            return IsIList(t) || t.GetTypeInfo().GetInterfaces().Any(IsIList);
        }

        private bool IsIEnumerable(Type t)
        {
            return t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        private bool ImplementsIEnumerable(Type t)
        {
            return IsIEnumerable(t) || t.GetTypeInfo().GetInterfaces().Any(IsIEnumerable);
        }

        private bool IsIDictionary(Type t)
        {
            return t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>);
        }

        private bool ImplementsIDictionary(Type t)
        {
            return IsIDictionary(t) || t.GetTypeInfo().GetInterfaces().Any(IsIDictionary);
        }

        private IMatcher<TMatched> CreateMatcherForExampleValue<TMatched>(TMatched value)
        {
            if (value == null)
            {
                return new IsNullMatcher<TMatched>();
            }
            else if (typeof(TMatched).IsSimpleType())
            {
                return new IsEqualMatcher<TMatched>(value);
            }
            else if (ImplementsIList(typeof(TMatched)))
            {
                return CreateListMatcher<TMatched>((IEnumerable)value);
            }
            else if (ImplementsIDictionary(typeof(TMatched)))
            {
                return CreateDictionaryMatcher<TMatched>((IDictionary)value);
            }
            else if (ImplementsIEnumerable(typeof(TMatched)))
            {
                return CreateListMatcher<TMatched>((IEnumerable)value);
            }
            else
            {
                return CreateComplexObjectMatcher(value);
            }
        }

        private IMatcher<TMatched> CreateDictionaryMatcher<TMatched>(IDictionary value)
        {
            var dictionaryTypes = GetDictionaryElementTypes(value);
            var keyType = dictionaryTypes[0];
            var valueType = dictionaryTypes[1];
            var descriptorType = typeof(EntryDescriptor<,>).MakeGenericType(keyType, valueType);

            var descriptorsUntyped = CreateDictionaryEntryDescriptors(value.GetEnumerator(), keyType, valueType, descriptorType).ToArray();
            var descriptors = Array.CreateInstance(descriptorType, descriptorsUntyped.Length);
            Array.Copy(descriptorsUntyped, descriptors, descriptors.Length);

            var matcherType = typeof(IsEquivalentDictionaryMatcher<,>).MakeGenericType(keyType, valueType);

            return (IMatcher<TMatched>)Activator.CreateInstance(matcherType, descriptors);
        }


        private IMatcher<TMatched> CreateListMatcher<TMatched>(IEnumerable list)
        {
            var elementType = GetEnumerableElementType(list);
            var elementMatchersUntyped = CreateListElementMatchers(list, elementType).ToArray();
            var matcherType = typeof(IsEquivalentListMatcher<>).MakeGenericType(elementType);

            var elementMatcherType = typeof(IMatcher<>).MakeGenericType(elementType);
            var elementMatchers = Array.CreateInstance(elementMatcherType, elementMatchersUntyped.Length);
            Array.Copy(elementMatchersUntyped, elementMatchers, elementMatchers.Length);

            return (IMatcher<TMatched>)Activator.CreateInstance(matcherType, elementMatchers);
        }

        private static Type[] GetDictionaryElementTypes(IDictionary dictionary)
        {
            var dictionaryGenericInterface = dictionary.GetType()
                .GetTypeInfo().GetInterfaces()
                .Single(x => x.IsConstructedGenericType && x.GetGenericTypeDefinition() == typeof(IDictionary<,>));

            return dictionaryGenericInterface.GenericTypeArguments;
        }

        private static Type GetEnumerableElementType(IEnumerable list)
        {
            var listGenericInterface = list.GetType()
                .GetTypeInfo().GetInterfaces()
                .Single(x => x.IsConstructedGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            var elementType = listGenericInterface.GenericTypeArguments[0];
            return elementType;
        }

        private IEnumerable<object> CreateDictionaryEntryDescriptors(IDictionaryEnumerator enumerator, Type keyType, Type valueType, Type descriptorType)
        {
            while (enumerator.MoveNext())
            {
                var entry = enumerator.Entry;

                var keyMatcher = CreateMatcherForExampleValueUntyped(entry.Key, keyType);
                var valueMatcher = CreateMatcherForExampleValueUntyped(entry.Value, valueType);

                yield return Activator.CreateInstance(descriptorType, keyMatcher, valueMatcher);
            }
        }

        private IEnumerable<object> CreateListElementMatchers(IEnumerable list, Type elementType)
        {
            foreach (var item in list)
            {
                yield return CreateMatcherForExampleValueUntyped(item, elementType);
            }
        }

        private IMatcher<TMatched> CreatePropertyMatcher<TMatched>(TMatched exampleObject, PropertyInfo property)
        {
            var propertyPath = MakePropertyPath<TMatched>(property);

            var exampleValue = property.GetValue(exampleObject, null);

            var propertyValueMatcher = CreateMatcherForExampleValueUntyped(exampleValue, property.PropertyType);

            var propertyMatcherType = typeof(PropertyMatcher<,,>)
                .MakeGenericType(typeof(TMatched), property.PropertyType, property.PropertyType);

            return (IMatcher<TMatched>)Activator.CreateInstance(propertyMatcherType, propertyPath, propertyValueMatcher);
        }

        private static LambdaExpression MakePropertyPath<TMatched>(PropertyInfo property)
        {
            var parameter = Expression.Parameter(typeof(TMatched));
            var propertyExpr = Expression.Property(parameter, property);
            return Expression.Lambda(propertyExpr, parameter);
        }

        private IMatcher<TMatched> CreateComplexObjectMatcher<TMatched>(TMatched value)
        {
            return CreateMatchingTypeComplexObjectMatcher(value);
        }

        private IMatcher<TMatched> CreateMatchingTypeComplexObjectMatcher<TMatched>(TMatched value)
        {
            var propertyMatchers = GetComparedProperties(typeof(TMatched))
                .Select(x => CreatePropertyMatcher(value, x));

            return new ObjectFeatureMatcher<TMatched>(value.GetType(), propertyMatchers);
        }

        private static IEnumerable<PropertyInfo> GetComparedProperties(Type type)
        {
            return type.GetTypeInfo()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                .OrderBy(x => x.Name);
        }

        public void DescribeTo(IDescription description)
        {
            _createdMatcher.DescribeTo(description);
        }

        public bool Matches(T item)
        {
            return _createdMatcher.Matches(item);
        }

        public void DescribeMismatch(T item, IDescription mismatchDescription)
        {
            _createdMatcher.DescribeMismatch(item, mismatchDescription);
        }
    }
}