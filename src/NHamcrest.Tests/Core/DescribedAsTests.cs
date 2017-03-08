

using Xunit;

namespace NHamcrest.Tests.Core
{
    public class DescribedAsTests
    {
        private readonly Matcher<string> _originalMatcher = new CustomMatcher<string>("originalDescription", s => true);

        [Fact]
        public void Description_is_overridden()
        {
            const string newDescription = "newDescription";
            var matcher = _originalMatcher.DescribedAs(newDescription);
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.Equal(newDescription, description.ToString());
        }

        [Fact]
        public void Description_can_be_formatted()
        {
            var matcher = _originalMatcher.DescribedAs("{0}, {1}", "Hello", "World!");
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.Equal("Hello, World!", description.ToString());
        }

        [Fact]
        public void Matching_is_delegated()
        {
            var flag = false;
            var customMatcher = new CustomMatcher<string>("originalDescription", s => { flag = true; return true; });
            var matcher = customMatcher.DescribedAs("somethingElse");

            matcher.Matches("");

            Assert.Equal(true, flag);
        }
    }
}