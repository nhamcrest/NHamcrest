namespace NHamcrest.Core
{
    internal class EntryDescriptor<TKey, TValue>
    {
        public EntryDescriptor(IMatcher<TKey> keyMatcher, IMatcher<TValue> valueMatcher)
        {
            KeyMatcher = keyMatcher;
            ValueMatcher = valueMatcher;
        }

        public IMatcher<TKey> KeyMatcher { get; }
        public IMatcher<TValue> ValueMatcher { get; }
    }
}