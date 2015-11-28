using System.Collections.Generic;

namespace AlgebraSystem.Expressions
{
    public class LinearExpression : Expression
    {
        private Binary op;

        private IList<Expression> list;

        public Expression this[int index]
        {
            get { return list[index]; }
        }

        public IList<Expression> List
        {
            get { return list; }
        }

        public Binary Operator
        {
            get { return op; }
        }

        public LinearExpression(Binary op)
        {
            this.list = new List<Expression>();
            this.op = op;
        }

        public LinearExpression(Binary op, IList<Expression> list) : this(op)
        {
            this.list = list;
        }

        public void Insert(Expression expr)
        {
            if (expr is LinearExpression && ((LinearExpression)expr).Operator == op && op != Binary.Divide && op != Binary.Power)
            {
                foreach (Expression ex in ((LinearExpression)expr).List)
                    Insert(ex);
            }
            else
                list.Add(expr);
        }
    }
}