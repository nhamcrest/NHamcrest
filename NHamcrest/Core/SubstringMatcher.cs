namespace NHamcrest.Core
{
    public abstract class SubstringMatcher : NonNullMatcher<string>
    {
        protected readonly string Substring;

        protected SubstringMatcher(string substring)
        {
            Substring = substring;
        }

        protected override bool MatchesSafely(string item)
        {
            return EvalSubstringOf(item);
        }

        public override void DescribeMismatchSafely(string item, IDescription mismatchDescription)
        {
            mismatchDescription.AppendText("was \"").AppendText(item).AppendText("\"");
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("a string ").AppendText(Relationship()).AppendText(" \"")
                .AppendValue(Substring).AppendText("\"");
        }

        protected abstract bool EvalSubstringOf(string @string);

        protected abstract string Relationship();
    }
}