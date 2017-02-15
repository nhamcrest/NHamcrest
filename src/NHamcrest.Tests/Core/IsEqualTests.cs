using MbUnit.Framework;
using NHamcrest.Core;
using NHAssert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class IsEqualTests
    {
        [Test]
        public void Match_if_equal()
        {
            NHAssert.That(true, Is.True());
        }

        [Test]
        public void No_match_if_not_equal()
        {
            var isEqual = new IsEqual<string>("test");

            var matches = isEqual.Matches("somethingelse");

            Assert.IsFalse(matches);
        }

		[Test]
		public void No_match_if_compared_to_null()
		{
			var isEqual = new IsEqual<string>("test");

			var matches = isEqual.Matches(null);

			Assert.IsFalse(matches);
			Assert.IsFalse(Is.EqualTo<string>(null).Matches("test"));
		}

        [Test]
        public void Append_description()
        {
            const string test = "test";
            var isEqual = IsEqual<string>.EqualTo(test);
            var description = new StringDescription();

            isEqual.DescribeTo(description);

            Assert.AreEqual(description.ToString(), test);
        }
    }
}