namespace MathExpressionParsing.Core;

public class MathConstant : Attribute
{
    public string Name { get; }

    public MathConstant(string name)
    {
        this.Name = name;
    }
}