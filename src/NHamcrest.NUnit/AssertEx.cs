using System.IO;
using NHamcrest.Core;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace NHamcrest.NUnit
{
    /// <summary>
    /// Assert extension for NUnit.
    /// </summary>
    public class AssertEx
    {
        /// <summary>
        /// Checks if actual matches in IMatcher.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="matcher"></param>
        public static void That<T>(T actual, IMatcher<T> matcher)
        {
            That(actual, matcher, null, null);
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
        /// <exception cref="AssertionException"></exception>
        public static void That<T>(T actual, IMatcher<T> matcher, string message, params object[] args)
        {
            if (matcher.Matches(actual))
                return;

            var writer = new TextMessageWriter(message, args);

            WriteExpected(matcher, writer);

            WriteActual(actual, matcher, writer);

            throw new AssertionException(writer.ToString());
        }

        private static void WriteExpected(ISelfDescribing matcher, TextWriter writer)
        {
            writer.Write(TextMessageWriter.Pfx_Expected);
            var description = new StringDescription();
            matcher.DescribeTo(description);
            writer.Write(description.ToString());
            writer.WriteLine();
        }

        private static void WriteActual<T>(T actual, IMatcher<T> matcher, TextWriter writer)
        {
            writer.Write(TextMessageWriter.Pfx_Actual);
            var mismatchDescription = new StringDescription();
            matcher.DescribeMismatch(actual, mismatchDescription);
            writer.Write(mismatchDescription.ToString());
            writer.WriteLine();
        }
    }
}