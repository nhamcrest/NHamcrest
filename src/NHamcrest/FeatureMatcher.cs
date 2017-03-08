namespace NHamcrest
{
    public abstract class FeatureMatcher<T, U> : NonNullDiagnosingMatcher<T> where T : class
    {
        private readonly IMatcher<U> _subMatcher;
        private readonly string _featureDescription;
        private readonly string _featureName;

        protected FeatureMatcher(IMatcher<U> subMatcher, string featureDescription, string featureName)
        {
            _subMatcher = subMatcher;
            _featureDescription = featureDescription;
            _featureName = featureName;
        }

        /// <summary>
        /// Implement this to extract the interesting feature.
        /// </summary>
        /// <param name="actual">The target object.</param>
        /// <returns>The feature to be matched.</returns>
        protected abstract U FeatureValueOf(T actual);

        protected override bool MatchesSafely(T collection, IDescription mismatchDescription)
        {
            var featureValue = FeatureValueOf(collection);
            
            if (_subMatcher.Matches(featureValue) == false)
            {
                mismatchDescription.AppendText(_featureName).AppendText(" ");
                _subMatcher.DescribeMismatch(featureValue, mismatchDescription);
                return false;
            }
            
            return true;
        }

        public sealed override void DescribeTo(IDescription description)
        {
            description.AppendText(_featureDescription)
                .AppendText(" ")
                .AppendDescriptionOf(_subMatcher);
        }
    }
}