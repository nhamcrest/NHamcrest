using NHamcrest.Core;
using System;
using Xunit;

namespace NHamcrest.Tests
{
    public class DiagnosingMatcherTests
    {
        [Fact]
        public void Matches_calls_matches()
        {
            var flag = false;
            var matcher = new TestDiagnosingMatcher(s => { flag = true; return true; });

            matcher.Matches("");

            Assert.True(flag, "Expected Matches(T, IDescription) to be called.");
        }

        [Fact]
        public void Matches_uses_a_null_description()
        {
            var matcher = new TestDiagnosingMatcher(s => true);

            matcher.Matches("");
        }

        [Fact]
        public void DescribeMismatch_calls_matches()
        {
            var flag = false;
            var matcher = new TestDiagnosingMatcher(s => { flag = true; return true; });

            matcher.DescribeMismatch("", Description.None);

            Assert.True(flag, "Expected Matches(T, IDescription) to be called.");
        }

        private class TestDiagnosingMatcher : DiagnosingMatcher<string>
        {
            private readonly Func<string, bool> _match;

            public TestDiagnosingMatcher(Func<string, bool> match)
            {
                _match = match;
            }

            protected override bool Matches(string item, IDescription mismatchDescription)
            {
                return mismatchDescription.GetType() != typeof(NullDescription) 
                    ? throw new Exception("Expected null description") 
                    : _match(item);
            }
        }
    }
}