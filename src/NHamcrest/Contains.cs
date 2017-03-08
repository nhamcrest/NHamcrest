using NHamcrest.Core;

namespace NHamcrest
{
    public static class Contains
    {
        public static StringContains String(string substring)
        {
            return new StringContains(substring);
        }
    }
}