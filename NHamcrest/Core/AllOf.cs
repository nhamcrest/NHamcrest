using System.Collections.Generic;
using System.Linq;

namespace NHamcrest.Core
{
    public class AllOf<T> : DiagnosingMatcher<T>
    {
        private readonly IEnumerable<IMatcher<T>> matchers;

        public AllOf(IEnumerable<IMatcher<T>> matchers)
        {
            this.matchers = matchers;
        }

        protected override bool Matches(T item, IDescription mismatchDescription)
        {
            var failingMatchers = matchers.Where(matcher => matcher.Matches(item) == false);

            foreach (var matcher in failingMatchers)
            {
                mismatchDescription.AppendDescriptionOf(matcher).AppendText(" ");
                matcher.DescribeMismatch(item, mismatchDescription);
                return false;
            }

            return true;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendList("(", " " + "and" + " ", ")", matchers.Cast<ISelfDescribing>());
        }
    }

    public static partial class Matches
    {
        [Factory]
        public static IMatcher<T> AllOf<T>(IEnumerable<IMatcher<T>> matchers)
        {
            return new AllOf<T>(matchers);
        }
    }
}