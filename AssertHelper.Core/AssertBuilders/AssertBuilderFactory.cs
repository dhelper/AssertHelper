using System;
using System.Linq;
using AssertHelper.Core.AssertBuilders.MSTest;
using AssertHelper.Core.AssertBuilders.NUnit;

namespace AssertHelper.Core.AssertBuilders
{
    internal class AssertBuilderFactory
    {
        protected static IAssertBuilder _assertBuilder;


        public static IAssertBuilder GetAssertBuilder()
        {
            if (_assertBuilder == null)
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                if (assemblies.Any(a => a.GetName().Name == "nunit.framework"))
                {
                    _assertBuilder = new NUnitAssertBuilder();
                }
                else if (assemblies.Any(a => a.GetName().Name == "Microsoft.VisualStudio.QualityTools.UnitTestFramework"))
                {
                    _assertBuilder = new MSTestAssertBuilder();
                }
            }

            return _assertBuilder;
        }

        protected static void Reset()
        {
            _assertBuilder = new NUnitAssertBuilder();
        }
    }
}