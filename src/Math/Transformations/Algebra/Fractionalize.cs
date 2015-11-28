using AlgebraSystem.Concrete.Rules.Algebra;

namespace AlgebraSystem.Math.Transformations.Algebra
{
    public class Fractionalize : RuleOrientedTransformation
    {
        public Fractionalize() : base(new Fraction()) { }

        //public Fractionalize(Expression expr) : base(expr, new Fraction()) { }
    }
}
