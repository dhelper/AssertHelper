using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AssertHelper.Core.AssrtBuilders;
using AssertHelper.Core.ExpressionConverters;

namespace AssertHelper.Core
{
    public static class Assert
    {
        private static readonly List<IExpressionTypeToAction> _actionConverters;
        private static readonly IExpressionTypeToAction _defaultActionConverter = new AssertTrueAction();
        static Assert()
        {
            _actionConverters = new List<IExpressionTypeToAction>
            {
                new UnaryNotExpressionToAction(),
                new BinaryExpressionWithConstantRightAction(),
                new BinaryExpressionWithConstantLeftAction(),
                new BinaryExpressionEquals(),
                new BinaryExpressionNotEquals(),
                new InstanceOfExpression()
            };
        }

        public static void This(Expression<Func<bool>> predicate)
        {
            var expression = predicate.Body;

            if (expression.NodeType == ExpressionType.AndAlso)
            {
                var logicalBinaryExpression = (BinaryExpression) expression;

                var assertExpressionRight = GetAssertExpression(logicalBinaryExpression.Right);
                var assertExpressionLeft = GetAssertExpression(logicalBinaryExpression.Left);

                var compileRight = assertExpressionRight.Compile();
                var compileLeft = assertExpressionLeft.Compile();

                All(new[] {compileRight, compileLeft});
            }
            else
            {
                var assertExpression = GetAssertExpression(expression);

                assertExpression.Compile().Invoke();
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
            foreach (Exception e in errorMessages)
            {
                if (errorText.Length > 0)
                    errorText.Append(Environment.NewLine);
                errorText.Append(digestStackTrace(e));
            }

            IAssertBuilder assertBuilder = AssertBuilderFaxctory.GetAssertBuilder();

            string errorMessage = string.Format("{0}/{1} conditions failed:{2}{2}{3}", errorMessages.Count, assertionsToRun.Length, Environment.NewLine, errorText);

            assertBuilder.GetFail(errorMessage).Compile().Invoke();
        }

        private static object digestStackTrace(Exception e)
        {
            // TODO: clean up exception stack trace

            return e;
        }

        
        private static Expression<Action> GetAssertExpression(Expression expression)
        {
            var actionConverter = _actionConverters.FirstOrDefault(action => action.IsValid(expression));
            if (actionConverter == null)
            {
                actionConverter = _defaultActionConverter;
            }

            var assert = actionConverter.GetAction(expression);
            return assert;
        }
    }

    internal class InstanceOfExpression : ExpressionTypeToAction<TypeBinaryExpression>
    {
        protected override bool IsValidInternal(TypeBinaryExpression typedExpression)
        {
            return typedExpression.NodeType == ExpressionType.TypeIs;
        }

        protected override Expression<Action> GetActionInternal(TypeBinaryExpression typedExpression)
        {
            return AssertBuilder.GetIsInstanceOf(typedExpression.TypeOperand, typedExpression.Expression);
        }
    }

    internal class NotInstanceOfExpression : ExpressionTypeToAction<TypeBinaryExpression>
    {
        protected override Expression<Action> GetActionInternal(TypeBinaryExpression typedExpression)
        {
            return AssertBuilder.GetIsInstanceOf(typedExpression.TypeOperand, typedExpression.Expression);
        }
    }
}
