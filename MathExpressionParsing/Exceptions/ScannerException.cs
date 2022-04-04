namespace MathExpressionParsing.Exceptions;

public class ScannerException : Exception
{
    private int Line { get; }
    public ScannerException(int line, string message) : base(message)
    {
        this.Line = line;
    }
}