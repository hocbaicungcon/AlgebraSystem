namespace AlgebraSystem.Expressions
{
    public class BinaryExpression : Expression
    {
        private Binary op;

        private Expression left;

        private Expression right;

        public Binary Operator
        {
            get { return op; }
        }

        public Expression Left
        {
            get { return left; }
        }

        public Expression Right
        {
            get { return right; }
        }

        public BinaryExpression(Binary Operator, Expression Left, Expression Right)
        {
            this.op = Operator;

            this.left = Left;
            this.right = Right;
        }
    }
}