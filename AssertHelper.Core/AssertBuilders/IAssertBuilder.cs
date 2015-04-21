using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.AssertBuilders
{
    public interface IAssertBuilder
    {
        Expression<Action> GetAreEqualAction(Expression left, Expression right, string lambda);
        Expression<Action> GetAreNotEqualAction(Expression left, Expression right, string lambda);
        Expression<Action> GetIsTrueAction(Expression expression, string lambda);
        Expression<Action> GetIsFalseAction(Expression expression, string lambda);
        Expression<Action> GetIsInstanceOf(Type typeOperand, Expression expression, string lambda);
        Expression<Action> GetFail(string message);
        Expression<Action> GetCollectionContains(Expression expected, Expression actual, string lambda);
        Expression<Action> GetCollectionEquals(Expression expected, Expression actual, string lambda);
        Expression<Action> GetCollectionNotEquals(Expression expected, Expression actual, string lambda);
        Expression<Action> GetStringContains(Expression expected, Expression actual, string lambda);
        Expression<Action> GetStringStartsWith(Expression expected, Expression actual, string lambda);
        Expression<Action> GetStringEndsWith(Expression expected, Expression actual, string lambda);
        Expression<Action> GetIsNullAction(Expression expression, string lambda);
        Expression<Action> GetIsNotNullAction(Expression expression, string lambda);
    }
}