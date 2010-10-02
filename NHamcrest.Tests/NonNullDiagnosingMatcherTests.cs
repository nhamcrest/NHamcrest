using System;
using MbUnit.Framework;

namespace NHamcrest.Tests
{
    public class NonNullDiagnosingMatcherTests
    {
        [Test]
        public void No_match_if_object_is_null()
        {
            var matcher = new TestNonNullDiagnosingMatcher(s => true);

            var match = matcher.Matches(null);

            Assert.IsFalse(match, "Expected no match for null.");
        }

        [Test]
        public void Pass_to_MatchesSafely_if_not_null()
        {
            var flag = false;
            var matcher = new TestNonNullDiagnosingMatcher(s => { flag = true; return true; });

            matcher.Matches("");

            Assert.IsTrue(flag, "Expected MatchesSafely(string, IDescription) to be called.");
        }

        [Test]
        public void Use_base_mismatch_description_when_null()
        {
            var matcher = new TestNonNullDiagnosingMatcher(s => true);
            var description = new StringDescription();

            matcher.DescribeMismatch(null, description);

            Assert.AreEqual("was null", description.ToString());
        }

        [Test]
        public void Use_mismatch_description_when_not_null()
        {
            var matcher = new TestNonNullDiagnosingMatcher(s => false);
            var description = new StringDescription();

            matcher.DescribeMismatch("", description);

            Assert.AreEqual("TestNonNullDiagnosingMatcher.MatchesSafely", description.ToString());
        }

        private class TestNonNullDiagnosingMatcher : NonNullDiagnosingMatcher<string>
        {
            private readonly Func<string, bool> match;

            public TestNonNullDiagnosingMatcher(Func<string, bool> match)
            {
                this.match = match;
            }

            protected override bool MatchesSafely(string item, IDescription mismatchDescription)
            {
                if (match(item))
                    return true;

                mismatchDescription.AppendText("TestNonNullDiagnosingMatcher.MatchesSafely");
                return false;
            }
        }
    }
}