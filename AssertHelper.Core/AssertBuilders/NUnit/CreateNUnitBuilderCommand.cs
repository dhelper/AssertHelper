namespace AssertHelper.Core.AssertBuilders.NUnit
{
    class CreateNUnitBuilderCommand : ICreateBuilderCommand
    {
        public string FrameworkName { get { return "NUnit"; } }
        virtual public string AssemblyName { get { return "nunit.framework"; } }
        virtual public IAssertBuilder CreateAssertBuilder()
        {
            return new AssertBuilder(new NUnitAssertBuilderFactory());
        }
    }
}