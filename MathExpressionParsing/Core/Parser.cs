using System.Runtime.InteropServices;
using MathExpressionParsing.Exceptions;

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

    public Expression Parse()
    {
        return ParseExpression();
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
        var expr = PostfixUnary();

        while (Match(TokenType.Carrot))
        {
            var op = Previous();
            var right = PostfixUnary();
            expr = new ExpressionBinary(expr, op, right);
        }

        return expr;
    }

    private Expression PostfixUnary()
    {
        var expr = Unary();

        while (Match(TokenType.Factorial))
        {
            var op = Previous();
            expr = new ExpressionUnary(expr, op);
        }

        return expr;
    }

    private Expression Unary()
    {
        // TODO: make this work with factorials/postfix unary ops
        while (Match(TokenType.Minus))
        {
            var op = Previous();
            var expr = Unary();
            return new ExpressionUnary(expr, op);
        }

        return Primary();
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
            if (Check(TokenType.LeftParen))
            {
                return FinishCall(Previous().Lexeme);
            }

            return MathConstant(Previous().Lexeme);
        }

        throw new ParserException(GetCurrent(), $"Expected literal, got {GetCurrent()}");
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

    private Expression MathConstant(string constName)
    {
        var constant = MathLibrary.FindConstant(constName);

        if (constant is null)
        {
            throw new ConstantNotFoundException(constName, $"Constant '{constName}' does not exist.");
        }

        return new ExpressionLiteral(constant);
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

        throw new ParserException(GetCurrent(), message);
    }

    private Token Previous()
    {
        return _tokens[_current - 1];
    }
}