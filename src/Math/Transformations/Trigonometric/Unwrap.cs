using AlgebraSystem.Concrete.Rules.Trigonometry;

namespace AlgebraSystem.Math.Transformations.Trigonometric
{
    public class Unwrap : RuleOrientedTransformation
    {
        public Unwrap() : base(new Unwrapping()) { }
    }
}
