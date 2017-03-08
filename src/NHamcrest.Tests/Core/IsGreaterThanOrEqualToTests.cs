
using NHamcrest.Core;
using Xunit;
using NHAssert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
	public class IsGreaterThanOrEqualToTests
	{
		[Fact]
		public void Match_if_greater()
		{
			const int five = 5;

			NHAssert.That(five, Is.GreaterThanOrEqualTo(3));
		}

		[Fact]
		public void No_match_if_less()
		{
			var isGreaterThanOrEqualToOne = new IsGreaterThanOrEqualTo<int>(1);

			var matches = isGreaterThanOrEqualToOne.Matches(0);

			Assert.False(matches);
		}

		[Fact]
		public void Match_if_equal()
		{
			var isGreaterThanOrEqualToOne = new IsGreaterThanOrEqualTo<int>(1);

			var matches = isGreaterThanOrEqualToOne.Matches(1);

			Assert.True(matches);
		}

		[Fact]
		public void Append_description()
		{
			const int six = 6;
			var greaterThan = Is.GreaterThanOrEqualTo(six);
			var description = new StringDescription();

			greaterThan.DescribeTo(description);

			Assert.Equal(description.ToString(), "greater than or equal to " + six);
		}
	}
}