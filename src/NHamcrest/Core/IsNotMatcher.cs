namespace NHamcrest.Core
{
    public class IsNotMatcher<T> : Matcher<T>
    {
        private readonly IMatcher<T> _matcher;

        public IsNotMatcher(IMatcher<T> matcher)
        {
            _matcher = matcher;
        }

        public override bool Matches(T arg)
        {
            return _matcher.Matches(arg) == false;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("not ").AppendDescriptionOf(_matcher);
        }
    }
}