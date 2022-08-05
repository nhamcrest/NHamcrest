using System;
using NHamcrest.Tests.TestClasses;
using Xunit;

namespace NHamcrest.Tests
{
    public class FeatureMatcherDescriptionTests
    {
        [Theory]
        [InlineData(1, "foo", "one description", "foo description")]
        [InlineData(3, "bar", "three description", "and bar description")]
        public void FeatureMatcherGeneratesCorrectDescriptionForAllProperties(int intVal, string stringVal, string intDescription, string stringDescription)
        {
            var sut = Describe.Object<SimpleFlatClass>()
                .Property(x => x.IntProperty, new FakeMatcher<int>(true, intDescription, i => ""))
                .Property(x => x.StringProperty, new FakeMatcher<string>(true, stringDescription, i => ""));

            var expectedDescription = "a(n) SimpleFlatClass where:\r\n" +
                                      "    member IntProperty value is " + intDescription + "\r\n" +
                                      "    member StringProperty value is " + stringDescription;

            sut.ShouldHaveDescription(expectedDescription);
        }

        [Theory]
        [InlineData(1, "foo", 2, "qux", "failed to match one", "boom!")]
        [InlineData(3, "bar", 17, "baz", "the world exploded", "stack overflow")]
        public void FeatureMatcherGeneratesCorrectMismatchDescriptionForAllFailedProperties(
            int intVal,
            string stringVal,
            int mismatchedIntVal,
            string mismatchedStringVal,
            string mismatchedIntDescription,
            string mismatchedStringDescription)
        {
            var sut = Describe.Object<SimpleFlatClass>()
                .Property(x => x.IntProperty, new FakeMatcher<int>(false, "", i => i == mismatchedIntVal ? mismatchedIntDescription : i.ToString()))
                .Property(x => x.StringProperty, new FakeMatcher<string>(false, "", i => i == mismatchedStringVal ? mismatchedStringDescription : i));

            var expectedDescription = "was a(n) SimpleFlatClass where:\r\n" +
                                      "    member IntProperty value " + mismatchedIntDescription + "\r\n" +
                                      "    member StringProperty value " + mismatchedStringDescription;

            var mismatched = new SimpleFlatClass
            {
                IntProperty = mismatchedIntVal,
                StringProperty = mismatchedStringVal
            };

            sut.ShouldHaveMismatchDescriptionForValue(mismatched, expectedDescription);
        }

        [Theory]
        [InlineData(1, "one description")]
        [InlineData(3, "three description")]
        public void FeatureMatcherGeneratesCorrectDescriptionForAProperty(int intVal, string intDescription)
        {
            var sut = Describe.Object<SimpleFlatClass>()
                .Property(x => x.IntProperty, new FakeMatcher<int>(true, intDescription, i => ""));

            var expectedDescription = "a(n) SimpleFlatClass where:\r\n" +
                                      "    member IntProperty value is " + intDescription;

            sut.ShouldHaveDescription(expectedDescription);
        }

        [Theory]
        [InlineData(1, "foo", 2, "qux", "boom!")]
        [InlineData(3, "bar", 17, "baz", "stack overflow")]
        public void FeatureMatcherGeneratesCorrectMismatchDescriptionForOnlyFailedProperties(
            int intVal,
            string stringVal,
            int mismatchedIntVal,
            string mismatchedStringVal,
            string mismatchedStringDescription)
        {
            var sut = Describe.Object<SimpleFlatClass>()
                .Property(x => x.IntProperty, new FakeMatcher<int>(true, "", i => ""))
                .Property(x => x.StringProperty, new FakeMatcher<string>(false, "", i => i == mismatchedStringVal ? mismatchedStringDescription : i));

            var expectedDescription = "was a(n) SimpleFlatClass where:\r\n" +
                                      "    member StringProperty value " + mismatchedStringDescription;

            var mismatched = new SimpleFlatClass
            {
                IntProperty = mismatchedIntVal,
                StringProperty = mismatchedStringVal
            };

            sut.ShouldHaveMismatchDescriptionForValue(mismatched, expectedDescription);
        }

