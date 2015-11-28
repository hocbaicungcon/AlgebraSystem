using AlgebraSystem.Expressions;
using System;
using System.Collections.Generic;

namespace AlgebraSystem.Concrete.Functions.Internal
{
    public class Derivative : Function
    {
        public static readonly string Alias = "D";

        public Derivative() : base(Alias) { }

        public override bool Accepts(int arguments)
        {
            return arguments == 2;
        }

        public override double Calculate(IList<double> arguments)
        {
            throw new NotImplementedException();
        }

        public override Expression Differentiate(Expression argument)
        {
            throw new NotImplementedException();
        }

        public override Expression Differentiate(IList<Expression> arguments, int position, int count)
        {
            throw new NotImplementedException();
        }

        public override Expression Evaluate(IList<Expression> arguments)
        {
            Expression function = arguments[0];
            Expression byVarExpr = arguments[1];

            if (!(byVarExpr is VariableExpression))
                throw new Exception("Second arguments of a derivative must be a variable");

            string byVar = ((VariableExpression)byVarExpr).Name;

            return Expression.Differentiate(function, byVar, null, collection, collection);
        }
    }
}