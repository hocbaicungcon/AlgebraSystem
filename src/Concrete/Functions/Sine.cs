using System;
using System.Collections.Generic;

namespace AlgebraSystem.Concrete.Functions
{
    public class Sine : Function
    {
        public static readonly string Alias = "sin";

        public Sine() : base(Alias) { }

        public override bool Accepts(int arguments)
        {
            return arguments == 1;
        }

        public override double Calculate(IList<double> arguments)
        {
            double x = arguments[0];
            return System.Math.Sin(x);
        }

        public override Expression Differentiate(Expression argument)
        {
            return Expression.Function(Cosine.Alias, argument);
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