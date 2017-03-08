using System;

namespace NHamcrest.Core
{
	public class IsLessThan<T> : Matcher<T> where T : IComparable<T>
	{
		private readonly T _object;

		public IsLessThan(T arg)
		{
			_object = arg;
		}

		public override void DescribeTo(IDescription description)
		{
			description.AppendText("less than ").AppendValue(_object);
		}

		public override bool Matches(T arg)
		{
			return arg.CompareTo(_object) < 0;
		}
	}
}