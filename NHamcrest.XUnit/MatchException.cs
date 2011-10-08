using Xunit.Sdk;

namespace NHamcrest.XUnit
{
    public class MatchException : AssertActualExpectedException
    {
        public MatchException(object expected, object actual, string userMessage) : base(expected, actual, userMessage)
        {
        }
    }
}