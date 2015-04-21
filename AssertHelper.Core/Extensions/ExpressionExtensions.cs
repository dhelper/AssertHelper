using System.Linq.Expressions;

namespace AssertHelper.Core.Extensions
{
    internal static class ExpressionExtensions
    {
        public static ConstantExpression ToConstantExpression(this string str)
        {
            return Expression.Constant(str, typeof (string));
        }
    }
}
