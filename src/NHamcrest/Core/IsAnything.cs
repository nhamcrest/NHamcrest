namespace NHamcrest.Core
{
    public class IsAnything<T> : Matcher<T>
    {
        private readonly string _message;

        public IsAnything() : this("ANYTHING") { }

        public IsAnything(string message)
        {
            _message = message;
        }

        public override bool Matches(T item)
        {
            return true;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText(_message);
        }
    }
}