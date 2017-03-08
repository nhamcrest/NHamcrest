using System;

namespace NHamcrest.Core
{
    public static class Throws
	{
		public static ThrowsMatcher<T> An<T>() where T : Exception
		{
			return new ThrowsMatcher<T>();
		}
	}
}