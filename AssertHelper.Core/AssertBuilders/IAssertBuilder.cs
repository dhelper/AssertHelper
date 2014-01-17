using AssertHelper.Core.AssertBuilders.NUnit;
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
        Expression<Action> GetCollectionEquals(Expression expected, Expression actual);
        Expression<Action> GetCollectionNotEquals(Expression expected, Expression actual);
        Expression<Action> GetStringContains(Expression expected, Expression actual);
        Expression<Action> GetStringStartsWith(Expression expected, Expression actual);
        Expression<Action> GetStringEndsWith(Expression expected, Expression actual);



        
    }
}