namespace MathExpressionParsing.Exceptions;

public class FunctionNotFoundException : Exception
{
    public FunctionNotFoundException(string message) : base(message)
    {
    }
}