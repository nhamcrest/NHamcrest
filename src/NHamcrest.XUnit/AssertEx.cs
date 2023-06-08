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
            That(actual, matcher, null);
        }

        /// <summary>
        /// Checks if actual matches in IMatcher.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="matcher"></param>
        /// <param name="message"></param>
        public static void That<T>(T actual, IMatcher<T> matcher, string message)
        {
            That(actual, matcher, message, null);
        }

        /// <summary>
        /// Checks if actual matches in IMatcher.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="matcher"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <exception cref="MatchException"></exception>
        public static void That<T>(T actual, IMatcher<T> matcher, string message, params object[] args)
        {
            if (matcher.Matches(actual))
                return;

            var description = new StringDescription();
            matcher.DescribeTo(description);

            var mismatchDescription = new StringDescription();
            matcher.DescribeMismatch(actual, mismatchDescription);

            string userMessage = args != null && args.Length > 0 
                ? string.Format(message, args)
                : message;

            throw new MatchException(description.ToString(), mismatchDescription.ToString(), userMessage);
        }
    }
}