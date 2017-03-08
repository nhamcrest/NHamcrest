
using Moq;
using NHamcrest.Internal;
using Xunit;


namespace NHamcrest.Tests.Internal
{
    public class SelfDescribingValueTests
    {
        [Fact]
        public void Value_is_appended_to_description()
        {
            const string value = "test";
            var selfDescribingValue = new SelfDescribingValue<string>(value);
            var descriptionMock = new Mock<IDescription>();

            selfDescribingValue.DescribeTo(descriptionMock.Object);

            descriptionMock.Verify(d => d.AppendValue(value), Times.Once);
        }
    }
}