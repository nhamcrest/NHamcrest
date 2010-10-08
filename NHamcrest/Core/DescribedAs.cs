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
        /// <summary>
        /// Wraps an existing matcher and overrides the description when it fails.
        /// </summary>
        [Factory]
        public static IMatcher<T> DescribedAs<T>(this IMatcher<T> matcher, string description, 
            params object[] values)
        {
            return new DescribedAs<T>(description, matcher, values);
        }
    }
}