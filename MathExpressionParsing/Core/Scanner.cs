using MathExpressionParsing.Exceptions;

namespace MathExpressionParsing.Core;

public class Scanner
{
    private string _input;
    private List<Token> _tokens = new();
    private int _start = 0;
    private int _current = 0;
    private int _line = 1;

    public Scanner()
    {
        _input = "";
    }

    public Scanner(string input)
    {
        this._input = input;
    }

    public void SetInput(string input)
    {
        _input = input;
        _start = 0;
        _current = 0;
        _line = 1;
    }

    public List<Token> ScanTokens()
    {
        while (!IsAtEnd())
        {
            _start = _current;
            ScanToken();
        }
        
        _tokens.Add(new Token(TokenType.Eof, "", null, _line));
        return _tokens;
    }

    private void ScanToken()
    {
        var c = Advance();
        switch (c)
        {
            case '(':
            {
                AddToken(TokenType.LeftParen);
                break;
            }
            case ')':
            {
                AddToken(TokenType.RightParen);
                break;
            }
            case ',':
            {
                AddToken(TokenType.Comma);
                break;
            }
            case '+':
            {
                AddToken(TokenType.Plus);
                break;
            }
            case '-':
            {
                AddToken(TokenType.Minus);
                break;
            }
            case '*':
            {
                AddToken(TokenType.Star);
                break;
            }
            case '/':
            {
                AddToken(TokenType.Slash);
                break;
            }
            case '.':
            {
                Number();
                break;
            }
            case '^':
            {
                AddToken(TokenType.Carrot);
                break;
            }
            case '!':
            {
                AddToken(TokenType.Factorial);
                break;
            }
            case ' ':
            case '\t':
            case '\r':
                break;

            case '\n':
            {
                _line++;
                break;
            }
            default:
            {
                if (char.IsDigit(c))
                {
                    Number();
                } else if (char.IsLetter(c))
                {
                    Identifier();
                }
                else
                {
                    throw new ScannerException(_line, $"Unexpected token.");
                }
                break;
            }
        }
    }

    private void Number()
    {
        var hadDecimal = false;
        if (GetCurrent() == '.')
        {
            hadDecimal = true;
            Advance();
        }
        
        while (char.IsDigit(GetCurrent()))
        {
            if (hadDecimal && GetCurrent() == '.')
            {
                throw new ScannerException(_line, "Numbers may only have one decimal point.");
            }
            Advance();
        }
        AddToken(TokenType.Number, double.Parse(_input.Substring(_start, _current - _start)));
    }

    private void Identifier()
    {
        while (char.IsLetter(GetCurrent()))
        {
            Advance();
        }
        
        AddToken(TokenType.Identifier, _input.Substring(_start, _current - _start));
    }

    private void AddToken(TokenType type, object? literal = null)
    {
        var text = _input.Substring(_start, _current - _start);
        _tokens.Add(new Token(type, text, literal, _line));
    }

    private bool IsAtEnd()
    {
        return _current >= _input.Length;
    }

    private char GetCurrent()
    {
        if (IsAtEnd()) return '\0';
        return _input[_current];
    }

    private char Advance()
    {
        return this._input[_current++];
    }
}