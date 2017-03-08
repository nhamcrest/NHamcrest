using System.Collections.Generic;

namespace NHamcrest.Core
{
    public class IsCollectionContaining<T> : NonNullDiagnosingMatcher<IEnumerable<T>>
    {
        private readonly IMatcher<T> _elementMatcher;

        public IsCollectionContaining(IMatcher<T> elementMatcher)
        {
            _elementMatcher = elementMatcher;
        }

        protected override bool MatchesSafely(IEnumerable<T> collection, IDescription mismatchDescription)
        {
            var isPastFirst = false;
            foreach (var item in collection)
            {
                if (_elementMatcher.Matches(item))
                {
                    return true;
                }
                if (isPastFirst)
                {
                    mismatchDescription.AppendText(", ");
                }
                _elementMatcher.DescribeMismatch(item, mismatchDescription);
                isPastFirst = true;
            }
            return false;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("a collection containing ")
                .AppendDescriptionOf(_elementMatcher);
        }
    }
}