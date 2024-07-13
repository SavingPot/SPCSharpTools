using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Tools
{
    public static class IEnumerableExtensions
    {
        public static bool ArrayEqual<T>(T[] first, T[] second)
        {
            for (int i = 0; i < first.Length; i++)
                if (!Equals(first[i], second[i]))
                    return false;

            return true;
        }

        [ChineseName("抽取")] public static TSource Extract<TSource>(this IList<TSource> sources, Random random) => sources[random.Next(sources.Count)];

        [ChineseName("检查NULL")]
        public static void CheckNull<TSource>(this IList<TSource> sources)
        {
            for (int i = 0; i < sources.Count; i++)
                if (sources[i] == null)
                    sources.RemoveAt(i);
        }

        public static void AddIfNone<TSource>(this List<TSource> ts, TSource source)
        {
            if (!ts.Contains(source))
                ts.Add(source);
        }

        public static string ElementsString<T>(this IEnumerable<T> ts)
        {
            if (ts == null)
                return null;

            int count = ts.Count();
            StringBuilder sb = new($"ElementCount: {count} [");

            for (int i = 0; i < count; i++)
            {
                sb.Append(ts.ElementAt(i));
            }

            sb.Append("]");
            return sb.ToString();
        }

        public static void For<T>(this IEnumerable<T> ts, Action<T> action)
        {
            int count = ts.Count();

            for (int i = 0; i < count; i++)
            {
                action.Invoke(ts.ElementAt(i));
            }
        }

        public static void ReverseFor<T>(this IEnumerable<T> ts, Action<T> action)
        {
            int count = ts.Count();

            for (int i = count - 1; i >= 0; i--)
            {
                action.Invoke(ts.ElementAt(i));
            }
        }

        public static List<T> Shuffle<T>(this IEnumerable<T> source,Random random)
        {
            List<T> list = source.ToList();

            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = random.Next(i + 1);
                T temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }

            return list;
        }

        public static void Foreach<T>(this IEnumerable<T> ts, Action<T> action)
        {
            int count = ts.Count();

            foreach (var item in ts)
            {
                action.Invoke(item);
            }
        }

        public static List<object> ForceToObjectList<TSource>(this List<TSource> sources) => sources.ConvertAll(s => (object)s);

        public static List<TSource> ForceToTargetTypeList<TSource>(this List<object> objects) => objects.ConvertAll(s => (TSource)s);

        public static List<object> ToObjectList<TSource>(this List<TSource> sources) => sources.ConvertAll(s => s as object);

        public static List<TSource> ToTargetTypeList<TSource>(this List<object> objects) where TSource : class => objects.ConvertAll(s => s as TSource);

        public static bool TryNullGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, out TValue value, TValue defaultValue)
        {
            if (key == null)
            {
                value = defaultValue;
                return false;
            }

            return dic.TryGetValue(key, out value);
        }

        public static bool IncludeAll<TBig, TSmall>(this IEnumerable<TBig> big, IEnumerable<TSmall> small, Func<TBig, TSmall, bool> equal)
        {
            return small.All(s => big.Any(b => equal(b, s)));
        }

        public static bool IncludeAny<TBig, TSmall>(this IEnumerable<TBig> big, IEnumerable<TSmall> small, Func<TBig, TSmall, bool> equal)
        {
            return small.Any(s => big.Any(b => equal(b, s)));
        }

        public static bool Include<T>(this IEnumerable<T> big, T small, Func<T, T, bool> equal)
        {
            return big.Any(b => equal(b, small));
        }
    }
}
