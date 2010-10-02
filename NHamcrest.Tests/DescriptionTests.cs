using System;
using System.Collections.Generic;
using System.Text;
using MbUnit.Framework;
using NHamcrest.Internal;
using Rhino.Mocks;

namespace NHamcrest.Tests
{
    [TestsOn(typeof(Description))]
    public class DescriptionTests
    {
        private TestDescription description;

        [SetUp]
        public void SetUp()
        {
            description = new TestDescription();
        }

        [Test]
        public void None_returns_a_null_description()
        {
            var nullDescription = Description.None;

            Assert.IsInstanceOfType(typeof(NullDescription), nullDescription);
        }

        [Test]
        public void AppendText_appends_each_char()
        {
            const string text = "test";

            description.AppendText(text);

            Assert.AreEqual(text.Length, description.Count);
        }

        [Test]
        public void AppendDescriptionOf_asks_value_to_describe_itself()
        {
            var selfDescribing = MockRepository.GenerateStub<ISelfDescribing>();

            description.AppendDescriptionOf(selfDescribing);

            selfDescribing.AssertWasCalled(sd => sd.DescribeTo(description));
        }

        [Test]
        public void Appending_a_null_value()
        {
            description.AppendValue(null);

            Assert.AreEqual("null", description.ToString());
        }

        [Test]
        public void Appending_a_char()
        {
            description.AppendValue('c');

            Assert.AreEqual("\"c\"", description.ToString());
        }

        [Test]
        public void Appending_a_long()
        {
            description.AppendValue(5L);

            Assert.AreEqual("5L", description.ToString());
        }

        [Test]
        public void Appending_a_float()
        {
            description.AppendValue(5f);

            Assert.AreEqual("5f", description.ToString());
        }

        [Test]
        public void Appending_a_decimal()
        {
            description.AppendValue(5m);

            Assert.AreEqual("5m", description.ToString());
        }

        [Test]
        public void Appending_an_array_of_values()
        {
            description.AppendValue(new [] { "one", "two", "three" });

            Assert.AreEqual("[one, two, three]", description.ToString());
        }

        [Test]
        public void Appending_a_string()
        {
            const string value = "test";

            description.AppendValue(value);

            Assert.AreEqual(value, description.ToString());
        }

        [Test]
        public void Appending_an_object()
        {
            var value = new TestObject();

            description.AppendValue(value);

            Assert.AreEqual(value.ToString(), description.ToString());
        }

        [Test]
        public void Append_enumerable()
        {
            description.AppendValueList("(", "'", ")", new List<string>{ "a", "b", "c" });

            Assert.AreEqual("(a'b'c)", description.ToString());
        }

        [Test]
        public void Append_self_describing_values()
        {
            var values = new List<ISelfDescribing>
                {
                  new SelfDescribingValue<int>(1),
                  new SelfDescribingValue<int>(2),
                  new SelfDescribingValue<int>(3),
                };
            
            description.AppendList("!", ":", "@", values);

            Assert.AreEqual("!1:2:3@", description.ToString());
        }

        private class TestDescription : Description
        {
            private readonly StringBuilder stringBuilder = new StringBuilder();

            public int Count { get; private set; }

            protected override void Append(string str)
            {
                Console.WriteLine("Appended string: {0}", str);
                stringBuilder.Append(str);
                base.Append(str);
            }

            protected override void Append(char c)
            {
                Count++;
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