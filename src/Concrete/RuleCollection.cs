using AlgebraSystem.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;

using Linq = System.Linq.Expressions;

namespace AlgebraSystem.Concrete
{
    public class RuleCollection : IRuleScope
    {
        private LinqParser linqParser;

        private Parser parser;

        private IList<Rule> rules;

        public RuleCollection(ILinqMethodScope linqMethods = null)
        {
            linqParser = new LinqParser(linqMethods);
            parser = new Parser();

            rules = new List<Rule>();
        }

        public IList<Rule> GetRules()
        {
            return rules;
        }

        public IList<Rule> GetRules(Func<Rule, bool> predicate)
        {
            return rules.Where(predicate).ToList();
        }

        public void Add(Expression pattern, Expression replacement)
        {
            rules.Add(new Rule(pattern, replacement));
        }

        public void Add(Linq.Expression pattern, Linq.Expression replacement)
        {
            Add(linqParser.Parse(pattern), linqParser.Parse(replacement));
        }

        public void Add(string pattern, string replacement)
        {
            Add(parser.Parse(pattern), parser.Parse(replacement));
        }

        public void Add(Expression pattern, Expression replacement, Func<IDictionary<string, Expression>, bool> conditions)
        {
            rules.Add(new Rule(pattern, replacement, conditions));
        }

        public void Add(Linq.Expression pattern, Linq.Expression replacement, Func<IDictionary<string, Expression>, bool> conditions)
        {
            Add(linqParser.Parse(pattern), linqParser.Parse(replacement), conditions);
        }

        public void Add(string pattern, string replacement, Func<IDictionary<string, Expression>, bool> conditions)
        {
            Add(parser.Parse(pattern), parser.Parse(replacement), conditions);
        }
    }
}
