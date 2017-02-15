using MbUnit.Framework;
using NHamcrest.Core;
using Assert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class IsCollectionContainingTests
    {
        [Test]
        public void Has_item()
        {
            Assert.That(new[] {"aaa", "bbb", "ccc"}, Has.Item("aaa"));
        }

        [Test]
        public void Has_item_with_matcher()
        {
            var elementMatcher = new CustomMatcher<string>("aaa", s => s == "aaa");

            Assert.That(new[] { "aaa", "bbb", "ccc" }, Has.Item<string>(elementMatcher));
        }

        [Test]
        public void Has_items()
        {
            Assert.That(new[] { "aaa", "bbb", "ccc" }, Has.Items("aaa", "bbb"));
        }

        [Test]
        public void Has_items_with_matchers()
        {
            var aaaMatcher = new CustomMatcher<string>("aaa", s => s == "aaa");
            var bbbMatcher = new CustomMatcher<string>("bbb", s => s == "bbb");

            Assert.That(new[] { "aaa", "bbb", "ccc" }, Has.Items<string>(aaaMatcher, bbbMatcher));
        }

        [Test]
        public void Describe_to_appends_matcher_description()
        {
            var matcher = Has.Item("aaa");
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.That(description.ToString(), Is.EqualTo("a collection containing aaa"));
        }

		[Test]
		public void Describe_mismatch()
		{
			var matcher = Has.Item("aaa");
			var description = new StringDescription();

			matcher.DescribeMismatch(new [] { "bbb", "ddd" }, description);

			Assert.That(description.ToString(), Is.EqualTo("was bbb, was ddd"));
		}
    }
}