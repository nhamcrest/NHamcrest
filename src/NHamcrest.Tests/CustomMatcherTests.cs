using System;
using MbUnit.Framework;
using Rhino.Mocks;

namespace NHamcrest.Tests
{
    public class CustomMatcherTests
    {
    	[Test]
    	public void Ctor_throws_if_description_is_null()
    	{
			Assert.Throws<ArgumentNullException>(() => new CustomMatcher<string>(null, s => true));
    	}

        [Test]
        public void DescribeTo_appends_provided_description()
        {
            const string fixedDescription = "description";
            var matcher = new CustomMatcher<string>(fixedDescription, s => true);
            var description = MockRepository.GenerateStub<IDescription>();

            matcher.DescribeTo(description);

            description.AssertWasCalled(d => d.AppendText(fixedDescription), 
                o => o.Message("AppendText was not called on the provided IDescription."));
        }

        [Test]
        public void Matches_uses_supplied_func()
        {
            var flag = false;
            var matcher = new CustomMatcher<string>("", s => { flag = true; return true; });

            matcher.Matches("");

            Assert.IsTrue(flag, "The Func passed into the custom matcher was not called.");
        }
    }
}