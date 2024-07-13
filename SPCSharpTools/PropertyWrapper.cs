using System;
using System.Reflection;

namespace SP.Tools
{
    public class PropertyWrapper<T>
    {
        private Func<T> getter;
        private Action<T> setter;

        public T Value
        {
            get
            {
                return getter();
            }
            set
            {
                setter(value);
            }
        }

        public PropertyWrapper(object target, PropertyInfo propertyInfo)
        {
            var methodInfo = propertyInfo.GetSetMethod();
            var @delegate = Delegate.CreateDelegate(typeof(Action<T>), target, methodInfo);
            setter = (Action<T>)@delegate;

            methodInfo = propertyInfo.GetGetMethod();
            @delegate = Delegate.CreateDelegate(typeof(Func<T>), target, methodInfo);
            getter = (Func<T>)@delegate;
        }
    }
}