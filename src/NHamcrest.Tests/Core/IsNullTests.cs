
using NHamcrest.Core;
using Xunit;
using Assert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class IsNullTests
    {
        [Fact]
        public void Match_if_null()
        {
            Assert.That(null, Is.Null());
        }

        [Fact]
        public void No_match_if_not_null()
        {
            var matcher = Is.Null<string>();

            var matches = matcher.Matches("");

            Assert.That(matches, Is.False());
        }

        [Fact]
        public void Describe_to()
        {
            var matcher = Is.Null<string>();
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.That(description.ToString(), Is.EqualTo("null"));
        }
    }

    public class IsNotNullTests
    {
        [Fact]
        public void Match_if_not_null()
        {
            Assert.That("", Is.NotNull());
        }

        [Fact]
        public void No_match_if_null()
        {
            var matcher = Is.NotNull<string>();

            var matches = matcher.Matches(null);

            Assert.That(matches, Is.False());
        }
    }
}