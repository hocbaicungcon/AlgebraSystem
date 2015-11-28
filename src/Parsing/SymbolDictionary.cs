using System.Collections.Generic;

namespace AlgebraSystem.Parsing
{
    public class SymbolDictionary : Dictionary<string, Symbol>
    {
        public SymbolDictionary()
        {
            Add("+", Symbol.Add);
            Add("-", Symbol.Subtract);
            Add("*", Symbol.Multiply);
            Add("/", Symbol.Divide);
            Add("^", Symbol.Power);
            Add("(", Symbol.OpenRoundBracket);
            Add(")", Symbol.CloseRoundBracket);
            Add(",", Symbol.Comma);
            Add(".", Symbol.Dot);
        }
    }
}