using System.Collections.Generic;

namespace NHamcrest.Tests.TestClasses
{
    public class ClassWithEnumerableOfClasses
    {
        public int IntValue { get; set; }
        public IEnumerable<SimpleFlatClass> OtherThings { get; set; }
    }
}