using MathExpressionParsing.Core;

namespace MathExpressionParsing.Exceptions;

public class ParserException : Exception
{
    public Token Token { get; }
    public ParserException(Token token, string message) : base(message)
    {
        this.Token = token;
    }
}