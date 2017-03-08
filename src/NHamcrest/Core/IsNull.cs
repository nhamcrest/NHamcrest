namespace NHamcrest.Core
{
    public class IsNullMatcher<T> : Matcher<T>
    {
        public override bool Matches(T item)
        {
            return ReferenceEquals(item, null);
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("null");
        }
    }

    public class IsNullMatcher : IsNullMatcher<object> { }
}