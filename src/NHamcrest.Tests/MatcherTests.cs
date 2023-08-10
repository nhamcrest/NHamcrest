using NHamcrest.Core;
using NSubstitute;
using Xunit;

namespace NHamcrest.Tests
{
    public class MatcherTests
    {
        [Fact]
        public void DescribeMismatch_appends_item()
        {
            const string item = "item";
            var matcher = new TestMatcher();
            var descriptionMock = Substitute.For<IDescription>();
            descriptionMock.AppendText(Arg.Any<string>()).Returns(descriptionMock);

            matcher.DescribeMismatch(item, descriptionMock);

            descriptionMock.AppendText("was ").Received();
            descriptionMock.AppendValue(item).Received();
        }

        [Fact]
        public void To_string()
        {
            const string text = "text";
            var matcher = new TestMatcher(text);

            var toString = matcher.ToString();

            Assert.Equal(text, toString);
        }

        [Fact]
        public void Matches_returns_false()
        {
            var matcher = new TestMatcher("");

            var matches = matcher.Matches("");

            Assert.False(matches);
        }

        [Fact]
        public void Describe_to()
        {
            var matcher = new TestMatcher("");
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.Equal("", description.ToString());
        }

        private class TestMatcher : Matcher<string>
        {
            private readonly string _text;

            public TestMatcher() : this("") { }

            public TestMatcher(string text)
            {
                _text = text;
            }

            public override void DescribeTo(IDescription description)
            {
                base.DescribeTo(description);
                description.AppendText(_text);
            }
        }
    }
}