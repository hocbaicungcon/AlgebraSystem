using System.Reflection;
using AlgebraSystem.Parsing;
using AlgebraSystem.Parsing.Reverse;
using System.Collections.Generic;
using System.Linq;

namespace AlgebraSystem.Concrete
{
    public class MethodCollection : ILinqMethodScope, ILinqMethodReverseScope
    {
        private IDictionary<string, MethodInfo> methods;

        public MethodCollection()
        {
            methods = new Dictionary<string, MethodInfo>();

            AddFromMathClass("sin", "Sin");
            AddFromMathClass("cos", "Cos");
            AddFromMathClass("tan", "Tan");
            //AddFromMathClass("log", "Log");
            AddFromMathClass("sqrt", "Sqrt");
        }

        public bool Exists(string name)
        {
            return methods.ContainsKey(name);
        }

        public bool Exists(MethodInfo method)
        {
            return methods.Where(p => p.Value.Equals(method)).Count() > 0;
        }

        public MethodInfo Get(string name)
        {
            return methods[name];
        }

        public string Get(MethodInfo method)
        {
            return methods.Where(p => p.Value.Equals(method)).First().Key;
        }

        public void Add(string name, MethodInfo method)
        {
            methods.Add(name, method);
        }

        public void AddFromMathClass(string name, string nameInMath)
        {
            methods.Add(name, typeof(System.Math).GetMethod(nameInMath));
        }
    }
}