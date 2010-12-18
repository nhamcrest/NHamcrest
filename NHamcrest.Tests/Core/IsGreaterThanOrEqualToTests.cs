using MbUnit.Framework;
using NHamcrest.Core;
using NHAssert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
	public class IsGreaterThanOrEqualToTests
	{
		[Test]
		public void Match_if_greater()
		{
			const int five = 5;

			NHAssert.That(five, Is.GreaterThanOrEqualTo(3));
		}

		[Test]
		public void No_match_if_less()
		{
			var isGreaterThanOrEqualToOne = new IsGreaterThanOrEqualTo<int>(1);

			var matches = isGreaterThanOrEqualToOne.Matches(0);

			Assert.IsFalse(matches);
		}

		[Test]
		public void Match_if_equal()
		{
			var isGreaterThanOrEqualToOne = new IsGreaterThanOrEqualTo<int>(1);

			var matches = isGreaterThanOrEqualToOne.Matches(1);

			Assert.IsTrue(matches);
		}

		[Test]
		public void Append_description()
		{
			const int six = 6;
			var greaterThan = Is.GreaterThanOrEqualTo(six);
			var description = new StringDescription();

			greaterThan.DescribeTo(description);

			Assert.AreEqual(description.ToString(), "greater than or equal to " + six);
		}
	}
}