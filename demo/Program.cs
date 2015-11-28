using AlgebraSystem.Concrete;
using System;

namespace AlgebraSystem.demo
{
    class Program
    {
        static FunctionCollection functions = new FunctionCollection();
        static MethodCollection methods = new MethodCollection();
        static VariableCollection variables = new VariableCollection();

        public static void Main()
        {
            solve("(x-y)/x");
            solve("(x^n)/(x^m)");
            solve("(x^n)/(x^(n-1))");
            solve("8 - 3 * (x + 5) + 4*x -2");
            solve("D(sin(x), x)");
            solve("D(cos(x), x)");
            solve("D(log(x)*cos(2*x), x)");
        }

        static void solve(string exprString)
        {
            Expression expr = Expression.FromString(exprString);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(exprString + " => ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Expression.Evaluate(expr, variables, functions));
        }
    }
}
