namespace AlgebraSystem.Concrete.Rules.Trigonometry
{
    public class Unwrapping : RuleCollection
    {
        public Unwrapping()
        {
            Add("tan(x)", "sin(x) / cos(x)");

            Add("sin(2 * x)", "2 * sin(x) * cos(x)");

            Add("cos(2 * x)", "(cos(x))^2 - (sin(x))^2");
        }
    }
}