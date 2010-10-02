namespace NHamcrest
{
    /// <summary>
    /// A matcher over acceptable values.
    /// A matcher is able to describe itself to give feedback when it fails.
    /// Matcher implementations should NOT directly implement this interface.
    /// Instead, extend the <see cref="Matcher{T}" /> abstract class,
    /// which will ensure that the Matcher API can grow to support
    /// new features and remain compatible with all Matcher implementations.
    /// For easy access to common Matcher implementations, use the static factory
    /// methods in CoreMatchers.
    /// </summary>
    /// <typeparam name="T">The type of object to match against.</typeparam>
    public interface IMatcher<T> : ISelfDescribing
    {
        /// <summary>
        /// Evaluates the matcher.
        /// </summary>
        /// <param name="item">The item to match against.</param>
        /// <returns>True if the item matches, otherwise false.</returns>
        bool Matches(T item);

        /// <summary>
        /// Generate a description of why the matcher has not accepted the item.
        /// The description will be part of a larger description of why a matching
        /// failed, so it should be concise. 
        /// This method assumes that <code>Matches(item)</code> is false, but 
        /// will not check this.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="mismatchDescription">A description of the mismatch.</param>
        void DescribeMismatch(T item, IDescription mismatchDescription);
    }
}