using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SP.Tools
{
    public static class MethodGetter
    {
        public static TimeSpan TimeTest(Action action)
        {
            DateTime oldTime = System.DateTime.Now;

            action();

            return System.DateTime.Now.Subtract(oldTime);
        }
        
        public static double TimeTestToMs(Action action)
        {
            return TimeTest(action).TotalMilliseconds;
        }
        
        public static double TimeTestToS(Action action)
        {
            return TimeTest(action).TotalSeconds;
        }

        public static string GetLastAndCurrentMethodName()
        {
            StackTrace st = new StackTrace();
            return st.GetFrame(2).GetMethod().Name + "->" + st.GetFrame(1).GetMethod().Name;
        }

        public static string GetLastAndCurrentMethodPath()
        {
            StackTrace st = new StackTrace();

            StackFrame frame1 = st.GetFrame(1);
            StackFrame frame2 = st.GetFrame(2);

            MethodBase method1 = frame1?.GetMethod();
            MethodBase method2 = frame2?.GetMethod();

            return GetMethodPath(method2) + "->" + GetMethodPath(method1);
        }

        public static string GetLastMethodName() => new StackTrace().GetFrame(2).GetMethod().Name;

        public static string GetCurrentMethodName() => new StackTrace().GetFrame(1).GetMethod().Name;

        public static string GetLastMethodPath()
        {
            StackTrace st = new StackTrace();

            StackFrame frame = st.GetFrame(2);
            MethodBase method = frame?.GetMethod();

            return GetMethodPath(method);
        }

        public static string GetCurrentMethodPath()
        {
            StackTrace st = new StackTrace();

            StackFrame frame = st.GetFrame(1);
            MethodBase method = frame?.GetMethod();

            return GetMethodPath(method);
        }

        public static string GetMethodPath(MethodBase method)
        {
            if (method == null)
                return null;

            Type type = method.DeclaringType;
            string typeFullName = type.FullName;
            string basicPath = $"{typeFullName}.{method.Name}";
            string methodName = method.Name;

            return $"{GetMemberName(type)}.{GetMemberName(method)}";
        }

        public static MethodBase GetRealMethod(MethodBase mtd)
        {
            var generatedType = mtd.DeclaringType;

            if (generatedType.GetInterfaces().Any(i => i == typeof(IAsyncStateMachine)))
            {
                var originalType = generatedType.DeclaringType;
                var foundMethod = originalType.GetMethods()
                    .Single(m => m.GetCustomAttribute<AsyncStateMachineAttribute>()?.StateMachineType == generatedType);

                return foundMethod;
            }
            else
            {
                return mtd;
            }
        }

        public static string GetMemberName(MemberInfo mem)
        {
            if (AttributeGetter.MatchAttribute(mem, typeof(ChineseNameAttribute)))
                return $"{AttributeGetter.GetChineseAttributeValue(mem)}";

            return mem.Name;
        }

        public static string GetMemberName(Type mem)
        {
            if (AttributeGetter.MatchAttribute(mem, typeof(ChineseNameAttribute)))
                return $"{mem.Namespace}.{AttributeGetter.GetChineseAttributeValue(mem)}";

            return mem.FullName;
        }

        public static string GetMemberName(MethodBase mtd)
        {
            mtd = GetRealMethod(mtd);

            if (AttributeGetter.MatchAttribute(mtd, typeof(ChineseNameAttribute)))
                return $"{AttributeGetter.GetChineseAttributeValue(mtd)}";

            return mtd.Name;
        }
    }
}
