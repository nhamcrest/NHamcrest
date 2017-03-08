namespace NHamcrest.Core
{
    public class CombinableMatcher<T> : Matcher<T>
    {
        private readonly IMatcher<T> _matcher;

        public CombinableMatcher(IMatcher<T> matcher)
        {
            _matcher = matcher;
        }

        public override bool Matches(T item)
        {
            return _matcher.Matches(item);
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendDescriptionOf(_matcher);
        }

        public CombinableMatcher<T> And(IMatcher<T> other)
        {
            return new CombinableMatcher<T>(new AllOf<T>(new[] { _matcher, other }));
        }

        public CombinableMatcher<T> Or(IMatcher<T> other)
        {
            return new CombinableMatcher<T>(new AnyOf<T>(new[] { _matcher, other }));
        }
    }

    public static partial class Matches
    {
        /// <summary>
        /// This is useful for fluently combining matchers that must both pass.  For example:
        /// <pre>
        /// Assert.That("ab", Matches.Both(Contains.String("a")).And(Contains.String("b")));
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
        /// Assert.That("ac", Matches.Either(Contains.String("a")).Or(Contains.String("b")));
        /// </pre>
        /// </summary>
        [Factory]
        public static CombinableMatcher<T> Either<T>(IMatcher<T> matcher)
        {
            return new CombinableMatcher<T>(matcher);
        }
    }
}