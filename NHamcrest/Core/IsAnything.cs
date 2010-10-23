namespace NHamcrest.Core
{
    public class IsAnything<T> : Matcher<T>
    {
        private readonly string message;

        public IsAnything() : this("ANYTHING") { }

        public IsAnything(string message)
        {
            this.message = message;
        }

        public override bool Matches(T item)
        {
            return true;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText(message);
        }
    }

    public partial class Is
    {
        // This matcher always evaluates to true.
        [Factory]
        public static Matcher<object> Anything()
        {
            return new IsAnything<object>();
        }

        /// <summary>
        /// This matcher always evaluates to true.
        /// </summary>
        /// <param name="description">A meaningful string used when describing itself.</param>
        /// <returns>A matcher.</returns>
        [Factory]
        public static Matcher<object> Anything(string description)
        {
            return new IsAnything<object>(description);
        }        
    }
}