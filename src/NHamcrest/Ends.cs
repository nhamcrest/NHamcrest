using NHamcrest.Core;

namespace NHamcrest
{
    public static class Ends
    {
        public static StringEndsWith With(string substring)
        {
            return new StringEndsWith(substring);
        }
    }
}