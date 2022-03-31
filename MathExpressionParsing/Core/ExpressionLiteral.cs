namespace MathExpressionParsing.Core;

public class ExpressionLiteral : Expression
{
    public object? Literal { get; }
    public ExpressionLiteral(object? literal)
    {
        this.Literal = literal;
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.VisitLiteralExpression(this);
    }
}