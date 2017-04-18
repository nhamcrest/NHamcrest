using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NHamcrest.Core;
using Xunit;


namespace NHamcrest.Tests
{
    public class DescriptionTests
    {
        private readonly TestDescription _description;

        public DescriptionTests()
        {
            _description = new TestDescription();
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

            _description.AppendDescriptionOf(selfDescribingMock.Object);

            selfDescribingMock.Verify(sd => sd.DescribeTo(_description), Times.Once);
        }

        [Fact]
        public void Appending_a_null_value()
        {
            _description.AppendValue(null);

            Assert.Equal("null", _description.ToString());
        }

        [Fact]
        public void Appending_a_char()
        {
            _description.AppendValue('c');

            Assert.Equal("'c'", _description.ToString());
        }

        [Fact]
        public void Appending_a_long()
        {
            _description.AppendValue(5L);

            Assert.Equal("5L", _description.ToString());
        }

        [Fact]
        public void Appending_a_float()
        {
            _description.AppendValue(5f);

            Assert.Equal("5f", _description.ToString());
        }

        [Fact]
        public void Appending_a_decimal()
        {
            _description.AppendValue(5m);

            Assert.Equal("5m", _description.ToString());
        }

        [Fact]
        public void Appending_an_array_of_string_values()
        {
            _description.AppendValue(new[] { "one", "two", "three" });

            Assert.Equal("[\"one\", \"two\", \"three\"]", _description.ToString());
        }

        [Fact]
        public void Appending_an_array_of_number_values()
        {
            _description.AppendValue(new[] { 1, 2, 3 });

            Assert.Equal("[1, 2, 3]", _description.ToString());
        }

        [Fact]
        public void Appending_a_string()
        {
            const string value = "test";

            _description.AppendValue(value);

            Assert.Equal("\"" + value + "\"", _description.ToString());
        }

        [Fact]
        public void Appending_an_object()
        {
            var value = new TestObject();

            _description.AppendValue(value);

            Assert.Equal(value.ToString(), _description.ToString());
        }

        [Fact]
        public void Append_enumerable()
        {
            _description.AppendValueList("(", "'", ")", new List<string> { "a", "b", "c" });

            Assert.Equal("(\"a\"'\"b\"'\"c\")", _description.ToString());
        }

        [Fact(Skip = "Requires to solve self describing values issue")]
        public void Append_self_describing_values()
        {
            //var values = new List<ISelfDescribing>
            //{
            //    new SelfDescribingValue<int>(1),
            //    new SelfDescribingValue<int>(2),
            //    new SelfDescribingValue<int>(3),
            //};

            //_description.AppendList("!", ":", "@", values);

            //Assert.Equal("!1:2:3@", _description.ToString());
        }

        private class TestDescription : Description
        {
            private readonly StringBuilder _stringBuilder = new StringBuilder();

            protected override void Append(string str)
            {
                Console.WriteLine("Appended string: {0}", str);
                _stringBuilder.Append(str);
            }

            public override string ToString()
            {
                return _stringBuilder.ToString();
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