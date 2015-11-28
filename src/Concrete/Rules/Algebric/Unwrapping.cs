namespace AlgebraSystem.Concrete.Rules.Algebric
{
    public class Unwrapping : RuleCollection
    {
        public Unwrapping() : base()
        {
            Add("(a + b) * x", "a * x + b * x");
            Add("x * (a + b)", "a * x + b * x");

            Add("(a + b) * (c + d)", "a * c + a * d + b * c + b * d");
        }
    }
}
