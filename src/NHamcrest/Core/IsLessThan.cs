using System;

namespace NHamcrest.Core
{
	public class IsLessThan<T> : Matcher<T> where T : IComparable<T>
	{
		private readonly T @object;

		public IsLessThan(T arg)
		{
			@object = arg;
		}

		public override void DescribeTo(IDescription description)
		{
			description.AppendText("less than ").AppendValue(@object);
		}

		public override bool Matches(T arg)
		{
			return arg.CompareTo(@object) < 0;
		}
	}

	public static partial class Is
	{
		public static IMatcher<T> LessThan<T>(T value) where T : IComparable<T>
		{
			return new IsLessThan<T>(value);
		}
	}
}