using AlgebraSystem.Concrete.Rules.Logarithm;

namespace AlgebraSystem.Math.Transformations.Logarithmic
{
    public class Unwrap : RuleOrientedTransformation
    {
        public Unwrap() : base(new Unwrapping()) { }
    }
}
