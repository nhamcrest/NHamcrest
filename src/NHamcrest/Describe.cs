using System.Linq;
using NHamcrest.Core;

namespace NHamcrest
{
    public static class Describe
    {
        public static IObjectFeatureMatcher<T> Object<T>()
        {
            return new ObjectFeatureMatcher<T>(Enumerable.Empty<IMatcher<T>>());
        }
    }
}