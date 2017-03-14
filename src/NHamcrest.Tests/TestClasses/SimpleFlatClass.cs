namespace NHamcrest.Tests.TestClasses
{
    public class SimpleFlatClass : ISimpleFlatClass
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
    }

    public interface ISimpleFlatClass
    {
        int IntProperty { get; }
        string StringProperty { get; }
    }

    public class AnotherSimpleFlatClass : ISimpleFlatClass
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
    }
}