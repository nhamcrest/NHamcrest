using MbUnit.Framework;

namespace NHamcrest.Tests
{
    public class FeatureMatcherTests
    {
        [Test]
        public void Match_if_submatcher_does()
        {
            const string name = "test";
            var subMatcher = new CustomMatcher<string>("Sub-matcher", s => s == name);
            var matcher = new TestFeatureMatcher(subMatcher, "", "");

            var matches = matcher.Matches(new TestObject { Name = name });

            Assert.IsTrue(matches, "Expected match.");
        }

        [Test]
        public void No_match_if_submatcher_fails()
        {
            var subMatcher = new CustomMatcher<string>("Sub-matcher", s => s == "test");
            var matcher = new TestFeatureMatcher(subMatcher, "", "");

            var matches = matcher.Matches(new TestObject { Name = "bob" });

            Assert.IsFalse(matches, "Expected no match.");
        }

        [Test]
        public void Describe_mismatch()
        {
            var subMatcher = new CustomMatcher<string>("Sub-matcher", s => s == "test");
            var matcher = new TestFeatureMatcher(subMatcher, "", "TestObject.Name");
            var description = new StringDescription();

            matcher.DescribeMismatch(new TestObject { Name = "bob" }, description);

            Assert.AreEqual("TestObject.Name was bob", description.ToString());
        }

        [Test]
        public void Describe_matcher()
        {
            var subMatcher = new CustomMatcher<string>("Sub-matcher description", s => s == "test");
            var matcher = new TestFeatureMatcher(subMatcher, "Feature description", "");
            var description = new StringDescription();

            matcher.DescribeTo(description);

            Assert.AreEqual("Feature description Sub-matcher description", description.ToString());
        }

        private class TestFeatureMatcher : FeatureMatcher<TestObject, string>
        {
            public TestFeatureMatcher(IMatcher<string> subMatcher, string featureDescription, string featureName)
                : base(subMatcher, featureDescription, featureName)
            { }

            protected override string FeatureValueOf(TestObject actual)
            {
                return actual.Name;
            }
        }

        private class TestObject
        {
            public string Name { get; set; }
        }
    }
}