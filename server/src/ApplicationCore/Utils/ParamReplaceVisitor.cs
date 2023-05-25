using System.Linq.Expressions;

namespace ApplicationCore.Utils;

internal class ParamReplaceVisitor : ExpressionVisitor
{
    private readonly Expression _oldValue;
    private readonly Expression _newValue;

    public ParamReplaceVisitor(Expression oldValue, Expression newValue)
    {
        _oldValue = oldValue;
        _newValue = newValue;
    }

    public override Expression Visit(Expression node)
    {
        if (node == _oldValue)
        {
            return _newValue;
        }

        return base.Visit(node);
    }
}