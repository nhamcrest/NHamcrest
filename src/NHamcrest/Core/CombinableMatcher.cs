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
            return new CombinableMatcher<T>(new AllOfMatcher<T>(new[] { _matcher, other }));
        }

        public CombinableMatcher<T> Or(IMatcher<T> other)
        {
            return new CombinableMatcher<T>(new AnyOfMatcher<T>(new[] { _matcher, other }));
        }
    }
}