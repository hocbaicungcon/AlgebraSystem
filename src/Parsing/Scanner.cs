using System.Collections.Generic;
using System.Linq;

namespace AlgebraSystem.Parsing
{
    public class Scanner
    {
        public static readonly IDictionary<string, Symbol> symbols = new SymbolDictionary();

        private IList<Token> result;

        public IList<Token> Result
        {
            get { return result; }
        }

        public Scanner() { }

        public Scanner(string Contents)
        {
            this.result = Parse(Contents);
        }

        public IList<Token> Parse(string Contents)
        {
            Contents += "\n";
            int mode = 0;

            string tk = "";
            IList<Token> tks = new List<Token>();

            bool last_token_valid = false;
            bool token_valid = false;

            foreach (char ch in Contents)
            {
                if (ch == '"')
                {
                    if (mode != 5)
                    {
                        tk = "";
                        mode = 5;
                    }
                    else
                    {
                        addToken(tks, tk, mode);
                        mode = 0;
                    }

                    continue;
                }

                if (mode == 3)
                {
                    last_token_valid = token_valid;
                    token_valid = false;
                }

                if (mode != 5)
                {
                    if (mode != 1 && mode != 2 && isWordNumber(ch))
                    {
                        addToken(tks, tk, mode);
                        tk = "";

                        if (isWord(ch))
                            mode = 1;
                        else
                            mode = 2;
                    }
                    else if (mode != 3 && isSymbol(ch))
                    {
                        if (mode != 2 || ch != '.')
                        {
                            addToken(tks, tk, mode);
                            tk = "";

                            last_token_valid = false;
                            token_valid = false;
                            mode = 3;
                        }
                    }
                    else if (isSpaceBreak(ch))
                    {
                        if (mode != 4)
                        {
                            addToken(tks, tk, mode);
                            mode = 4;
                        }

                        continue;
                    }
                }

                tk += ch;

                if (mode == 3)
                {
                    if (getSymbol(tk) != Symbol.NULL)
                        token_valid = true;

                    if (token_valid == false && last_token_valid == true)
                    {
                        addToken(tks, tk.Substring(0, tk.Length - 1), mode);

                        tk = ch.ToString();
                        token_valid = true;
                    }
                }
            }

            return tks;
        }

        private void addToken(IList<Token> tks, string tk, int mode)
        {
            if (mode == 1)
                tks.Add(new Token(Symbol.Word, tk));
            else if (mode == 2)
                tks.Add(new Token(Symbol.Number, tk));
            else if (mode == 3)
                tks.Add(new Token(getSymbol(tk)));
            else if (mode == 5)
                tks.Add(new Token(Symbol.String, tk));
        }

        private bool isWordNumber(char ch)
        {
            return ch == '_' || char.IsDigit(ch) || char.IsLetter(ch);
        }

        private bool isWord(char ch)
        {
            return char.IsLetter(ch) || ch == '_';
        }

        private bool isSpaceBreak(char ch)
        {
            return ch == '\n' || ch == ' ' || ch == '\t';
        }

        private bool isSymbol(char ch)
        {
            return !isWordNumber(ch) && !isSpaceBreak(ch);
        }

        private Symbol getSymbol(string str)
        {
            return GetSymbol(str);
        }

        public static Symbol GetSymbol(string str)
        {
            if (symbols.ContainsKey(str))
                return symbols[str];

            return Symbol.NULL;
        }

        public static string GetString(Symbol sym)
        {
            KeyValuePair<string, Symbol> pair = symbols.Where(p => p.Value == sym).FirstOrDefault();

            return pair.Key;
        }
    }
}