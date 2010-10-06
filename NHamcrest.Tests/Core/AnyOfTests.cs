using System;
using MbUnit.Framework;
using NHamcrest.Core;

namespace NHamcrest.Tests.Core
{
    public class AnyOfTests
    {
        private readonly CustomMatcher<string> failingMatcher = new CustomMatcher<string>("Failing matcher", s => false);
        private readonly CustomMatcher<string> successfulMatcher = new CustomMatcher<string>("Successful matcher", s => true);
        private readonly CustomMatcher<string> explodingMatcher = new CustomMatcher<string>("Exploding matcher!",
            s => { throw new Exception("BANG!!!1!. Didn't expect exploding matcher to run."); });

        [Test]
        public void Match_if_any_matchers_succeed()
        {
            var matcher = Matches.AnyOf(new[] { successfulMatcher, failingMatcher });
            Assert.AreEqual(true, matcher.Matches(""), "Expected match if any matchers succeed.");

            matcher = Matches.AnyOf(new[] { failingMatcher, successfulMatcher });
            Assert.AreEqual(true, matcher.Matches(""), "Expected match if any matchers succeed.");
        }

        [Test]
        public void No_match_if_all_matchers_fail()
        {
            var matcher = Matches.AnyOf(new[] { failingMatcher, failingMatcher });

            Assert.AreEqual(false, matcher.Matches(""), "Expected no match if all matchers fail.");
        }

        [Test]
        public void Shortcut_matching_when_matcher_succeeds()
        {
            var matcher = Matches.AnyOf(new[] { successfulMatcher, explodingMatcher });
            Assert.AreEqual(true, matcher.Matches(""));
        }

        [Test]
        public void Description_is_concatenated_from_matchers()
        {
            var matcher = Matches.AnyOf(new[] { failingMatcher, successfulMatcher });
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.AreEqual("(Failing matcher or Successful matcher)", description.ToString());
        }
    }
}