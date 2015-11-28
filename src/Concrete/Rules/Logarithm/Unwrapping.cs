namespace AlgebraSystem.Concrete.Rules.Logarithm
{
    public class Unwrapping : RuleCollection
    {
        public Unwrapping() : base()
        {
            Add("log(a * b)", "log(a) + log(b)");

            Add("log(a / b)", "log(a) - log(b)");

            Add("log(a ^ b)", "b * log(a)");
        }
    }
}
