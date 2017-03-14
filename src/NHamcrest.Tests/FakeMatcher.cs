using System;

namespace NHamcrest.Tests
{
    public class FakeMatcher<T> : IMatcher<T>
    {
        private readonly bool _matches;
        private readonly string _description;
        private readonly Func<T, string> _mismatchDescription;

        public FakeMatcher(bool matches, string description, Func<T, string> mismatchDescription)
        {
            _matches = matches;
            _description = description;
            _mismatchDescription = mismatchDescription;
        }

        public void DescribeTo(IDescription description)
        {
            description.AppendText(_description);
        }

        public bool Matches(T item)
        {
            return _matches;
        }

        public void DescribeMismatch(T item, IDescription mismatchDescription)
        {
            mismatchDescription.AppendText(_mismatchDescription(item));
        }
    }
}