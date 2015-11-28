using AlgebraSystem.Expressions;
using System.Collections.Generic;

namespace AlgebraSystem.Math.Reduction
{
    public class EliminateSubtractions
    {
        private Expression result;

        public Expression Result
        {
            get { return result; }
        }

        public EliminateSubtractions() { }

        public EliminateSubtractions(Expression expr)
        {
            this.result = Eliminate(expr);
        }

        public Expression Eliminate(Expression expr)
        {
            if (expr is FunctionExpression)
            {
                FunctionExpression fexpr = (FunctionExpression)expr;

                IList<Expression> arguments = new List<Expression>();

                foreach (Expression arg in fexpr.Arguments)
                    arguments.Add(Eliminate(arg));

                return Expression.Function(fexpr.Name, arguments);
            }
            else if (expr is BinaryExpression)
            {
                BinaryExpression bexpr = (BinaryExpression)expr;

                Expression left = Eliminate(bexpr.Left);
                Expression right = Eliminate(bexpr.Right);

                if (bexpr.Operator == Binary.Subtract)
                {
                    if (right is NumberExpression)
                    {
                        double num = ((NumberExpression)right).Value;
                        num *= -1;

                        right = Expression.Number(num);
                    }
                    else
                        right = Expression.Multiply(Expression.Number(-1), right);

                    return Expression.FromBinary(Binary.Add, left, right);
                }

                return Expression.FromBinary(bexpr.Operator, left, right);
            }

            return expr;
        }
    }
}