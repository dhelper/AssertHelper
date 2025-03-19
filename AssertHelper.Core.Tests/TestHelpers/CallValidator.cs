using System;
using System.Linq.Expressions;
using FakeItEasy;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    public class CallValidator
    {
        private ArgumentCollection _arguments;
        public bool WasCalled { get; set; }

        public void WasAssertCalledWithArguments<T>(T argument)
        {
            Assert.That(WasCalled, Is.True);
            Assert.That(GetValue<T>(0), Is.EqualTo(argument));
        }

        public void WasAssertCalledWithArguments<T1, T2>(T1 argument1, T2 argument2)
        {
            Assert.That(WasCalled, Is.True);

            Assert.That(GetValue<T1>(0), Is.EqualTo(argument1));
            Assert.That(GetValue<T2>(1), Is.EqualTo(argument2));
        }

        private T GetValue<T>(int index)
        {
            if (_arguments[index].GetType().IsInstanceOfType(typeof(T)))
            {
                return _arguments.Get<T>(index);
            }

            var argumentExpression = _arguments.Get<Expression>(index);

            return Expression.Lambda<Func<T>>(argumentExpression).Compile().Invoke();
        }

        virtual internal void SetArguments(ArgumentCollection arguments)
        {
            _arguments = arguments;
        }
    }

   
}