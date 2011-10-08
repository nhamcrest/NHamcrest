using System;

namespace NHamcrest.XUnit
{
    public class AssertEx
    {
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
