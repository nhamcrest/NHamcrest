using MbUnit.Framework;
using NHamcrest.Internal;
using Rhino.Mocks;

namespace NHamcrest.Tests.Internal
{
    public class SelfDescribingValueTests
    {
        [Test]
        public void Value_is_appended_to_description()
        {
            const string value = "test";
            var selfDescribingValue = new SelfDescribingValue<string>(value);
            var description = MockRepository.GenerateStub<IDescription>();

            selfDescribingValue.DescribeTo(description);

            description.AssertWasCalled(d => d.AppendValue(value));
        }
    }
}