using System;

namespace NHamcrest.Tests.Internal
{
    public class AssertionError : Exception
    {
        public AssertionError(string error) : base(error) { }
    }
}