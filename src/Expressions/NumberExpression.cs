namespace AlgebraSystem.Expressions
{
    public class NumberExpression : Expression
    {
        private double val;

        public double Value
        {
            get { return val; }
        }

        public NumberExpression(double value)
        {
            this.val = value;
        }

        public Expression Add(NumberExpression expr)
        {
            return Number(val + expr.Value);
        }

        public Expression Subtract(NumberExpression expr)
        {
            return Number(val - expr.Value);
        }

        public Expression Multiply(NumberExpression expr)
        {
            return Number(val * expr.Value);
        }

        public Expression Divide(NumberExpression expr)
        {
            return Number(val / expr.Value);
        }

        public Expression Power(NumberExpression expr)
        {
            return Number(System.Math.Pow(val, expr.Value));
        }
    }
}