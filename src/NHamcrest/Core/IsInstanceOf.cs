using System;

namespace NHamcrest.Core
{
    public class IsInstanceOf<T> : DiagnosingMatcher<T>
    {
        private readonly Type _expectedType;

        public IsInstanceOf() : this(typeof(T)) { }

        protected IsInstanceOf(Type expectedType)
        {
            _expectedType = expectedType;
        }
        
        protected override bool Matches(T item, IDescription mismatchDescription)
        {
            if (ReferenceEquals(item, null))
            {
                mismatchDescription.AppendText("null");
                return false;
            }

            if (_expectedType.IsInstanceOfType(item) == false)
            {
                mismatchDescription.AppendValue(item).AppendText(" is an instance of {0} not {1}", item.GetType().FullName, 
                    _expectedType.FullName);
                return false;
            }

            return true;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("an instance of ").AppendText(_expectedType.FullName);
        }
    }

    public class IsInstanceOf : IsInstanceOf<object>
    {
        public IsInstanceOf(Type expectedType) : base(expectedType) { }
    }
}