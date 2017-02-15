using MbUnit.Framework;
using NHamcrest.Core;

namespace NHamcrest.Tests.Core
{
    public class AllOfTests
    {
        private readonly CustomMatcher<string> failingMatcher = new CustomMatcher<string>("Failing matcher", s => false);
        private readonly CustomMatcher<string> successfulMatcher = new CustomMatcher<string>("Successful matcher", s => true);

        [Test]
        public void Match_if_all_matchers_succeed()
        {
            var matcher = Matches.AllOf(new[] { successfulMatcher, successfulMatcher });

            Assert.AreEqual(true, matcher.Matches(""), "Expected match if all matchers succeed.");
        }

        [Test]
        public void No_match_if_any_matcher_fails()
        {
            var matcher = Matches.AllOf(failingMatcher, successfulMatcher);

            Assert.AreEqual(false, matcher.Matches(""), "Expected no match if any matcher fails.");
        }

        [Test]
        public void Mismatch_description_appended_if_matcher_fails()
        {
            var matcher = Matches.AllOf(new[] { failingMatcher });
            var description = new StringDescription();

            matcher.DescribeMismatch("bob", description);

            Assert.AreEqual("Failing matcher was bob", description.ToString());
        }

        [Test]
        public void Description_is_concatenated_from_matchers()
        {
            var matcher = Matches.AllOf(new[] { failingMatcher, successfulMatcher });
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.AreEqual("(Failing matcher and Successful matcher)", description.ToString());
        }
    }
}