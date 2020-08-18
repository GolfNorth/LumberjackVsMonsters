using System.Collections.Generic;

namespace LumberjackVsMonsters
{
    public sealed class ObjectPool<T> where T : IPoolable, new()
    {
        private readonly Queue<T> _queue;
        private readonly HashSet<T> _pool;
        private int _count;

        public int Count => _count;

        public IEnumerable<T> All => _pool;

        public ObjectPool()
        {
            _queue = new Queue<T>();
            _pool = new HashSet<T>();
            _count = 0;
        }

        public T Acquire()
        {
            T obj;

            if (_queue.Count == 0)
            {
                obj = new T();
                obj.Initialize();
                obj.IsEnabled = true;
                _pool.Add(obj);
            }
            else
            {
                obj = _queue.Dequeue();
                obj.Enable();
                obj.IsEnabled = true;
            }

            _count++;

            return obj;
        }

        public void Release(T obj)
        {
            obj.Disable();
            obj.IsEnabled = false;
            _queue.Enqueue(obj);

            _count--;
        }
    }
}