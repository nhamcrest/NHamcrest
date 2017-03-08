namespace NHamcrest.Internal
{
    public class SelfDescribingValue<T> : ISelfDescribing
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
}