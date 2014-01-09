using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.AssertBuilders
{
    internal static class AssertBuilderFactory
    {
        private static readonly NUnitAssertBuilder _assertBuilder;

        static AssertBuilderFactory()
        {
            _assertBuilder = new NUnitAssertBuilder();
        }

        public static IAssertBuilder GetAssertBuilder()
        {
            return _assertBuilder;
        }

    }


    public interface IAssertBuilder
    {
        Expression<Action> GetAreEqualAction(Expression left, Expression right);
        Expression<Action> GetAreNotEqualAction(Expression left, Expression right);
        Expression<Action> GetIsTrueAction(Expression expression);
        Expression<Action> GetIsFalseAction(Expression expression);
        Expression<Action> GetIsInstanceOf(Type typeOperand, Expression expression);
        Expression<Action> GetFail(string message);
        Expression<Action> GetCollectionContains(Expression collection, Expression value);
    }
}