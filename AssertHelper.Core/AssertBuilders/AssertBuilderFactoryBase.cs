using System;
using System.Linq;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders
{
    internal abstract class AssertBuilderFactoryBase : IAssertBuilderFactory
    {
        private readonly Type _assertType;
        private readonly Type _stringAssertType;
        private readonly Type _collectionAssertType;


        protected Type AssertType { get { return _assertType; } }
        protected Type StringAssertType { get { return _stringAssertType; } }
        protected Type CollectionAssertType { get { return _collectionAssertType; } }

        protected AssertBuilderFactoryBase(string assertAssemblyName)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name == assertAssemblyName)
                .SelectMany(a => a.GetTypes()).ToArray();

            _assertType = types.Single(t => t.Name == "Assert");
            _stringAssertType = types.Single(type => type.Name == "StringAssert");
            _collectionAssertType = types.Single(type => type.Name == "CollectionAssert");
        }

        public IUnaryAssertMethod CreateIsTrue()
        {
            return new IsTrue(_assertType);
        }

        public IUnaryAssertMethod CreateIsFalse()
        {
            return new IsFalse(_assertType);
        }

        public IUnaryAssertMethod CreateIsNull()
        {
            return new IsNull(_assertType);
        }

        public IUnaryAssertMethod CreateIsNotNull()
        {
            return new IsNotNull(_assertType);
        }

        public IBinaryAssertMethod CreateAreEqual()
        {
            return new AreEqual(_assertType);
        }

        public IBinaryAssertMethod CreateAreNotEqual()
        {
            return new AreNotEqual(_assertType);
        }

        public abstract IBinaryAssertMethod<Type, Expression> CreateIsInstanceOfType();

        public IUnaryAssertMethod<string> CreateFail()
        {
            return new Fail(_assertType);
        }

        public IBinaryAssertMethod CreateStringContains()
        {
            return new StringContains(_stringAssertType);
        }

        public IBinaryAssertMethod CreateStringStartsWith()
        {
            return new StringStartsWith(_stringAssertType);
        }

        public IBinaryAssertMethod CreateStringEndsWith()
        {
            return new StringEndsWith(_stringAssertType);
        }

        public abstract IBinaryAssertMethod CreateCollectionContains();
        public abstract IBinaryAssertMethod CreateCollectionAreEqual();
        public abstract IBinaryAssertMethod CreateCollectionAreNotEqual();
    }
}