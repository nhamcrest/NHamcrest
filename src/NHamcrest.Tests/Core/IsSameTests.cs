
using NHamcrest.Core;
using Xunit;
using Assert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class IsSameTests
    {
        [Fact]
        public void Match_if_same_object()
        {
            var a = new A();
            
            Assert.That(a, Is.SameAs(a));
        }

        [Fact]
        public void No_match_if_different_object()
        {
            var a = new A();
            var a1 = new A();
            var matcher = Is.SameAs(a1);

            var matches = matcher.Matches(a);

            Assert.That(matches, Is.False());
        }

        [Fact]
        public void Describe_to()
        {
            var a = new A();
            var matcher = Is.SameAs(a);
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.That(description.ToString(), Is.EqualTo("SameAs(NHamcrest.Tests.Core.IsSameTests+A)"));
        }

        private class A { }
    }
}