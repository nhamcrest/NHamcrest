using MbUnit.Framework;
using NHamcrest.Core;
using Assert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class IsNullTests
    {
        [Test]
        public void Match_if_null()
        {
            Assert.That(null, Is.NullValue());
        }

        [Test]
        public void No_match_if_not_null()
        {
            var matcher = Is.NullValue<string>();

            var matches = matcher.Matches("");

            Assert.That(matches, Is.False());
        }

        [Test]
        public void Describe_to()
        {
            var matcher = Is.NullValue<string>();
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.That(description.ToString(), Is.EqualTo("null"));
        }
    }

    public class IsNotNullTests
    {
        [Test]
        public void Match_if_not_null()
        {
            Assert.That("", Is.NotNullValue());
        }

        [Test]
        public void No_match_if_null()
        {
            var matcher = Is.NotNullValue<string>();

            var matches = matcher.Matches(null);

            Assert.That(matches, Is.False());
        }
    }
}