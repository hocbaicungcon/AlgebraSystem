using AlgebraSystem.Expressions;
using System.Collections.Generic;

namespace AlgebraSystem.Math
{
    public class BinaryToLinear
    {
        private Expression result;

        public Expression Result
        {
            get { return result; }
        }

        public BinaryToLinear() { }

        public BinaryToLinear(Expression expr)
        {
            this.result = Transform(expr);
        }

        public Expression Transform(Expression expr)
        {
            if (expr is FunctionExpression)
            {
                FunctionExpression fexpr = (FunctionExpression)expr;

                IList<Expression> arguments = new List<Expression>();

                foreach (Expression arg in fexpr.Arguments)
                    arguments.Add(Transform(arg));

                return Expression.Function(fexpr.Name, arguments);
            }
            else if (expr is BinaryExpression)
            {
                BinaryExpression bexpr = (BinaryExpression)expr;

                Expression left = Transform(bexpr.Left);
                Expression right = Transform(bexpr.Right);

                if (bexpr.Operator == Binary.Divide || bexpr.Operator == Binary.Power)
                    return Expression.FromBinary(bexpr.Operator, left, right);
                else
                {
                    LinearExpression linear = new LinearExpression(bexpr.Operator);

                    linear.Insert(left);
                    linear.Insert(right);

                    return linear;
                }
            }
            else if (expr is LinearExpression)
            {
                LinearExpression lexpr = (LinearExpression)expr;

                IList<Expression> list = new List<Expression>();

                foreach (Expression item in lexpr.List)
                    list.Add(Transform(item));

                return new LinearExpression(lexpr.Operator, list);
            }

            return expr;
        }
    }
}