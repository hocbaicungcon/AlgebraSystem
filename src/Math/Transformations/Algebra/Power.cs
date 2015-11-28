using AlgebraSystem.Concrete.Rules.Algebra;

namespace AlgebraSystem.Math.Transformations.Algebra
{
    public class Power : RuleOrientedTransformation
    {
        public Power() : base(new Exponential()) { }

        //public Power(Expression expr) : base(expr, new Exponential()) { }
    }
}
