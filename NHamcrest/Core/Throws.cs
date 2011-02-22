using System;

namespace NHamcrest.Core
{
	public class Throws<T> : DiagnosingMatcher<Action> where T : Exception
	{
		private Func<Exception, bool> predicate = e => true;

		public override void DescribeTo(IDescription description)
		{
			description.AppendText("the block to throw an exception of type {0}", typeof(T));
		}

		protected override bool Matches(Action action, IDescription mismatchDescription)
		{
			try
			{
				action();
				mismatchDescription.AppendText("no exception was thrown");
			}
			catch (T ex)
			{
				if (predicate(ex))
					return true;

				mismatchDescription.AppendText("the exception was of the correct type, but did not match the predicate")
					.AppendNewLine()
					.AppendValue(ex);
			}
			catch (Exception ex)
			{
				mismatchDescription.AppendText("an exception of type {0} was thrown", ex.GetType())
					.AppendNewLine()
					.AppendValue(ex);
			}
			return false;
		}

		public Throws<T> With(Func<Exception, bool> predicate)
		{
			this.predicate = predicate;
			return this;
		}
	}

	public class Throws
	{
		public static Throws<T> An<T>() where T : Exception
		{
			return new Throws<T>();
		}
	}
}