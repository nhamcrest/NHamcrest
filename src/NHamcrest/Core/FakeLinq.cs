using System.Collections;
using System.Collections.Generic;

namespace NHamcrest.Core
{
    internal static class FakeLinq
    {
        public static IEnumerable<TResult> Cast<TResult>(IEnumerable source)
        {
            foreach (var element in source)
            {
                yield return (TResult)element;
            }
        }
    }
}