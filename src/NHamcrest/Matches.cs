using System.Collections.Generic;
using NHamcrest.Core;

namespace NHamcrest
{
    public static class Matches
    {
        public static IMatcher<T> AllOf<T>(IEnumerable<IMatcher<T>> matchers)
        {
            return new AllOfMatcher<T>(matchers);
        }

        public static IMatcher<T> AllOf<T>(params IMatcher<T>[] matchers)
        {
            return new AllOfMatcher<T>(matchers);
        }

        public static IMatcher<T> AnyOf<T>(IEnumerable<IMatcher<T>> matchers)
        {
            return new AnyOfMatcher<T>(matchers);
        }

        public static IMatcher<T> AnyOf<T>(params IMatcher<T>[] matchers)
        {
            return new AnyOfMatcher<T>(matchers);
        }

        /// <summary>
        /// This is useful for fluently combining matchers that must both pass.  For example:
        /// <pre>
        /// Assert.That("ab", Matches.Both(Contains.String("a")).And(Contains.String("b")));
        /// </pre>
        /// </summary>
        public static CombinableMatcher<T> Both<T>(IMatcher<T> matcher)
        {
            return new CombinableMatcher<T>(matcher);
        }

        /// <summary>
        /// This is useful for fluently combining matchers where either may pass, for example:
        /// <pre>
        /// Assert.That("ac", Matches.Either(Contains.String("a")).Or(Contains.String("b")));
        /// </pre>
        /// </summary>
        public static CombinableMatcher<T> Either<T>(IMatcher<T> matcher)
        {
            return new CombinableMatcher<T>(matcher);
        }
    }
}