namespace NHamcrest.Core
{
    public class StringEndsWith : SubstringMatcher
    {
        public StringEndsWith(string substring) : base(substring)
        {
        }

        protected override bool EvalSubstringOf(string @string)
        {
            return @string.EndsWith(Substring, StringComparison);
        }

        protected override string Relationship()
        {
            return "ending with";
        }
    }
}