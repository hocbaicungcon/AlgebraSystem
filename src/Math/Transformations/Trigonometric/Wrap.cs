using AlgebraSystem.Concrete.Rules.Trigonometry;

namespace AlgebraSystem.Math.Transformations.Trigonometric
{
    public class Wrap : RuleOrientedTransformation
    {
        public Wrap() : base(new Wrapping()) { }
    }
}
