using System;

namespace NHamcrest.Core
{
	public class IsLessThanOrEqualTo<T> : Matcher<T> where T : IComparable<T>
	{
		private readonly T _object;

		public IsLessThanOrEqualTo(T arg)
		{
			_object = arg;
		}

		public override void DescribeTo(IDescription description)
		{
			description.AppendText("less than or equal to ").AppendValue(_object);
		}

		public override bool Matches(T arg)
		{
			return arg.CompareTo(_object) <= 0;
		}
	}
}