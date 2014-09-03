using AssertHelper.Core.AssertBuilders.NUnit;

namespace AssertHelper.Core.AssertBuilders
{
    internal class AssertBuilderFactory
    {
        protected static IAssertBuilder _assertBuilder;

        static AssertBuilderFactory()
        {
            _assertBuilder = new NUnitAssertBuilder();
        }

        public static IAssertBuilder GetAssertBuilder()
        {
            return _assertBuilder;
        }

        protected static void Reset()
        {
            _assertBuilder = new NUnitAssertBuilder();
        }
    }
}