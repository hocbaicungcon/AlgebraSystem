namespace AlgebraSystem.Math.Reduction
{
    public class NegativeExpression : Expression
    {
        private Expression value;

        private Expression original;

        public Expression Value
        {
            get { return value; }
        }

        public Expression Original
        {
            get { return original; }
        }

        public NegativeExpression(Expression value)
        {
            this.value = value;
        }

        public NegativeExpression(Expression value, Expression original)
        {
            this.value = value;
            this.original = original;
        }
    }
}