        [Fact]
        public void FeatureMatcherGeneratesCorrectTypeNameInDescription()
        {
            var sut = Describe.Object<AnotherFlatClass>()
                .Property(x => x.Id, new FakeMatcher<Guid>(true, "", i => ""));

            sut.ShouldHaveDescription(Starts.With("a(n) AnotherFlatClass where:"));
        }

        [Fact]
        public void FeatureMatcherGeneratesCorrectDescriptionForNoProperties()
        {
            var sut = Describe.Object<SimpleFlatClass>();
            sut.ShouldHaveDescription("a(n) SimpleFlatClass");
        }

        [Fact]
        public void FeatureMatcherGeneratesCorrectTypeNameInMismatchDescription()
        {
            var sut = Describe.Object<AnotherFlatClass>()
                .Property(x => x.Id, new FakeMatcher<Guid>(false, "", i => ""));

            sut.ShouldHaveMismatchDescriptionForValue(new AnotherFlatClass(), Starts.With("was a(n) AnotherFlatClass where:"));
        }

        [Fact]
        public void FeatureMatcherGeneratesCorrectMismatchDescriptionForNullValues()
        {
            var sut = Describe.Object<AnotherFlatClass>()
                .Property(x => x.Id, new FakeMatcher<Guid>(true, "", i => ""));

            sut.ShouldHaveMismatchDescriptionForValue(null, "was null");
        }

        [Theory]
        [InlineData(1, "foo")]
        [InlineData(3, "bar")]
        public void FeatureMatcherGeneratesNestedPropertyDescriptionsCorrectly(
            int intVal,
            string intDescription)
        {
            var sut = Describe.Object<NestedClass>()
                .Property(x => x.InnerClass.IntProperty, new FakeMatcher<int>(true, intDescription, i => ""));

            var expectedDescription = "a(n) NestedClass where:\r\n" +
                                      "    member InnerClass.IntProperty value is " + intDescription;

            sut.ShouldHaveDescription(expectedDescription);
        }

        [Theory]
        [InlineData(1, 17, "foo")]
        [InlineData(3, 24, "bar")]
        public void FeatureMatcherGeneratesNestedPropertyMismatchDescriptionsCorrectly(
            int intVal,
            int mismatchedIntVal,
            string mismatchIntDescription)
        {
            var sut = Describe.Object<NestedClass>()
                .Property(x => x.InnerClass.IntProperty, new FakeMatcher<int>(false, "", i => i == mismatchedIntVal ? mismatchIntDescription : i.ToString()));

            var expectedDescription = "was a(n) NestedClass where:\r\n" +
                                      "    member InnerClass.IntProperty value " + mismatchIntDescription;

            var mismatched = new NestedClass
            {
                InnerClass = new SimpleFlatClass
                {
                    IntProperty = mismatchedIntVal,
                }
            };

            sut.ShouldHaveMismatchDescriptionForValue(mismatched, expectedDescription);
        }

        [Theory]
        [InlineData(1, "foo")]
        [InlineData(3, "bar")]
        public void FeatureMatcherGeneratesNestedMatcherDesciptionsCorrectly(
            int intVal,
            string intDescription)
        {
            var sut = Describe.Object<DoubleNestedClass>()
                .Property(x => x.Nested, Describe.Object<NestedClass>()
                    .Property(x => x.InnerClass, Describe.Object<SimpleFlatClass>()
                        .Property(x => x.IntProperty, new FakeMatcher<int>(true, intDescription, i => ""))));

            var expectedDescription = "a(n) DoubleNestedClass where:\r\n" +
                                      "    member Nested value is a(n) NestedClass where:\r\n" +
                                      "        member InnerClass value is a(n) SimpleFlatClass where:\r\n" +
                                      "            member IntProperty value is " + intDescription;

            sut.ShouldHaveDescription(expectedDescription);
        }

