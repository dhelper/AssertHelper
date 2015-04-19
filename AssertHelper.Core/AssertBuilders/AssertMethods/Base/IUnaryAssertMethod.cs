using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods.Base
{
    interface IUnaryAssertMethod<in T>
    {
        Expression<Action> Assert(T expression, string lambda);
    }

    interface IUnaryAssertMethod : IUnaryAssertMethod<Expression>
    {

    }
}