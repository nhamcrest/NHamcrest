namespace NHamcrest
{
    public abstract class DiagnosingMatcher<T> : Matcher<T>
    {
        public sealed override bool Matches(T item)
        {
            return Matches(item, Description.None);
        }

        public override void DescribeMismatch(T item, IDescription mismatchDescription)
        {
            Matches(item, mismatchDescription);
        }

        protected abstract bool Matches(T item, IDescription mismatchDescription);
    }
}