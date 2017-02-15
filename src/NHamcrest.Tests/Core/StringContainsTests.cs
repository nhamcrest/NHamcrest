using MbUnit.Framework;
using NHamcrest.Core;
using Assert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class StringContainsTests
    {
        [Test]
        public void Match_if_string_contains_substring()
        {
            Assert.That("the cat sat on the mat", Contains.String("cat"));
        }

        [Test]
        public void Match_if_case_insensitive_string_contains_substring()
        {
            Assert.That("the Cat sat on the mat", Contains.String("cat").CaseInsensitive());
        }

        [Test]
        public void No_match_if_string_does_not_contain_substring()
        {
            var matcher = Contains.String("bob");

            var matches = matcher.Matches("the cat sat on the mat");

            Assert.That(matches, Is.False());
        }

        [Test]
        public void Describe_mismatch()
        {
            var matcher = Contains.String("bob");
            var description = new StringDescription();

            matcher.DescribeMismatch("the cat sat on the mat", description);

            Assert.That(description.ToString(), Is.EqualTo("was \"the cat sat on the mat\""));
        }

        [Test]
        public void Describe_to()
        {
            var matcher = Contains.String("bob");
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.That(description.ToString(), Is.EqualTo("a string containing \"bob\""));
        }
    }
}