using System.Collections.Generic;

namespace NHamcrest.Core
{
    public class Every<T> : NonNullDiagnosingMatcher<IEnumerable<T>>
    {
        private readonly IMatcher<T> matcher;

        public Every(IMatcher<T> matcher)
        {
            this.matcher = matcher;
        }

        protected override bool MatchesSafely(IEnumerable<T> collection, IDescription mismatchDescription)
        {
            foreach (var item in collection)
            {
                if (matcher.Matches(item))
                    continue;

                mismatchDescription.AppendText("an item ");
                matcher.DescribeMismatch(item, mismatchDescription);
                return false;
            }
            return true;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("every item is ").AppendDescriptionOf(matcher);
        }
    }

    public static class Every
    {
        /// <summary>
        /// A collection matcher that returns true if every item matches.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemMatcher">A matcher to apply to every element in a collection.</param>
        /// <returns>True if every item matches.</returns>
        [Factory]
        public static IMatcher<IEnumerable<T>> Item<T>(IMatcher<T> itemMatcher)
        {
            return new Every<T>(itemMatcher);
        }
    }

    public static class NotEvery
    {
        /// <summary>
        /// A collection matcher that returns true if every item matches.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemMatcher">A matcher to apply to every element in a collection.</param>
        /// <returns>True if every item matches.</returns>
        [Factory]
        public static IsNot<IEnumerable<T>> Item<T>(IMatcher<T> itemMatcher)
        {
            return new IsNot<IEnumerable<T>>(new Every<T>(itemMatcher));
        }
    }
}