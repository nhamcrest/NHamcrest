namespace NHamcrest.Core
{
    public class StringContains : SubstringMatcher
    {
        public StringContains(string substring) : base(substring) { }

        protected override bool EvalSubstringOf(string s)
        {
            return s.IndexOf(Substring, StringComparison) >= 0;
        }

        protected override string Relationship()
        {
            return "containing";
        }
    }
}