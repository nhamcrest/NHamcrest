using System;

namespace NHamcrest.Core
{
	public class IsGreaterThan<T> : Matcher<T> where T : IComparable<T>
	{
		private readonly T @object;

		public IsGreaterThan(T arg)
		{
			@object = arg;
		}

		public override void DescribeTo(IDescription description)
		{
			description.AppendText("greater than ").AppendValue(@object);
		}

		public override bool Matches(T arg)
		{
			return arg.CompareTo(@object) > 0;
		}
	}

	public static partial class Is
	{
		public static IMatcher<T> GreaterThan<T>(T value) where T : IComparable<T>
		{
			return new IsGreaterThan<T>(value);
		}
	}
}