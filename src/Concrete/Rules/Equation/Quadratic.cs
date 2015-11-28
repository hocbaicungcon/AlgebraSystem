namespace AlgebraSystem.Concrete.Rules.Equation
{
    public class Quadratic : RuleCollection
    {
        public Quadratic()
        {
            Add("E(a * x^2 + b * x + c, x)", "Array((-b + sqrt(b^2 - 4*a*c)) / (2*a), (-b - sqrt(b^2 - 4*a*c)) / (2*a))");

            Add("E(x^2 + x + c,x)", "Array((-1 + sqrt(1 - 4 * c)) / 2, (-1 - sqrt(1 - 4 * c)) / 2)");
        }
    }
}
