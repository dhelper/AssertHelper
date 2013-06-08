using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AssertHelper.Core
{
    public static class Assert
    {
        private static readonly MethodInfo IsFalse = typeof(NUnit.Framework.Assert).GetMethod("IsFalse", new[] { typeof(bool) });
        private static readonly MethodInfo IsTrue = typeof(NUnit.Framework.Assert).GetMethod("IsTrue", new[] { typeof(bool) });
        private static readonly MethodInfo AreEqual = typeof(NUnit.Framework.Assert).GetMethod("AreEqual", new[] { typeof(object), typeof(object) });
        private static readonly MethodInfo AreNotEqual = typeof(NUnit.Framework.Assert).GetMethod("AreNotEqual", new[] { typeof(object), typeof(object) });


        public static void This(Expression<Func<bool>> predicate)
        {
            var expression = predicate.Body;

            Expression<Action> assert;

            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Not)
            {
                assert = GetNotExpression(unaryExpression);
            }
            else
            {
                var binaryExpression = expression as BinaryExpression;
                if (binaryExpression != null)
                {
                    var constantExpressionRight = binaryExpression.Right as ConstantExpression;

                    if (constantExpressionRight != null)
                    {
                        assert = CreateBooleanAssertExpression(constantExpressionRight, binaryExpression.Left, binaryExpression.NodeType);
                    }
                    else
                    {
                        var constantExpressionLeft = binaryExpression.Left as ConstantExpression;

                        if (constantExpressionLeft != null)
                        {
                            assert = CreateBooleanAssertExpression(constantExpressionLeft, binaryExpression.Right, binaryExpression.NodeType);
                        }
                        else
                        {
                            var methodInfo = binaryExpression.NodeType == ExpressionType.Equal ? AreEqual : AreNotEqual;

                            assert = Expression.Lambda<Action>(Expression.Call(methodInfo, binaryExpression.Left, binaryExpression.Right));
                        }
                    }
                }
                else
                {
                    assert = Expression.Lambda<Action>(Expression.Call(IsTrue, expression));
                }
            }

            assert.Compile().Invoke();
        }

        private static Expression<Action> GetNotExpression(UnaryExpression unaryExpression)
        {
            var operand = unaryExpression.Operand;

            return Expression.Lambda<Action>(Expression.Call(IsFalse, operand));
        }

        private static Expression<Action> CreateBooleanAssertExpression(ConstantExpression constantExpressionRight, Expression resultExpression, ExpressionType nodeType)
        {
            var value = (bool)constantExpressionRight.Value;
            if (nodeType == ExpressionType.NotEqual)
            {
                value = !value;
            }

            var expression = value ? Expression.Call(IsTrue, resultExpression) : Expression.Call(IsFalse, resultExpression);

            return Expression.Lambda<Action>(expression);
        }
    }
}
