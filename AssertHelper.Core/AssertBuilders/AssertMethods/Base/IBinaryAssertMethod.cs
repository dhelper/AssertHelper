using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods.Base
{
    interface IBinaryAssertMethod : IBinaryAssertMethod<Expression, Expression>
    {

    }

    interface IBinaryAssertMethod<in T1, in T2>
    {
        Expression<Action> Assert(T1 left, T2 right, string lambda);
    }
}