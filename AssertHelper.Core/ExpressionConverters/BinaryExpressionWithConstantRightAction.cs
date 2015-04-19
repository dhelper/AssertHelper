using System;
using System.Linq;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    internal class BinaryExpressionWithConstantRightAction : BinaryExpressionWithConstantBase
    {
        protected override Func<BinaryExpression, Expression> GetConstantExpression
        {
            get { return expr => expr.Right;}
        }

        protected override Func<BinaryExpression, Expression> GetValueExpression
        {
            get { return expr => expr.Left; }
        }        

        public override string GetLembda(BinaryExpression binaryExpression)
        {
            var left = binaryExpression.Left.ToString();

            var shortLeft = left.Split('.').Last();

            return binaryExpression.ToString().Replace(left, shortLeft);
        }
    }
}