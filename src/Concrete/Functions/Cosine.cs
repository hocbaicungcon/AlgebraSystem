using System;
using System.Collections.Generic;

namespace AlgebraSystem.Concrete.Functions
{
    public class Cosine : Function
    {
        public static readonly string Alias = "cos";

        public Cosine() : base(Alias) { }

        public override bool Accepts(int arguments)
        {
            return arguments == 1;
        }

        public override double Calculate(IList<double> arguments)
        {
            double x = arguments[0];
            return System.Math.Cos(x);
        }

        public override Expression Differentiate(Expression argument)
        {
            return Expression.Multiply(Expression.Number(-1), Expression.Function(Sine.Alias, argument));
        }

        public override Expression Differentiate(IList<Expression> arguments, int position, int count)
        {
            throw new NotImplementedException();
        }

        public override Expression Evaluate(IList<Expression> arguments)
        {
            return Expression.Function(Alias, arguments);
        }
    }
}