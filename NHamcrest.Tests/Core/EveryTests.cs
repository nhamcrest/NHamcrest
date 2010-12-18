using MbUnit.Framework;
using NHamcrest.Core;
using Assert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class EveryTests
    {
        private readonly IMatcher<string> startsWithA = new CustomMatcher<string>("starts with an A", 
            s => s.StartsWith("A"));

        [Test]
        public void Match_if_all_elements_match()
        {
            var strings = new[] {"Aaaa", "Abbbb", "Acccc"};

            Assert.That(strings, Every.Item(startsWithA));
        }

        [Test]
        public void No_match_if_any_element_does_not_match()
        {
            var strings = new[] { "Aaaa", "Bbbbb", "Acccc" };

            Assert.That(strings, NotEvery.Item(startsWithA));
        }

		[Test]
		public void Describe_to()
		{
			var matcher = Every.Item(startsWithA);
			var description = new StringDescription();

			matcher.DescribeTo(description);

			Assert.That(description.ToString(), Is.EqualTo("every item starts with an A"));
		}
    }
}