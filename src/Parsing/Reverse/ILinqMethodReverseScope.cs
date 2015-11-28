using System.Reflection;

namespace AlgebraSystem.Parsing.Reverse
{
    public interface ILinqMethodReverseScope
    {
        bool Exists(string name);

        MethodInfo Get(string name);
    }
}
