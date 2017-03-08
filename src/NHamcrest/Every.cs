using System.Collections.Generic;
using NHamcrest.Core;

namespace NHamcrest
{
    public static class Every
    {
        /// <summary>
        /// A collection matcher that returns true if every item matches.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemMatcher">A matcher to apply to every element in a collection.</param>
        /// <returns>True if every item matches.</returns>
        public static IMatcher<IEnumerable<T>> Item<T>(IMatcher<T> itemMatcher)
        {
            return new EveryMatcher<T>(itemMatcher);
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
        public static IsNotMatcher<IEnumerable<T>> Item<T>(IMatcher<T> itemMatcher)
        {
            return new IsNotMatcher<IEnumerable<T>>(new EveryMatcher<T>(itemMatcher));
        }
    }
}