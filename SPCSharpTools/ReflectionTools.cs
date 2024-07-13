using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SP.Tools
{
    public static class ReflectionTools
    {
        /* -------------------------------------------------------------------------- */
        /*                                 Extensions                                 */
        /* -------------------------------------------------------------------------- */
        public static bool NoneConstructor(this Type type)
        {
            return type.GetConstructors(BindingFlags_All).Length == 0;
        }

        public static bool HasConstructor(this Type type)
        {
            return type.GetConstructors(BindingFlags_All).Length != 0;
        }

        public static bool NoneDefaultConstructor(this Type type)
        {
            return !type.GetConstructors(BindingFlags_All).Any(c => c.GetParameters().Length == 0);
        }

        public static bool HasDefaultConstructor(this Type type)
        {
            return type.GetConstructors(BindingFlags_All).Any(c => c.GetParameters().Length == 0);
        }







        /* -------------------------------------------------------------------------- */
        /*                                   Method                                   */
        /* -------------------------------------------------------------------------- */

        public static Action CreateAction(object target, MethodInfo method)
        {
            return (Action)Delegate.CreateDelegate(typeof(Action), target, method);
        }

        public static Action<T> CreateAction1<T>(object target, MethodInfo method)
        {
            return (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), target, method);
        }

        public static Action<T1, T2> CreateAction2<T1, T2>(object target, MethodInfo method)
        {
            return (Action<T1, T2>)Delegate.CreateDelegate(typeof(Action<T1, T2>), target, method);
        }

        public static Action<T1, T2, T3> CreateAction3<T1, T2, T3>(object target, MethodInfo method)
        {
            return (Action<T1, T2, T3>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3>), target, method);
        }

        public static Action<T1, T2, T3, T4> CreateAction4<T1, T2, T3, T4>(object target, MethodInfo method)
        {
            return (Action<T1, T2, T3, T4>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4>), target, method);
        }

        public static Action<T1, T2, T3, T4, T5> CreateAction5<T1, T2, T3, T4, T5>(object target, MethodInfo method)
        {
            return (Action<T1, T2, T3, T4, T5>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5>), target, method);
        }



        public static Action ExpressStaticAction(MethodInfo method)
        {
            var call = Expression.Call(method);
            var lambda = Expression.Lambda<Action>(call);
            return lambda.Compile();
        }

        public static Action<T> ExpressStaticAction<T>(MethodInfo method)
        {
            var arguments = new[] { Expression.Parameter(typeof(T)) };
            var call = Expression.Call(method, arguments);
            var lambda = Expression.Lambda<Action<T>>(call, arguments);
            return lambda.Compile();
        }

        public static Action<T1, T2> ExpressStaticAction<T1, T2>(MethodInfo method)
        {
            var arguments = new[] { Expression.Parameter(typeof(T1)), Expression.Parameter(typeof(T2)) };
            var call = Expression.Call(method, arguments);
            var lambda = Expression.Lambda<Action<T1, T2>>(call, arguments);
            return lambda.Compile();
        }

        public static Action<T1, T2, T3> ExpressStaticAction<T1, T2, T3>(MethodInfo method)
        {
            var arguments = new[] { Expression.Parameter(typeof(T1)), Expression.Parameter(typeof(T2)), Expression.Parameter(typeof(T3)) };
            var call = Expression.Call(method, arguments);
            var lambda = Expression.Lambda<Action<T1, T2, T3>>(call, arguments);
            return lambda.Compile();
        }

        public static Action<T1, T2, T3, T4> ExpressStaticAction<T1, T2, T3, T4>(MethodInfo method)
        {
            var arguments = new[] { Expression.Parameter(typeof(T1)), Expression.Parameter(typeof(T2)), Expression.Parameter(typeof(T3)), Expression.Parameter(typeof(T4)) };
            var call = Expression.Call(method, arguments);
            var lambda = Expression.Lambda<Action<T1, T2, T3, T4>>(call, arguments);
            return lambda.Compile();
        }

        public static Action<T1, T2, T3, T4, T5> ExpressStaticAction<T1, T2, T3, T4, T5>(MethodInfo method)
        {
            var arguments = new[] { Expression.Parameter(typeof(T1)), Expression.Parameter(typeof(T2)), Expression.Parameter(typeof(T3)), Expression.Parameter(typeof(T4)), Expression.Parameter(typeof(T5)) };
            var call = Expression.Call(method, arguments);
            var lambda = Expression.Lambda<Action<T1, T2, T3, T4, T5>>(call, arguments);
            return lambda.Compile();
        }



        public static dynamic CreateActionByParameter(Type param, object target, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("CreateAction1").MakeGenericMethod(param);

            return creator.Invoke(null, new object[] { target, method });
        }

        public static dynamic CreateActionByParameter(Type param1, Type param2, object target, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("CreateAction2").MakeGenericMethod(param1, param2);

            return creator.Invoke(null, new object[] { target, method });
        }

        public static dynamic CreateActionByParameter(Type param1, Type param2, Type param3, object target, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("CreateAction3").MakeGenericMethod(param1, param2, param3);

            return creator.Invoke(null, new object[] { target, method });
        }

        public static dynamic CreateActionByParameter(Type param1, Type param2, Type param3, Type param4, object target, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("CreateAction4").MakeGenericMethod(param1, param2, param3, param4);

            return creator.Invoke(null, new object[] { target, method });
        }

        public static dynamic CreateActionByParameter(Type param1, Type param2, Type param3, Type param4, Type param5, object target, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("CreateAction5").MakeGenericMethod(param1, param2, param3, param4, param5);

            return creator.Invoke(null, new object[] { target, method });
        }







        public static Func<TResult> CreateFunc<TResult>(object target, MethodInfo methodInfo)
        {
            return (Func<TResult>)Delegate.CreateDelegate(typeof(Func<TResult>), target, methodInfo);
        }

        public static Func<T1, TResult> CreateFunc1<T1, TResult>(object target, MethodInfo methodInfo)
        {
            return (Func<T1, TResult>)Delegate.CreateDelegate(typeof(Func<T1, TResult>), target, methodInfo);
        }

        public static Func<T1, T2, TResult> CreateFunc2<T1, T2, TResult>(object target, MethodInfo methodInfo)
        {
            return (Func<T1, T2, TResult>)Delegate.CreateDelegate(typeof(Func<T1, T2, TResult>), target, methodInfo);
        }

        public static Func<T1, T2, T3, TResult> CreateFunc3<T1, T2, T3, TResult>(object target, MethodInfo methodInfo)
        {
            return (Func<T1, T2, T3, TResult>)Delegate.CreateDelegate(typeof(Func<T1, T2, T3, TResult>), target, methodInfo);
        }

        public static Func<T1, T2, T3, T4, TResult> CreateFunc4<T1, T2, T3, T4, TResult>(object target, MethodInfo methodInfo)
        {
            return (Func<T1, T2, T3, T4, TResult>)Delegate.CreateDelegate(typeof(Func<T1, T2, T3, T4, TResult>), target, methodInfo);
        }

        public static Func<T1, T2, T3, T4, T5, TResult> CreateFunc5<T1, T2, T3, T4, T5, TResult>(object target, MethodInfo methodInfo)
        {
            return (Func<T1, T2, T3, T4, T5, TResult>)Delegate.CreateDelegate(typeof(Func<T1, T2, T3, T4, T5, TResult>), target, methodInfo);
        }



        public static Func<TResult> ExpressStaticFunc<TResult>(MethodInfo method)
        {
            var call = Expression.Call(method);
            var lambda = Expression.Lambda<Func<TResult>>(call);
            return lambda.Compile();
        }

        public static Func<T1, TResult> ExpressStaticFunc1<T1, TResult>(MethodInfo method)
        {
            var arguments = new[] { Expression.Parameter(typeof(T1)) };
            var call = Expression.Call(method, arguments);
            var lambda = Expression.Lambda<Func<T1, TResult>>(call, arguments);
            return lambda.Compile();
        }

        public static Func<T1, T2, TResult> ExpressStaticFunc2<T1, T2, TResult>(MethodInfo method)
        {
            var arguments = new[] { Expression.Parameter(typeof(T1)), Expression.Parameter(typeof(T2)) };
            var call = Expression.Call(method, arguments);
            var lambda = Expression.Lambda<Func<T1, T2, TResult>>(call, arguments);
            return lambda.Compile();
        }

        public static Func<T1, T2, T3, TResult> ExpressStaticFunc3<T1, T2, T3, TResult>(MethodInfo method)
        {
            var arguments = new[] { Expression.Parameter(typeof(T1)), Expression.Parameter(typeof(T2)), Expression.Parameter(typeof(T3)) };
            var call = Expression.Call(method, arguments);
            var lambda = Expression.Lambda<Func<T1, T2, T3, TResult>>(call, arguments);
            return lambda.Compile();
        }

        public static Func<T1, T2, T3, T4, TResult> ExpressStaticFunc4<T1, T2, T3, T4, TResult>(MethodInfo method)
        {
            var arguments = new[] { Expression.Parameter(typeof(T1)), Expression.Parameter(typeof(T2)), Expression.Parameter(typeof(T3)), Expression.Parameter(typeof(T4)) };
            var call = Expression.Call(method, arguments);
            var lambda = Expression.Lambda<Func<T1, T2, T3, T4, TResult>>(call, arguments);
            return lambda.Compile();
        }

        public static Func<T1, T2, T3, T4, T5, TResult> ExpressStaticFunc5<T1, T2, T3, T4, T5, TResult>(MethodInfo method)
        {
            var arguments = new[] { Expression.Parameter(typeof(T1)), Expression.Parameter(typeof(T2)), Expression.Parameter(typeof(T3)), Expression.Parameter(typeof(T4)), Expression.Parameter(typeof(T5)) };
            var call = Expression.Call(method, arguments);
            var lambda = Expression.Lambda<Func<T1, T2, T3, T4, T5, TResult>>(call, arguments);
            return lambda.Compile();
        }





        public static dynamic CreateFuncByParameter(Type returnType, object target, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("CreateFunc").MakeGenericMethod(returnType);

            return creator.Invoke(null, new object[] { target, method });
        }

        public static dynamic CreateFuncByParameter(Type param, Type returnType, object target, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("CreateFunc1").MakeGenericMethod(param, returnType);

            return creator.Invoke(null, new object[] { target, method });
        }

        public static dynamic CreateFuncByParameter(Type param1, Type param2, Type returnType, object target, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("CreateFunc2").MakeGenericMethod(param1, param2, returnType);

            return creator.Invoke(null, new object[] { target, method });
        }

        public static dynamic CreateFuncByParameter(Type pram1, Type param2, Type param3, Type returnType, object target, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("CreateFunc3").MakeGenericMethod(pram1, param2, param3, returnType);

            return creator.Invoke(null, new object[] { target, method });
        }

        public static dynamic CreateFuncByParameter(Type pram1, Type param2, Type param3, Type param4, Type returnType, object target, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("CreateFunc4").MakeGenericMethod(pram1, param2, param3, param4, returnType);

            return creator.Invoke(null, new object[] { target, method });
        }

        public static dynamic CreateFuncByParameter(Type pram1, Type param2, Type param3, Type param4, Type param5, Type returnType, object target, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("CreateFunc5").MakeGenericMethod(pram1, param2, param3, param4, param5, returnType);

            return creator.Invoke(null, new object[] { target, method });
        }


        public static dynamic ExpressStaticFuncByParameter(Type returnType, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("ExpressStaticFunc").MakeGenericMethod(returnType);

            return creator.Invoke(null, new[] { method });
        }

        public static dynamic ExpressStaticFuncByParameter(Type param, Type returnType, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("ExpressStaticFunc1").MakeGenericMethod(param, returnType);

            return creator.Invoke(null, new[] { method });
        }

        public static dynamic ExpressStaticFuncByParameter(Type param1, Type param2, Type returnType, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("ExpressStaticFunc2").MakeGenericMethod(param1, param2, returnType);

            return creator.Invoke(null, new[] { method });
        }

        public static dynamic ExpressStaticFuncByParameter(Type param1, Type param2, Type param3, Type returnType, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("ExpressStaticFunc3").MakeGenericMethod(param1, param2, param3, returnType);

            return creator.Invoke(null, new[] { method });
        }

        public static dynamic ExpressStaticFuncByParameter(Type param1, Type param2, Type param3, Type param4, Type returnType, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("ExpressStaticFunc4").MakeGenericMethod(param1, param2, param3, param4, returnType);

            return creator.Invoke(null, new[] { method });
        }

        public static dynamic ExpressStaticFuncByParameter(Type param1, Type param2, Type param3, Type param4, Type param5, Type returnType, MethodInfo method)
        {
            var creator = typeof(ReflectionTools).GetMethod("ExpressStaticFunc5").MakeGenericMethod(param1, param2, param3, param4, param5, returnType);

            return creator.Invoke(null, new[] { method });
        }











        /* -------------------------------------------------------------------------- */
        /*                                   Field                                   */
        /* -------------------------------------------------------------------------- */
        public static Action FieldSetterWrapperAction(object target, PropertyInfo propertyInfo)
        {
            return (Action)Delegate.CreateDelegate(typeof(Action), target, propertyInfo.GetSetMethod());
        }

        public static Action<T> FieldSetterWrapperAction<T>(object target, PropertyInfo propertyInfo)
        {
            return (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), target, propertyInfo.GetSetMethod());
        }

        public static Action<T1, T2> FieldSetterWrapperAction<T1, T2>(object target, PropertyInfo propertyInfo)
        {
            return (Action<T1, T2>)Delegate.CreateDelegate(typeof(Action<T1, T2>), target, propertyInfo.GetSetMethod());
        }

        public static Action<T1, T2, T3> FieldSetterWrapperAction<T1, T2, T3>(object target, PropertyInfo propertyInfo)
        {
            return (Action<T1, T2, T3>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3>), target, propertyInfo.GetSetMethod());
        }

        public static Action<T1, T2, T3, T4> FieldSetterWrapperAction<T1, T2, T3, T4>(object target, PropertyInfo propertyInfo)
        {
            return (Action<T1, T2, T3, T4>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4>), target, propertyInfo.GetSetMethod());
        }

        public static Action<T1, T2, T3, T4, T5> FieldSetterWrapperAction<T1, T2, T3, T4, T5>(object target, PropertyInfo propertyInfo)
        {
            return (Action<T1, T2, T3, T4, T5>)Delegate.CreateDelegate(typeof(Action<T1, T2, T3, T4, T5>), target, propertyInfo.GetSetMethod());
        }









        public static MethodInfo[] GetAllMethods(this Type type)
        {
            return type.GetMethods(BindingFlags_All);
        }

        public static MethodInfo[] GetAllMethodsIncludingBases(this Type type)
        {
            Type currentType = type;
            List<MethodInfo> all = new List<MethodInfo>();

            while (currentType != null)
            {
                all.AddRange(GetAllMethods(currentType));
                currentType = currentType.BaseType;
            }

            return all.ToArray();
        }

        public static PropertyInfo[] GetAllProperties(this Type type)
        {
            return type.GetProperties(BindingFlags_All);
        }

        // public static PropertyInfo[] GetAllPropertiesIncludingBases(this Type type)
        // {
        //     Type currentType = type;
        //     List<PropertyInfo> all = new List<PropertyInfo>();

        //     while (currentType != null)
        //     {
        //         all.AddRange(GetAllProperties(currentType));
        //         currentType = currentType.BaseType;
        //     }

        //     return all.ToArray();
        // }




        public static MethodInfo GetMethodFromAll(this Type type, string name)
        {
            return type.GetMethod(name, BindingFlags_All);
        }

        public static MethodInfo GetMethodFromAllIncludingBases(this Type type, string name)
        {
            Type currentType = type;
            MethodInfo value = null;

            while (value == null && currentType != null)
            {
                value = GetMethodFromAll(currentType, name);
                currentType = currentType.BaseType;
            }

            return value;
        }

        public static PropertyInfo GetPropertyFromAll(this Type type, string name)
        {
            return type.GetProperty(name, BindingFlags_All);
        }

        public static FieldInfo GetFieldFromAll(this Type type, string name)
        {
            return type.GetField(name, BindingFlags_All);
        }

        public static FieldInfo GetFieldFromAllIncludingBases(this Type type, string name)
        {
            Type currentType = type;
            FieldInfo value = null;

            while (value == null && currentType != null)
            {
                value = GetFieldFromAll(currentType, name);
                currentType = currentType.BaseType;
            }

            return value;
        }

        // public static PropertyInfo GetPropertyFromAllIncludingBases(this Type type, string name)
        // {
        //     Type currentType = type;
        //     PropertyInfo value = null;

        //     while (value == null && currentType != null)
        //     {
        //         value = GetPropertyFromAll(currentType, name);
        //         currentType = currentType.BaseType;
        //     }

        //     return value;
        // }



        /* -------------------------------------------------------------------------- */
        /*                                     BindingFlags                                     */
        /* -------------------------------------------------------------------------- */
        public static readonly BindingFlags BindingFlags_All = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
        public static readonly BindingFlags BindingFlags_AllInstance = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
        public static readonly BindingFlags BindingFlags_AllStatic = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;
        public static readonly BindingFlags BindingFlags_Public = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
        public static readonly BindingFlags BindingFlags_PublicInstance = BindingFlags.Public | BindingFlags.Instance;
        public static readonly BindingFlags BindingFlags_PublicStatic = BindingFlags.Public | BindingFlags.Static;
        public static readonly BindingFlags BindingFlags_NonPublic = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
        public static readonly BindingFlags BindingFlags_NonPublicInstance = BindingFlags.NonPublic | BindingFlags.Instance;
        public static readonly BindingFlags BindingFlags_NonPublicStatic = BindingFlags.NonPublic | BindingFlags.Static;
    }
}