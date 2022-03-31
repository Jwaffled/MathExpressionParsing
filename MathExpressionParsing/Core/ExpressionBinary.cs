namespace MathExpressionParsing.Core;

public class ExpressionBinary : Expression
{
    public Token Operator { get; }
    public Expression Left { get; }
    public Expression Right { get; }

    public ExpressionBinary(Expression left, Token op, Expression right)
    {
        this.Left = left;
        this.Operator = op;
        this.Right = right;
    }
    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.VisitBinaryExpression(this);
    }
}