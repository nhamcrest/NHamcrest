namespace NHamcrest.Core
{
    public class IsSame<T> : Matcher<T>
    {
        private readonly T @object;

        public IsSame(T @object)
        {
            this.@object = @object;
        }

        public override bool Matches(T arg)
        {
            return ReferenceEquals(arg, @object);
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("SameAs(")
                    .AppendValue(@object)
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