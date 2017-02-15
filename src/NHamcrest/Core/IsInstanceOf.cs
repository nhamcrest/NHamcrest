using System;

namespace NHamcrest.Core
{
    public class IsInstanceOf<T> : DiagnosingMatcher<T>
    {
        private readonly Type expectedType;

        public IsInstanceOf() : this(typeof(T)) { }

        protected IsInstanceOf(Type expectedType)
        {
            this.expectedType = expectedType;
        }
        
        protected override bool Matches(T item, IDescription mismatchDescription)
        {
            if (ReferenceEquals(item, null))
            {
                mismatchDescription.AppendText("null");
                return false;
            }

            if (expectedType.IsInstanceOfType(item) == false)
            {
                mismatchDescription.AppendValue(item).AppendText(" is an instance of {0} not {1}", item.GetType().FullName, 
                    expectedType.FullName);
                return false;
            }

            return true;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("an instance of ").AppendText(expectedType.FullName);
        }
    }

    public class IsInstanceOf : IsInstanceOf<object>
    {
        public IsInstanceOf(Type expectedType) : base(expectedType) { }
    }

    public static partial class Is
    {
        /**
         * Is the value an instance of a particular type? 
         * This version assumes no relationship between the required type and
         * the signature of the method that sets it up, for example in
         * <code>Assert.That(anObject, Is.InstanceOf(typeof(Thing)));</code>
         */
        [Factory]
        public static IMatcher<T> InstanceOf<T>()
        {
            return new IsInstanceOf<T>();
        }

        [Factory]
        public static IMatcher<object> InstanceOf(Type expectedType)
        {
            return new IsInstanceOf(expectedType);
        }

        /**
         * Is the value an instance of a particular type? 
         * Use this version to make generics conform, for example in 
         * the JMock clause <code>with(any(Thing.class))</code> 
         */
        [Factory]
        public static IMatcher<T> Any<T>()
        {
            return new IsInstanceOf<T>();
        }
    }
}