using System;
using System.Linq;
using Mindplugg;
using Xunit;

namespace Mingplugg
{
    public class CircularBufferTest
    {
        [Fact]
        public void _1_Should_be_able_to_add_and_get_items_back()
        {
            CircularBuffer<char> collection = new CircularBuffer<char>(42);

            foreach (char c in "mindplugg")
            {
                collection.Add(c);
            }

            Assert.Equal("mindplugg", new string(collection.ToArray()));
        }

        [Fact]
        public void _1_Clear_Should_clear_items()
        {
            CircularBuffer<char> collection = new CircularBuffer<char>(42);

            foreach (char c in "mindplugg")
            {
                collection.Add(c);
            }

            collection.Clear();

            Assert.Equal(string.Empty, new string(collection.ToArray()));
        }

        [Fact]
        public void _1_Add_Should_overwrite_oldest_item_When_full()
        {
            CircularBuffer<char> collection = new CircularBuffer<char>(5);

            foreach (char c in "mindplugg")
            {
                collection.Add(c);
            }

            Assert.Equal("plugg", new string(collection.ToArray()));
        }

        [Fact]
        public void _2_Add_Should_be_thread_safe()
        {
            CircularBuffer<char> collection = new CircularBuffer<char>(26);

            "azertyuiopqsdfghjklmwxcvbn".AsParallel().ForAll(collection.Add);
            "azertyuiopqsdfghjklmwxcvbn".AsParallel().ForAll(collection.Add);

            Assert.All(collection, c => Assert.Contains(c, "azertyuiopqsdfghjklmwxcvbn"));
        }

        [Fact]
        public void _B_Clear_Should_remove_item_references()
        {
            CircularBuffer<object> collection = new CircularBuffer<object>(42);

            object o = new object();
            WeakReference reference = new WeakReference(o);

            collection.Add(o);

            o = null;
            collection.Clear();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Assert.False(reference.IsAlive);
        }
    }
}
