using NHamcrest.Core;
using Xunit;
using NHAssert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class IsEqualTests
    {
        [Fact]
        public void Match_if_equal()
        {
            NHAssert.That(true, Is.True());
        }

        [Fact]
        public void No_match_if_not_equal()
        {
            var isEqual = new IsEqualMatcher<string>("test");

            var matches = isEqual.Matches("somethingelse");

            Assert.False(matches);
        }

        [Fact]
        public void No_match_if_compared_to_null()
        {
            var isEqual = new IsEqualMatcher<string>("test");

            var matches = isEqual.Matches(null);

            Assert.False(matches);
            Assert.False(Is.EqualTo<string>(null).Matches("test"));
        }

        [Fact]
        public void Append_description()
        {
            const string test = "test";
            var isEqual = IsEqualMatcher<string>.EqualTo(test);
            var description = new StringDescription();

            isEqual.DescribeTo(description);

            Assert.Equal($"\"{test}\"", description.ToString());
        }
    }
}