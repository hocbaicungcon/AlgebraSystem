using System.Collections.Generic;

namespace AlgebraSystem.Concrete
{
    public class VariableCollection : IVariableScope
    {
        private IDictionary<string, Expression> vars;

        public VariableCollection()
        {
            vars = new Dictionary<string, Expression>();

            Add("pi", System.Math.PI);
            Add("e", System.Math.E);
            //Add("i", Expression.Complex(0, 1));
        }

        public bool Exists(string name)
        {
            return vars.ContainsKey(name);
        }

        public Expression Get(string name)
        {
            return vars[name];
        }

        public void Add(string name, Expression expr)
        {
            vars.Add(name, expr);
        }

        public void Add(string name, double value)
        {
            vars.Add(name, Expression.Number(value));
        }
    }
}