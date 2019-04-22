using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Mindplugg
{
    public class OtherCircularBuffer<T> : IEnumerable<T>
    {
        private int counter = 0;
        ConcurrentQueue<T> queue;

        public readonly int Size;

        public OtherCircularBuffer(int capacity)
        {
            Size = capacity;
            queue = new ConcurrentQueue<T>();
        }

        public void Add(T item)
        {
            queue.Enqueue(item);
            counter++;
            if(counter > Size)
            {
                if(queue.TryDequeue(out _))
                {
                    counter--;
                }
            }
        }

        public void Clear()
        {
            queue = new ConcurrentQueue<T>();
        }
        
        #region IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return queue.GetEnumerator();
        }
        #endregion        
    }
}
