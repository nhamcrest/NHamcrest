
using NHamcrest.Core;
using Xunit;

namespace NHamcrest.Tests.Core
{
    public class CombinableMatcherTests
    {
        private readonly CustomMatcher<string> failingMatcher = new CustomMatcher<string>("Failing matcher", s => false);
        private readonly CustomMatcher<string> successfulMatcher = new CustomMatcher<string>("Successful matcher", s => true);

        [Fact]
        public void Both_returns_match_if_both_succeed()
        {
            var matcher = Matches.Both(new CombinableMatcher<string>(successfulMatcher).And(successfulMatcher));

            Assert.Equal(true, matcher.Matches(""));
        }

        [Fact]
        public void Both_returns_no_match_if_either_fails()
        {
            var matcher = Matches.Both(new CombinableMatcher<string>(successfulMatcher).And(failingMatcher));

            Assert.Equal(false, matcher.Matches(""));
        }

        [Fact]
        public void Either_returns_match_if_any_succeeds()
        {
            var matcher = Matches.Either(new CombinableMatcher<string>(successfulMatcher).Or(successfulMatcher));
            Assert.Equal(true, matcher.Matches(""));

            matcher = Matches.Either(new CombinableMatcher<string>(successfulMatcher).Or(failingMatcher));
            Assert.Equal(true, matcher.Matches(""));

            matcher = Matches.Either(new CombinableMatcher<string>(failingMatcher).Or(successfulMatcher));
            Assert.Equal(true, matcher.Matches(""));
        }

        [Fact]
        public void Either_returns_no_match_if_all_fail()
        {
            var matcher = Matches.Either(new CombinableMatcher<string>(failingMatcher).And(failingMatcher));

            Assert.Equal(false, matcher.Matches(""));
        }

		[Fact]
		public void DescribeTo()
		{
			var matcher = new CombinableMatcher<string>(failingMatcher);
			var description = new StringDescription();

			matcher.DescribeTo(description);

			Assert.Equal("Failing matcher", description.ToString());
		}
    }
}