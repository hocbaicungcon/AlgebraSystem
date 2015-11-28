namespace AlgebraSystem.Parsing
{
    public class Token
    {
        public Symbol Type { get; set; }

        public string Value { get; set; }

        public Token() { }

        public Token(Symbol Type) : this(Type, null) { }

        public Token(Symbol Type, string Value)
        {
            this.Type = Type;
            this.Value = Value;
        }
    }
}