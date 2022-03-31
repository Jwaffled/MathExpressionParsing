namespace MathExpressionParsing.Core;

public class Token
{
    public TokenType Type { get; }
    public string Lexeme { get; }
    public object? Literal { get; }
    public int Line { get; }

    public Token(TokenType type, string lexeme, object? literal, int line)
    {
        this.Type = type;
        this.Lexeme = lexeme;
        this.Literal = literal;
        this.Line = line;
    }

    public override string ToString()
    {
        return $"{Type} {Lexeme} {Literal}";
    }

    public override bool Equals(object? obj)
    {
        if (obj?.GetType() == typeof(Token))
        {
            var o = (Token) obj;
            if (o.Literal is not null)
            {
                return o.Type == Type && o.Lexeme.Equals(Lexeme) && o.Line == Line && o.Literal.Equals(Literal);
            }
            return o.Type == Type && o.Lexeme.Equals(Lexeme) && o.Line == Line && o.Literal == Literal;
        }

        return false;
    }
}