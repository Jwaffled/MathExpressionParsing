namespace MathExpressionParsing.Core;

public enum TokenType
{
    // Tokens
    LeftParen,
    RightParen,
    Comma,

    // Mathematical operators
    Plus,
    Minus,
    Star,
    Slash,
    Carrot,
    Factorial,
    
    // Special cases
    Identifier,
    Number,
    // End of input
    Eof
}