namespace NHamcrest.Core
{
    public abstract class NonNullMatcher<T> : Matcher<T> where T : class
    {
        /// <summary>
        /// Subclasses should implement this. The item will never be null.
        /// </summary>
        protected abstract bool MatchesSafely(T item);

        /// <summary>
        /// Subclasses should override this. The item will already have been checked for
        /// the specific type and will never be null.
        /// </summary>
        public virtual void DescribeMismatchSafely(T item, IDescription mismatchDescription)
        {
            base.DescribeMismatch(item, mismatchDescription);
        }

        /// <summary>
        /// Sealed to prevent accidental override.
        /// If you need to override this, there's no point in extending NonNullMatcher.
        /// Instead, extend Matcher.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public sealed override bool Matches(T item)
        {
            return item != null && MatchesSafely(item);
        }

        public override void DescribeMismatch(T item, IDescription description)
        {
            if (item == null)
            {
                base.DescribeMismatch(item, description);
            }
            else
            {
                DescribeMismatchSafely(item, description);
            }
        }
    }
}