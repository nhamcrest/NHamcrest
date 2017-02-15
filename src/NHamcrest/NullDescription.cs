using System.Collections.Generic;

namespace NHamcrest
{
    public class NullDescription : IDescription
    {
        public IDescription AppendText(string text)
        {
            return this;
        }

        public IDescription AppendText(string format, params object[] args)
        {
            return this;
        }

        public IDescription AppendDescriptionOf(ISelfDescribing value)
        {
            return this;
        }

        public IDescription AppendValue(object value)
        {
            return this;
        }

    	public IDescription AppendNewLine()
    	{
    		return this;
    	}

    	public IDescription AppendValueList<T>(string start, string separator, string end, params T[] values)
        {
            return this;
        }

        public IDescription AppendValueList<T>(string start, string separator, string end, IEnumerable<T> values)
        {
            return this;
        }

        public IDescription AppendList(string start, string separator, string end, IEnumerable<ISelfDescribing> values)
        {
            return this;
        }

        public override string ToString()
        {
            return "";
        }
    }
}