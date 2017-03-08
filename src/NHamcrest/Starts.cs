using NHamcrest.Core;

namespace NHamcrest
{
    public static class Starts
    {
        public static StringStartsWith With(string substring)
        {
            return new StringStartsWith(substring);
        }
    }
}