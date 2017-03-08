using NHamcrest.Core;

namespace NHamcrest.Tests.Internal
{
    public static class Assert
    {
        public static void That<T>(T actual, IMatcher<T> matcher)
        {
            That(actual, matcher, "");
        }

        private static void That<T>(T actual, IMatcher<T> matcher, string reason)
        {
            if (matcher.Matches(actual))
                return;
            
            var description = new StringDescription();
            description.AppendText(reason)
                .AppendText("\nExpected: ")
                .AppendDescriptionOf(matcher)
                .AppendText("\n     but: ");
            matcher.DescribeMismatch(actual, description);

            throw new AssertionError(description.ToString());
        }
    }
}