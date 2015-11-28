using System.Collections.Generic;

namespace AlgebraSystem.Math.Calculus
{
    public interface IDifferentiationScope
    {
        Expression Differentiate(string name, Expression argument);

        Expression Differentiate(string name, IList<Expression> arguments, int argumentPosition, int count);
    }
}