using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SP.Tools
{
    public static class AttributeGetter
    {
        public static bool MatchAttribute<attributeT, classT>() => MatchAttribute(typeof(classT), typeof(attributeT));

        public static bool MatchAttribute(MemberInfo classType, Type attributeType)
        {
            if (classType == null)
                throw new ArgumentNullException($"{MethodGetter.GetLastAndCurrentMethodName()}: {nameof(classType)} 值不能为空");

            if (attributeType == null)
                throw new ArgumentNullException($"{MethodGetter.GetLastAndCurrentMethodName()}: {nameof(attributeType)} 值不能为空");

            Attribute att;

            try
            {
                att = Attribute.GetCustomAttribute(classType, attributeType, true);
            }
            catch (TypeLoadException ex)
            {
                throw new Exception($"获取 {classType.Name} 的特性 {attributeType.FullName} 时失败, 原因如下:\n{ex}");
            }
            catch (Exception ex)
            {
                throw new Exception($"获取特性时失败, 原因如下:\n{ex}");
            }

            return att != null;
        }

        public static bool TryGetAttribute<T>(MemberInfo member, out T attribute) where T : Attribute
        {
            attribute = member.GetCustomAttribute<T>();

            return attribute != null;
        }

        public static bool TryGetAttribute(MemberInfo member, Type attributeType, out Attribute attribute)
        {
            attribute = member.GetCustomAttribute(attributeType);

            return attribute != null;
        }


        public static string GetChineseAttributeValue(MemberInfo mem) => GetAttribute<ChineseNameAttribute>(mem)?.chineseName;



        public static T GetAttribute<T>(MemberInfo mem) where T : Attribute => GetAttributeInAttributes<T>(Attribute.GetCustomAttributes(mem, true));

        public static T GetAttributeInAttributes<T>(Attribute[] attributesGet) where T : Attribute
        {
            foreach (Attribute attribute in attributesGet)
                if (attribute is T t)
                    return t;

            return null;
        }
    }
}
