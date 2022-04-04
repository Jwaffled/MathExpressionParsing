namespace MathExpressionParsing.Core;

public class ExpressionUnary : Expression
{
    public Expression Right { get; }
    public Token Operator { get; }

    public ExpressionUnary(Expression right, Token op)
    {
        this.Right = right;
        this.Operator = op;
    }
    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.VisitUnaryExpression(this);
    }
}