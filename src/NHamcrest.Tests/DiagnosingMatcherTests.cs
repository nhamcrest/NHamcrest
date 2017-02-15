using System;
using MbUnit.Framework;

namespace NHamcrest.Tests
{
    public class DiagnosingMatcherTests
    {
        [Test]
        public void Matches_calls_matches()
        {
            var flag = false;
            var matcher = new TestDiagnosingMatcher(s => { flag = true; return true; });

            matcher.Matches("");

            Assert.IsTrue(flag, "Expected Matches(T, IDescription) to be called.");
        }

        [Test]
        public void Matches_uses_a_null_description()
        {
            var matcher = new TestDiagnosingMatcher(s => true);

            matcher.Matches("");
        }

        [Test]
        public void DescribeMismatch_calls_matches()
        {
            var flag = false;
            var matcher = new TestDiagnosingMatcher(s => { flag = true; return true; });

            matcher.DescribeMismatch("", Description.None);

            Assert.IsTrue(flag, "Expected Matches(T, IDescription) to be called.");
        }

        private class TestDiagnosingMatcher : DiagnosingMatcher<string>
        {
            private readonly Func<string, bool> match;

            public TestDiagnosingMatcher(Func<string, bool> match)
            {
                this.match = match;
            }

            protected override bool Matches(string item, IDescription mismatchDescription)
            {
                if (mismatchDescription.GetType() != typeof(NullDescription))
                    throw new Exception("Expected null description");

                return match(item);
            }
        }
    }
}