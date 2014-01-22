using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    internal class BinaryExpressionWithConstantLeftAction : BinaryExpressionWithConstantBase
    {
        protected override Func<BinaryExpression, Expression> GetConstantExpression
        {
            get { return expr => expr.Left; }
        }

        protected override Func<BinaryExpression, Expression> GetValueExpression
        {
            get { return expr => expr.Right; }
        }
    }
}