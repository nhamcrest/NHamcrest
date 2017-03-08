using System.Collections.Generic;

namespace NHamcrest.Core
{
    public class EveryMatcher<T> : NonNullDiagnosingMatcher<IEnumerable<T>>
    {
        private readonly IMatcher<T> _matcher;

        public EveryMatcher(IMatcher<T> matcher)
        {
            _matcher = matcher;
        }

        protected override bool MatchesSafely(IEnumerable<T> collection, IDescription mismatchDescription)
        {
            foreach (var item in collection)
            {
                if (_matcher.Matches(item))
                    continue;

                mismatchDescription.AppendText("an item ");
                _matcher.DescribeMismatch(item, mismatchDescription);
                return false;
            }
            return true;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("every item ").AppendDescriptionOf(_matcher);
        }
    }
}