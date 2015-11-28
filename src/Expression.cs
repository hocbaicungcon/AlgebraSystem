using AlgebraSystem.Parsing;
using AlgebraSystem.Expressions;
using System.Collections.Generic;
using System;

using Linq = System.Linq.Expressions;
using AlgebraSystem.Math.Reduction;
using AlgebraSystem.Math;
using AlgebraSystem.Math.Calculus;
using AlgebraSystem.Parsing.Reverse;

using Algebra = AlgebraSystem.Math.Transformations.Algebra;
using Logarithmic = AlgebraSystem.Math.Transformations.Logarithmic;
using Trigonometric = AlgebraSystem.Math.Transformations.Trigonometric;

namespace AlgebraSystem
{
    public abstract class Expression
    {
        public static readonly IEnumerable<Type> transformations = new Type[]
        {
            //typeof(Logarithmic.Unwrap),
            //typeof(Trigonometric.Unwrap),
            typeof(Algebra.Unwrap),
            typeof(Algebra.Power),
            typeof(Algebra.Fractionalize),
            typeof(Algebra.Organize),
            typeof(Algebra.WrapNumbers),
            typeof(Algebra.Wrap),
            //typeof(Logarithmic.Wrap),
            //typeof(Trigonometric.Wrap),
            typeof(Algebra.Fractionalize)
        };

        public static Expression FromLinq(Linq.Expression linqExpression)
        {
            LinqParser parser = new LinqParser(linqExpression);
            return parser.Result;
        }

        public static Expression FromString(string expr)
        {
            Parser parser = new Parser(expr);
            return parser.Result;
        }

        public static Expression FromBinary(Binary op, Expression left, Expression right)
        {
            return new BinaryExpression(op, left, right);
        }

        public static Expression Add(Expression left, Expression right)
        {
            return FromBinary(Binary.Add, left, right);
        }

        public static Expression Subtract(Expression left, Expression right)
        {
            return FromBinary(Binary.Subtract, left, right);
        }

        public static Expression Multiply(Expression left, Expression right)
        {
            return FromBinary(Binary.Multiply, left, right);
        }

        public static Expression Divide(Expression left, Expression right)
        {
            return FromBinary(Binary.Divide, left, right);
        }

        public static Expression Power(Expression left, Expression right)
        {
            return FromBinary(Binary.Power, left, right);
        }

        public static Expression Number(double value)
        {
            return new NumberExpression(value);
        }

        /*
        public static Expression Complex(double real, double imaginary)
        {
            return new ComplexExpression(real, imaginary);
        }
        */

        public static Expression Variable(string name)
        {
            return new VariableExpression(name);
        }

        public static Expression Function(string name, IList<Expression> arguments = null)
        {
            if (arguments == null)
                return new FunctionExpression(name);

            return new FunctionExpression(name, arguments);
        }

        public static Expression Function(string name, Expression argument)
        {
            IList<Expression> arguments = new List<Expression>();
            arguments.Add(argument);

            return Function(name, arguments);
        }

        public static Expression TransformFunction(FunctionExpression expr, Func<Expression, Expression> transformation)
        {
            IList<Expression> arguments = new List<Expression>();

            foreach (Expression arg in expr.Arguments)
                arguments.Add(transformation(arg));

            return Function(expr.Name, arguments);
        }

        public static bool Equals(Expression left, Expression right)
        {
            if (!left.GetType().Equals(right.GetType()))
                return false;

            if (left is BinaryExpression)
            {
                BinaryExpression bleft = (BinaryExpression)left;
                BinaryExpression bright = (BinaryExpression)right;

                return bleft.Operator == bright.Operator && Equals(bleft.Left, bright.Left) && Equals(bleft.Right, bright.Right);
            }
            else if (left is LinearExpression)
            {
                LinearExpression lleft = (LinearExpression)left;
                LinearExpression lright = (LinearExpression)right;

                bool eq = lleft.Operator == lright.Operator && lleft.List.Count == lright.List.Count;

                if (eq == false)
                    return false;

                for (int i = 0; i < lleft.List.Count; i++)
                    eq = eq && Equals(lleft[i], lright[i]);

                return eq;
            }
            else if (left is NumberExpression)
                return equalsNumber((NumberExpression)left, (NumberExpression)right);
            else if (left is VariableExpression)
                return equalsVariable((VariableExpression)left, (VariableExpression)right);
            else if (left is FunctionExpression)
                return equalsFunction((FunctionExpression)left, (FunctionExpression)right);

            return false;
        }

        private static bool equalsNumber(NumberExpression left, NumberExpression right)
        {
            return left.Value == right.Value;
        }

