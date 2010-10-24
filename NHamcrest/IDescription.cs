using System.Collections.Generic;

namespace NHamcrest
{
    /// <summary>
    /// A description of a Matcher. A Matcher will describe itself to a description
    /// which can later be used for reporting.
    /// </summary>
    public interface IDescription
    {
        /// <summary>
        /// Appends some plain text to the description.
        /// </summary>
        /// <param name="text">The text to append.</param>
        /// <returns>The description.</returns>
        IDescription AppendText(string text);

        /// <summary>
        /// Appends some plain text to the description.
        /// </summary>
        /// <param name="format">A format string.</param>
        /// <param name="args">The values to use.</param>
        /// <returns>The description.</returns>
        IDescription AppendText(string format, params object[] args);

        /// <summary>
        /// Appends the description of an <see cref="ISelfDescribing"/> value to this description.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IDescription AppendDescriptionOf(ISelfDescribing value);

        /// <summary>
        /// Appends an arbitary value to the description.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IDescription AppendValue(object value);

        /// <summary>
        /// Appends a list of values to the description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="start"></param>
        /// <param name="separator"></param>
        /// <param name="end"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IDescription AppendValueList<T>(string start, string separator, string end, IEnumerable<T> values);

        /// <summary>
        /// Appends a list of <see cref="ISelfDescribing" /> objects to the description.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="separator"></param>
        /// <param name="end"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IDescription AppendList(string start, string separator, string end, IEnumerable<ISelfDescribing> values);
    }
}