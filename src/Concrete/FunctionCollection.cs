using AlgebraSystem.Concrete.Functions;
using AlgebraSystem.Concrete.Functions.Internal;
using AlgebraSystem.Math.Calculus;
using System.Collections.Generic;

namespace AlgebraSystem.Concrete
{
    public class FunctionCollection : IFunctionScope, IDifferentiationScope
    {
        private IDictionary<string, Function> functions;

        public FunctionCollection()
        {
            functions = new Dictionary<string, Function>();

            Add(new Derivative());
            Add(new Equation());

            Add(new Sine());
            Add(new Cosine());
            Add(new Logarithm());
            Add(new SquareRoot());
        }

        public bool Accepts(string name, int count)
        {
            return functions[name].Accepts(count);
        }

        public double Calculate(string name, IList<double> arguments)
        {
            return functions[name].Calculate(arguments);
        }

        public Expression Evaluate(string name, IList<Expression> arguments)
        {
            return functions[name].Evaluate(arguments);
        }

        public Expression Differentiate(string name, Expression argument)
        {
            return functions[name].Differentiate(argument);
        }

        public Expression Differentiate(string name, IList<Expression> arguments, int position, int count)
        {
            return functions[name].Differentiate(arguments, position, count);
        }

        public bool Exists(string name)
        {
            return functions.ContainsKey(name);
        }

        public void Add(Function function)
        {
            function.SetCollection(this);
            functions.Add(function.Name, function);
        }
    }
}