
using NHamcrest.Core;
using Xunit;
using Assert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class IsCollectionContainingTests
    {
        [Fact]
        public void Has_item()
        {
            Assert.That(new[] {"aaa", "bbb", "ccc"}, Has.Item(Is.EqualTo("aaa")));
        }

        [Fact]
        public void Has_items_with_matchers()
        {
            Assert.That(new[] { "aaa", "bbb", "ccc" }, Has.Items(Is.EqualTo("aaa"), Is.EqualTo("bbb")));
        }

        [Fact]
        public void Describe_to_appends_matcher_description()
        {
            var matcher = Has.Item(Is.EqualTo("aaa"));
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.That(description.ToString(), Is.EqualTo("a collection containing \"aaa\""));
        }

		[Fact]
		public void Describe_mismatch()
		{
			var matcher = Has.Item(Is.EqualTo("aaa"));
			var description = new StringDescription();

			matcher.DescribeMismatch(new [] { "bbb", "ddd" }, description);

			Assert.That(description.ToString(), Is.EqualTo("was \"bbb\", was \"ddd\""));
		}
    }
}