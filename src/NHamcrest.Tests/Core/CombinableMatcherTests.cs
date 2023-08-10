using NHamcrest.Core;
using Xunit;

namespace NHamcrest.Tests.Core
{
    public class CombinableMatcherTests
    {
        private readonly CustomMatcher<string> _failingMatcher = new CustomMatcher<string>("Failing matcher", s => false);
        private readonly CustomMatcher<string> _successfulMatcher = new CustomMatcher<string>("Successful matcher", s => true);

        [Fact]
        public void Both_returns_match_if_both_succeed()
        {
            var matcher = Matches.Both(new CombinableMatcher<string>(_successfulMatcher).And(_successfulMatcher));

            Assert.True(matcher.Matches(""));
        }

        [Fact]
        public void Both_returns_no_match_if_either_fails()
        {
            var matcher = Matches.Both(new CombinableMatcher<string>(_successfulMatcher).And(_failingMatcher));

            Assert.False(matcher.Matches(""));
        }

        [Fact]
        public void Either_returns_match_if_any_succeeds()
        {
            var matcher = Matches.Either(new CombinableMatcher<string>(_successfulMatcher).Or(_successfulMatcher));
            Assert.True(matcher.Matches(""));

            matcher = Matches.Either(new CombinableMatcher<string>(_successfulMatcher).Or(_failingMatcher));
            Assert.True(matcher.Matches(""));

            matcher = Matches.Either(new CombinableMatcher<string>(_failingMatcher).Or(_successfulMatcher));
            Assert.True(matcher.Matches(""));
        }

        [Fact]
        public void Either_returns_no_match_if_all_fail()
        {
            var matcher = Matches.Either(new CombinableMatcher<string>(_failingMatcher).And(_failingMatcher));

            Assert.False(matcher.Matches(""));
        }

        [Fact]
        public void DescribeTo()
        {
            var matcher = new CombinableMatcher<string>(_failingMatcher);
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.Equal("Failing matcher", description.ToString());
        }
    }
}