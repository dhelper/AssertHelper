using System;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    public abstract class AssertTestBase
    {
        protected string AssertMessage { get; private set; }

        [TestFixtureSetUp]
        public void GetErrorString()
        {
            AssertMessage = GetAssertionMessage(FailedAssertionAction);
        }

        public static string GetAssertionMessage(Action failedAssertionAction)
        {
            try
            {
                failedAssertionAction();

                Assert.Fail("Line should have thrown an exception");
            }
            catch (AssertionException exc)
            {
                return exc.Message;
            }

            // Unreached
            return null;
        }

        protected abstract Action FailedAssertionAction { get; }
    }
}