using AlgebraSystem.Expressions;
using System;
using System.Collections.Generic;
using System.Reflection;

using Linq = System.Linq.Expressions;

namespace AlgebraSystem.Parsing.Reverse
{
    public class ToLinq
    {
        private MethodInfo powMethod = typeof(System.Math).GetMethod("Pow");

        private ILinqMethodReverseScope methods;

        private Linq.Expression result;

        public Linq.Expression Result
        {
            get { return result; }
        }

        public ToLinq(ILinqMethodReverseScope methods = null)
        {
            this.methods = methods;
        }

        public ToLinq(Expression expr, ILinqMethodReverseScope methods = null) : this(methods)
        {
            this.result = Reverse(expr);
        }

        public Linq.Expression Reverse(Expression expr)
        {
            if (expr is NumberExpression)
            {
                NumberExpression nexpr = (NumberExpression)expr;
                return Linq.Expression.Constant(nexpr.Value);
            }
            else if (expr is VariableExpression)
            {
                VariableExpression vexpr = (VariableExpression)expr;
                return Linq.Expression.Parameter(typeof(double), vexpr.Name);
            }
            else if (expr is FunctionExpression)
            {
                FunctionExpression fexpr = (FunctionExpression)expr;

                IList<Linq.Expression> arguments = new List<Linq.Expression>();

                foreach (Expression arg in fexpr.Arguments)
                    arguments.Add(Reverse(arg));

                if (!exists(fexpr.Name))
                    throw new Exception("Method name doesn't exist in the scope");

                MethodInfo method = get(fexpr.Name);

                return Linq.Expression.Call(method, arguments);
            }
            else if (expr is BinaryExpression)
            {
                BinaryExpression bexpr = (BinaryExpression)expr;

                Linq.Expression left = Reverse(bexpr.Left);
                Linq.Expression right = Reverse(bexpr.Right);

                if (bexpr.Operator == Binary.Power)
                    return Linq.Expression.Call(powMethod, left, right);

                return Linq.Expression.MakeBinary(getOperator(bexpr.Operator), left, right);
            }

            throw new Exception("Can't reverse expression");
        }

        private Linq.ExpressionType getOperator(Binary op)
        {
            switch (op)
            {
                case Binary.Add:
                    return Linq.ExpressionType.Add;
                case Binary.Subtract:
                    return Linq.ExpressionType.Subtract;
                case Binary.Multiply:
                    return Linq.ExpressionType.Multiply;
                case Binary.Divide:
                    return Linq.ExpressionType.Divide;
                case Binary.Power:
                    return Linq.ExpressionType.Power;
            }

            throw new Exception("Can't reverse operator");
        }

        private bool exists(string name)
        {
            if (methods == null)
                return false;

            return methods.Exists(name);
        }

        private MethodInfo get(string name)
        {
            return methods.Get(name);
        }
    }
}