using System;
using AlgebraSystem.Expressions;

namespace AlgebraSystem.Concrete.Rules.Algebra
{
    class Exponential : RuleCollection
    {
        public Exponential()
        {
            Add("x * x", "x^2");

            Add("x * x^n", "x ^ (n + 1)");

            Add("(x^n) * (x^m)", "x ^ (n + m)");

            Add("x / x^n", "1 / x ^ (n - 1)");

            Add("x^n / x", "x ^ (n - 1)");

            Add("(x^n) / (x^m)", "x ^ (n - m)");

            //Add("1 / (x^n)", "x^(-n)", vars => vars["n"] is NumberExpression && ((NumberExpression)vars["n"]).Value < 0);

            Add("x^n", "1 / (x^(-n))", vars => vars["n"] is NumberExpression && ((NumberExpression)vars["n"]).Value < 0);

            Add("(x^a)^b", "x^(a * b)");

            //Add("x^n", "sqrt(x) * x^(n - 0.5)", vars => vars["n"] is NumberExpression && isInteger(((NumberExpression)vars["n"]).Value - 0.5)); 
        }

        private bool isInteger(double v)
        {
            return System.Math.Floor(v) == v;
        }
    }
}
