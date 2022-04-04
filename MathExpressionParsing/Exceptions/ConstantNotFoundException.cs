namespace MathExpressionParsing.Exceptions;

public class ConstantNotFoundException : Exception
{
    public string Name { get; }

    public ConstantNotFoundException(string name, string message) : base(message)
    {
        this.Name = name;
    }
}