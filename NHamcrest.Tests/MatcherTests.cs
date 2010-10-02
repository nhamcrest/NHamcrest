using MbUnit.Framework;
using Rhino.Mocks;

namespace NHamcrest.Tests
{
    public class MatcherTests
    {
        [Test]
        public void DescribeMismatch_appends_item()
        {
            var matcher = new TestMatcher();
            var description = MockRepository.GenerateStub<IDescription>();
            description.Stub(d => d.AppendText(Arg<string>.Is.Anything)).Return(description);
            const string item = "item";
            
            matcher.DescribeMismatch(item, description);

            description.AssertWasCalled(d => d.AppendText("was "));
            description.AssertWasCalled(d => d.AppendValue(item));
        }

        [Test]
        public void ToString()
        {
            const string text = "text";
            var matcher = new TestMatcher(text);

            var toString = matcher.ToString();

            Assert.AreEqual(text, toString);
        }

        private class TestMatcher : Matcher<string>
        {
            private readonly string text;

            public TestMatcher() : this("") { }

            public TestMatcher(string text)
            {
                this.text = text;
            }

            public override void DescribeTo(IDescription description)
            {
                description.AppendText(text);
            }
        }
    }
}