using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssertHelper.Core.ExpressionConverters
{
    public class EnumerableEquals : IExpressionTypeToAction
    {
        public bool IsValid(System.Linq.Expressions.Expression expr)
        {
            return false;
        }

        public System.Linq.Expressions.Expression<Action> GetAction(System.Linq.Expressions.Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
