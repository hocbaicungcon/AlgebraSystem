namespace AlgebraSystem
{
    public interface IVariableScope
    {
        bool Exists(string name);

        Expression Get(string name);
    }
}
