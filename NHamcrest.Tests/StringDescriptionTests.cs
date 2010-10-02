using System.Text;
using MbUnit.Framework;
using NHamcrest.Internal;

namespace NHamcrest.Tests
{
    [TestsOn(typeof(StringDescription))]
    public class StringDescriptionTests
    {
        [Test]
        public void Default_ctor_provides_new_string_builder()
        {
            var desc = new StringDescription().ToString();

            Assert.AreEqual("", desc);
        }

        [Test]
        public void StringBuilder_passed_in_to_ctor_is_used()
        {
            var stringBuilder = new StringBuilder();
            var description = new StringDescription(stringBuilder);
            const string value = "test";

            stringBuilder.Append(value);

            Assert.AreEqual(value, description.ToString());
        }

        [Test]
        public void Static_ToString_returns_self_described_value()
        {
            const string description = "test";

            var text = StringDescription.ToString(new SelfDescribingValue<string>(description));

            Assert.AreEqual(description, text);
        }

        [Test]
        public void Static_AsString_returns_self_described_value()
        {
            const string description = "test";

            var text = StringDescription.AsString(new SelfDescribingValue<string>(description));

            Assert.AreEqual(description, text);
        }
    }
}
