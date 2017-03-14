using NHamcrest.Core;
using Xunit;

namespace NHamcrest.Tests.Core
{
    public class AllOfTests
    {
        private readonly CustomMatcher<string> _failingMatcher = new CustomMatcher<string>("Failing matcher", s => false);
        private readonly CustomMatcher<string> _successfulMatcher = new CustomMatcher<string>("Successful matcher", s => true);

        [Fact]
        public void Match_if_all_matchers_succeed()
        {
            var matcher = Matches.AllOf(_successfulMatcher, _successfulMatcher);

            Assert.Equal(true, matcher.Matches(""));
        }

        [Fact]
        public void No_match_if_any_matcher_fails()
        {
            var matcher = Matches.AllOf(_failingMatcher, _successfulMatcher);

            Assert.Equal(false, matcher.Matches(""));
        }

        [Fact]
        public void Mismatch_description_appended_if_matcher_fails()
        {
            var matcher = Matches.AllOf(_failingMatcher);
            var description = new StringDescription();

            matcher.DescribeMismatch("bob", description);

            Assert.Equal("Failing matcher was \"bob\"", description.ToString());
        }

        [Fact]
        public void Description_is_concatenated_from_matchers()
        {
            var matcher = Matches.AllOf(_failingMatcher, _successfulMatcher);
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.Equal("(Failing matcher and Successful matcher)", description.ToString());
        }
    }
}