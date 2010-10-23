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
            const string test = "test";

            NHAssert.That(test, Is.EqualTo(test));
        }

        [Test]
        public void No_match_if_not_equal()
        {
            var isEqual = new IsEqual<string>("test");

            var matches = isEqual.Matches("somethingelse");

            Assert.IsFalse(matches);
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