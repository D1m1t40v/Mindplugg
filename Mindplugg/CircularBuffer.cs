using System.Collections;
using System.Collections.Generic;

namespace Mindplugg
{
    public class CircularBuffer<T> : IEnumerable<T>
    {
        private int headIndex = -1;
        private int tailIndex = 0;
        private T[] tab;
        private bool hasBeenFull = false;

        public readonly int Size;

        public CircularBuffer(int capacity)
        {
            Size = capacity;
            tab = new T[Size];
        }

        public void Add(T item)
        {
            headIndex++;
            if (hasBeenFull)
            {
                tailIndex++;
            }

            if (headIndex >= tab.Length)
            {
                headIndex = 0;
                tailIndex = 1;
                hasBeenFull = true;
            }

            tab[headIndex] = item;
        }

        public void Clear()
        {
            tab = new T[Size];
            headIndex = -1;
            tailIndex = 0;
        }

        #region IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new BufferEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BufferEnumerator(this);
        }

        #endregion

        #region IEnumerator
        private class BufferEnumerator : IEnumerator<T>
        {
            private CircularBuffer<T> _parentBuffer;
            private int readIndex;
            private bool ignoreFirstHeadIndex;

            public BufferEnumerator(CircularBuffer<T> parentBuffer)
            {
                _parentBuffer = parentBuffer;
                readIndex = _parentBuffer.tailIndex - 1;
                ignoreFirstHeadIndex = _parentBuffer.hasBeenFull;
            }

            public T Current => _parentBuffer.tab[readIndex];

            object IEnumerator.Current => _parentBuffer.tab[readIndex];

            public void Dispose() { }

            public bool MoveNext()
            {
                if (readIndex == _parentBuffer.headIndex)
                {
                    if (ignoreFirstHeadIndex)
                    {
                        ignoreFirstHeadIndex = false;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (readIndex >= _parentBuffer.Size - 1)
                {
                    readIndex = 0;
                    return true;
                }

                readIndex++;
                return true;
            }

            public void Reset()
            {
                readIndex = _parentBuffer.tailIndex - 1;
            }
        }
        #endregion IEnumerator
    }
}