using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    internal interface IExpressionTypeToAction
    {
        bool IsValid(Expression expr);
        Expression<Action> GetAction(Expression expression);
    }
}