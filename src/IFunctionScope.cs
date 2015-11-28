using AlgebraSystem.Expressions;
using System.Collections.Generic;

namespace AlgebraSystem
{
    public interface IFunctionScope
    {
        bool Exists(string name);

        bool Accepts(string name, int count);

        double Calculate(string name, IList<double> arguments);

        Expression Evaluate(string name, IList<Expression> arguments);
    }
}
