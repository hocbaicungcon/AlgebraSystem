namespace AlgebraSystem.Concrete.Rules.Algebra
{
    public class Wrapping : RuleCollection
    {
        public Wrapping()
        {
            Add("a * x + b * x", "(a + b) * x");

            Add("x * a + b * x", "(a + b) * x");

            Add("x * a + x * b", "(a + b) * x");

            Add("a * x + x", "(a + 1) * x");

            Add("x + x", "2 * x");

            Add("x / a + x / b", "(1 / a + 1 / b) * x");

            Add("x + x / a", "(1 + 1 / a) * x");

            Add("a * x + x / b", "(a + 1 / b) * x");

            Add("x * a + x", "(a + 1) * x");

            Add("a * (b / c)", "(a * b) / c");

            Add("(a / b) * (c / d)", "(a * c) / (b * d)");

            Add("a / c + b / c", "(a + b) / c");
        }
    }
}