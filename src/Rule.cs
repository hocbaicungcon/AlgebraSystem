using AlgebraSystem.Expressions;
using AlgebraSystem.Math.Reduction;
using System;
using System.Collections.Generic;

namespace AlgebraSystem
{
    public class Rule
    {
        private Expression pattern;

        private Expression replacement;

        private Func<IDictionary<string, Expression>, bool> conditions;

        private IDictionary<string, Expression> matches;

        public Expression Pattern
        {
            get { return pattern; }
        }

        public Expression Replacement
        {
            get { return replacement; }
        }

        public Func<IDictionary<string, Expression>, bool> Conditions
        {
            get { return conditions; }
        }

        public Rule(Expression pattern, Expression replacement) : this(pattern, replacement, d => true) { }

        public Rule(Expression pattern, Expression replacement, Func<IDictionary<string, Expression>, bool> conditions)
        {
            this.pattern = pattern;
            this.replacement = replacement;

            this.conditions = conditions;

            matches = new Dictionary<string, Expression>();
        }

        public Expression Apply(Expression expr)
        {
            matches.Clear();
            bool matching = match(expr);

            if (matching == false)
                return null;

            Expression result = applyMatches((new EliminateSubtractions(replacement)).Result, matches);

            return result;
        }

        private bool match(Expression expr)
        {
            matches.Clear();
            bool matching = match(expr, pattern);

            return matching && conditions(matches);
        }

        private bool match(Expression expr, Expression pattern)
        {
            if (!(pattern is VariableExpression))
            {
                if (!expr.GetType().Equals(pattern.GetType()))
                    return false;

                if (pattern is BinaryExpression)
                {
                    BinaryExpression bexpr = (BinaryExpression)expr;
                    BinaryExpression bpattern = (BinaryExpression)pattern;

                    if (bexpr.Operator != bpattern.Operator)
                        return false;

                    return match(bexpr.Left, bpattern.Left) && match(bexpr.Right, bpattern.Right);
                }
                else if (pattern is FunctionExpression)
                {
                    FunctionExpression fexpr = (FunctionExpression)expr;
                    FunctionExpression fpattern = (FunctionExpression)pattern;

                    if (fexpr.Name != fpattern.Name || fexpr.Arguments.Count != fpattern.Arguments.Count)
                        return false;

                    bool matching = true;

                    for (int i = 0; i < fexpr.Arguments.Count; i++)
                        matching = matching && match(fexpr.Arguments[i], fpattern.Arguments[i]);

                    return matching;
                }

                return expr.Equals(pattern);
            }
            else
            {
                VariableExpression vpattern = (VariableExpression)pattern;
                string name = vpattern.Name;

                if (matches.ContainsKey(name))
                {
                    Expression fmatch = matches[name];

                    if (!expr.Equals(fmatch))
                        return false;
                }

                matches[name] = expr;
                return true;
            }
        }

        private Expression applyMatches(Expression expr, IDictionary<string, Expression> matches)
        {
            if (expr is VariableExpression)
            {
                VariableExpression vexpr = (VariableExpression)expr;

                if (matches.ContainsKey(vexpr.Name))
                    return matches[vexpr.Name];

                return vexpr;
            }
            else if (expr is FunctionExpression)
            {
                FunctionExpression fexpr = (FunctionExpression)expr;

                IList<Expression> args = new List<Expression>();

                foreach (Expression arg in fexpr.Arguments)
                    args.Add(applyMatches(arg, matches));

                return Expression.Function(fexpr.Name, args);
            }
            else if (expr is BinaryExpression)
            {
                BinaryExpression bexpr = (BinaryExpression)expr;

                Expression left = applyMatches(bexpr.Left, matches);
                Expression right = applyMatches(bexpr.Right, matches);

                /*
                if (bexpr.Operator == Binary.Subtract)
                {
                    if (right is NumberExpression)
                    {
                        NumberExpression nright = (NumberExpression)right;
                        right = Expression.Number(-nright.Value);
                    }
                    else
                        right = Expression.Multiply(Expression.Number(-1), right);

                    return Expression.Add(left, right);
                }
                */

                return Expression.FromBinary(bexpr.Operator, left, right);
            }

            return expr;
        }
    }
}