using AlgebraSystem.Expressions;

namespace AlgebraSystem.Concrete.Rules.Algebra
{
    public class Organization : RuleCollection
    {
        public Organization(string var = null) : base()
        {
            Add("x * c", "c * x", vars => vars["c"] is NumberExpression && !(vars["x"] is NumberExpression));

            Add("c + x", "x + c", vars => vars["c"] is NumberExpression && !(vars["x"] is NumberExpression));

            Add("x + x^n", "x^n + x", vars => vars["n"] is NumberExpression && ((NumberExpression)vars["n"]).Value > 1);

            Add("c + x^n", "x^n + c", vars => vars["c"] is NumberExpression && vars["n"] is NumberExpression && ((NumberExpression)vars["n"]).Value > 1);

            Add("x^m + x^n", "x^n + x^m", vars => vars["m"] is NumberExpression && vars["n"] is NumberExpression && ((NumberExpression)vars["n"]).Value > ((NumberExpression)vars["m"]).Value);
        }
    }
}
