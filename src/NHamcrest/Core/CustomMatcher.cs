using System;

namespace NHamcrest.Core
{
    /// <summary>
    /// Utility class for writing one off matchers.
    /// <example>
    /// var aNonEmptyString = new CustomMatcher&lt;string&gt;("a non empty string", s => string.IsNullOrEmpty(s) == false);
    /// </example>
    /// </summary>
    public sealed class CustomMatcher<T> : Matcher<T>
    {
        private readonly string _fixedDescription;
        private readonly Func<T, bool> _matches;

        public CustomMatcher(string description, Func<T, bool> matches)
        {
            if (description == null)
            {
                throw new ArgumentNullException("description");
            }
            _fixedDescription = description;
            _matches = matches;
        }

        public override bool Matches(T item)
        {
            return _matches(item);
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText(_fixedDescription);
        }
    }
}