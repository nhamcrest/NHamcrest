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

    public static class Has
    {
        [Factory]
        public static IMatcher<IEnumerable<T>> Item<T>(IMatcher<T> elementMatcher)
        {
            return new IsCollectionContaining<T>(elementMatcher);
        }

        [Factory]
        public static IMatcher<IEnumerable<T>> Item<T>(T element)
        {
            return Item(Is.EqualTo(element));
        }

        [Factory]
        public static IMatcher<IEnumerable<T>> Items<T>(params IMatcher<T>[] elementMatchers)
        {
            var all = new List<IMatcher<IEnumerable<T>>>();
            
            foreach (var elementMatcher in elementMatchers)
            {
                var matcher = new IsCollectionContaining<T>(elementMatcher);
                all.Add(matcher);
            }

            return Matches.AllOf(all);
        }

        [Factory]
        public static IMatcher<IEnumerable<T>> Items<T>(params T[] elements)
        {
            var matchers = new List<IMatcher<IEnumerable<T>>>();

            foreach (var element in elements)
            {
                var matcher = Item(element);
                matchers.Add(matcher);
            }
            
            return Matches.AllOf(matchers);
        }
    }
}