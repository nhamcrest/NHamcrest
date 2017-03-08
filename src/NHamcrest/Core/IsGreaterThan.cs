using System;

namespace NHamcrest.Core
{
	public class IsGreaterThan<T> : Matcher<T> where T : IComparable<T>
	{
		private readonly T _object;

		public IsGreaterThan(T arg)
		{
			_object = arg;
		}

		public override void DescribeTo(IDescription description)
		{
			description.AppendText("greater than ").AppendValue(_object);
		}

		public override bool Matches(T arg)
		{
			return arg.CompareTo(_object) > 0;
		}
	}
}