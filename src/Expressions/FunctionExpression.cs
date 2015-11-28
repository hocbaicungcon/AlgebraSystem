using System.Collections.Generic;

namespace AlgebraSystem.Expressions
{
    public class FunctionExpression : Expression
    {
        private string name;

        private IList<Expression> arguments;

        public string Name
        {
            get { return name; }
        }

        public IList<Expression> Arguments
        {
            get { return arguments; }
        }

        public FunctionExpression(string Name) : this(Name, new List<Expression>()) { }

        public FunctionExpression(string Name, IList<Expression> Arguments)
        {
            this.name = Name;
            this.arguments = Arguments;
        }
    }
}
