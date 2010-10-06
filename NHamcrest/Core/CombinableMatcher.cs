namespace NHamcrest.Core
{
    public class CombinableMatcher<T> : Matcher<T>
    {
        private readonly IMatcher<T> matcher;

        public CombinableMatcher(IMatcher<T> matcher)
        {
            this.matcher = matcher;
        }

        public override bool Matches(T item)
        {
            return matcher.Matches(item);
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendDescriptionOf(matcher);
        }

        public CombinableMatcher<T> And(IMatcher<T> other)
        {
            return new CombinableMatcher<T>(new AllOf<T>(new[] { matcher, other }));
        }

        public CombinableMatcher<T> Or(IMatcher<T> other)
        {
            return new CombinableMatcher<T>(new AnyOf<T>(new[] { matcher, other }));
        }
    }

    public static partial class Matches
    {
        /// <summary>
        /// This is useful for fluently combining matchers that must both pass.  For example:
        /// <pre>
        ///   Assert.That(string, Matches.Both(containsString("a")).And(containsString("b")));
        /// </pre>
        /// </summary>
        [Factory]
        public static CombinableMatcher<T> Both<T>(IMatcher<T> matcher)
        {
            return new CombinableMatcher<T>(matcher);
        }

        /// <summary>
        /// This is useful for fluently combining matchers where either may pass, for example:
        /// <pre>
        ///   Assert.That(string, Matches.Either(Contains.String("a")).Or(Contains.String("b")));
        /// </pre>
        /// </summary>
        [Factory]
        public static CombinableMatcher<T> Either<T>(IMatcher<T> matcher)
        {
            return new CombinableMatcher<T>(matcher);
        }
    }
}