using AssertHelper.Core.AssertBuilders.NUnit;

namespace AssertHelper.Core.AssertBuilders
{
    internal static class AssertBuilderFactory
    {
        private static readonly NUnitAssertBuilder _assertBuilder;

        static AssertBuilderFactory()
        {
            _assertBuilder = new NUnitAssertBuilder();
        }

        public static IAssertBuilder GetAssertBuilder()
        {
            return _assertBuilder;
        }

    }
}