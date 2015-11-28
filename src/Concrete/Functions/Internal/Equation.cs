using AlgebraSystem.Expressions;
using AlgebraSystem.Math.Transformations.Equation;
using System;
using System.Collections.Generic;

namespace AlgebraSystem.Concrete.Functions.Internal
{
    public class Equation : Function
    {
        public static readonly IEnumerable<Type> equations = new Type[]
        {
            typeof(Linear)
        };

        public static readonly string Alias = "E";

        public Equation() : base(Alias) { }

        public override bool Accepts(int arguments)
        {
            return arguments == 2 || arguments == 3;
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
            if (arguments.Count == 2)
            {
                Expression fullExpression = Expression.Function(Alias, arguments);
                return Expression.Apply(equations, fullExpression, null, collection);
            }
            else
            {
                IList<Expression> args = new List<Expression>();

                args.Add(arguments[0]);
                args.Add(arguments[1]);

                Expression fullExpression = Expression.Function(Alias, args);
                Expression solution = Expression.Apply(equations, fullExpression, null, collection);

                if (solution is FunctionExpression && ((FunctionExpression)solution).Name == "Array")
                {
                    Expression indexExpr = arguments[2];
                    FunctionExpression fsolution = (FunctionExpression)solution;

                    return fsolution.Arguments[(int)((NumberExpression)indexExpr).Value];
                }
                else
                    return solution;
            }
        }
    }
}