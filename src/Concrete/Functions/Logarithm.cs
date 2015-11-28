using System;
using System.Collections.Generic;

namespace AlgebraSystem.Concrete.Functions
{
    public class Logarithm : Function
    {
        public static readonly string Alias = "log";

        public Logarithm() : base(Alias) { }

        public override bool Accepts(int arguments)
        {
            return arguments == 1 || arguments == 2;
        }

        public override double Calculate(IList<double> arguments)
        {
            double b;
            double x;

            if (arguments.Count == 1)
            {
                x = arguments[0];
                return System.Math.Log(x);
            }

            b = arguments[0];
            x = arguments[1];

            return System.Math.Log(x, b);
        }

        public override Expression Differentiate(Expression argument)
        {
            return Expression.Divide(Expression.Number(1), argument);
        }

        public override Expression Differentiate(IList<Expression> arguments, int position, int count)
        {
            if (count != 2)
                throw new Exception("Logarithm function can't take more than 2 arguments");

            Expression b = arguments[0];
            Expression x = arguments[1];

            if (position == 1)
                return Expression.Divide(Expression.Number(1), Expression.Multiply(x, Expression.Function(Alias, b)));

            return Expression.Divide(Expression.Multiply(Expression.Number(-1), Expression.Function(Alias, b)), Expression.Multiply(x, Expression.Power(Expression.Function(Alias, x), Expression.Number(2))));
        }

        public override Expression Evaluate(IList<Expression> arguments)
        {
            return Expression.Function(Alias, arguments);
        }
    }
}