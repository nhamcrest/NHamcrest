namespace NHamcrest.Core
{
    public class IsEqual<T> : Matcher<T>
    {
        private readonly T @object;

        public IsEqual(T equalArg)
        {
            @object = equalArg;
        }

        public override bool Matches(T arg)
        {
            return AreEqual(arg, @object);
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendValue(@object);
        }

        private static bool AreEqual(T left, T right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (ReferenceEquals(null, left) || ReferenceEquals(null, right))
                return false;

            return left.Equals(right);
        }

        [Factory]
        public static IMatcher<T> EqualTo(T operand)
        {
            return new IsEqual<T>(operand);
        }
    }

    public static partial class Is
    {
        public static IMatcher<T> EqualTo<T>(T value)
        {
            return IsEqual<T>.EqualTo(value);
        }

        public static IMatcher<bool> True()
        {
            return IsEqual<bool>.EqualTo(true);
        }

        public static IMatcher<bool> False()
        {
            return IsEqual<bool>.EqualTo(false);
        }
    }
}