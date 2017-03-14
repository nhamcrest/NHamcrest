namespace NHamcrest.Core
{
    internal class CastMatcher<TSource, TDest> : IMatcher<TSource>
    {
        private readonly IMatcher<TDest> _innerMatcher;

        public CastMatcher(IMatcher<TDest> innerMatcher)
        {
            _innerMatcher = innerMatcher;
        }

        public void DescribeTo(IDescription description)
        {
            _innerMatcher.DescribeTo(description);
        }

        private bool Matches(TSource o, IDescription mismatchDescription)
        {
            if (!(o is TDest))
            {
                mismatchDescription.AppendText("it was not of type {0}", typeof (TDest).Name);
                return false;
            }

            var cast = (TDest)(object)o;

            if (!_innerMatcher.Matches(cast))
            {
                _innerMatcher.DescribeMismatch(cast, mismatchDescription);
                return false;
            }

            return true;
        }

        public bool Matches(TSource item)
        {
            return Matches(item, new NullDescription());
        }

        public void DescribeMismatch(TSource item, IDescription mismatchDescription)
        {
            Matches(item, mismatchDescription);
        }
    }
}