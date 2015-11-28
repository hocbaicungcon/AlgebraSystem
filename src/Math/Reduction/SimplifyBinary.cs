using AlgebraSystem.Expressions;
using System;
using System.Collections.Generic;

namespace AlgebraSystem.Math.Reduction
{
    public class SimplifyBinary
    {
        private IVariableScope variables;

        private IFunctionScope functions;

        private Expression result;

        public Expression Result
        {
            get { return result; }
        }

        public SimplifyBinary(IVariableScope variables = null, IFunctionScope functions = null)
        {
            this.variables = variables;
            this.functions = functions;
        }

        public SimplifyBinary(Expression expr, IVariableScope variables = null, IFunctionScope functions = null) : this(variables, functions)
        {
            this.result = Simplify(expr);
        }

        public Expression Simplify(Expression expr)
        {
            if (expr is VariableExpression)
            {
                VariableExpression vexpr = (VariableExpression)expr;
                string name = vexpr.Name;

                if (existsVariable(name))
                    return getVariable(name);
            }
            else if (expr is FunctionExpression)
            {
                FunctionExpression fexpr = (FunctionExpression)expr;
                string name = fexpr.Name;

                bool exists = existsFunction(name);

                if (exists && !acceptsFunction(name, fexpr.Arguments.Count))
                    throw new Exception("Function doesn't accept as many arguments");

                IList<double> varguments = new List<double>();
                IList<Expression> arguments = new List<Expression>();

                foreach (Expression arg in fexpr.Arguments)
                {
                    Expression sarg = Simplify(arg);
                    arguments.Add(sarg);

                    if (sarg is NumberExpression)
                        varguments.Add(((NumberExpression)sarg).Value);
                }

                if (exists)
                {
                    if (varguments.Count == arguments.Count)
                        return Expression.Number(calculateFunction(name, varguments));

                    return evaluateFunction(name, arguments);
                }
                else
                    return Expression.Function(name, arguments);
            }
            else if (expr is BinaryExpression)
            {
                BinaryExpression bexpr = (BinaryExpression)expr;

                Binary op = bexpr.Operator;

                Expression left = Simplify(bexpr.Left);
                Expression right = Simplify(bexpr.Right);

                if (left is NumberExpression && right is NumberExpression)
                    return calculate(op, (NumberExpression)left, (NumberExpression)right);
                else if (left is NumberExpression || right is NumberExpression)
                {
                    double num;
                    Expression other;

                    bool isLeft = true;

                    if (left is NumberExpression)
                    {
                        num = ((NumberExpression)left).Value;
                        other = right;
                    }
                    else
                    {
                        isLeft = false;

                        num = ((NumberExpression)right).Value;
                        other = left;
                    }

                    if (num == 0 && op == Binary.Add ||
                        num == 0 && op == Binary.Subtract && !isLeft ||
                        num == 1 && op == Binary.Multiply ||
                        num == 1 && op == Binary.Divide && !isLeft ||
                        num == 1 && op == Binary.Power && !isLeft)
                        return other;

                    if (num == 0 && op == Binary.Multiply ||
                        num == 0 && op == Binary.Divide && isLeft ||
                        num == 0 && op == Binary.Power && isLeft)
                        return Expression.Number(0);

                    if (num == 0 && op == Binary.Power && !isLeft ||
                        num == 1 && op == Binary.Power && isLeft)
                        return Expression.Number(1);

                    if (isLeft && op == Binary.Add)
                        return Expression.Add(right, left);

                    if (!isLeft && op == Binary.Multiply)
                        return Expression.Multiply(right, left);
                }
                else
                {
                    if (op == Binary.Divide && left.Equals(right))
                        return Expression.Number(1);

                    if (op == Binary.Subtract && left.Equals(right))
                        return Expression.Number(0);

                    if (op == Binary.Add && right is BinaryExpression && ((BinaryExpression)right).Operator == Binary.Multiply && ((BinaryExpression)right).Left is NumberExpression && ((NumberExpression)((BinaryExpression)right).Left).Value == -1)
                    {
                        BinaryExpression bright = (BinaryExpression)right;

                        if (left.Equals(bright.Right))
                            return Expression.Number(0);
                    }
                }

                return Expression.FromBinary(bexpr.Operator, left, right);
            }
            else if (expr is LinearExpression)
            {
                LinearExpression lexpr = (LinearExpression)expr;

                IList<Expression> list = new List<Expression>();

                foreach (Expression item in lexpr.List)
                    list.Add(Simplify(item));

                return new LinearExpression(lexpr.Operator, list);
            }

            return expr;
        }
        
        private bool existsVariable(string name)
        {
            if (variables == null)
                return false;

            return variables.Exists(name);
        }

        private bool existsFunction(string name)
        {
            if (functions == null)
                return false;

            return functions.Exists(name);
        }

        private bool acceptsFunction(string name, int count)
        {
            if (functions == null)
                return false;

            return functions.Accepts(name, count);
        }

        private Expression getVariable(string name)
        {
            return variables.Get(name);
        }

        private double calculateFunction(string name, IList<double> arguments)
        {
            return functions.Calculate(name, arguments);
        }

        private Expression evaluateFunction(string name, IList<Expression> arguments)
        {
            return functions.Evaluate(name, arguments);
        }

        private Expression calculate(Binary op, NumberExpression left, NumberExpression right)
        {
            switch (op)
            {
                case Binary.Add:
                    return left.Add(right);
                case Binary.Subtract:
                    return left.Subtract(right);
                case Binary.Multiply:
                    return left.Multiply(right);
                case Binary.Divide:
                    return left.Divide(right);
                case Binary.Power:
                    return left.Power(right);
            }

            throw new Exception("Unknown operator");
        }
    }
}