using MbUnit.Framework;
using NHamcrest.Core;

namespace NHamcrest.Tests.Core
{
    public class CombinableMatcherTests
    {
        private readonly CustomMatcher<string> failingMatcher = new CustomMatcher<string>("Failing matcher", s => false);
        private readonly CustomMatcher<string> successfulMatcher = new CustomMatcher<string>("Successful matcher", s => true);

        [Test]
        public void Both_returns_match_if_both_succeed()
        {
            var matcher = Matches.Both(new CombinableMatcher<string>(successfulMatcher).And(successfulMatcher));

            Assert.AreEqual(true, matcher.Matches(""), "Expected a match if both matchers succeed.");
        }

        [Test]
        public void Both_returns_no_match_if_either_fails()
        {
            var matcher = Matches.Both(new CombinableMatcher<string>(successfulMatcher).And(failingMatcher));

            Assert.AreEqual(false, matcher.Matches(""), "Expected no match if either matcher fails.");
        }

        [Test]
        public void Either_returns_match_if_any_succeeds()
        {
            var matcher = Matches.Either(new CombinableMatcher<string>(successfulMatcher).Or(successfulMatcher));
            Assert.AreEqual(true, matcher.Matches(""), "Expected a match either matcher succeeds.");

            matcher = Matches.Either(new CombinableMatcher<string>(successfulMatcher).Or(failingMatcher));
            Assert.AreEqual(true, matcher.Matches(""), "Expected a match either matcher succeeds.");

            matcher = Matches.Either(new CombinableMatcher<string>(failingMatcher).Or(successfulMatcher));
            Assert.AreEqual(true, matcher.Matches(""), "Expected a match if either matcher succeeds.");
        }

        [Test]
        public void Either_returns_no_match_if_all_fail()
        {
            var matcher = Matches.Either(new CombinableMatcher<string>(failingMatcher).And(failingMatcher));

            Assert.AreEqual(false, matcher.Matches(""), "Expected no match if either matcher fails.");
        }

		[Test]
		public void DescribeTo()
		{
			var matcher = new CombinableMatcher<string>(failingMatcher);
			var description = new StringDescription();

			matcher.DescribeTo(description);

			Assert.AreEqual("Failing matcher", description.ToString(), "Expected no match if either matcher fails.");
		}
    }
}