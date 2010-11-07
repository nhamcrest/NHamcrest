namespace NHamcrest.Core
{
    /// <summary>
    /// Provides a custom description to another matcher.
    /// </summary>
    public class DescribedAs<T> : Matcher<T>
    {
        private readonly string descriptionTemplate;
        private readonly IMatcher<T> matcher;
        private readonly object[] values;

        public DescribedAs(string descriptionTemplate, IMatcher<T> matcher, object[] values)
        {
            this.descriptionTemplate = descriptionTemplate;
            this.matcher = matcher;
            this.values = values;
        }

        public override bool Matches(T item)
        {
            return matcher.Matches(item);
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText(string.Format(descriptionTemplate, values));
        }
    }

    public static class DescribedAsExtension
    {

    }
}