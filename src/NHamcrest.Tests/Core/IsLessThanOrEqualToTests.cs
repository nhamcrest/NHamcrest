using NHamcrest.Core;
using Xunit;
using NHAssert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class IsLessThanOrEqualToTests
    {
        [Fact]
        public void Match_if_less()
        {
            const int five = 5;

            NHAssert.That(five, Is.LessThanOrEqualTo(6));
        }

        [Fact]
        public void No_match_if_not_less()
        {
            var lessThanOrEqualTo = new IsLessThanOrEqualTo<int>(8);

            var matches = lessThanOrEqualTo.Matches(9);

            Assert.False(matches);
        }

        [Fact]
        public void Match_if_equal()
        {
            var isLessThanOrEqualTo = new IsLessThanOrEqualTo<int>(8);

            var matches = isLessThanOrEqualTo.Matches(8);

            Assert.True(matches);
        }

        [Fact]
        public void Append_description()
        {
            const int six = 6;
            var lessThan = Is.LessThanOrEqualTo(six);
            var description = new StringDescription();

            lessThan.DescribeTo(description);

            Assert.Equal(description.ToString(), "less than or equal to " + six);
        }
    }
}