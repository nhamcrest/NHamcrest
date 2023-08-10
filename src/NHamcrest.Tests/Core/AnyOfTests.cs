using NHamcrest.Core;
using System;
using System.Collections.Generic;
using Xunit;

namespace NHamcrest.Tests.Core
{
    public class AnyOfTests
    {
        private readonly CustomMatcher<string> _failingMatcher = new CustomMatcher<string>("Failing matcher", s => false);
        private readonly CustomMatcher<string> _successfulMatcher = new CustomMatcher<string>("Successful matcher", s => true);
        private readonly CustomMatcher<string> _explodingMatcher = new CustomMatcher<string>("Exploding matcher!",
            s => throw new Exception("BANG!!!1!. Didn't expect exploding matcher to run."));

        [Fact]
        public void Match_if_any_matchers_succeed()
        {
            var matcher = Matches.AnyOf(_successfulMatcher, _failingMatcher);
            Assert.True(matcher.Matches(""));

            matcher = Matches.AnyOf(new List<IMatcher<string>> { _failingMatcher, _successfulMatcher });
            Assert.True(matcher.Matches(""));
        }

        [Fact]
        public void No_match_if_all_matchers_fail()
        {
            var matcher = Matches.AnyOf(_failingMatcher, _failingMatcher);

            Assert.False(matcher.Matches(""));
        }

        [Fact]
        public void Shortcut_matching_when_matcher_succeeds()
        {
            var matcher = Matches.AnyOf(_successfulMatcher, _explodingMatcher);
            Assert.True(matcher.Matches(""));
        }

        [Fact]
        public void Description_is_concatenated_from_matchers()
        {
            var matcher = Matches.AnyOf(_failingMatcher, _successfulMatcher);
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.Equal("(Failing matcher or Successful matcher)", description.ToString());
        }
    }
}