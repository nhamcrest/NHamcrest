namespace NHamcrest.Internal
{
    public class SelfDescribingValue<T> : ISelfDescribing
    {
        private readonly T value;

        public SelfDescribingValue(T value)
        {
            this.value = value;
        }

        public void DescribeTo(IDescription description)
        {
            description.AppendValue(value);
        }
    }
}