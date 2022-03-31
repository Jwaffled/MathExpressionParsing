namespace MathExpressionParsing.Core;

public class FunctionNotFoundException : Exception
{
    public FunctionNotFoundException(string message) : base(message)
    {
    }
}