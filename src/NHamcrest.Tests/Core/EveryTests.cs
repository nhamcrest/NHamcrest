using NHamcrest.Core;
using Xunit;
using Assert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class EveryTests
    {
        private readonly IMatcher<string> _startsWithA = new CustomMatcher<string>("starts with an A",
            s => s.StartsWith("A"));

        [Fact]
        public void Match_if_all_elements_match()
        {
            var strings = new[] { "Aaaa", "Abbbb", "Acccc" };

            Assert.That(strings, Every.Item(_startsWithA));
        }

        [Fact]
        public void No_match_if_any_element_does_not_match()
        {
            var strings = new[] { "Aaaa", "Bbbbb", "Acccc" };

            Assert.That(strings, NotEvery.Item(_startsWithA));
        }

        [Fact]
        public void Describe_to()
        {
            var matcher = Every.Item(_startsWithA);
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.That(description.ToString(), Is.EqualTo("every item starts with an A"));
        }
    }
}