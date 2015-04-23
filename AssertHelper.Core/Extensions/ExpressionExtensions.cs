using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace AssertHelper.Core.Extensions
{
    internal static class ExpressionExtensions
    {
        public static Regex FindOuterBracketsRegex = new Regex("^\\(|\\)*$",RegexOptions.CultureInvariant| RegexOptions.Compiled);

        public static ConstantExpression ToConstantExpression(this string str)
        {
            return Expression.Constant(str, typeof (string));
        }

        public static string PrettyPrint(this Expression expression)
        {
            var expressionString = expression.ToString();
            var prettyPrint = Regex.Replace(expressionString, @"value\([^)]+\)\.", string.Empty);

            if (prettyPrint.StartsWith("("))
            {
                return FindOuterBracketsRegex.Replace(prettyPrint, string.Empty);
            }
           
            return prettyPrint;
        }
    }
}
