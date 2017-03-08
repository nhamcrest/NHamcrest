using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NHamcrest.Internal;
using Xunit;


namespace NHamcrest.Tests
{
    public class DescriptionTests
    {
        private TestDescription description;

        public DescriptionTests()
        {
            description = new TestDescription();
        }

        [Fact]
        public void None_returns_a_null_description()
        {
            var nullDescription = Description.None;

            Assert.IsType(typeof(NullDescription), nullDescription);
        }

        [Fact]
        public void AppendDescriptionOf_asks_value_to_describe_itself()
        {
            var selfDescribingMock = new Mock<ISelfDescribing>();

            description.AppendDescriptionOf(selfDescribingMock.Object);

            selfDescribingMock.Verify(sd => sd.DescribeTo(description), Times.Once);
        }

        [Fact]
        public void Appending_a_null_value()
        {
            description.AppendValue(null);

            Assert.Equal("null", description.ToString());
        }

        [Fact]
        public void Appending_a_char()
        {
            description.AppendValue('c');

            Assert.Equal("\"c\"", description.ToString());
        }

        [Fact]
        public void Appending_a_long()
        {
            description.AppendValue(5L);

            Assert.Equal("5L", description.ToString());
        }

        [Fact]
        public void Appending_a_float()
        {
            description.AppendValue(5f);

            Assert.Equal("5f", description.ToString());
        }

        [Fact]
        public void Appending_a_decimal()
        {
            description.AppendValue(5m);

            Assert.Equal("5m", description.ToString());
        }

        [Fact]
        public void Appending_an_array_of_values()
        {
            description.AppendValue(new[] { "one", "two", "three" });

            Assert.Equal("[one, two, three]", description.ToString());
        }

        [Fact]
        public void Appending_a_string()
        {
            const string value = "test";

            description.AppendValue(value);

            Assert.Equal(value, description.ToString());
        }

        [Fact]
        public void Appending_an_object()
        {
            var value = new TestObject();

            description.AppendValue(value);

            Assert.Equal(value.ToString(), description.ToString());
        }

        [Fact]
        public void Append_enumerable()
        {
            description.AppendValueList("(", "'", ")", new List<string> { "a", "b", "c" });

            Assert.Equal("(a'b'c)", description.ToString());
        }

        [Fact]
        public void Append_self_describing_values()
        {
            var values = new List<ISelfDescribing>
                {
                  new SelfDescribingValue<int>(1),
                  new SelfDescribingValue<int>(2),
                  new SelfDescribingValue<int>(3),
                };

            description.AppendList("!", ":", "@", values);

            Assert.Equal("!1:2:3@", description.ToString());
        }

        private class TestDescription : Description
        {
            private readonly StringBuilder stringBuilder = new StringBuilder();

            protected override void Append(string str)
            {
                Console.WriteLine("Appended string: {0}", str);
                stringBuilder.Append(str);
            }

            public override string ToString()
            {
                return stringBuilder.ToString();
            }
        }

        private class TestObject
        {
            public override string ToString()
            {
                return "Hello!";
            }
        }
    }
}