using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Tools
{
    public class ObjectPool<T> where T : new()
    {
        public Stack<T> stack = new Stack<T>();

        public virtual T Get()
        {
            T obj = stack.Count == 0 ? Generation() : stack.Pop();

            return obj;
        }

        public virtual void Recover(T obj)
        {
            stack.Push(obj);
        }

        public virtual T Generation()
        {
            return new T();
        }
    }

    public class StringBuilderPool : ObjectPool<StringBuilder>
    {
        public override void Recover(StringBuilder obj)
        {
            obj.Clear();

            base.Recover(obj);
        }
    }
}