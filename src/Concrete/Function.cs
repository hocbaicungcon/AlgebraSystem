using System.Collections.Generic;

namespace AlgebraSystem.Concrete
{
    public abstract class Function
    {
        private string name;

        protected FunctionCollection collection;

        public string Name
        {
            get { return name; }
        }

        public Function(string name)
        {
            this.name = name;
        }

        public void SetCollection(FunctionCollection collection)
        {
            this.collection = collection;
        }

        public abstract bool Accepts(int arguments);

        public abstract double Calculate(IList<double> arguments);

        public abstract Expression Evaluate(IList<Expression> arguments);

        public abstract Expression Differentiate(Expression argument);

        public abstract Expression Differentiate(IList<Expression> arguments, int position, int count);
    }
}