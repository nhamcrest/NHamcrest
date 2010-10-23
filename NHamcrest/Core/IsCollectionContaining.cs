using System.Collections.Generic;
using System.Linq;

namespace NHamcrest.Core
{
    public class IsCollectionContaining<T> : NonNullDiagnosingMatcher<IEnumerable<T>>
    {
        private readonly IMatcher<T> elementMatcher;

        public IsCollectionContaining(IMatcher<T> elementMatcher)
        {
            this.elementMatcher = elementMatcher;
        }

        protected override bool MatchesSafely(IEnumerable<T> collection, IDescription mismatchDescription)
        {
            var isPastFirst = false;
            foreach (var item in collection)
            {
                if (elementMatcher.Matches(item))
                {
                    return true;
                }
                if (isPastFirst)
                {
                    mismatchDescription.AppendText(", ");
                }
                elementMatcher.DescribeMismatch(item, mismatchDescription);
                isPastFirst = true;
            }
            return false;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("a collection containing ")
                .AppendDescriptionOf(elementMatcher);
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
            var all = elementMatchers.Select(elementMatcher => new IsCollectionContaining<T>(elementMatcher))
                .Cast<IMatcher<IEnumerable<T>>>()
                .ToList();

            return Matches.AllOf(all);
        }

        [Factory]
        public static IMatcher<IEnumerable<T>> Items<T>(params T[] elements)
        {
            return Matches.AllOf(elements.Select(element => Item(element)));
        }
    }
}