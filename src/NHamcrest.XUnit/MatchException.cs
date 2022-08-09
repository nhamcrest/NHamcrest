using Xunit.Sdk;

namespace NHamcrest.XUnit
{
    /// <summary>
    /// MatchException that derives from AssertActualExpectedException for NHamcrest purposes.
    /// </summary>
    public class MatchException : AssertActualExpectedException
    {
        /// <summary>
        /// Instantiates a new MatchException.
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="userMessage"></param>
        public MatchException(object expected, object actual, string userMessage) : base(expected, actual, userMessage)
        {
        }
    }
}