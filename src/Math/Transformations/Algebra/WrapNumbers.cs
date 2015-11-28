using AlgebraSystem.Concrete.Rules.Algebra;

namespace AlgebraSystem.Math.Transformations.Algebra
{
    public class WrapNumbers : RuleOrientedTransformation
    {
        public WrapNumbers() : base(new NumericWrapping()) { }

        //public WrapNumbers(Expression expr) : base(expr, new NumericWrapping()) { }
    }
}
