using AlgebraSystem.Concrete.Rules.Algebra;

namespace AlgebraSystem.Math.Transformations.Algebra
{
    public class Organize : RuleOrientedTransformation
    {
        public Organize() : base(new Organization()) { }

        //public Organize(Expression expr) : base(expr, new Organization()) { }
    }
}
