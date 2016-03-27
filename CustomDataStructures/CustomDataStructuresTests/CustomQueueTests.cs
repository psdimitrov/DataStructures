namespace CustomDataStructures.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CustomQueueTests
    {
        private CustomQueue<int> queue = new CustomQueue<int>();

        [TestMethod]
        public void CustomQueueTest_CreateQueueWithCollection5Elements_FiveElementsInQueue()
        {
            this.queue = new CustomQueue<int>(new []{1,2,3,4,5});

            Assert.AreEqual(5, this.queue.Count);
        }

        [TestMethod]
        public void EnqueueTest_EnqueuedTwoElements_TwoElementsInQueue()
        {
            this.queue.Enqueue(1);
            this.queue.Enqueue(2);

            Assert.AreEqual(2, this.queue.Count);
        }

        [TestMethod]
        public void EnqueueTest_EnqueuedTwelveElements_TwelveElementsInQueue()
        {
            this.queue.Enqueue(1);
            this.queue.Enqueue(2);
            this.queue.Enqueue(3);
            this.queue.Enqueue(4);
            this.queue.Enqueue(5);
            this.queue.Enqueue(6);
            this.queue.Enqueue(7);
            this.queue.Enqueue(8);
            this.queue.Enqueue(9);
            this.queue.Enqueue(10);
            this.queue.Enqueue(11);
            this.queue.Enqueue(12);

            Assert.AreEqual(12, this.queue.Count);
        }

        [TestMethod]
        public void DequeueTest_EnqueuedTwoElementsDequeueOne_OneElementInQueue()
        {
            this.queue.Enqueue(1);
            this.queue.Enqueue(2);

            this.queue.Dequeue();

            Assert.AreEqual(1, this.queue.Count);
        }

        [TestMethod]
        public void DequeueTest_EnqueuedAndDequeueOneElement_ElementDequeued()
        {
            this.queue.Enqueue(10);            

            var element = this.queue.Dequeue();

            Assert.AreEqual(10, element);
        }

        [TestMethod]
        public void DequeueTest_EnqueuedThreeDequeueOneElement_ElementDequeued()
        {
            this.queue.Enqueue(10);
            this.queue.Enqueue(20);
            this.queue.Enqueue(30);

            var element = this.queue.Dequeue();

            Assert.AreEqual(10, element);
        }
    }
}