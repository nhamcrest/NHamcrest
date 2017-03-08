using System.Collections.Generic;

using NHamcrest.Internal;
using Xunit;

namespace NHamcrest.Tests
{
    public class NullDescriptionTests
    {
        private NullDescription _description;

        public NullDescriptionTests()
        {
            _description = new NullDescription();
        }

        [Fact]
        public void AppendText_returns_itself()
        {
            var returnedDescription = _description.AppendText("test");

            Assert.Equal(_description, returnedDescription);
        }

        [Fact]
        public void AppendDescriptionOf_returns_itself()
        {
            var returnedDescription = _description.AppendDescriptionOf(new SelfDescribingValue<string>(""));

            Assert.Equal(_description, returnedDescription);
        }

        [Fact]
        public void AppendValue_returns_itself()
        {
            var returnedDescription = _description.AppendValue("");

            Assert.Equal(_description, returnedDescription);
        }

        [Fact]
        public void AppendValueList_with_params_returns_itself()
        {
            var returnedDescription = _description.AppendValueList("", "", "", "");

            Assert.Equal(_description, returnedDescription);
        }

        [Fact]
        public void AppendValueList_with_enumerable_returns_itself()
        {
            var returnedDescription = _description.AppendValueList("", "", "", (IEnumerable<string>)new List<string>());

            Assert.Equal(_description, returnedDescription);
        }

        [Fact]
        public void AppendList_with_params_returns_itself()
        {
            var returnedDescription = _description.AppendList("", "", "", new List<ISelfDescribing>());

            Assert.Equal(_description, returnedDescription);
        }

        [Fact]
        public void ToString_returns_empty_string()
        {
            var toString = _description.ToString();

            Assert.Equal("", toString);
        }
    }
}