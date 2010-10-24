using System;
using System.Collections.Generic;
using NHamcrest.Internal;

namespace NHamcrest
{
    public abstract class Description : IDescription
    {
        /// <summary>
        /// A description that consumes input but does nothing.
        /// </summary>
        public static readonly IDescription None = new NullDescription();

        public IDescription AppendText(string text)
        {
            Append(text);
            return this;
        }

        public IDescription AppendText(string format, params object[] args)
        {
            Append(string.Format(format, args));
            return this;
        }

        public IDescription AppendDescriptionOf(ISelfDescribing value)
        {
            value.DescribeTo(this);
            return this;
        }

        public IDescription AppendValue(object value)
        {
            if (value == null)
            {
                Append("null");
            }
            else if (value is char)
            {
                Append('"' + value.ToString() + '"');
            }
            else if (value is long)
            {
                Append(value + "L");
            }
            else if (value is float)
            {
                Append(value + "f");
            }
            else if (value is decimal)
            {
                Append(value + "m");
            }
            else if (value.GetType().IsArray)
            {
                AppendValueList("[", ", ", "]", IterateArray((Array) value));
            }
            else
            {
                Append(value.ToString());
            }
            return this;
        }

        private static IEnumerable<object> IterateArray(Array array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                yield return array.GetValue(i);
            }
        }

        public IDescription AppendValueList<T>(string start, string separator, string end, IEnumerable<T> values)
        {
            return AppendList(start, separator, end, ToSelfDescribingValues(values));
        }

        private static IEnumerable<ISelfDescribing> ToSelfDescribingValues<T>(IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                yield return new SelfDescribingValue<T>(value);
            }
        }

        public IDescription AppendList(string start, string separator, string end, IEnumerable<ISelfDescribing> values)
        {
            var separate = false;

            Append(start);
            foreach (var value in values)
            {
                if (separate) Append(separator);
                AppendDescriptionOf(value);
                separate = true;
            }
            Append(end);

            return this;
        }

        /// <summary>
        /// Append a string to the description.  
        /// The default implementation passes every character to Append(char).
        /// Override in subclasses to provide an efficient implementation.
        /// </summary>
        /// <param name="str">The string to append.</param>
        protected virtual void Append(string str)
        {
            foreach (var t in str)
            {
                Append(t);
            }
        }

        /// <summary>
        /// Append a char to the description.
        /// </summary>
        /// <param name="c">The char to append.</param>
        protected abstract void Append(char c);
    }
}