using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.MSTest
{
    class MsTestAssertBuilderFactory : AssertBuilderFactoryBase
    {
        public MsTestAssertBuilderFactory()
            : base("Microsoft.VisualStudio.QualityTools.UnitTestFramework")
        {
        }

        public override IBinaryAssertMethod<Type, Expression> CreateIsInstanceOfType()
        {
            return new InstaceOfTypeInv(AssertType);
        }

        public override IBinaryAssertMethod CreateCollectionContains()
        {
            return new CollectionContainsWithICollection(CollectionAssertType);
        }

        public override IBinaryAssertMethod CreateCollectionAreEqual()
        {
            return new CollectionAreEqualWithICollection(CollectionAssertType);
        }

        public override IBinaryAssertMethod CreateCollectionAreNotEqual()
        {
            return new CollectionAreNotEqualWithICollection(CollectionAssertType);
        }
    }
}