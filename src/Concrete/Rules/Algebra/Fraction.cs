namespace AlgebraSystem.Concrete.Rules.Algebra
{
    public class Fraction : RuleCollection
    {
        public Fraction()
        {

            Add("(a * b) / a", "b");

            Add("(a * b) / b", "a");

            Add("a / (a * b)", "1 / b");

            Add("b / (a * b)", "1 / a");
            Add("a / a", "1");

            Add("(a * b) / (a * c)", "b / c");

            Add("(a * b) / (c * a)", "b / c");

            Add("(b * a) / (a * c)", "b / c");

            Add("(b * a) / (c * a)", "b / c");

            Add("a / (b / c)", "(a * c) / b");
        }
    }
}