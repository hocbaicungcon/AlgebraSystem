using System;
using System.Collections.Generic;

namespace AlgebraSystem
{
    public interface IRuleScope
    {
        IList<Rule> GetRules();

        IList<Rule> GetRules(Func<Rule, bool> predicate);
    }
}