using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SP.Tools
{
    public class DVDictionary<TKey, TValue1, TValue2>
    {
        public Dictionary<TKey, DoubleValues<TValue1, TValue2>> pairs = new Dictionary<TKey, DoubleValues<TValue1, TValue2>>();

        public DoubleValues<TValue1, TValue2> this[TKey k]
        {
            get
            {
                return pairs[k];
            }
            set
            {
                pairs[k] = value;
            }
        }

        public void Add(TKey k, TValue1 v1, TValue2 v2)
        {
            pairs.Add(k, new DoubleValues<TValue1, TValue2>(v1, v2));
        }
        
        public void Remove(TKey k)
        {
            pairs.Remove(k);
        }

        public int Count => pairs.Count;

        public bool ContainsKey(TKey k)
        {
            return pairs.ContainsKey(k);
        }
    }

    public class DoubleValues<TValue1, TValue2>
    {
        public TValue1 value1;
        public TValue2 value2;

        public DoubleValues(TValue1 k1, TValue2 k2)
        {
            this.value1 = k1;
            this.value2 = k2;
        }
    }
}