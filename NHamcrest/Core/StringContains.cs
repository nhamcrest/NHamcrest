using System;

namespace NHamcrest.Core
{
    public class StringContains : SubstringMatcher
    {
        private StringComparison stringComparison = StringComparison.CurrentCulture;

        public StringContains(string substring) : base(substring) { }

        protected override bool EvalSubstringOf(string s)
        {
            return s.IndexOf(Substring, stringComparison) >= 0;
        }

        protected override string Relationship()
        {
            return "containing";
        }

        public StringContains CaseInsensitive()
        {
            stringComparison = StringComparison.CurrentCultureIgnoreCase;
            return this;
        }
    }

    public static class Contains
    {
        [Factory]
        public static StringContains String(string substring)
        {
            return new StringContains(substring);
        }
    }
}