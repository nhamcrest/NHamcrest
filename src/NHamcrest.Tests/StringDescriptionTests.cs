using NHamcrest.Core;
using System.Text;
using Xunit;

namespace NHamcrest.Tests
{
    public class StringDescriptionTests
    {
        [Fact]
        public void Default_ctor_provides_new_string_builder()
        {
            var desc = new StringDescription().ToString();

            Assert.Equal("", desc);
        }

        [Fact]
        public void StringBuilder_passed_in_to_ctor_is_used()
        {
            var stringBuilder = new StringBuilder();
            var description = new StringDescription(stringBuilder);
            const string value = "test";

            stringBuilder.Append(value);

            Assert.Equal(value, description.ToString());
        }

        [Fact]
        public void Append_string()
        {
            var description = new StringDescription(new StringBuilder());
            const string value = "test";

            description.AppendText(value);

            Assert.Equal(value, description.ToString());
        }

        [Fact]
        public void Append_char()
        {
            var description = new StringDescription(new StringBuilder());
            const char value = 't';

            description.AppendValue(value);

            Assert.Equal("'t'", description.ToString());
        }

        //[Fact(Skip = "Self describing values")]
        //public void Static_ToString_returns_self_described_value()
        //{
            //const string description = "test";

            //var text = StringDescription.ToString(new SelfDescribingValue<string>(description));

            //Assert.Equal(description, text);
        //}

        //[Fact(Skip = "Self describing values")]
        //public void Static_AsString_returns_self_described_value()
        //{
            //const string description = "test";

            //var text = StringDescription.AsString(new SelfDescribingValue<string>(description));

            //Assert.Equal(description, text);
        //}
    }
}
