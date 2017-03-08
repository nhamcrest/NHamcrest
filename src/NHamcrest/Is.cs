using System;
using NHamcrest.Core;

namespace NHamcrest
{
    public static class Is
    {
        public static IMatcher<T> Null<T>()
        {
            return new IsNullMatcher<T>();
        }

        public static IMatcher<object> Null()
        {
            return new IsNullMatcher();
        }

        public static IMatcher<T> NotNull<T>()
        {
            return Not(Null<T>());
        }

        public static IMatcher<object> NotNull()
        {
            return Not(Null());
        }

        public static IMatcher<T> LessThanOrEqualTo<T>(T value) where T : IComparable<T>
        {
            return new IsLessThanOrEqualTo<T>(value);
        }

        public static IMatcher<T> LessThan<T>(T value) where T : IComparable<T>
        {
            return new IsLessThan<T>(value);
        }

        /**
         * Is the value an instance of a particular type? 
         * This version assumes no relationship between the required type and
         * the signature of the method that sets it up, for example in
         * <code>Assert.That(anObject, Is.InstanceOf(typeof(Thing)));</code>
         */
        public static IMatcher<T> InstanceOf<T>()
        {
            return new IsInstanceOf<T>();
        }

        public static IMatcher<object> InstanceOf(Type expectedType)
        {
            return new IsInstanceOf(expectedType);
        }

        /**
         * Is the value an instance of a particular type? 
         * Use this version to make generics conform, for example in 
         * the JMock clause <code>with(any(Thing.class))</code> 
         */
        public static IMatcher<T> Any<T>()
        {
            return new IsInstanceOf<T>();
        }

        public static IMatcher<T> GreaterThanOrEqualTo<T>(T value) where T : IComparable<T>
        {
            return new IsGreaterThanOrEqualTo<T>(value);
        }

        public static IMatcher<T> GreaterThan<T>(T value) where T : IComparable<T>
        {
            return new IsGreaterThan<T>(value);
        }

        public static IMatcher<T> EqualTo<T>(T value)
        {
            return IsEqual<T>.EqualTo(value);
        }

        public static IMatcher<bool> True()
        {
            return IsEqual<bool>.EqualTo(true);
        }

        public static IMatcher<bool> False()
        {
            return IsEqual<bool>.EqualTo(false);
        }

        // This matcher always evaluates to true.
        public static Matcher<object> Anything()
        {
            return new IsAnything<object>();
        }

        /// <summary>
        /// This matcher always evaluates to true.
        /// </summary>
        /// <param name="description">A meaningful string used when describing itself.</param>
        /// <returns>A matcher.</returns>
        public static Matcher<object> Anything(string description)
        {
            return new IsAnything<object>(description);
        }

        // This is a shortcut to the frequently used Is.Not(Equal.To(x)).
        //
        // For example:  Assert.That(cheese, Is.Not(Equal.To(smelly))))
        //         vs.  Assert.That(cheese, Is.Not(smelly)))
        public static IMatcher<T> Not<T>(T value)
        {
            return Not(EqualTo(value));
        }

        public static Matcher<T> Not<T>(IMatcher<T> matcher)
        {
            return new IsNotMatcher<T>(matcher);
        }

        public static IMatcher<T> SameAs<T>(T @object)
        {
            return new IsSameMatcher<T>(@object);
        }
    }
}