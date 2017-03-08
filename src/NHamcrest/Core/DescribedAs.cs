namespace NHamcrest.Core
{
    /// <summary>
    /// Provides a custom description to another matcher.
    /// </summary>
    public class DescribedAs<T> : Matcher<T>
    {
        private readonly string _descriptionTemplate;
        private readonly IMatcher<T> _matcher;
        private readonly object[] _values;

        public DescribedAs(string descriptionTemplate, IMatcher<T> matcher, object[] values)
        {
            _descriptionTemplate = descriptionTemplate;
            _matcher = matcher;
            _values = values;
        }

        public override bool Matches(T item)
        {
            return _matcher.Matches(item);
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText(string.Format(_descriptionTemplate, _values));
        }
    }
}