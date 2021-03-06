﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AssertHelper.Core.AssertBuilders;
using AssertHelper.Core.ExpressionConverters;

namespace AssertHelper.Core
{
    public static class Expect
    {
        private static readonly List<IExpressionTypeToAction> _actionConverters;
        private static readonly IExpressionTypeToAction _defaultActionConverter = new AssertTrueAction();
        static Expect()
        {
            _actionConverters = new List<IExpressionTypeToAction>
            {
                new StringContains(),
                new StringStartsWith(),
                new StringEndsWith(),
                new UnaryNotExpressionToAction(),
                new BinaryExpressionWithConstantRightAction(),
                new BinaryExpressionWithConstantLeftAction(),
                new EnumerableEquals(),
                new BinaryExpressionEquals(),
                new BinaryExpressionNotEquals(),
                new IsInstanceOf(),
                new EnumerableContains(),
                new CollectionContains()                
            };
        }

        public static void That(Expression<Func<bool>> predicate)
        {
            var expression = predicate.Body;

            IEnumerable<Expression<Action>> assertExpressions = GetAllAssertExpressions(expression).ToArray();
            if (assertExpressions.Count() > 1)
            {
                var actions = assertExpressions.Select(expression1 => expression1.Compile());
                All(actions.ToArray());
            }
            else
            {
                assertExpressions.Single().Compile().Invoke();
            }
        }

        public static void All(params Action[] assertionsToRun)
        {
            var errorMessages = new List<Exception>();
            foreach (var action in assertionsToRun)
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception exc)
                {
                    errorMessages.Add(exc);
                }
            }

            if (errorMessages.Count <= 0)
                return;

            var errorText = new StringBuilder();
            foreach (var e in errorMessages)
            {
                if (errorText.Length > 0)
                {
                    errorText.Append(Environment.NewLine);
                }

                errorText.Append(digestStackTrace(e));
            }

            var assertBuilder = AssertBuilderFactory.GetAssertBuilder();

            var errorMessage = string.Format("{0}/{1} conditions failed:{2}{2}{3}", errorMessages.Count, assertionsToRun.Length, Environment.NewLine, errorText);

            assertBuilder.GetFail(errorMessage).Compile().Invoke();
        }

        private static object digestStackTrace(Exception e)
        {
            // TODO: clean up exception stack trace

            return e;
        }

        private static IEnumerable<Expression<Action>> GetAllAssertExpressions(Expression expression)
        {
            if (expression.NodeType == ExpressionType.AndAlso)
            {
                var logicalBinaryExpression = (BinaryExpression)expression;
                var rightSide = GetAllAssertExpressions(logicalBinaryExpression.Right);
                var leftSide = GetAllAssertExpressions(logicalBinaryExpression.Left);

                return leftSide.Concat(rightSide);
            }

            return new[] { GetAssertExpression(expression) };
        }

        private static Expression<Action> GetAssertExpression(Expression expression)
        {
            var converter = _actionConverters.FirstOrDefault(action => action.IsValid(expression));
            var actionConverter = converter ?? _defaultActionConverter;

            return actionConverter.GetAction(expression);
        }
    }
}
