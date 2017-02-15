using System;

namespace NHamcrest.Core
{
	public class IsLessThanOrEqualTo<T> : Matcher<T> where T : IComparable<T>
	{
		private readonly T @object;

		public IsLessThanOrEqualTo(T arg)
		{
			@object = arg;
		}

		public override void DescribeTo(IDescription description)
		{
			description.AppendText("less than or equal to ").AppendValue(@object);
		}

		public override bool Matches(T arg)
		{
			return arg.CompareTo(@object) <= 0;
		}
	}

	public static partial class Is
	{
		public static IMatcher<T> LessThanOrEqualTo<T>(T value) where T : IComparable<T>
		{
			return new IsLessThanOrEqualTo<T>(value);
		}
	}
}