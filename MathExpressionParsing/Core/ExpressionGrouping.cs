namespace MathExpressionParsing.Core;

public class ExpressionGrouping : Expression
{
    public Expression Expression { get; }

    public ExpressionGrouping(Expression expr)
    {
        this.Expression = expr;
    }
    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.VisitGroupingExpression(this);
    }
}