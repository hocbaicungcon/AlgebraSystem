using AlgebraSystem.Expressions;
using System;
using System.Collections.Generic;

namespace AlgebraSystem.Parsing.Reverse
{
    public class ToTokens
    {
        private IList<Token> result;

        public IList<Token> Result
        {
            get { return result; }
        }

        public ToTokens() { }

        public ToTokens(Expression expr)
        {
            this.result = Reverse(expr);
        }

        public IList<Token> Reverse(Expression expr)
        {
            return Reverse(expr, -1);
        }

        public IList<Token> Reverse(Expression expr, int top = -1)
        {
            IList<Token> tks = new List<Token>();

            if (expr is BinaryExpression)
            {
                BinaryExpression bexpr = (BinaryExpression)expr;

                IList<Token> leftTokens = Reverse(bexpr.Left, opToNum(bexpr.Operator));
                IList<Token> rightTokens = Reverse(bexpr.Right, opToNum(bexpr.Operator));

                bool brackets = false;

                if (top > opToNum(bexpr.Operator) || (bexpr.Operator == Binary.Divide && top > 0))
                    brackets = true;

                if (brackets)
                    tks.Add(new Token(Symbol.OpenRoundBracket));

                addMany(tks, leftTokens);
                tks.Add(new Token(opToSym(bexpr.Operator)));
                addMany(tks, rightTokens);

                if (brackets)
                    tks.Add(new Token(Symbol.CloseRoundBracket));
            }
            else if (expr is LinearExpression) // Mainly for debugging
            {
                LinearExpression lexpr = (LinearExpression)expr;

                bool brackets = false;

                if (top > opToNum(lexpr.Operator))
                    brackets = true;

                if (brackets)
                    tks.Add(new Token(Symbol.OpenRoundBracket));

                for (int i = 0; i < lexpr.List.Count; i++)
                {
                    if (i > 0)
                        tks.Add(new Token(opToSym(lexpr.Operator)));

                    IList<Token> subTks = Reverse(lexpr[i], opToNum(lexpr.Operator));
                    addMany(tks, subTks);
                }

                if (brackets)
                    tks.Add(new Token(Symbol.CloseRoundBracket));
            }
            else if (expr is NumberExpression)
            {
                NumberExpression nexpr = (NumberExpression)expr;
                double value = nexpr.Value;

                if (value < 0)
                    tks.Add(new Token(Symbol.OpenRoundBracket));

                tks.Add(new Token(Symbol.Number, value.ToString()));

                if (value < 0)
                    tks.Add(new Token(Symbol.CloseRoundBracket));
            }
            else if (expr is VariableExpression)
            {
                VariableExpression vexpr = (VariableExpression)expr;
                string name = vexpr.Name;

                tks.Add(new Token(Symbol.Word, name));
            }
            else if (expr is FunctionExpression)
            {
                FunctionExpression fexpr = (FunctionExpression)expr;
                string name = fexpr.Name;

                tks.Add(new Token(Symbol.Word, name));
                tks.Add(new Token(Symbol.OpenRoundBracket));

                for (int i = 0; i < fexpr.Arguments.Count; i++)
                {
                    if (i > 0)
                        tks.Add(new Token(Symbol.Comma));

                    IList<Token> argTks = Reverse(fexpr.Arguments[i]);
                    addMany(tks, argTks);
                }

                tks.Add(new Token(Symbol.CloseRoundBracket));
            }

            return tks;
        }

        private Symbol opToSym(Binary op)
        {
            switch (op)
            {
                case Binary.Add:
                    return Symbol.Add;
                case Binary.Subtract:
                    return Symbol.Subtract;
                case Binary.Multiply:
                    return Symbol.Multiply;
                case Binary.Divide:
                    return Symbol.Divide;
                case Binary.Power:
                    return Symbol.Power;
            }

            throw new Exception("Can't convert operator");
        }

        private int opToNum(Binary op)
        {
            switch (op)
            {
                case Binary.Add:
                case Binary.Subtract:
                    return 0;

                case Binary.Multiply:
                    return 1;

                case Binary.Power:
                    return 2;

                case Binary.Divide:
                    return 3;
            }

            return -1;
        }

        private void addMany(IList<Token> tks, IList<Token> toAdd)
        {
            foreach (Token tk in toAdd)
                tks.Add(tk);
        }
    }
}