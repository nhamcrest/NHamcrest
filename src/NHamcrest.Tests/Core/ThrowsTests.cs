using System;
using MbUnit.Framework;
using NHamcrest.Core;
using NHAssert = NHamcrest.Tests.Internal.Assert;

namespace NHamcrest.Tests.Core
{
	public class ThrowsTests
	{
		[Test]
		public void Describe_matcher()
		{
			var matcher = new Throws<ArgumentNullException>();
			var description = new StringDescription();

			matcher.DescribeTo(description);

			NHAssert.That(description.ToString(), Is.EqualTo("the block to throw an exception of type System.ArgumentNullException"));
		}

		[Test]
		public void Match_if_action_throws_expected_exception()
		{
			NHAssert.That(() => DoIt(), Throws.An<ArgumentNullException>());
		}

		[Test]
		public void Match_if_action_throws_superclass_of_expected_exception()
		{
			NHAssert.That(() => DoIt(), Throws.An<ArgumentException>());
		}

		[Test]
		public void No_match_if_action_does_not_throw()
		{
			var matcher = new Throws<ArgumentException>();

			var match = matcher.Matches(() => { });

			NHAssert.That(match, Is.False());
		}

		[Test]
		public void Describe_mismatch_if_action_does_not_throw()
		{
			var matcher = new Throws<ArgumentException>();
			var description = new StringDescription();

			matcher.DescribeMismatch(() => { }, description);

			NHAssert.That(description.ToString(), Is.EqualTo("no exception was thrown"));
		}

		[Test]
		public void No_match_if_action_throws_different_exception()
		{
			var matcher = new Throws<NullReferenceException>();

			var match = matcher.Matches(DoIt);

			NHAssert.That(match, Is.False());
		}

		[Test]
		public void Describe_mismatch_if_action_throws_different_exception()
		{
			var matcher = new Throws<NullReferenceException>();
			var description = new StringDescription();

			matcher.DescribeMismatch(DoIt, description);

			NHAssert.That(description.ToString(), Starts.With("an exception of type System.ArgumentNullException was thrown"));
		}

		[Test]
		public void Match_if_thrown_exception_matches_predicate()
		{
			NHAssert.That(DoIt, Throws.An<ArgumentNullException>().With(e => e.Message == "message" 
				&& e.InnerException.GetType() == typeof(Exception)));
		}

		[Test]
		public void No_match_if_thrown_exception_does_not_match_predicate()
		{
			var matcher = new Throws<ArgumentNullException>().With(e => e.Message == "something else");

			var matches = matcher.Matches(DoIt);

			NHAssert.That(matches, Is.False());
		}

		[Test]
		public void Describe_mismatch_if_thrown_exception_does_not_match_predicate()
		{
			var matcher = new Throws<ArgumentNullException>().With(e => e.Message == "something else");
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