using MbUnit.Framework;
using NHamcrest.Core;
using NHAssert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
	public class IsLessThanOrEqualToTests
	{
		[Test]
		public void Match_if_less()
		{
			const int five = 5;

			NHAssert.That(five, Is.LessThanOrEqualTo(6));
		}

		[Test]
		public void No_match_if_not_less()
		{
			var lessThanOrEqualTo = new IsLessThanOrEqualTo<int>(8);

			var matches = lessThanOrEqualTo.Matches(9);

			Assert.IsFalse(matches);
		}

		[Test]
		public void Match_if_equal()
		{
			var isLessThanOrEqualTo = new IsLessThanOrEqualTo<int>(8);

			var matches = isLessThanOrEqualTo.Matches(8);

			Assert.IsTrue(matches);
		}

		[Test]
		public void Append_description()
		{
			const int six = 6;
			var lessThan = Is.LessThanOrEqualTo(six);
			var description = new StringDescription();

			lessThan.DescribeTo(description);

			Assert.AreEqual(description.ToString(), "less than or equal to " + six);
		}
	}
}