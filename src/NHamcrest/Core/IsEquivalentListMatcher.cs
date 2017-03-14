using System;
using System.Collections.Generic;
using System.Linq;

namespace NHamcrest.Core
{
    internal class IsEquivalentListMatcher<T> : NonNullDiagnosingMatcher<IEnumerable<T>>
    {
        private readonly IEnumerable<IMatcher<T>> _matcherCollection;

        public IsEquivalentListMatcher(IEnumerable<IMatcher<T>> matcherCollection)
        {
            _matcherCollection = matcherCollection;
        }

        protected override bool MatchesSafely(IEnumerable<T> collection, IDescription mismatchDescription)
        {
            var collectionArray = collection.ToArray();
            var matcherArray = _matcherCollection.ToArray();

            for (var i = 0; i < Math.Max(collectionArray.Length, matcherArray.Length); i++)
            {
                if (i >= collectionArray.Length)
                {
                    mismatchDescription.AppendText("was too short (expected to be of length {0}, was {1})", matcherArray.Length, collectionArray.Length);
                    return false;
                }

                if (i >= matcherArray.Length)
                {
                    mismatchDescription.AppendText("was too long (expected to be of length {0}, was {1})", matcherArray.Length, collectionArray.Length);
                    return false;
                }

                if (!matcherArray[i].Matches(collectionArray[i]))
                {
                    mismatchDescription.AppendText("was not matched at position {0}:", i);

                    using (mismatchDescription.IndentBy(4))
                    {
                        mismatchDescription.AppendNewLine()
                            .AppendText("expected: ")
                            .AppendDescriptionOf(matcherArray[i])
                            .AppendNewLine()
                            .AppendText("but: ");

                        matcherArray[i].DescribeMismatch(collectionArray[i], mismatchDescription);
                    }

                    return false;
                }
            }

            return true;
        }

        public override void DescribeTo(IDescription description)
        {
            var matcherArray = _matcherCollection.ToArray();

            if (matcherArray.Length == 0)
            {
                description.AppendText("an empty list");
                return;
            }

            description.AppendText("a list containing:");

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