using System.Reflection;

namespace SP.Tools
{
    public static class ObjectExtensions
    {
        public static MethodInfo ActionReadIntoMethodInfo(this object obj)
        {
            return (MethodInfo)obj.GetType().GetProperty("Method").GetValue(obj);
        }
    }
}