using System;
using System.Linq;
using AssertHelper.Core.AssertBuilders.MSTest;
using AssertHelper.Core.AssertBuilders.NUnit;

namespace AssertHelper.Core.AssertBuilders
{
    using System.Reflection;

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
                    _assertBuilder = new AssertBuilder(new NUnitAssertBuilderFactory());
                }
                else if (assemblies.Any(a => a.GetName().Name == "nunit.core"))
                {
                    // Using NUnit, but the nunit.framework assembly is not yet loaded.
                    // Since the NUnit assert builder requires that assembly, load it explicitly.
                    Assembly.Load(typeof(global::NUnit.Framework.Assert).Assembly.GetName());
                    _assertBuilder = new AssertBuilder(new NUnitAssertBuilderFactory());
                }
                else if (assemblies.Any(a => a.GetName().Name == "Microsoft.VisualStudio.QualityTools.UnitTestFramework"))
                {
                    _assertBuilder = new AssertBuilder(new MsTestAssertBuilderFactory());
                }
                else
                {
                    throw new InvalidOperationException("Unsupported unit testing framework. Currently supported: NUnit, MSTest");
                }
            }

            return _assertBuilder;
        }

        protected static void Reset()
        {
            _assertBuilder = null;
        }
    }
}