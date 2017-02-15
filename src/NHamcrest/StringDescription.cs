using System;
using System.Text;

namespace NHamcrest
{
    public class StringDescription : Description
    {
        private readonly StringBuilder @out;

        public StringDescription() : this(new StringBuilder()) { }

        public StringDescription(StringBuilder @out)
        {
            this.@out = @out;
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
            @out.Append(str);
        }

        public override string ToString()
        {
            return @out.ToString();
        }
    }
}