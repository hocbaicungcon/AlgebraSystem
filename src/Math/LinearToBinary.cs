using AlgebraSystem.Expressions;
using System.Collections.Generic;
using System;

namespace AlgebraSystem.Math
{
    public class LinearToBinary
    {
        private Expression result;

        public Expression Result
        {
            get { return result; }
        }

        public LinearToBinary() { }

        public LinearToBinary(Expression expr)
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
            else if (expr is LinearExpression)
            {
                LinearExpression lexpr = (LinearExpression)expr;

                if (lexpr.List.Count == 0)
                    throw new Exception("Can't transform an empty linear tree");

                Expression first = Transform(lexpr[0]);

                lexpr.List.RemoveAt(0);
                List<Expression> rest = (List<Expression>)lexpr.List;

                foreach (Expression e in rest)
                    first = Expression.FromBinary(lexpr.Operator, first, Transform(e));

                return first;
            }
            else if (expr is BinaryExpression)
            {
                BinaryExpression bexpr = (BinaryExpression)expr;

                Expression left = Transform(bexpr.Left);
                Expression right = Transform(bexpr.Right);

                return Expression.FromBinary(bexpr.Operator, left, right);
            }

            return expr;
        }
    }
}