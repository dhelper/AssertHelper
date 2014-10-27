using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders
{
    interface IAssertBuilderFactory
    {
        IUnaryAssertMethod CreateIsTrue();
        IUnaryAssertMethod CreateIsFalse();
        IUnaryAssertMethod CreateIsNull();
        IUnaryAssertMethod CreateIsNotNull();
        IBinaryAssertMethod CreateAreEqual();
        IBinaryAssertMethod CreateAreNotEqual();
        IBinaryAssertMethod<Type, Expression> CreateIsInstanceOfType();
        IUnaryAssertMethod<string> CreateFail();
        IBinaryAssertMethod CreateStringContains();
        IBinaryAssertMethod CreateStringStartsWith();
        IBinaryAssertMethod CreateStringEndsWith();
        IBinaryAssertMethod CreateCollectionContains();
        IBinaryAssertMethod CreateCollectionAreEqual();
        IBinaryAssertMethod CreateCollectionAreNotEqual();
    }
}