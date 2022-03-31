namespace MathExpressionParsing.Core;

public class Parser
{
    private List<Token> _tokens;
    private int _current = 0;

    public Parser()
    {
        this._tokens = new List<Token>()
        {
            new Token(TokenType.Eof, "", null, 1)
        };
    }
    public Parser(List<Token> tokens)
    {
        this._tokens = tokens;
    }

    public void SetTokens(List<Token> tokens)
    {
        _tokens = tokens;
    }

    public List<Expression> Parse()
    {
        var list = new List<Expression>();
        while (!IsAtEnd())
        {
            list.Add(ParseExpression());
        }

        return list;
    }

    private Expression ParseExpression()
    {
        return Term();
    }

    private Expression Term()
    {
        var expr = Factor();

        while (Match(TokenType.Minus, TokenType.Plus))
        {
            var op = Previous();
            var right = Factor();
            expr = new ExpressionBinary(expr, op, right);
        }

        return expr;
    }

    private Expression Factor()
    {
        var expr = Power();

        while (Match(TokenType.Star, TokenType.Slash))
        {
            var op = Previous();
            var right = Power();
            expr = new ExpressionBinary(expr, op, right);
        }

        return expr;
    }

    private Expression Power()
    {
        var expr = Primary();

        while (Match(TokenType.Carrot))
        {
            var op = Previous();
            var right = Primary();
            expr = new ExpressionBinary(expr, op, right);
        }

        return expr;
    }

    private Expression Primary()
    {
        if (Match(TokenType.Number)) return new ExpressionLiteral(Previous().Literal);

        if (Match(TokenType.LeftParen))
        {
            var expr = ParseExpression();
            ConsumeToken(TokenType.RightParen, "Expected ')'.");
            return new ExpressionGrouping(expr);
        }

        if (Match(TokenType.Identifier))
        {
            return FinishCall(Previous().Lexeme);
        }

        throw new ParserError(GetCurrent(), $"Expected literal, got {GetCurrent()}");
    }

    private Expression FinishCall(string callName)
    {
        Advance();
        List<Expression> args = new();
        if (!Check(TokenType.RightParen))
        {
            do
            {
                args.Add(ParseExpression());
            } while (Match(TokenType.Comma));
        }

        var paren = ConsumeToken(TokenType.RightParen, "Expected ')' to end function call.");

        return new ExpressionCall(callName, paren, args);
    }

    private bool Match(params TokenType[] tokenType)
    {
        foreach (var type in tokenType)
        {
            if (Check(type))
            {
                Advance();
                return true;
            }
        }

        return false;
    }

    private bool Check(TokenType tokenType)
    {
        if (IsAtEnd()) return false;
        return tokenType == GetCurrent().Type;
    }

    private bool IsAtEnd()
    {
        return GetCurrent().Type == TokenType.Eof;
    }

    private Token GetCurrent()
    {
        return _tokens[_current];
    }

    private Token GetNext()
    {
        return _tokens[_current + 1];
    }

    private Token Advance()
    {
        if (!IsAtEnd()) _current++;
        return Previous();
    }

    private Token ConsumeToken(TokenType type, string message)
    {
        if (Check(type)) return Advance();

        throw new ParserError(GetCurrent(), message);
    }

    private Token Previous()
    {
        return _tokens[_current - 1];
    }

    public class ParserError : Exception
    {
        public Token Token { get; }
        public ParserError(Token token, string message) : base(message)
        {
            this.Token = token;
        }
    }
}