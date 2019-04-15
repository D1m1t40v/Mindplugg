using System;
using System.Collections;
using System.Collections.Generic;

namespace Mindplugg
{
    public class CircularBuffer<T> : IEnumerable<T>
    {
        public CircularBuffer(int capacity)
        {
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        #region IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
