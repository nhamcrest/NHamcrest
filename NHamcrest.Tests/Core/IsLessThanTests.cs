using MbUnit.Framework;
using NHamcrest.Core;
using NHAssert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
	public class IsLessThanTests
	{
		[Test]
		public void Match_if_less()
		{
			const int five = 5;

			NHAssert.That(five, Is.LessThan(6));
		}

		[Test]
		public void No_match_if_not_less()
		{
			var isLessThanEight = new IsLessThan<int>(8);

			var matches = isLessThanEight.Matches(9);

			Assert.IsFalse(matches);
		}

		[Test]
		public void No_match_if_equal()
		{
			var isLessThanEight = new IsLessThan<int>(8);

			var matches = isLessThanEight.Matches(8);

			Assert.IsFalse(matches);
		}

		[Test]
		public void Append_description()
		{
			const int six = 6;
			var lessThan = Is.LessThan(six);
			var description = new StringDescription();

			lessThan.DescribeTo(description);

			Assert.AreEqual(description.ToString(), "less than " + six);
		}
	}
}