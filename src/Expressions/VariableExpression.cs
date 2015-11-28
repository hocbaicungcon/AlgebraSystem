namespace AlgebraSystem.Expressions
{
    public class VariableExpression : Expression
    {
        private string name;

        public string Name
        {
            get { return name; }
        }

        public VariableExpression(string Name)
        {
            this.name = Name;
        }
    }
}
