namespace AlgebraSystem.Concrete.Rules.Trigonometry
{
    class Wrapping : RuleCollection
    {
        public Wrapping()
        {
            Add("(sin(x))^2 + (cos(x))^2", "1");

            Add("sin(x) / cos(x)", "tan(x)");

            Add("sin(x) * cos(x)", "sin(2 * x) / 2");

            Add("(cos(x))^2 - (sin(x))^2", "cos(2 * x)");
        }
    }
}