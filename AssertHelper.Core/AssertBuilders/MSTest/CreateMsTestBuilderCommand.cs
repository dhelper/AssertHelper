namespace AssertHelper.Core.AssertBuilders.MSTest
{
    class CreateMsTestBuilderCommand : ICreateBuilderCommand
    {
        public string FrameworkName { get { return "MSTest"; } }
        public string AssemblyName { get { return "Microsoft.VisualStudio.QualityTools.UnitTestFramework"; } }
        public IAssertBuilder CreateAssertBuilder()
        {
            return new AssertBuilder(new MsTestAssertBuilderFactory());
        }
    }
}