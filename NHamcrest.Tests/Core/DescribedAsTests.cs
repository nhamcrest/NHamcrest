using MbUnit.Framework;
using NHamcrest.Core;

namespace NHamcrest.Tests.Core
{
    public class DescribedAsTests
    {
        private readonly IMatcher<string> originalMatcher = new CustomMatcher<string>("originalDescription", s => true);

        [Test]
        public void Description_is_overridden()
        {
            const string newDescription = "newDescription";
            var matcher = originalMatcher.DescribedAs(newDescription);
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.AreEqual(newDescription, description.ToString());
        }

        [Test]
        public void Description_can_be_formatted()
        {
            var matcher = originalMatcher.DescribedAs("{0}, {1}", "Hello", "World!");
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.AreEqual("Hello, World!", description.ToString());
        }

        [Test]
        public void Matching_is_delegated()
        {
            var flag = false;
            var customMatcher = new CustomMatcher<string>("originalDescription", s => { flag = true; return true; });
            var matcher = customMatcher.DescribedAs("somethingElse");

            matcher.Matches("");

            Assert.AreEqual(true, flag);
        }
    }
}