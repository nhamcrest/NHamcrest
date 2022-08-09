using NHamcrest.Core;

namespace NHamcrest.XUnit
{
    /// <summary>
    /// Extends the xUnit.Assert class with a `.That` method.
    /// </summary>
    public class Assert : Xunit.Assert
    {
        /// <summary>
        /// Checks if actual matches in IMatcher.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="matcher"></param>
        /// <exception cref="MatchException"></exception>
        public static void That<T>(T actual, IMatcher<T> matcher)
        {
            if (matcher.Matches(actual))
                return;

            var description = new StringDescription();
            matcher.DescribeTo(description);

            var mismatchDescription = new StringDescription();
            matcher.DescribeMismatch(actual, mismatchDescription);

            throw new MatchException(description.ToString(), mismatchDescription.ToString(), null);
        }
    }
}
