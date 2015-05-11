using System;
using System.Collections.Generic;
using System.Linq;
using AssertHelper.Core.AssertBuilders.MSTest;
using AssertHelper.Core.AssertBuilders.NUnit;

namespace AssertHelper.Core.AssertBuilders
{
    internal class AssertBuilderFactory
    {
        private static readonly IEnumerable<ICreateBuilderCommand> CreateCommands = new List<ICreateBuilderCommand>
        {
            new CreateNUnitBuilderCommand(),
            new CreateNUnitCoreBuilderCommand(),
            new CreateMsTestBuilderCommand()
        };

        protected static IAssertBuilder _assertBuilder;

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

        protected static void Reset()
        {
            _assertBuilder = null;
        }
    }
}