using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;
using AssertHelper.Core.AssertBuilders.MSTest;

namespace AssertHelper.Core.AssertBuilders.NUnit
{
    class NUnitAssertBuilderFactory : AssertBuilderFactoryBase
    {
        public NUnitAssertBuilderFactory()
            : base("nunit.framework")
        {
        }

        public override IBinaryAssertMethod<Type, Expression> CreateIsInstanceOfType()
        {
            return new InstaceOfType(AssertType);
        }

        public override IBinaryAssertMethod CreateCollectionContains()
        {
            return new CollectionContainsWithIEnumerable(CollectionAssertType);
        }

        public override IBinaryAssertMethod CreateCollectionAreEqual()
        {
            return new CollectionAreEqualWithIEnumerable(CollectionAssertType);
        }

        public override IBinaryAssertMethod CreateCollectionAreNotEqual()
        {
            return new CollectionAreNotEqualWithIEnumerable(CollectionAssertType);
        }
    }
}