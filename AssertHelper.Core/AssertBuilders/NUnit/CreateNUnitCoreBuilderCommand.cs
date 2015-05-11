using System.Reflection;

namespace AssertHelper.Core.AssertBuilders.NUnit
{
    class CreateNUnitCoreBuilderCommand : CreateNUnitBuilderCommand
    {
        public override string AssemblyName { get { return "nunit.core"; } }
        public override IAssertBuilder CreateAssertBuilder()
        {
            // Using NUnit, but the nunit.framework assembly is not yet loaded.
            // Since the NUnit assert builder requires that assembly, load it explicitly.
            Assembly.Load("nunit.framework");

            return base.CreateAssertBuilder();
        }
    }
}