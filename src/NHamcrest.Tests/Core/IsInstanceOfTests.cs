
using NHamcrest.Core;
using Xunit;
using Assert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class IsInstanceOfTests
    {
        [Fact]
        public void Match_if_instance_of_type()
        {
            var shaneLong = new ShaneLong();

            Assert.That(shaneLong, Is.InstanceOf<ShaneLong>());
        }

        [Fact]
        public void Match_if_can_be_type()
        {
            var jobiMcAnuff = new JobiMcAnuff();

            Assert.That(jobiMcAnuff, Is.Any<ShaneLong>());
        }

        [Fact]
        public void No_match_if_something_else()
        {
            var simonChurch = new SimonChurch();
            var matcher = Is.InstanceOf(typeof(ShaneLong));

            var matches = matcher.Matches(simonChurch);

            Assert.That(matches, Is.False());
        }

        [Fact]
        public void No_match_if_null()
        {
            var matcher = Is.InstanceOf(typeof(ShaneLong));

            var matches = matcher.Matches(null);

            Assert.That(matches, Is.False());
        }

        [Fact]
        public void Describe_mismatch()
        {
            var simonChurch = new SimonChurch();
            var matcher = Is.InstanceOf(typeof(ShaneLong));
            var description = new StringDescription();

            matcher.DescribeMismatch(simonChurch, description);

            const string errorMessage = "Simon Church is an instance of NHamcrest.Tests.Core.IsInstanceOfTests+SimonChurch not" + 
                                        " NHamcrest.Tests.Core.IsInstanceOfTests+ShaneLong";
            Assert.That(description.ToString(), Is.EqualTo(errorMessage));
        }

        [Fact]
        public void Describe_mismatch_when_null()
        {
            var matcher = Is.InstanceOf(typeof(ShaneLong));
            var description = new StringDescription();

            matcher.DescribeMismatch(null, description);

            Assert.That(description.ToString(), Is.EqualTo("null"));
        }

        [Fact]
        public void Describe_to()
        {
            var matcher = Is.InstanceOf(typeof(ShaneLong));
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.That(description.ToString(), Is.EqualTo("an instance of NHamcrest.Tests.Core.IsInstanceOfTests+ShaneLong"));
        }

        private class ShaneLong { }

        private class JobiMcAnuff : ShaneLong { }

        private class SimonChurch
        {
            public override string ToString()
            {
                return "Simon Church";
            }
        }
    }
}