using System.Collections.Generic;

namespace NHamcrest.Tests.TestClasses
{
    public class ClassWithListOfClasses
    {
        public int IntValue { get; set; }
        public List<SimpleFlatClass> OtherThings { get; set; }
    }
}