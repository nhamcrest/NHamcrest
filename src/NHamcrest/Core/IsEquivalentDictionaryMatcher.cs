using System;
using System.Collections.Generic;
using System.Linq;

namespace NHamcrest.Core
{
    internal class IsEquivalentDictionaryMatcher<TKey, TValue> : NonNullDiagnosingMatcher<IDictionary<TKey, TValue>>
    {
        private readonly IEnumerable<EntryDescriptor<TKey, TValue>> _entryDescriptors;

        public IsEquivalentDictionaryMatcher(IEnumerable<EntryDescriptor<TKey, TValue>> entryDescriptors)
        {
            _entryDescriptors = entryDescriptors;
        }

        protected override bool MatchesSafely(IDictionary<TKey, TValue> dictionary, IDescription mismatchDescription)
        {
            var descriptorList = _entryDescriptors.ToList();
            var entriesList = dictionary.AsEnumerable().Cast<KeyValuePair<TKey, TValue>?>().ToList();

            var differentEntriesList = new List<Tuple<KeyValuePair<TKey, TValue>, EntryDescriptor<TKey, TValue>>>();
            var missingDescriptorsList = new List<EntryDescriptor<TKey, TValue>>();

            for (var i = 0; i < descriptorList.Count; i++)
            {
                var descriptor = descriptorList[i];
                var matchingKeyEntry = entriesList.FirstOrDefault(x => descriptor.KeyMatcher.Matches(x.Value.Key));

                if (matchingKeyEntry == null)
                {
                    missingDescriptorsList.Add(descriptor);
                    continue;
                }

                entriesList.Remove(matchingKeyEntry);

                if (!descriptor.ValueMatcher.Matches(matchingKeyEntry.Value.Value))
                {
                    differentEntriesList.Add(Tuple.Create(matchingKeyEntry.Value, descriptor));
                }
            }

            if (entriesList.Count == 0 && differentEntriesList.Count == 0 && missingDescriptorsList.Count == 0) return true;

            mismatchDescription.AppendText("was a dictionary that:");

            using (mismatchDescription.IndentBy(4))
            {
                var first = true;

                foreach (var differentEntry in differentEntriesList)
                {
                    if (!first) mismatchDescription.AppendText(",");
                    mismatchDescription.AppendNewLine();
                    mismatchDescription.AppendText("had a different entry where:");

                    using (mismatchDescription.IndentBy(4))
                    {
                        mismatchDescription.AppendNewLine()
                            .AppendText("key: ")
                            .AppendDescriptionOf(differentEntry.Item2.KeyMatcher)
                            .AppendNewLine()
                            .AppendText("value: ");

                        differentEntry.Item2.ValueMatcher.DescribeMismatch(differentEntry.Item1.Value, mismatchDescription);
                    }

                    first = false;
                }

                foreach (var entryDescriptor in missingDescriptorsList)
                {
                    if (!first) mismatchDescription.AppendText(",");

                    mismatchDescription.AppendNewLine();
                    mismatchDescription.AppendText("did not have an entry where:");

                    using (mismatchDescription.IndentBy(4))
                    {
                        mismatchDescription.AppendNewLine()
                            .AppendText("key: ")
                            .AppendDescriptionOf(entryDescriptor.KeyMatcher)
                            .AppendNewLine()
                            .AppendText("value: ")
                            .AppendDescriptionOf(entryDescriptor.ValueMatcher);
                    }

                    first = false;
                }

                foreach (var additionalEntry in entriesList)
                {
                    if (!first) mismatchDescription.AppendText(",");

                    mismatchDescription.AppendNewLine();
                    mismatchDescription.AppendText("had an additional entry where:");

                    using (mismatchDescription.IndentBy(4))
                    {
                        mismatchDescription.AppendNewLine()
                            .AppendText("key: ")
                            .AppendValue(additionalEntry.Value.Key)
                            .AppendNewLine()
                            .AppendText("value: ")
                            .AppendValue(additionalEntry.Value.Value);
                    }

                    first = false;
                }

                return false;
            }
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("a dictionary consisting of:");

            var first = true;

            using (description.IndentBy(4))
            {
                foreach (var entryDescriptor in _entryDescriptors)
                {
                    if (!first) description.AppendText(",");

                    description.AppendNewLine()
                        .AppendText("an entry where:");

                    using (description.IndentBy(4))
                    {
                        description.AppendNewLine()
                            .AppendText("key: ")
                            .AppendDescriptionOf(entryDescriptor.KeyMatcher)
                            .AppendNewLine()
                            .AppendText("value: ")
                            .AppendDescriptionOf(entryDescriptor.ValueMatcher);
                    }

                    first = false;
                }
            }
        }
    }
}