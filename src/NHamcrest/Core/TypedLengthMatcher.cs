using System.Collections.Generic;
using System.Linq;

namespace NHamcrest.Core
{
    public class TypedLengthMatcher<T> : IMatcher<IEnumerable<T>>
    {
        private readonly int _length;

        public TypedLengthMatcher(int length)
        {
            _length = length;
        }

        public void DescribeTo(IDescription description)
        {
            description.AppendText("a collection of length {0}", _length);
        }

        public bool Matches(IEnumerable<T> item)
        {
            return Matches(item, new NullDescription());
        }

        public void DescribeMismatch(IEnumerable<T> item, IDescription mismatchDescription)
        {
            Matches(item, mismatchDescription);
        }

        private bool Matches(IEnumerable<T> collection, IDescription mismatchDescription)
        {
            var actualLength = collection.Count();
            if (actualLength == _length) return true;

            mismatchDescription.AppendText("collection had length {0}", actualLength);
            return false;
        }
    }
}