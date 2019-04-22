using System;
using System.Linq;
using Mindplugg;
using Xunit;

namespace Mingplugg
{
    public class OtherCircularBufferTest
    {
        [Fact]
        public void _1_Should_be_able_to_add_and_get_items_back()
        {
            OtherCircularBuffer<char> collection = new OtherCircularBuffer<char>(42);

            foreach (char c in "mindplugg")
            {
                collection.Add(c);
            }

            Assert.Equal("mindplugg", new string(collection.ToArray()));
        }

        [Fact]
        public void _1_Clear_Should_clear_items()
        {
            OtherCircularBuffer<char> collection = new OtherCircularBuffer<char>(42);

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
            OtherCircularBuffer<char> collection = new OtherCircularBuffer<char>(5);

            foreach (char c in "mindplugg")
            {
                collection.Add(c);
            }

            Assert.Equal("plugg", new string(collection.ToArray()));
        }

        [Fact]
        public void _2_Add_Should_be_thread_safe()
        {
            OtherCircularBuffer<char> collection = new OtherCircularBuffer<char>(26);

            for (int i = 0; i < 100; ++i)
            {
                "azertyuiopqsdfghjklmwxcvbn".AsParallel().ForAll(collection.Add);
                "azertyuiopqsdfghjklmwxcvbn".AsParallel().ForAll(collection.Add);

                Assert.All(collection, c => Assert.Contains(c, "azertyuiopqsdfghjklmwxcvbn"));

                collection.Clear();
            }
        }

        [Fact]
        public void _B_Clear_Should_remove_item_references()
        {
            OtherCircularBuffer<object> collection = new OtherCircularBuffer<object>(42);

            object o = new object();
            WeakReference reference = new WeakReference(o);

            collection.Add(o);

            o = null;
            collection.Clear();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Assert.False(reference.IsAlive, "reference is still alive, switch in Release configuration is this is not expected");
        }
    }
}
