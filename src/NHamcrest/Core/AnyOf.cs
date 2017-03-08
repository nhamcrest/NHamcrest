using System.Collections.Generic;

namespace NHamcrest.Core
{
    public class AnyOf<T> : Matcher<T>
    {
        private readonly IEnumerable<IMatcher<T>> _matchers;

        public AnyOf(IEnumerable<IMatcher<T>> matchers)
        {
            _matchers = matchers;
        }

        public override bool Matches(T item)
        {
            foreach (var matcher in _matchers)
            {
                if (matcher.Matches(item))
                    return true;
            }
            return false;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendList("(", " " + "or" + " ", ")", FakeLinq.Cast<ISelfDescribing>(_matchers));
        }
    }

    public static partial class Matches
    {
        [Factory]
        public static IMatcher<T> AnyOf<T>(IEnumerable<IMatcher<T>> matchers)
        {
            return new AnyOf<T>(matchers);
        }

        [Factory]
        public static IMatcher<T> AnyOf<T>(params IMatcher<T>[] matchers)
        {
            return new AnyOf<T>(matchers);
        }
    }
}
