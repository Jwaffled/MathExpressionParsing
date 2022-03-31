namespace MathExpressionParsing.Core;

public class ExpressionCall : Expression
{
    public string FunctionName { get; }
    public List<Expression> Args { get; }

    public ExpressionCall(string funcName, Token paren, List<Expression> args)
    {
        this.FunctionName = funcName;
        this.Args = args;
    }
    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.VisitCallExpression(this);
    }
}