using System.Collections.Generic;

namespace NHamcrest.Core
{
    public class AnyOf<T> : Matcher<T>
    {
        private readonly IEnumerable<IMatcher<T>> matchers;

        public AnyOf(IEnumerable<IMatcher<T>> matchers)
        {
            this.matchers = matchers;
        }

        public override bool Matches(T item)
        {
            foreach (var matcher in matchers)
            {
                if (matcher.Matches(item))
                    return true;
            }
            return false;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendList("(", " " + "or" + " ", ")", FakeLinq.Cast<ISelfDescribing>(matchers));
        }
    }

    public static partial class Matches
    {
        [Factory]
        public static IMatcher<T> AnyOf<T>(IEnumerable<IMatcher<T>> matchers)
        {
            return new AnyOf<T>(matchers);
        }
    }
}
