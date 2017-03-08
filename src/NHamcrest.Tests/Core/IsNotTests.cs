
using NHamcrest.Core;
using Xunit;
using NHamcrest.XUnit;
using Assert = NHamcrest.XUnit.Assert;

namespace NHamcrest.Tests.Core
{
    public class IsNotTests
    {
        private readonly IMatcher<string> always = new CustomMatcher<string>("Always", s => true);

        [Fact]
        public void No_match_if_inner_matcher_matches()
        {
            var isNot = new IsNot<string>(always);

            var matches = isNot.Matches("test");

            Assert.That(matches, Is.EqualTo(false));
        }

        [Fact]
        public void Match_if_inner_matcher_does_not_match()
        {
            Assert.That("test", Is.Not("somethingelse"));
        }

        [Fact]
        public void Description_adds_not()
        {
            var matcher = Is.Not(always);
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.That(description.ToString(), Is.EqualTo("not Always"));
        }
    }
}