using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SP.Tools
{
    public class EventList<T> : ICollection<T>, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>
    {
        public List<T> ts = new List<T>();
        public Action OnClear = () => { };
        public Action<T> OnAdd = (value) => { };
        public Action<int, T> OnInsert = (index, value) => { };
        public Action<T> OnRemove = (value) => { };
        public Action<int> OnRemoveAt = (index) => { };
        public Action<T[], int> OnCopyTo = (values, index) => { };
        public Action<T[]> OnAddRange = (values) => { };
        public Action<T> OnSetValue = (value) => { };

        public T this[int index]
        {
            get
            {
                return ts[index];
            }
            set
            {
                ts[index] = value;
                OnSetValue(value);
            }
        }

        public void Clear()
        {
            ts.Clear();
            OnClear();
        }

        public void Add(T value)
        {
            ts.Add(value);
            OnAdd(value);
        }

        public void Insert(int index, T value)
        {
            ts.Insert(index, value);
            OnInsert(index, value);
        }

        public bool Remove(T value)
        {
            bool b = ts.Remove(value);

            OnRemove(value);
            return b;
        }

        public void RemoveAt(int index)
        {
            ts.RemoveAt(index);

            OnRemoveAt(index);
        }

        public void CopyTo(T[] values, int index)
        {
            ts.CopyTo(values, index);

            OnCopyTo(values, index);
        }

        public void AddRange(T[] values)
        {
            ts.AddRange(values);

            OnAddRange(values);
        }

        public List<ConvertT> ConvertAll<ConvertT>(Converter<T, ConvertT> converter)
        {
            return ts.ConvertAll(converter);
        }

        public int IndexOf(T value)
        {
            return ts.IndexOf(value);
        }

        public bool IsReadOnly => ((ICollection<T>)ts).IsReadOnly;

        public int Count => ts.Count;

        public bool Contains(T value)
        {
            return ts.Contains(value);
        }

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)ts).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}