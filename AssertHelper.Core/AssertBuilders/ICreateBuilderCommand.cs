namespace AssertHelper.Core.AssertBuilders
{
    internal interface ICreateBuilderCommand
    {
        string FrameworkName { get; }
        string AssemblyName { get; }
        IAssertBuilder CreateAssertBuilder();
    }
}