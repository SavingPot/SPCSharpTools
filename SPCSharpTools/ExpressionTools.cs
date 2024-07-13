using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace SP.Tools
{
    public static class ExpressionTools
    {
        public static UnaryExpression Box(this Expression expression)
        {
            return Expression.Convert(expression, typeof(object));
        }

        public static IndexExpression ListItem(this Expression listExpression, int index)
        {
            return ListItem(listExpression, Expression.Constant(index));
        }

        public static IndexExpression ListItem(this Expression listExpression, Expression index)
        {
            return Expression.Property(listExpression, "Item", index);
        }

        public static IndexExpression ListCount(this Expression listExpression)
        {
            return Expression.Property(listExpression, "Count", null);
        }

        public static MethodCallExpression ListAdd(this Expression listExpression, Type listTypeWithGeneric, Expression element)
        {
            return Expression.Call(listExpression, listTypeWithGeneric.GetMethod("Add"), element);
        }

        public static IndexExpression ArrayItem(this Expression arrayExpression, int index)
        {
            return ArrayItem(arrayExpression, Expression.Constant(index));
        }

        public static IndexExpression ArrayItem(this Expression arrayExpression, Expression index)
        {
            return Expression.ArrayAccess(arrayExpression, index);
        }

        public static UnaryExpression ArrayLength(this Expression arrayExpression)
        {
            return Expression.ArrayLength(arrayExpression);
        }
    }
}