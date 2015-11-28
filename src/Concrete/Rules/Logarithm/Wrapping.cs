namespace AlgebraSystem.Concrete.Rules.Logarithm
{
    public class Wrapping : RuleCollection
    {
        public Wrapping()
        {
            Add("log(a) + log(b)", "log(a * b)");

            Add("b * log(a)", "log(a ^ b)");

            Add("x*log(a) + log(b)", "log((a^x) * b)");

            Add("x*log(a) + y*log(b)", "log((a^x) * (b^y))");
        }
    }
}
