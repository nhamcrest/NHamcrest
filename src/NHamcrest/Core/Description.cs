using System;
using System.Collections.Generic;
using System.Globalization;

namespace NHamcrest.Core
{
    public abstract class Description : IDescription
    {
        /// <summary>
        /// A description that consumes input but does nothing.
        /// </summary>
        public static readonly IDescription None = new NullDescription();

        private int _currentIdent = 0;

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
                Append("'");
                Append(value.ToString());
                Append("'");
            }
            else if (value is string)
            {
                Append("\"");
                Append(value.ToString());
                Append("\"");
            }
            else if (value is long)
            {
                Append(((long)value).ToString(CultureInfo.InvariantCulture) + "L");
            }
            else if (value is float)
            {
                Append(((float)value).ToString(CultureInfo.InvariantCulture) + "f");
            }
            else if (value is double)
            {
                Append(((double)value).ToString(CultureInfo.InvariantCulture) + "d");
            }
            else if (value is decimal)
            {
                Append(((decimal)value).ToString(CultureInfo.InvariantCulture) + "m");
            }
            else if (value.GetType().IsArray)
            {
                AppendValueList("[", ", ", "]", IterateArray((Array)value));
            }
            else
            {
                Append(value.ToString());
            }
            return this;
        }

        public IDescription AppendNewLine()
        {
            Append(Environment.NewLine);
            ApplyIdent();
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

        private void ApplyIdent()
        {
            Append(new string(' ', _currentIdent));
        }

        public IDisposable IndentBy(int numberOfSpaces)
        {
            _currentIdent += numberOfSpaces;
            return new Nesting(() => _currentIdent -= numberOfSpaces);
        }

        /// <summary>
    	/// Append a string to the description.  
    	/// The default implementation passes every character to Append(char).
    	/// Override in subclasses to provide an efficient implementation.
    	/// </summary>
    	/// <param name="str">The string to append.</param>
    	protected abstract void Append(string str);

        private class SelfDescribingValue<T> : ISelfDescribing
        {
            private readonly T _value;

            public SelfDescribingValue(T value)
            {
                _value = value;
            }

            public void DescribeTo(IDescription description)
            {
                description.AppendValue(_value);
            }
        }

        private class Nesting : IDisposable
        {
            private readonly Action _disposeAction;

            public Nesting(Action disposeAction)
            {
                _disposeAction = disposeAction;
            }

            public void Dispose()
            {
                _disposeAction();
            }
        }
    }
}