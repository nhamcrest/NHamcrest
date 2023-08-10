using NHamcrest.Core;
using Xunit;
using NHAssert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class IsGreaterThanTests
    {
        [Fact]
        public void Match_if_greater()
        {
            const int five = 5;

            NHAssert.That(five, Is.GreaterThan(3));
        }

        [Fact]
        public void No_match_if_not_greater()
        {
            var isGreaterThanOne = new IsGreaterThan<int>(1);

            var matches = isGreaterThanOne.Matches(0);

            Assert.False(matches);
        }

        [Fact]
        public void No_match_if_equal()
        {
            var isGreaterThanOne = new IsGreaterThan<int>(1);

            var matches = isGreaterThanOne.Matches(1);

            Assert.False(matches);
        }

        [Fact]
        public void Append_description()
        {
            const int six = 6;
            var greaterThan = Is.GreaterThan(six);
            var description = new StringDescription();

            greaterThan.DescribeTo(description);

            Assert.Equal(description.ToString(), "greater than " + six);
        }
    }
}