using System;
using System.Collections.Generic;

namespace AlgebraSystem.Concrete.Functions
{
    public class SquareRoot : Function
    {
        public static readonly string Alias = "sqrt";

        public SquareRoot() : base(Alias) { }

        public override bool Accepts(int arguments)
        {
            return arguments == 1;
        }

        public override double Calculate(IList<double> arguments)
        {
            double arg = arguments[0];

            return System.Math.Sqrt(arg);
        }

        public override Expression Differentiate(Expression argument)
        {
            return Expression.Divide(Expression.Number(1), Expression.Multiply(Expression.Number(2), Expression.Function(Alias, argument)));
        }

        public override Expression Differentiate(IList<Expression> arguments, int position, int count)
        {
            throw new NotImplementedException();
        }

        public override Expression Evaluate(IList<Expression> arguments)
        {
            Expression arg = arguments[0];

            return Expression.Power(arg, Expression.Number(0.5));
        }
    }
}
