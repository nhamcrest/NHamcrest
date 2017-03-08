using System;
using System.Text;

namespace NHamcrest
{
    public class StringDescription : Description
    {
        private readonly StringBuilder _out;

        public StringDescription() : this(new StringBuilder()) { }

        public StringDescription(StringBuilder @out)
        {
            _out = @out;
        }

        /// <summary>
        /// Return the description of an <see cref="ISelfDescribing"/> object as a string.
        /// </summary>
        /// <param name="selfDescribing">The object to be described.</param>
        /// <returns>The description of the object.</returns>
        public static string ToString(ISelfDescribing selfDescribing)
        {
            return new StringDescription().AppendDescriptionOf(selfDescribing).ToString();
        }

        /// <summary>
        /// Alias for ToString(ISelfDescribing).
        /// </summary>
        public static String AsString(ISelfDescribing selfDescribing)
        {
            return ToString(selfDescribing);
        }

        protected override void Append(string str)
        {
            _out.Append(str);
        }

        public override string ToString()
        {
            return _out.ToString();
        }
    }
}