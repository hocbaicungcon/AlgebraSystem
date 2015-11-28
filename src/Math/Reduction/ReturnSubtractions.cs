using AlgebraSystem.Expressions;
using System.Collections.Generic;

namespace AlgebraSystem.Math.Reduction
{
    public class ReturnSubtractions
    {
        private Expression result;

        public Expression Result
        {
            get { return result; }
        }

        public ReturnSubtractions() { }

        public ReturnSubtractions(Expression expr)
        {
            this.result = Return(expr);
        }

        public Expression Return(Expression expr)
        {
            Expression returned = ReturnSubtraction(expr);

            if (returned is NegativeExpression)
                return ((NegativeExpression)returned).Original;

            return returned;
        }

        public Expression ReturnSubtraction(Expression expr)
        {
            if (expr is FunctionExpression)
            {
                FunctionExpression fexpr = (FunctionExpression)expr;

                IList<Expression> args = new List<Expression>();

                foreach (Expression arg in fexpr.Arguments)
                    args.Add(Return(arg));

                return Expression.Function(fexpr.Name, args);
            }
            else if (expr is BinaryExpression)
            {
                BinaryExpression bexpr = (BinaryExpression)expr;

                Expression left = ReturnSubtraction(bexpr.Left);
                Expression right = ReturnSubtraction(bexpr.Right);

                Expression oleft = left;
                Expression oright = right;

                if (bexpr.Operator == Binary.Multiply || bexpr.Operator == Binary.Divide)
                {
                    /*
                    bool negative = false;

                    NumberExpression number = null;
                    bool isLeft = false;

                    if (left is NumberExpression && ((NumberExpression)left).Value < 0)
                    {
                        negative = true;
                        nleft = Expression.Number(((NumberExpression)left).Value * -1);

                        isLeft = true;
                        number = (NumberExpression)nleft;
                    }
                    else if (right is NumberExpression && ((NumberExpression)right).Value < 0)
                    {
                        negative = true;
                        nright = Expression.Number(((NumberExpression)right).Value * -1);

                        number = (NumberExpression)nright;
                    }

                    if (left is NegativeExpression || right is NegativeExpression)
                        negative = !negative;

                    if (left is NegativeExpression)
                    {
                        nleft = ((NegativeExpression)nleft).Value;
                        left = ((NegativeExpression)left).Original;
                    }
                    else if (right is NegativeExpression)
                    {
                        nright = ((NegativeExpression)nright).Value;
                        right = ((NegativeExpression)right).Original;
                    }

                    Expression toReturn = null;

                    if (number != null && number.Value == 1 && (bexpr.Operator == Binary.Multiply || bexpr.Operator == Binary.Divide && !isLeft))
                    {
                        if (isLeft)
                            toReturn = nright;
                        else
                            toReturn = nleft;
                    }
                    else
                        toReturn = Expression.FromBinary(bexpr.Operator, nleft, nright);

                    if (!negative)
                        return toReturn;

                    return new NegativeExpression(toReturn, Expression.FromBinary(bexpr.Operator, left, right));
                    */

                    bool negative = false;

                    if (left is NegativeExpression)
                    {
                        negative = !negative;

                        oleft = ((NegativeExpression)left).Original;
                        left = ((NegativeExpression)left).Value;
                    }

                    if (right is NegativeExpression)
                    {
                        negative = !negative;

                        oright = ((NegativeExpression)right).Original;
                        right = ((NegativeExpression)right).Value;
                    }

                    Expression full = null;

                    if (left is NumberExpression && ((NumberExpression)left).Value == 1 && bexpr.Operator == Binary.Multiply)
                        full = right;
                    else if (right is NumberExpression && ((NumberExpression)right).Value == 1 && (bexpr.Operator == Binary.Multiply || bexpr.Operator == Binary.Divide))
                        full = left;
                    else
                        full = Expression.FromBinary(bexpr.Operator, left, right);

                    if (negative)
                        full = new NegativeExpression(full, Expression.FromBinary(bexpr.Operator, oleft, oright));

                    return full;
                }
                else if (bexpr.Operator == Binary.Add && (left is NegativeExpression || right is NegativeExpression))
                {
                    if (!(left is NegativeExpression) && right is NegativeExpression)
                        return Expression.Subtract(left, ((NegativeExpression)right).Value);
                    else if (left is NegativeExpression && !(right is NegativeExpression))
                        return Expression.Subtract(right, ((NegativeExpression)left).Value);
                    else
                        return Expression.Subtract(((NegativeExpression)left).Original, ((NegativeExpression)right).Value);
                }

                return Expression.FromBinary(bexpr.Operator, left, right);
            }
            else if (expr is NumberExpression)
            {
                NumberExpression nexpr = (NumberExpression)expr;

                if (nexpr.Value < 0)
                    return new NegativeExpression(Expression.Number(-1 * nexpr.Value), nexpr);

                return nexpr;
            }

            return expr;
        }
    }
}