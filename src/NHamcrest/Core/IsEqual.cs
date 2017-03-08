namespace NHamcrest.Core
{
    public class IsEqual<T> : Matcher<T>
    {
        private readonly T _object;

        public IsEqual(T equalArg)
        {
            _object = equalArg;
        }

        public override bool Matches(T arg)
        {
            return AreEqual(arg, _object);
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendValue(_object);
        }

        private static bool AreEqual(T left, T right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (ReferenceEquals(null, left) || ReferenceEquals(null, right))
                return false;

            return left.Equals(right);
        }

        public static IMatcher<T> EqualTo(T operand)
        {
            return new IsEqual<T>(operand);
        }
    }
}