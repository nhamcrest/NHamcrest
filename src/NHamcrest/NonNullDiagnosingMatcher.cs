namespace NHamcrest
{
    public abstract class NonNullDiagnosingMatcher<T> : Matcher<T>
    {
        /// <summary>
        /// Subclasses should implement this. The item will already have been checked
        /// for the specific type and will never be null.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="mismatchDescription"></param>
        /// <returns></returns>
        protected abstract bool MatchesSafely(T collection, IDescription mismatchDescription);

        public override bool Matches(T item)
        {
            return ReferenceEquals(item, null) == false && MatchesSafely(item, new NullDescription());
        }

        public override void DescribeMismatch(T item, IDescription mismatchDescription)
        {
            if (ReferenceEquals(item, null))
            {
                base.DescribeMismatch(item, mismatchDescription);
            }
            else
            {
                MatchesSafely(item, mismatchDescription);
            }
        }
    }
}