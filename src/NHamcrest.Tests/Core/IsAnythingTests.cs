using NHamcrest.Core;
using Xunit;
using Assert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class IsAnythingTests
    {
        [Fact]
        public void Always_returns_true()
        {
            Assert.That("", Is.Anything());
        }

        [Fact]
        public void Appends_description()
        {
            const string test = "test";
            var matcher = Is.Anything(test);
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.That(description.ToString(), Is.EqualTo(test));
        }

        [Fact]
        public void Default_description()
        {
            var matcher = Is.Anything();
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.That(description.ToString(), Is.EqualTo("ANYTHING"));
        }
    }
}