namespace AlgebraSystem.Concrete.Rules.Algebra
{
    public class Unwrapping : RuleCollection
    {
        public Unwrapping() : base()
        {
            Add("(a + b) * x", "a * x + b * x");

            Add("x * (a + b)", "a * x + b * x");

            Add("(a + b) / c", "a / c + b / c");

            Add("(a * b) / c", "a * (b / c)");

            Add("a / (b * c)", "(1 / b) * (a / c)");

            Add("(a * b) / (c * d)", "(a / c) * (b / d)");

            //Add("(a + b) * (c + d)", "a * c + a * d + b * c + b * d");
        }
    }
}
