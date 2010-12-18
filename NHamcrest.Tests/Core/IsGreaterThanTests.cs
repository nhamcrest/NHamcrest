using MbUnit.Framework;
using NHamcrest.Core;
using NHAssert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
	public class IsGreaterThanTests
	{
		[Test]
		public void Match_if_greater()
		{
			const int five = 5;

			NHAssert.That(five, Is.GreaterThan(3));
		}

		[Test]
		public void No_match_if_not_greater()
		{
			var isGreaterThanOne = new IsGreaterThan<int>(1);

			var matches = isGreaterThanOne.Matches(0);

			Assert.IsFalse(matches);
		}

		[Test]
		public void No_match_if_equal()
		{
			var isGreaterThanOne = new IsGreaterThan<int>(1);

			var matches = isGreaterThanOne.Matches(1);

			Assert.IsFalse(matches);
		}

		[Test]
		public void Append_description()
		{
			const int six = 6;
			var greaterThan = Is.GreaterThan(six);
			var description = new StringDescription();

			greaterThan.DescribeTo(description);

			Assert.AreEqual(description.ToString(), "greater than " + six);
		}
	}
}