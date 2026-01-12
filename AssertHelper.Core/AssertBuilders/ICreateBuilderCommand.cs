namespace AssertHelper.Core.AssertBuilders
{
    /// <summary>
    /// Command interface for creating framework-specific assert builders.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface encapsulate the logic for creating assert builders
    /// for specific test frameworks (NUnit, MSTest, etc.). Each command knows which framework
    /// it supports and can create the appropriate builder instance.
    /// </remarks>
    internal interface ICreateBuilderCommand
    {
        /// <summary>
        /// Gets the user-friendly name of the test framework (e.g., "NUnit", "MSTest").
        /// </summary>
        string FrameworkName { get; }
        
        /// <summary>
        /// Gets the assembly name used to detect if this framework is available (e.g., "nunit.framework").
        /// </summary>
        string AssemblyName { get; }
        
        /// <summary>
        /// Creates the assert builder for this framework.
        /// </summary>
        /// <returns>An <see cref="IAssertBuilder"/> instance configured for this framework.</returns>
        IAssertBuilder CreateAssertBuilder();
    }
}