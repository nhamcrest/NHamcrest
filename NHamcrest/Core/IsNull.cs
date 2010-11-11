namespace NHamcrest.Core
{
    public class IsNull<T> : Matcher<T>
    {
        public override bool Matches(T item)
        {
            return ReferenceEquals(item, null);
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("null");
        }
    }

    public class IsNull : IsNull<object> { }

    public static partial class Is
    {
        [Factory]
        public static IMatcher<T> Null<T>()
        {
            return new IsNull<T>();
        }

        [Factory]
        public static IMatcher<object> Null()
        {
            return new IsNull();
        }

        [Factory]
        public static IMatcher<T> NotNull<T>()
        {
            return Not(Null<T>());
        }

        [Factory]
        public static IMatcher<object> NotNull()
        {
            return Not(Null());
        }
    }
}