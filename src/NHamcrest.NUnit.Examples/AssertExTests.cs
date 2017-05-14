using System;
using NUnit.Framework;
using Is = NHamcrest.Is;
using Throws = NHamcrest.Core.Throws;

namespace NHamcrest.NUnit.Tests
{
    public class AssertExTests
    {
        [Test]
        public void Pass()
        {
            AssertEx.That(1, Is.EqualTo(1));
        }

        [Test]
        public void Fail()
        {
            AssertEx.That(1, Is.EqualTo(3));
        }

        [Test]
        public void One_more()
        {
            AssertEx.That(() => { throw new InvalidOperationException(); }, Throws.An<InvalidOperationException>());
        }
    }
}
