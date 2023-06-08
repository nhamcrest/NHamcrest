namespace NHamcrest.Extensions
{
    public static class IsExtensions
    {
        public static IMatcher<T> Is<T>(T value)
        {
            return NHamcrest.Is.EqualTo(value);
        }
    }
}