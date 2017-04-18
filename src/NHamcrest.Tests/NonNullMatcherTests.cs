
using System;
using NHamcrest.Core;
using Xunit;

namespace NHamcrest.Tests
{
    public class NonNullMatcherTests
    {
        [Fact]
        public void No_match_if_object_is_null()
        {
            var matcher = new TestNonNullMatcher(s => true);

            var match = matcher.Matches(null);

            Assert.False(match, "Expected no match for null.");
        }

        [Fact]
        public void Pass_to_MatchesSafely_if_not_null()
        {
            var flag = false;
            var matcher = new TestNonNullMatcher(s => { flag = true; return true; });

            matcher.Matches("");

            Assert.True(flag, "Expected MatchesSafely(string, IDescription) to be called.");
        }

        [Fact]
        public void Use_base_mismatch_description_when_null()
        {
            var matcher = new TestNonNullMatcher(s => true);
            var description = new StringDescription();

            matcher.DescribeMismatch(null, description);

            Assert.Equal("was null", description.ToString());
        }

        [Fact]
        public void Use_mismatch_description_when_not_null()
        {
            var matcher = new TestNonNullMatcher(s => false);
            var description = new StringDescription();

            matcher.DescribeMismatch("", description);

            Assert.Equal("TestNonNullMatcher.DescribeMismatchSafely", description.ToString());
        }

        [Fact]
        public void Describe_mismatch_safely()
        {
            var matcher = new TestNonNullMatcher(s => false);
            var description = new StringDescription();

            matcher.DescribeMismatchSafely2("something", description);

            Assert.Equal("was \"something\"", description.ToString());
        }

        private class TestNonNullMatcher : NonNullMatcher<string>
        {
            private readonly Func<string, bool> _match;

            public TestNonNullMatcher(Func<string, bool> match)
            {
                _match = match;
            }

            protected override bool MatchesSafely(string item)
            {
                return _match(item);
            }

            public override void DescribeMismatchSafely(string item, IDescription mismatchDescription)
            {
                mismatchDescription.AppendText("TestNonNullMatcher.DescribeMismatchSafely");
            }

            public void DescribeMismatchSafely2(string item, IDescription mismatchDescription)
            {
                base.DescribeMismatchSafely(item, mismatchDescription);
            }
        }
    }
}