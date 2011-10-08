using System;
using NHamcrest.Core;
using Xunit;

namespace NHamcrest.XUnit.Examples
{
    public class AssertExTests
    {
        [Fact]
        public void Pass()
        {
            AssertEx.That(1, Is.EqualTo(1));
        }

        [Fact]
        public void Fail()
        {
            AssertEx.That(1, Is.EqualTo(3));
        }

        [Fact]
        public void One_more()
        {
            AssertEx.That(() => { throw new InvalidOperationException(); }, Throws.An<AccessViolationException>());
        }
    }
}
