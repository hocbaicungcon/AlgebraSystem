using AlgebraSystem.Expressions;
using AlgebraSystem.Math.Reduction;
using System;

namespace AlgebraSystem
{
    public class Calculator
    {
        private IVariableScope variables;

        private IFunctionScope functions;

        private double result;

        private SimplifyBinary simplifier;

        public double Result
        {
            get { return result; }
        }

        public Calculator(IVariableScope variables = null, IFunctionScope functions = null)
        {
            this.variables = variables;
            this.functions = functions;

            this.simplifier = new SimplifyBinary(variables, functions);
        }

        public Calculator(Expression expr, IVariableScope variables = null, IFunctionScope functions = null) : this(variables, functions)
        {
            this.result = Calculate(expr);
        }

        public double Calculate(Expression expr)
        {
            Expression simplified = simplifier.Simplify(expr);

            if (!(simplified is NumberExpression))
                throw new Exception("Expression can't be calculated");

            return ((NumberExpression)simplified).Value;
        }
    }
}