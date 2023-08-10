using NHamcrest.Core;
using NSubstitute;
using System;
using Xunit;

namespace NHamcrest.Tests
{
    public class CustomMatcherTests
    {
        [Fact]
        public void Ctor_throws_if_description_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new NHamcrest.Core.CustomMatcher<string>(null, s => true));
        }

        [Fact]
        public void DescribeTo_appends_provided_description()
        {
            const string fixedDescription = "description";
            var matcher = new CustomMatcher<string>(fixedDescription, s => true);
            var descriptionMock = Substitute.For<IDescription>();

            matcher.DescribeTo(descriptionMock);

            descriptionMock.Received().AppendText(fixedDescription);
        }

        [Fact]
        public void Matches_uses_supplied_func()
        {
            var flag = false;
            var matcher = new CustomMatcher<string>("", s => { flag = true; return true; });

            matcher.Matches("");

            Assert.True(flag, "The Func passed into the custom matcher was not called.");
        }
    }
}