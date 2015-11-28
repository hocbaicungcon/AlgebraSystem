using AlgebraSystem.Expressions;
using System;
using System.Collections.Generic;

namespace AlgebraSystem.Math
{
    public class RuleOrientedTransformation
    {
        private IRuleScope rules;

        private Expression result;
        
        public Expression Result
        {
            get { return result; }
        }

        public RuleOrientedTransformation(IRuleScope rules = null)
        {
            this.rules = rules;
        }

        //public RuleOrientedTransformation(Expression expr, IRuleScope rules = null) : this(rules)
        //{
        //    this.result = Transform(expr);
        //}

        public Expression Transform(Expression expr, Func<Expression, Expression> subTransform)
        {
            if (expr is LinearExpression)
            {
                LinearExpression lexpr = (LinearExpression)expr;
                IList<Rule> exprRules = rules.GetRules(r => r.Pattern is BinaryExpression && ((BinaryExpression)r.Pattern).Operator == lexpr.Operator);

                IList<Expression> list = new List<Expression>();

                foreach (Expression item in lexpr.List)
                    list.Add(subTransform(item));
                    //list.Add(new LinearToBinary(Transform(item)).Result);

                IList<Expression> rlist = new List<Expression>();

                for (int i = 0; i < list.Count; i++)
                {
                    Expression left = list[i];
                    Expression right = null;

                    Expression result = null;

                    int j = i + 1;

                    for (; j < list.Count; j++)
                    {
                        right = list[j];

                        Expression optionA = Expression.FromBinary(lexpr.Operator, left, right);
                        Expression optionB = Expression.FromBinary(lexpr.Operator, right, left);

                        foreach (Rule rule in exprRules)
                        {
                            result = rule.Apply(optionA);

                            if (result == null)
                                result = rule.Apply(optionB);

                            if (result != null)
                                break;
                        }

                        if (result != null)
                            break;
                    }

                    if (result != null)
                    {
                        list.RemoveAt(j);
                        list.RemoveAt(i);

                        list.Insert(i, result);
                        i--;
                    }
                }

                return new LinearExpression(lexpr.Operator, list);
            }
            else if (expr is BinaryExpression)
            {
                BinaryExpression bexpr = (BinaryExpression)expr;

                Expression left = subTransform(bexpr.Left);
                Expression right = subTransform(bexpr.Right);
                //Expression left = (new LinearToBinary(Transform(bexpr.Left))).Result;
                //Expression right = (new LinearToBinary(Transform(bexpr.Right))).Result;

                Expression full = Expression.FromBinary(bexpr.Operator, left, right);

                IList<Rule> exprRules = rules.GetRules(r => r.Pattern is BinaryExpression && ((BinaryExpression)r.Pattern).Operator == bexpr.Operator);

                foreach (Rule rule in exprRules)
                {
                    Expression result = rule.Apply(full);

                    if (result != null)
                        return result;
                }

                return full;
            }
            else if (expr is FunctionExpression)
            {
                FunctionExpression fexpr = (FunctionExpression)expr;

                IList<Expression> args = new List<Expression>();

                foreach (Expression arg in fexpr.Arguments)
                    args.Add(subTransform(arg));

                FunctionExpression full = (FunctionExpression)Expression.Function(fexpr.Name, args);

                IList<Rule> exprRules = rules.GetRules(r => r.Pattern is FunctionExpression && ((FunctionExpression)r.Pattern).Name == fexpr.Name && ((FunctionExpression)r.Pattern).Arguments.Count == fexpr.Arguments.Count);

                foreach (Rule rule in exprRules)
                {
                    Expression result = rule.Apply(full);

                    if (result != null)
                        return result;
                }

                return full;
            }

            return expr;
        }
    }
}