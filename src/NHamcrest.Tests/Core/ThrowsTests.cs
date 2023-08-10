using NHamcrest.Core;
using System;
using Xunit;
using NHAssert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
    public class ThrowsTests
    {
        [Fact]
        public void Describe_matcher()
        {
            var matcher = new ThrowsMatcher<ArgumentNullException>();
            var description = new StringDescription();

            matcher.DescribeTo(description);

            NHAssert.That(description.ToString(), Is.EqualTo("the block to throw an exception of type System.ArgumentNullException"));
        }

        [Fact]
        public void Match_if_action_throws_expected_exception()
        {
            NHAssert.That(DoIt, Throws.An<ArgumentNullException>());
        }

        [Fact]
        public void Match_if_action_throws_superclass_of_expected_exception()
        {
            NHAssert.That(DoIt, Throws.An<ArgumentException>());
        }

        [Fact]
        public void No_match_if_action_does_not_throw()
        {
            var matcher = new ThrowsMatcher<ArgumentException>();

            var match = matcher.Matches(() => { });

            NHAssert.That(match, Is.False());
        }

        [Fact]
        public void Describe_mismatch_if_action_does_not_throw()
        {
            var matcher = new ThrowsMatcher<ArgumentException>();
            var description = new StringDescription();

            matcher.DescribeMismatch(() => { }, description);

            NHAssert.That(description.ToString(), Is.EqualTo("no exception was thrown"));
        }

        [Fact]
        public void No_match_if_action_throws_different_exception()
        {
            var matcher = new ThrowsMatcher<NullReferenceException>();

            var match = matcher.Matches(DoIt);

            NHAssert.That(match, Is.False());
        }

        [Fact]
        public void Describe_mismatch_if_action_throws_different_exception()
        {
            var matcher = new ThrowsMatcher<NullReferenceException>();
            var description = new StringDescription();

            matcher.DescribeMismatch(DoIt, description);

            NHAssert.That(description.ToString(), Starts.With("an exception of type System.ArgumentNullException was thrown"));
        }

        [Fact]
        public void Match_if_thrown_exception_matches_predicate()
        {
            NHAssert.That(DoIt, Throws.An<ArgumentNullException>()
                .With(e => e.Message == "message" && e.InnerException.GetType() == typeof(Exception)));
        }

        [Fact]
        public void No_match_if_thrown_exception_does_not_match_predicate()
        {
            var matcher = new ThrowsMatcher<ArgumentNullException>()
                .With(e => e.Message == "something else");

            var matches = matcher.Matches(DoIt);

            NHAssert.That(matches, Is.False());
        }

        [Fact]
        public void Describe_mismatch_if_thrown_exception_does_not_match_predicate()
        {
            var matcher = new ThrowsMatcher<ArgumentNullException>().With(e => e.Message == "something else");
            var description = new StringDescription();

            matcher.DescribeMismatch(DoIt, description);

            NHAssert.That(description.ToString(), Starts.With("the exception was of the correct type, but did not match the predicate"));
        }

        private void DoIt()
        {
            throw new ArgumentNullException("message", new Exception());
        }
    }
}