namespace NHamcrest
{
    public abstract class FeatureMatcher<T, U> : NonNullDiagnosingMatcher<T> where T : class
    {
        private readonly IMatcher<U> subMatcher;
        private readonly string featureDescription;
        private readonly string featureName;

        protected FeatureMatcher(IMatcher<U> subMatcher, string featureDescription, string featureName)
        {
            this.subMatcher = subMatcher;
            this.featureDescription = featureDescription;
            this.featureName = featureName;
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
            
            if (subMatcher.Matches(featureValue) == false)
            {
                mismatchDescription.AppendText(featureName).AppendText(" ");
                subMatcher.DescribeMismatch(featureValue, mismatchDescription);
                return false;
            }
            
            return true;
        }

        public sealed override void DescribeTo(IDescription description)
        {
            description.AppendText(featureDescription)
                .AppendText(" ")
                .AppendDescriptionOf(subMatcher);
        }
    }
}