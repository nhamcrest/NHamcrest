using System.Collections.Generic;
using MbUnit.Framework;
using NHamcrest.Internal;

namespace NHamcrest.Tests
{
    public class NullDescriptionTests
    {
        private NullDescription description;

        [SetUp]
        public void SetUp()
        {
            description = new NullDescription();
        }

        [Test]
        public void AppendText_returns_itself()
        {
            var returnedDescription = description.AppendText("test");

            Assert.AreEqual(description, returnedDescription);
        }

        [Test]
        public void AppendDescriptionOf_returns_itself()
        {
            var returnedDescription = description.AppendDescriptionOf(new SelfDescribingValue<string>(""));

            Assert.AreEqual(description, returnedDescription);
        }

        [Test]
        public void AppendValue_returns_itself()
        {
            var returnedDescription = description.AppendValue("");

            Assert.AreEqual(description, returnedDescription);
        }

        [Test]
        public void AppendValueList_with_params_returns_itself()
        {
            var returnedDescription = description.AppendValueList("", "", "", "");

            Assert.AreEqual(description, returnedDescription);
        }

        [Test]
        public void AppendValueList_with_enumerable_returns_itself()
        {
            var returnedDescription = description.AppendValueList("", "", "", (IEnumerable<string>)new List<string>());

            Assert.AreEqual(description, returnedDescription);
        }

        [Test]
        public void AppendList_with_params_returns_itself()
        {
            var returnedDescription = description.AppendList("", "", "", new List<ISelfDescribing>());

            Assert.AreEqual(description, returnedDescription);
        }

        [Test]
        public void ToString_returns_empty_string()
        {
            var toString = description.ToString();

            Assert.AreEqual("", toString);
        }
    }
}