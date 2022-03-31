using System.Collections;
using System.Collections.Generic;
using MathExpressionParsing.Core;

namespace Tests;

public class ScannerTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            "(",
            new List<Token>()
            {
                new Token(TokenType.LeftParen, "(", null, 1),
                new Token(TokenType.Eof, "", null, 1)
            }
        };

        yield return new object[]
        {
            "15 + (6 * 2)",
            new List<Token>()
            {
                new Token(TokenType.Number, "15", 15d, 1),
                new Token(TokenType.Plus, "+", null, 1),
                new Token(TokenType.LeftParen, "(", null, 1),
                new Token(TokenType.Number, "6", 6d, 1),
                new Token(TokenType.Star, "*", null, 1),
                new Token(TokenType.Number, "2", 2d, 1),
                new Token(TokenType.RightParen, ")", null, 1),
                new Token(TokenType.Eof, "", null, 1)
            }
        };

        yield return new object[]
        {
            "identifier + 5",
            new List<Token>()
            {
                new Token(TokenType.Identifier, "identifier", "identifier", 1),
                new Token(TokenType.Plus, "+", null, 1),
                new Token(TokenType.Number, "5", 5d, 1),
                new Token(TokenType.Eof, "", null, 1)
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}