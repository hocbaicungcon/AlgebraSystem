using AlgebraSystem.Concrete.Rules.Algebra;

namespace AlgebraSystem.Math.Transformations.Algebra
{
    public class Unwrap : RuleOrientedTransformation
    {
        public Unwrap() : base(new Unwrapping()) { }

        //public Unwrap(Expression expr) : base(expr, new Unwrapping()) { }
    }
}
