using AlgebraSystem.Concrete.Rules.Logarithm;

namespace AlgebraSystem.Math.Transformations.Logarithmic
{
    public class Wrap : RuleOrientedTransformation
    {
        public Wrap() : base(new Wrapping()) { }
    }
}