        [Theory]
        [InlineData(1, 17, "foo")]
        [InlineData(3, 24, "bar")]
        public void FeatureMatcherGeneratesNestedMatcherMismatchDesciptionsCorrectly(
            int intVal,
            int mismatchedIntVal,
            string mismatchIntDescription)
        {
            var sut = Describe.Object<DoubleNestedClass>()
                .Property(x => x.Nested, Describe.Object<NestedClass>()
                    .Property(x => x.InnerClass, Describe.Object<SimpleFlatClass>()
                        .Property(x => x.IntProperty, new FakeMatcher<int>(false, "", i => i == mismatchedIntVal ? mismatchIntDescription : i.ToString()))));

            var expectedDescription = "was a(n) DoubleNestedClass where:\r\n" +
                                      "    member Nested value was a(n) NestedClass where:\r\n" +
                                      "        member InnerClass value was a(n) SimpleFlatClass where:\r\n" +
                                      "            member IntProperty value " + mismatchIntDescription;

            var mismatched = new DoubleNestedClass()
            {
                Nested = new NestedClass
                {
                    InnerClass = new SimpleFlatClass
                    {
                        IntProperty = mismatchedIntVal,
                    }
                }
            };

            sut.ShouldHaveMismatchDescriptionForValue(mismatched, expectedDescription);
        }

        [Theory]
        [InlineData(1, "foo")]
        [InlineData(3, "bar")]
        public void FeatureMatcherGeneratesCastMatcherDesciptionsCorrectly(
            int intVal,
            string intDescription)
        {
            var sut = Describe.Object<SimpleFlatClass>()
                .Cast<DerivedFlatClass>(c => c.Property(x => x.AnotherIntProperty, new FakeMatcher<int>(true, intDescription, i => "")));

            var expectedDescription = "a(n) SimpleFlatClass where:\r\n" +
                                      "    subtype DerivedFlatClass where:\r\n" +
                                      "        member AnotherIntProperty value is " + intDescription;

            sut.ShouldHaveDescription(expectedDescription);
        }

        [Theory]
        [InlineData(1, 17, "foo")]
        [InlineData(3, 24, "bar")]
        public void FeatureMatcherGeneratesCastMatcherMismatchDesciptionsCorrectly(
            int intVal,
            int mismatchedIntVal,
            string mismatchIntDescription)
        {
            var sut = Describe.Object<SimpleFlatClass>()
                .Cast<DerivedFlatClass>(c => c.Property(x => x.AnotherIntProperty, new FakeMatcher<int>(false, "", i => i == mismatchedIntVal ? mismatchIntDescription : i.ToString())));

            var expectedDescription = "was a(n) SimpleFlatClass {DerivedFlatClass} where:\r\n" +
                                      "    subtype DerivedFlatClass where:\r\n" +
                                      "        member AnotherIntProperty value " + mismatchIntDescription;

            var mismatched = new DerivedFlatClass
            {
                AnotherIntProperty = mismatchedIntVal
            };

            sut.ShouldHaveMismatchDescriptionForValue(mismatched, expectedDescription);
        }

        [Fact]
        public void FeatureMatcherGeneratesCastMatcherMismatchDesciptionsCorrectlyWhenCastFails()
        {
            var sut = Describe.Object<SimpleFlatClass>()
                .Cast<DerivedFlatClass>(c => c.Property(x => x.AnotherIntProperty, new FakeMatcher<int>(true, "", i => "")));

            var expectedDescription = "was a(n) SimpleFlatClass where:\r\n" +
                                      "    it was not of type DerivedFlatClass";

            var mismatched = new SimpleFlatClass();

            sut.ShouldHaveMismatchDescriptionForValue(mismatched, expectedDescription);
        }
    }
}
