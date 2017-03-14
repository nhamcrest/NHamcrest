using System.Collections.Generic;

namespace NHamcrest.Tests.TestClasses
{
    public class ClassWithDictionaryOfClasses
    {
        public string StringValue { get; set; }
        public Dictionary<string, SimpleFlatClass> Map { get; set; }
    }
}