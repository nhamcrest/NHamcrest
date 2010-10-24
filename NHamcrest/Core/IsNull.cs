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

    public static partial class Is
    {
        [Factory]
        public static IMatcher<T> NullValue<T>()
        {
            return new IsNull<T>();
        }

        [Factory]
        public static IMatcher<object> NullValue()
        {
            return new IsNull<object>();
        }

        [Factory]
        public static IMatcher<T> NotNullValue<T>()
        {
            return Not(NullValue<T>());
        }

        [Factory]
        public static IMatcher<object> NotNullValue()
        {
            return Not(NullValue());
        }
    }
}