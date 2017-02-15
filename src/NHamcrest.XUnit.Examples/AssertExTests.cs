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
            Assert.That(1, Is.EqualTo(1));
        }

        [Fact]
        public void Fail()
        {
            Assert.That(1, Is.EqualTo(3));
        }

        [Fact]
        public void One_more()
        {
            Assert.That(() => { throw new InvalidOperationException(); }, Throws.An<AccessViolationException>());
        }
    }
}
