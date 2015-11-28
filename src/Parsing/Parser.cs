using System;
using System.Collections.Generic;

namespace AlgebraSystem.Parsing
{
    public class Parser
    {
        private Expression result;

        public Expression Result
        {
            get { return result; }
        }

        public Parser() { }

        public Parser(IList<Token> tokens)
        {
            this.result = Parse(tokens);
        }

        public Parser(string expr)
        {
            this.result = Parse(expr);
        }

        public Expression Parse(string expr)
        {
            Scanner scanner = new Scanner(expr);
            return Parse(scanner.Result);
        }

        public Expression Parse(IList<Token> tokens)
        {
            if (tokens.Count > 1 && tokens[0].Type == Symbol.Subtract)
            {
                if (tokens[1].Type == Symbol.Number)
                {
                    tokens.RemoveAt(0);

                    double num = double.Parse(tokens[0].Value);
                    num *= -1;

                    tokens[0].Value = num.ToString();
                }
                else
                {
                    tokens[0].Type = Symbol.Number;
                    tokens[0].Value = "-1";

                    tokens.Insert(1, new Token(Symbol.Multiply));
                }
            }

            int index = 0;

            return sums(tokens, ref index);
        }

        private Expression sums(IList<Token> tokens, ref int index)
        {
            Expression x = factors(tokens, ref index);
            Expression y;

            while (index < tokens.Count)
            {
                Symbol op = tokens[index].Type;

                if (op != Symbol.Add && op != Symbol.Subtract)
                    return x;

                index++;
                y = factors(tokens, ref index);

                if (op == Symbol.Add)
                    x = Expression.Add(x, y);
                else
                    x = Expression.Subtract(x, y);
            }

            return x;
        }

        private Expression factors(IList<Token> tokens, ref int index)
        {
            Expression x = powers(tokens, ref index);
            Expression y;

            while (index < tokens.Count)
            {
                Symbol op = tokens[index].Type;

                if (op != Symbol.Multiply && op != Symbol.Divide)
                    return x;

                index++;
                y = powers(tokens, ref index);

                if (op == Symbol.Multiply)
                    x = Expression.Multiply(x, y);
                else
                    x = Expression.Divide(x, y);
            }

            return x;
        }

        private Expression powers(IList<Token> tokens, ref int index)
        {
            Expression x = brackets(tokens, ref index);
            Expression y;

            while (index < tokens.Count)
            {
                Symbol op = tokens[index].Type;

                if (op != Symbol.Power)
                    return x;

                index++;
                y = brackets(tokens, ref index);

                x = Expression.Power(x, y);
            }

            return x;
        }

        private Expression brackets(IList<Token> tokens, ref int index)
        {
            if (index < tokens.Count && tokens[index].Type == Symbol.OpenRoundBracket)
            {
                index++;
                IList<Token> inside = new List<Token>();

                int ob = 1;

                while (ob > 0 && index < tokens.Count)
                {
                    if (tokens[index].Type == Symbol.OpenRoundBracket)
                        ob++;
                    else if (tokens[index].Type == Symbol.CloseRoundBracket)
                        ob--;

                    if (ob > 0)
                        inside.Add(tokens[index]);

                    index++;
                }

                if (ob > 0)
                    throw new Exception("Expected closing bracket");

                return Parse(inside);
            }

            return functions(tokens, ref index);
        }

        private Expression functions(IList<Token> tokens, ref int index)
        {
            if (index < tokens.Count - 1 && tokens[index].Type == Symbol.Word && tokens[index + 1].Type == Symbol.OpenRoundBracket)
            {
                string name = tokens[index].Value;
                index += 2;

                IList<Expression> arguments = new List<Expression>();

                int ob = 1;

                IList<Token> current = new List<Token>();

                while (ob > 0 && index < tokens.Count)
                {
                    if (tokens[index].Type == Symbol.OpenRoundBracket)
                        ob++;
                    else if (tokens[index].Type == Symbol.CloseRoundBracket)
                        ob--;

                    if (ob == 0 || (ob == 1 && tokens[index].Type == Symbol.Comma))
                    {
                        arguments.Add(Parse(current));
                        current.Clear();
                    }
                    else
                        current.Add(tokens[index]);

                    index++;
                }

                if (ob > 0)
                    throw new Exception("Expected closing bracket");

                return Expression.Function(name, arguments);
            }

            return variables(tokens, ref index);
        }

        private Expression variables(IList<Token> tokens, ref int index)
        {
            if (index < tokens.Count && tokens[index].Type == Symbol.Word)
            {
                string name = tokens[index].Value;
                index++;

                return Expression.Variable(name);
            }

            return numbers(tokens, ref index);
        }

        private Expression numbers(IList<Token> tokens, ref int index)
        {
            if (index < tokens.Count && tokens[index].Type == Symbol.Number)
            {
                double number = double.Parse(tokens[index].Value);
                index++;

                return Expression.Number(number);
            }

            throw new Exception("Unknown token");
        }
    }
}