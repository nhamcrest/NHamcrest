namespace NHamcrest
{
    /// <summary>
    /// The ability of an object to describe itself.
    /// </summary>
    public interface ISelfDescribing
    {
        /// <summary>
        /// Generates a description of the object.  The description may be part of a
        /// a description of a larger object of which this is just a component, so it 
        /// should be worded appropriately.
        /// </summary>
        /// <param name="description">The description to be built or appended to.</param>
        void DescribeTo(IDescription description);
    }
}
