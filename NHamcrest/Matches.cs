using System.Collections.Generic;
using NHamcrest.Core;

namespace NHamcrest
{
    public static class Matches
    {
        public static IMatcher<T> AllOf<T>(IEnumerable<IMatcher<T>> matchers)
        {
            return new AllOf<T>(matchers);
        }
    }
}