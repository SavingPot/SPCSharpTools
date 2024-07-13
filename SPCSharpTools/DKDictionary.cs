using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SP.Tools
{
    public class DKDictionary<TKey1, TKey2, TValue>
    {
        public Dictionary<DoubleKeys<TKey1, TKey2>, TValue> pairs = new Dictionary<DoubleKeys<TKey1, TKey2>, TValue>();

        public TValue this[TKey1 k1, TKey2 k2]
        {
            get
            {
                for (int i = 0; i < pairs.Count; i++)
                {
                    var element = pairs.ElementAt(i);

                    if (object.Equals(element.Key.key1, k1) && object.Equals(element.Key.key2, k2))
                    {
                        return element.Value;
                    }
                }

                throw new ArgumentException("无法匹配到元素");
            }
            set
            {
                for (int i = 0; i < pairs.Count; i++)
                {
                    var element = pairs.ElementAt(i);

                    if (object.Equals(element.Key.key1, k1) && object.Equals(element.Key.key2, k2))
                    {
                        pairs[element.Key] = value;
                        return;
                    }
                }

                throw new ArgumentException("无法匹配到元素");
            }
        }

        public void Add(TKey1 k1, TKey2 k2, TValue v)
        {
            pairs.Add(new DoubleKeys<TKey1, TKey2>(k1, k2), v);
        }

        public void Remove(TKey1 k1, TKey2 k2)
        {
            for (int i = 0; i < pairs.Count; i++)
            {
                var element = pairs.ElementAt(i);

                if (object.Equals(element.Key.key1, k1) && object.Equals(element.Key.key2, k2))
                {
                    pairs.Remove(element.Key);
                }
            }
        }

        public int Count => pairs.Count;

        public bool ContainsKey(TKey1 k1, TKey2 k2)
        {
            for (int i = 0; i < pairs.Count; i++)
            {
                var element = pairs.ElementAt(i);

                if (object.Equals(element.Key.key1, k1) && object.Equals(element.Key.key2, k2))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class DoubleKeys<TKey1, TKey2>
    {
        public TKey1 key1;
        public TKey2 key2;

        public DoubleKeys(TKey1 k1, TKey2 k2)
        {
            this.key1 = k1;
            this.key2 = k2;
        }
    }
}