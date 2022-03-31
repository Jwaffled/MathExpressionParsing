namespace MathExpressionParsing.Core;

public class Interpreter : Expression.IVisitor<object?>
{
    public object? VisitLiteralExpression(ExpressionLiteral expr)
    {
        return expr.Literal;
    }

    public object? VisitBinaryExpression(ExpressionBinary expr)
    {
        var left = Evaluate(expr.Left);
        var right = Evaluate(expr.Right);
        switch (expr.Operator.Type)
        {
            case TokenType.Plus:
            {
                CheckNumberOperands(expr.Operator, left, right);
                return (double) left + (double) right;
            }

            case TokenType.Minus:
            {
                CheckNumberOperands(expr.Operator, left, right);
                return (double) left - (double) right;
            }

            case TokenType.Star:
            {
                CheckNumberOperands(expr.Operator, left, right);
                return (double) left * (double) right;
            }

            case TokenType.Slash:
            {
                CheckNumberOperands(expr.Operator, left, right);
                return (double) left / (double) right;
            }

            case TokenType.Carrot:
            {
                CheckNumberOperands(expr.Operator, left, right);
                return Math.Pow((double) left, (double) right);
            }
        }

        return null;
    }

    public object? VisitGroupingExpression(ExpressionGrouping expr)
    {
        return Evaluate(expr.Expression);
    }

    public object? VisitCallExpression(ExpressionCall expr)
    {
        var args = new List<object?>();
        foreach (var argument in expr.Args)
        {
            args.Add(Evaluate(argument));
        }

        return MathFunctions.FindAndInvoke(expr.FunctionName, args.Select(x => (double) x).ToArray());
    }

    private void CheckNumberOperands(Token op, object? left, object? right)
    {
        if (left is double && right is double) return;
        throw new RuntimeError(op, "Operands must be numbers.");
    }

    public object? Evaluate(Expression expr)
    {
        return expr.Accept(this);
    }

    private class RuntimeError : Exception
    {
        public Token Token { get; }

        public RuntimeError(Token token, string message) : base(message)
        {
            this.Token = token;
        }
    }
}