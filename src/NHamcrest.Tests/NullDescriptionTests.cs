using System.Collections.Generic;

using NHamcrest.Internal;
using Xunit;

namespace NHamcrest.Tests
{
    public class NullDescriptionTests
    {
        private NullDescription description;

        public NullDescriptionTests()
        {
            description = new NullDescription();
        }

        [Fact]
        public void AppendText_returns_itself()
        {
            var returnedDescription = description.AppendText("test");

            Assert.Equal(description, returnedDescription);
        }

        [Fact]
        public void AppendDescriptionOf_returns_itself()
        {
            var returnedDescription = description.AppendDescriptionOf(new SelfDescribingValue<string>(""));

            Assert.Equal(description, returnedDescription);
        }

        [Fact]
        public void AppendValue_returns_itself()
        {
            var returnedDescription = description.AppendValue("");

            Assert.Equal(description, returnedDescription);
        }

        [Fact]
        public void AppendValueList_with_params_returns_itself()
        {
            var returnedDescription = description.AppendValueList("", "", "", "");

            Assert.Equal(description, returnedDescription);
        }

        [Fact]
        public void AppendValueList_with_enumerable_returns_itself()
        {
            var returnedDescription = description.AppendValueList("", "", "", (IEnumerable<string>)new List<string>());

            Assert.Equal(description, returnedDescription);
        }

        [Fact]
        public void AppendList_with_params_returns_itself()
        {
            var returnedDescription = description.AppendList("", "", "", new List<ISelfDescribing>());

            Assert.Equal(description, returnedDescription);
        }

        [Fact]
        public void ToString_returns_empty_string()
        {
            var toString = description.ToString();

            Assert.Equal("", toString);
        }
    }
}