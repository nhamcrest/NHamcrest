using System;
using System.Collections.Generic;

using NHamcrest.Core;
using Xunit;

namespace NHamcrest.Tests.Core
{
    public class AnyOfTests
    {
        private readonly CustomMatcher<string> failingMatcher = new CustomMatcher<string>("Failing matcher", s => false);
        private readonly CustomMatcher<string> successfulMatcher = new CustomMatcher<string>("Successful matcher", s => true);
        private readonly CustomMatcher<string> explodingMatcher = new CustomMatcher<string>("Exploding matcher!",
            s => { throw new Exception("BANG!!!1!. Didn't expect exploding matcher to run."); });

        [Fact]
        public void Match_if_any_matchers_succeed()
        {
            var matcher = Matches.AnyOf(new[] { successfulMatcher, failingMatcher });
            Assert.Equal(true, matcher.Matches(""));

            matcher = Matches.AnyOf(new List<IMatcher<string>> { failingMatcher, successfulMatcher });
            Assert.Equal(true, matcher.Matches(""));
        }

        [Fact]
        public void No_match_if_all_matchers_fail()
        {
            var matcher = Matches.AnyOf(new[] { failingMatcher, failingMatcher });

            Assert.Equal(false, matcher.Matches(""));
        }

        [Fact]
        public void Shortcut_matching_when_matcher_succeeds()
        {
            var matcher = Matches.AnyOf(new[] { successfulMatcher, explodingMatcher });
            Assert.Equal(true, matcher.Matches(""));
        }

        [Fact]
        public void Description_is_concatenated_from_matchers()
        {
            var matcher = Matches.AnyOf(new[] { failingMatcher, successfulMatcher });
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.Equal("(Failing matcher or Successful matcher)", description.ToString());
        }
    }
}