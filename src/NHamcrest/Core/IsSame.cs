namespace NHamcrest.Core
{
    public class IsSame<T> : Matcher<T>
    {
        private readonly T _object;

        public IsSame(T @object)
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

    public static partial class Is
    {
        [Factory]
        public static IMatcher<T> SameAs<T>(T @object)
        {
            return new IsSame<T>(@object);
        }
    }
}