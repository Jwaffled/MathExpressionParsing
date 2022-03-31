namespace MathExpressionParsing.Core;

public abstract class Expression
{
    public interface IVisitor<T>
    {
        T VisitLiteralExpression(ExpressionLiteral expr);
        T VisitBinaryExpression(ExpressionBinary expr);
        T VisitGroupingExpression(ExpressionGrouping expr);
        T VisitCallExpression(ExpressionCall expr);
    }
    public abstract T Accept<T>(IVisitor<T> visitor);
}