using AlgebraSystem.Concrete.Functions;
using AlgebraSystem.Expressions;

namespace AlgebraSystem.Math.Calculus
{
    public class Differentiation
    {
        private string var;

        private IDifferentiationScope functions;

        private Expression result;

        public Expression Result
        {
            get { return result; }
        }

        public Differentiation(string var = "x", IDifferentiationScope functions = null)
        {
            this.var = var;
            this.functions = functions;
        }

        public Differentiation(Expression expr, string var = "x", IDifferentiationScope functions = null) : this(var, functions)
        {
            this.result = Differentiate(expr);
        }

        public Expression Differentiate(Expression expr)
        {
            if (expr is NumberExpression)
                return Expression.Number(0);
            else if (expr is VariableExpression)
            {
                VariableExpression vexpr = (VariableExpression)expr;

                if (vexpr.Name != var)
                    return Expression.Number(0);

                return Expression.Number(1);
            }
            else if (expr is FunctionExpression)
            {
                FunctionExpression fexpr = (FunctionExpression)expr;

                if (fexpr.Arguments.Count == 0)
                    return Expression.Number(0);

                int pos = 0;

                Expression result = null;

                foreach (Expression arg in fexpr.Arguments)
                {
                    Expression darg = Differentiate(arg);

                    if (darg.Equals(Expression.Number(0)))
                    {
                        pos++;
                        continue;
                    }
                    else
                    {
                        Expression argResult = null;
                        Expression derivative = null;

                        if (fexpr.Arguments.Count > 1)
                            derivative = functions.Differentiate(fexpr.Name, fexpr.Arguments, pos, fexpr.Arguments.Count);
                        else
                            derivative = functions.Differentiate(fexpr.Name, fexpr.Arguments[0]);

                        if (darg.Equals(Expression.Number(1)))
                            argResult = derivative;
                        else
                            argResult = Expression.Multiply(derivative, darg);

                        if (result == null)
                            result = argResult;
                        else
                            result = Expression.Add(result, argResult);
                    }

                    pos++;
                }

                if (result == null)
                    result = Expression.Number(0);

                return result;
            }
            else if (expr is BinaryExpression)
            {
                BinaryExpression bexpr = (BinaryExpression)expr;

                Expression left = bexpr.Left;
                Expression right = bexpr.Right;

                Expression dleft = Differentiate(left);
                Expression dright = Differentiate(right);

                if (bexpr.Operator == Binary.Add)
                    return Expression.Add(dleft, dright);
                else if (bexpr.Operator == Binary.Subtract)
                    return Expression.Subtract(dleft, dright);
                else if (bexpr.Operator == Binary.Multiply)
                {
                    if (dleft.Equals(Expression.Number(0)))
                        return Expression.Multiply(left, dright);
                    else if (dright.Equals(Expression.Number(0)))
                        return Expression.Multiply(dleft, right);

                    return Expression.Add(Expression.Multiply(dleft, right), Expression.Multiply(left, dright));
                }
                else if (bexpr.Operator == Binary.Divide)
                {
                    if (dright.Equals(Expression.Number(0)))
                        return Expression.Divide(dleft, right);
                    else  if (dleft.Equals(Expression.Number(0)))
                        return Expression.Divide(Expression.Multiply(Expression.Multiply(Expression.Number(-1), left), dright), Expression.Power(right, Expression.Number(2)));

                    return Expression.Divide(Expression.Subtract(Expression.Multiply(dleft, right), Expression.Multiply(left, dright)), Expression.Power(right, Expression.Number(2)));
                }
                else if (bexpr.Operator == Binary.Power)
                {
                    if (dright.Equals(Expression.Number(0)))
                        return Expression.Multiply(Expression.Multiply(right, Expression.Power(left, Expression.Subtract(right, Expression.Number(1)))), dleft);
                    else if (dleft.Equals(Expression.Number(0)))
                        return Expression.Multiply(Expression.Multiply(Expression.Power(left, right), dright), log(left));

                    return Expression.Multiply(expr, Expression.Add(Expression.Multiply(dright, log(left)), Expression.Multiply(right, Expression.Divide(dleft, left))));
                }
            }

            return expr;
        }

        private Expression log(Expression expr)
        {
            return Expression.Function(Logarithm.Alias, expr);
        }
    }
}