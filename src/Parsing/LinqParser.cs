using AlgebraSystem.Expressions;
using System;
using System.Collections.Generic;
using System.Reflection;

using Linq = System.Linq.Expressions;

namespace AlgebraSystem.Parsing
{
    public class LinqParser
    {
        private readonly MethodInfo powMethod = typeof(System.Math).GetMethod("Pow");

        private ILinqMethodScope methods;

        private Expression result;

        public Expression Result
        {
            get { return result; }
        }

        public LinqParser(ILinqMethodScope methods = null)
        {
            this.methods = methods;
        }

        public LinqParser(Linq.Expression linqExpression, ILinqMethodScope methods = null) : this(methods)
        {
            this.result = Parse(linqExpression);
        }

        public LinqParser(Linq.LambdaExpression lambdaExpression, ILinqMethodScope methods = null) : this(lambdaExpression.Body, methods) { }

        public Expression Parse(Linq.LambdaExpression lambdaExpression)
        {
            return Parse(lambdaExpression.Body);
        }

        public Expression Parse(Linq.Expression linqExpression)
        {
            if (linqExpression is Linq.ConstantExpression)
            {
                Linq.ConstantExpression cexpr = (Linq.ConstantExpression)linqExpression;

                if (!(cexpr.Value is double))
                    throw new Exception("Can't parse a non-numeric constant");

                return new NumberExpression((double)cexpr.Value);
            }
            else if (linqExpression is Linq.ParameterExpression)
            {
                Linq.ParameterExpression pexpr = (Linq.ParameterExpression)linqExpression;
                return new VariableExpression(pexpr.Name);
            }
            else if (linqExpression is Linq.MethodCallExpression)
            {
                Linq.MethodCallExpression mexpr = (Linq.MethodCallExpression)linqExpression;

                IList<Expression> arguments = new List<Expression>();

                foreach (Linq.Expression arg in mexpr.Arguments)
                    arguments.Add(Parse(arg));

                if (!exists(mexpr.Method))
                    throw new Exception("Method name doesn't exist in the scope");

                if (mexpr.Method.Equals(powMethod))
                    return new BinaryExpression(Binary.Power, arguments[0], arguments[1]);

                string name = get(mexpr.Method);

                return new FunctionExpression(name, arguments);
            }
            else if (linqExpression is Linq.BinaryExpression)
            {
                Linq.BinaryExpression bexpr = (Linq.BinaryExpression)linqExpression;

                Expression left = Parse(bexpr.Left);
                Expression right = Parse(bexpr.Right);

                return new BinaryExpression(getOperator(bexpr.NodeType), left, right);
            }

            throw new Exception("Can't parse expression");
        }

        private bool exists(MethodInfo method)
        {
            if (methods == null)
                return false;

            return methods.Exists(method);
        }

        private string get(MethodInfo method)
        {
            return methods.Get(method);
        }

        private Binary getOperator(Linq.ExpressionType type)
        {
            switch (type)
            {
                case Linq.ExpressionType.Add:
                    return Binary.Add;
                case Linq.ExpressionType.Subtract:
                    return Binary.Subtract;
                case Linq.ExpressionType.Multiply:
                    return Binary.Multiply;
                case Linq.ExpressionType.Divide:
                    return Binary.Divide;
                case Linq.ExpressionType.Power:
                    return Binary.Power;
            }

            throw new Exception("Can't parse a non-binary operator");
        }
    }
}