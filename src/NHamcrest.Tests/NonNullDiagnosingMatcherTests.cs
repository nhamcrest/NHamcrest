using System;
using Xunit;

namespace NHamcrest.Tests
{
    public class NonNullDiagnosingMatcherTests
    {
        [Fact]
        public void No_match_if_object_is_null()
        {
            var matcher = new TestNonNullDiagnosingMatcher(s => true);

            var match = matcher.Matches(null);

            Assert.False(match, "Expected no match for null.");
        }

        [Fact]
        public void Pass_to_MatchesSafely_if_not_null()
        {
            var flag = false;
            var matcher = new TestNonNullDiagnosingMatcher(s => { flag = true; return true; });

            matcher.Matches("");

            Assert.True(flag, "Expected MatchesSafely(string, IDescription) to be called.");
        }

        [Fact]
        public void Use_base_mismatch_description_when_null()
        {
            var matcher = new TestNonNullDiagnosingMatcher(s => true);
            var description = new StringDescription();

            matcher.DescribeMismatch(null, description);

            Assert.Equal("was null", description.ToString());
        }

        [Fact]
        public void Use_mismatch_description_when_not_null()
        {
            var matcher = new TestNonNullDiagnosingMatcher(s => false);
            var description = new StringDescription();

            matcher.DescribeMismatch("", description);

            Assert.Equal("TestNonNullDiagnosingMatcher.MatchesSafely", description.ToString());
        }

        private class TestNonNullDiagnosingMatcher : NonNullDiagnosingMatcher<string>
        {
            private readonly Func<string, bool> match;

            public TestNonNullDiagnosingMatcher(Func<string, bool> match)
            {
                this.match = match;
            }

            protected override bool MatchesSafely(string collection, IDescription mismatchDescription)
            {
                if (match(collection))
                    return true;

                mismatchDescription.AppendText("TestNonNullDiagnosingMatcher.MatchesSafely");
                return false;
            }
        }
    }
}