        private static bool equalsVariable(VariableExpression left, VariableExpression right)
        {
            return left.Name == right.Name;
        }

        private static bool equalsFunction(FunctionExpression left, FunctionExpression right)
        {
            if (left.Name != right.Name)
                return false;

            if (left.Arguments.Count != right.Arguments.Count)
                return false;

            for (int i = 0; i < left.Arguments.Count; i++)
            {
                if (!Equals(left.Arguments[i], right.Arguments[i]))
                    return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Expression))
                return false;

            return Equals(this, (Expression)obj);
        }

        public bool Contains(Expression expr)
        {
            return Contains(this, expr);
        }

        public Expression Differentiate(string var, IVariableScope variables, IFunctionScope functions, IDifferentiationScope derivatives)
        {
            return Differentiate(this, var, variables, functions, derivatives);
        }

        public Expression Evaluate(IVariableScope variables, IFunctionScope functions)
        {
            return Evaluate(this, variables, functions);
        }

        public Expression Apply(IEnumerable<Type> transformations, IVariableScope variables, IFunctionScope functions)
        {
            return Apply(transformations, this, variables, functions);
        }

        public static Expression Differentiate(Expression expr, string var, IVariableScope variables, IFunctionScope functions, IDifferentiationScope derivatives)
        {
            Expression derivative = (new Differentiation(expr, var, derivatives)).Result;

            return Evaluate(derivative, variables, functions);
        }

        public static Expression Evaluate(Expression expr, IVariableScope variables, IFunctionScope functions)
        {
            return Evaluate(transformations, expr, variables, functions);
        }

        public static Expression Evaluate(IEnumerable<Type> transformations, Expression expr, IVariableScope variables, IFunctionScope functions)
        {
            Expression result = (new EliminateSubtractions(expr)).Result;
            result = (new SimplifyBinary(result, variables, functions)).Result;

            Expression presult = null;

            int count = 0;

            while (!result.Equals(presult) && count < 10)
            {
                presult = result;
                result = Apply(transformations, result, variables, functions);

                count++;
            }

            return (new ReturnSubtractions(result)).Result;
        }

        public static Expression Apply(Expression expr, IVariableScope variables, IFunctionScope functions)
        {
            return Apply(transformations, expr, variables, functions);
        }

        public static Expression Apply(IEnumerable<Type> transformations, Expression expr, IVariableScope variables, IFunctionScope functions)
        {
            Expression result = expr;

            foreach (Type type in transformations)
            {
                result = (new EliminateSubtractions(result)).Result;
                result = (new BinaryToLinear(result)).Result;

                RuleOrientedTransformation transformation = (RuleOrientedTransformation)Activator.CreateInstance(type);

                result = transformation.Transform(result, e => Apply(e, variables, functions));

                result = (new LinearToBinary(result)).Result;
                result = (new SimplifyBinary(result, variables, functions)).Result;
            }

            return result;
        }

        public static bool Contains(Expression expr, Expression sub)
        {
            if (expr.GetType().Equals(sub.GetType()))
                return expr.Equals(sub);

            if (expr is BinaryExpression)
            {
                BinaryExpression bexpr = (BinaryExpression)expr;

                return Contains(bexpr.Left, sub) && Contains(bexpr.Right, sub);
            }
            else if (expr is LinearExpression)
            {
                LinearExpression lexpr = (LinearExpression)expr;

                bool contains = true;

                foreach (Expression item in lexpr.List)
                    contains = contains && Contains(item, sub);

                return contains;
            }
            else if (expr is FunctionExpression)
            {
                FunctionExpression fexpr = (FunctionExpression)expr;

                bool contains = true;

                foreach (Expression arg in fexpr.Arguments)
                    contains = contains && Contains(arg, sub);

                return contains;
            }

            return false;
        }

        public override string ToString()
        {
            IList<Token> tks = (new ToTokens(this)).Result;

            string result = "";

            foreach (Token tk in tks)
            {
                Symbol sym = tk.Type;

                if (sym == Symbol.Number || sym == Symbol.Word)
                    result += tk.Value;
                else
                {
                    bool leftSpace = false;
                    bool rightSpace = false;

                    if (sym == Symbol.Add || sym == Symbol.Subtract || sym == Symbol.Multiply || sym == Symbol.Divide || sym == Symbol.Power)
                    {
                        leftSpace = true;
                        rightSpace = true;
                    }
                    else if (sym == Symbol.Comma)
                        rightSpace = true;

                    if (leftSpace)
                        result += " ";

                    result += Scanner.GetString(sym);

                    if (rightSpace)
                        result += " ";
                }
            }

            return result;
        }
    }
}