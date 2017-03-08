using System.Collections.Generic;

namespace NHamcrest.Core
{
    public class AllOf<T> : DiagnosingMatcher<T>
    {
        private readonly IEnumerable<IMatcher<T>> _matchers;

        public AllOf(IEnumerable<IMatcher<T>> matchers)
        {
            _matchers = matchers;
        }

        protected override bool Matches(T item, IDescription mismatchDescription)
        {
            foreach (var matcher in _matchers)
            {
                if (matcher.Matches(item))
                    continue;

                mismatchDescription.AppendDescriptionOf(matcher).AppendText(" ");
                matcher.DescribeMismatch(item, mismatchDescription);
                return false;
            }

            return true;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendList("(", " " + "and" + " ", ")", FakeLinq.Cast<ISelfDescribing>(_matchers));
        }        
    }

    public static partial class Matches
    {
        [Factory]
        public static IMatcher<T> AllOf<T>(IEnumerable<IMatcher<T>> matchers)
        {
            return new AllOf<T>(matchers);
        }

        [Factory]
        public static IMatcher<T> AllOf<T>(params IMatcher<T>[] matchers)
        {
            return new AllOf<T>(matchers);
        }
    }
}