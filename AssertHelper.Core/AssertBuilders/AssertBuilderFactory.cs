using System;
using System.Collections.Generic;
using System.Linq;
using AssertHelper.Core.AssertBuilders.MSTest;
using AssertHelper.Core.AssertBuilders.NUnit;

namespace AssertHelper.Core.AssertBuilders
{
    /// <summary>
    /// Factory responsible for detecting and creating the appropriate test framework-specific assert builder.
    /// </summary>
    /// <remarks>
    /// This class automatically detects which testing framework is being used (NUnit or MSTest) by examining
    /// the loaded assemblies in the current AppDomain. It maintains a singleton instance of the appropriate
    /// builder for the duration of the test run.
    /// </remarks>
    internal class AssertBuilderFactory
    {
        private static readonly IEnumerable<ICreateBuilderCommand> CreateCommands = new List<ICreateBuilderCommand>
        {
            new CreateNUnitBuilderCommand(),
            new CreateNUnitCoreBuilderCommand(),
            new CreateMsTestBuilderCommand()
        };

        protected static IAssertBuilder _assertBuilder;

        /// <summary>
        /// Gets the appropriate assert builder for the current test framework.
        /// </summary>
        /// <returns>An <see cref="IAssertBuilder"/> instance for the detected test framework.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no supported test framework is detected in the current AppDomain.
        /// </exception>
        /// <remarks>
        /// The builder is created once and cached. Subsequent calls return the same instance.
        /// The framework is detected by checking for framework-specific assemblies in the AppDomain.
        /// </remarks>
        public static IAssertBuilder GetAssertBuilder()
        {
            if (_assertBuilder == null)
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                var createBuilderCommand = CreateCommands.FirstOrDefault(command => assemblies.Any(assembly => assembly.GetName().Name == command.AssemblyName));
                if (createBuilderCommand == null)
                {
                    var supportedFrameworks = CreateCommands.Select(command => command.FrameworkName).Distinct();
                    throw new InvalidOperationException(
                        "Unit testing framework not found. Currently supported: "
                        + string.Join(", ", supportedFrameworks));
                }

                _assertBuilder = createBuilderCommand.CreateAssertBuilder();
            }

            return _assertBuilder;
        }

        /// <summary>
        /// Resets the cached assert builder, forcing re-detection on next call.
        /// </summary>
        /// <remarks>
        /// This method is primarily used for testing purposes.
        /// </remarks>
        protected static void Reset()
        {
            _assertBuilder = null;
        }
    }
}