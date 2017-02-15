using System;

namespace NHamcrest.Core
{
	public class IsGreaterThanOrEqualTo<T> : Matcher<T> where T : IComparable<T>
	{
		private readonly T @object;

		public IsGreaterThanOrEqualTo(T arg)
		{
			@object = arg;
		}

		public override void DescribeTo(IDescription description)
		{
			description.AppendText("greater than or equal to ").AppendValue(@object);
		}

		public override bool Matches(T arg)
		{
			return arg.CompareTo(@object) >= 0;
		}
	}

	public static partial class Is
	{
		public static IMatcher<T> GreaterThanOrEqualTo<T>(T value) where T : IComparable<T>
		{
			return new IsGreaterThanOrEqualTo<T>(value);
		}
	}
}