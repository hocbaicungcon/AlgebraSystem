using AlgebraSystem.Expressions;

namespace AlgebraSystem.Concrete.Rules.Algebra
{
    public class NumericWrapping : RuleCollection
    {
        public NumericWrapping()
        {
            Add("a + b", "a + b", vars => vars["a"] is NumberExpression && vars["b"] is NumberExpression);

            Add("a * b", "a * b", vars => vars["a"] is NumberExpression && vars["b"] is NumberExpression);
        }
    }
}
