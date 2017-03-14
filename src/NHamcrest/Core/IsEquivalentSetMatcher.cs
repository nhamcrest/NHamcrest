using System.Collections.Generic;
using System.Linq;

namespace NHamcrest.Core
{
    internal class IsEquivalentSetMatcher<T> : NonNullDiagnosingMatcher<IEnumerable<T>>
    {
        private readonly IEnumerable<IMatcher<T>> _matcherCollection;

        public IsEquivalentSetMatcher(IEnumerable<IMatcher<T>> matcherCollection)
        {
            _matcherCollection = matcherCollection;
        }

        protected override bool MatchesSafely(IEnumerable<T> collection, IDescription mismatchDescription)
        {
            var collectionArray = collection.ToArray();
            var matcherArray = _matcherCollection.Select(Has.Item).ToArray();

            foreach (var matcher in matcherArray)
            {
                if (!matcher.Matches(collectionArray))
                {
                    matcher.DescribeMismatch(collectionArray, mismatchDescription);
                    return false;
                }
            }

            var lengthMatcher = Is.OfLength(matcherArray.Length);
            if (!lengthMatcher.Matches(collectionArray))
            {
                lengthMatcher.DescribeMismatch(collectionArray, mismatchDescription);
                return false;
            }

            return true;
        }

        public override void DescribeTo(IDescription description)
        {
            var matcherArray = _matcherCollection.ToArray();

            if (matcherArray.Length == 0)
            {
                description.AppendText("an empty set");
                return;
            }

            description.AppendText("a set containing:");

            using (description.IndentBy(4))
            {
                description.AppendNewLine();

                var first = true;

                foreach (var matcher in matcherArray)
                {
                    if (!first) description.AppendText(",").AppendNewLine();

                    description.AppendDescriptionOf(matcher);

                    first = false;
                }
            }
        }
    }
}