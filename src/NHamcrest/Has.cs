using System.Collections.Generic;
using NHamcrest.Core;

namespace NHamcrest
{
    public static class Has
    {
        public static IMatcher<IEnumerable<T>> Item<T>(IMatcher<T> elementMatcher)
        {
            return new IsCollectionContaining<T>(elementMatcher);
        }

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
    }
}