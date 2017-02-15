using System;

namespace NHamcrest
{
    /// <summary>
    /// Utility class for writing one off matchers.
    /// <example>
    /// var aNonEmptyString = new CustomMatcher&lt;string&gt;("a non empty string", s => string.IsNullOrEmpty(s) == false);
    /// </example>
    /// </summary>
    public sealed class CustomMatcher<T> : Matcher<T>
    {
        private readonly string fixedDescription;
        private readonly Func<T, bool> matches;

        public CustomMatcher(string description, Func<T, bool> matches)
        {
            if (description == null)
            {
                throw new ArgumentNullException("description");
            }
            fixedDescription = description;
            this.matches = matches;
        }

        public override bool Matches(T item)
        {
            return matches(item);
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText(fixedDescription);
        }
    }
}