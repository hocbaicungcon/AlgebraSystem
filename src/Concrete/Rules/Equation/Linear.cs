namespace AlgebraSystem.Concrete.Rules.Equation
{
    public class Linear : RuleCollection
    {
        public Linear()
        {
            Add("E(x, x)", "0");

            Add("E(x + c, x)", "-c");

            Add("E(a * x, x)", "0");

            Add("E(a * x + c, x)", "-c / a");

            Add("E(x * a + c, x)", "-c / a");

            Add("E(c + x, x)", "c");

            Add("E(c + a * x)", "-c / a");

            Add("E(c + x * a)", "-c / a");

            Add("E(x/a, x)", "0");

            Add("E(x/a + c, x)", "-c * a");

            Add("E(c + x/a, x)", "-c * a");
        }
    }
}
