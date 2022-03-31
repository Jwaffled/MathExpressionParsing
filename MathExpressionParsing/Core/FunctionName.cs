namespace MathExpressionParsing.Core;

public class FunctionName : Attribute
{
    public string Name { get; }
    public int? ArgumentLength { get; }

    public FunctionName(string name, int argLength)
    {
        this.ArgumentLength = argLength;
        this.Name = name;
    }

    public FunctionName(string name)
    {
        this.ArgumentLength = null;
        this.Name = name;
    }
}