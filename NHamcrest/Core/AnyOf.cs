using System.Collections.Generic;
using System.Linq;

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
            return matchers.Any(m => m.Matches(item));
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendList("(", " " + "or" + " ", ")", matchers.Cast<ISelfDescribing>());
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
