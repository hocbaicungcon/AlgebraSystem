using System.Reflection;

namespace AlgebraSystem.Parsing
{
    public interface ILinqMethodScope
    {
        string Get(MethodInfo method);

        bool Exists(MethodInfo method);
    }
}
