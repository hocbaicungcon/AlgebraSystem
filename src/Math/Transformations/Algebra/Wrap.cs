using AlgebraSystem.Concrete.Rules.Algebra;

namespace AlgebraSystem.Math.Transformations.Algebra
{
    public class Wrap : RuleOrientedTransformation
    {
        public Wrap() : base(new Wrapping()) { }

        //public Wrap(Expression expr) : base(expr, new Wrapping()) { }
    }
}
