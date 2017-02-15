namespace NHamcrest.Core
{
    public class IsNot<T> : Matcher<T>
    {
        private readonly IMatcher<T> matcher;

        public IsNot(IMatcher<T> matcher)
        {
            this.matcher = matcher;
        }

        public override bool Matches(T arg)
        {
            return matcher.Matches(arg) == false;
        }

        public override void DescribeTo(IDescription description)
        {
            description.AppendText("not ").AppendDescriptionOf(matcher);
        }
    }

    public static partial class Is
    {
        // This is a shortcut to the frequently used Is.Not(Equal.To(x)).
        //
        // For example:  Assert.That(cheese, Is.Not(Equal.To(smelly))))
        //         vs.  Assert.That(cheese, Is.Not(smelly)))
        [Factory]
        public static IMatcher<T> Not<T>(T value)
        {
            return Not(EqualTo(value));
        }

        [Factory]
        public static Matcher<T> Not<T>(IMatcher<T> matcher)
        {
            return new IsNot<T>(matcher);
        }
    }
}