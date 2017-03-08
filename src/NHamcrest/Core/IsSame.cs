namespace NHamcrest.Core
{
    public class IsSameMatcher<T> : Matcher<T>
    {
        private readonly T _object;

        public IsSameMatcher(T @object)
        {
            _object = @object;
        }

        public override bool Matches(T arg)
        {
            return ReferenceEquals(arg, _object);
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("SameAs(")
                    .AppendValue(_object)
                    .AppendText(")");
        }
    }
}