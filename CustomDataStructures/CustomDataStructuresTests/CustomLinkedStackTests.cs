namespace CustomDataStructuresTests
{
    using System;
    using System.Linq;

    using CustomDataStructures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CustomLinkedStackTests
    {
        private CustomLinkedStack<int> stack;

        [TestInitialize]
        public void TestInitiallize()
        {
            this.stack = new CustomLinkedStack<int>();
        }

        [TestMethod]
        public void PushTest_OneElementPushed_CountIsOne()
        {
            this.stack.Push(5);

            Assert.AreEqual(1, this.stack.Count);
        }

        [TestMethod]
        public void PushTest_TwoElementsPushed_CountIsTwo()
        {
            this.stack.Push(5);
            this.stack.Push(1);

            Assert.AreEqual(2, this.stack.Count);
        }

        [TestMethod]
        public void CountTest_NoElementsPushed_CountIsZero()
        {

            Assert.AreEqual(0, this.stack.Count);
        }

        [TestMethod]
        public void CustomStackTest_OneElementPushedAndPopped_ElementPopped()
        {
            this.stack.Push(3);

            var element = this.stack.Pop();

            Assert.AreEqual(3, element, "Element is wrong");
        }

        [TestMethod]
        public void PopTest_PushedSeveralElementsPoppedTwoElements_PoppedElementBeforeLast()
        {
            this.stack.Push(9);
            this.stack.Push(99);
            this.stack.Push(1);

            this.stack.Pop();

            var element = this.stack.Pop();

            Assert.AreEqual(99, element, "Wrong element!");
        }

        [TestMethod]
        public void PopTest_PushedSixElementsPoppedThreeElements_CountIsThree()
        {
            this.stack.Push(9);
            this.stack.Push(99);
            this.stack.Push(1);
            this.stack.Push(100);
            this.stack.Push(19);
            this.stack.Push(1000);

            this.stack.Pop();
            this.stack.Pop();
            this.stack.Pop();

            Assert.AreEqual(3, this.stack.Count, "Wrong count!");
        }

        [TestMethod]
        public void ClearTest()
        {
            this.stack.Push(4);
            this.stack.Push(7);
            this.stack.Push(15);
            this.stack.Push(1);
            this.stack.Push(99);

            this.stack.Clear();
            Assert.AreEqual(0, this.stack.Count, "Wrong");
        }

        [TestMethod]
        public void PeekTest__PushedSeveralElementsPeekedOnce_PeekedLastElement()
        {
            this.stack.Push(1);
            this.stack.Push(4);
            this.stack.Push(7);
            this.stack.Push(15);
            this.stack.Push(1);
            this.stack.Push(99);

            var element = this.stack.Peek();
            Assert.AreEqual(99, element, "Wrong element peeked");
        }

        [TestMethod]
        public void PeekTest__PushedSeveralElementsPeekedOnce_CountIsSameAsCountElementsPushed()
        {
            this.stack.Push(1);
            this.stack.Push(4);
            this.stack.Push(7);
            this.stack.Push(15);
            this.stack.Push(1);
            this.stack.Push(99);

            var element = this.stack.Peek();
            Assert.AreEqual(6, this.stack.Count, "Wrong count after peek");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekTest_PeekOnEmptyStack_Exception()
        {
            this.stack.Peek();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopTest_PopOnEmptyStack_Exception()
        {
            this.stack.Pop();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekTest_PushedTwoPoppedTwoPeekOnEmptyStack_Exception()
        {
            this.stack.Push(1);
            this.stack.Push(2);
            this.stack.Pop();
            this.stack.Pop();
            this.stack.Peek();
        }

        [TestMethod]
        public void PushTest_PushedElevenItems_ElevenItemsInStack()
        {
            this.stack.Push(1);
            this.stack.Push(2);
            this.stack.Push(3);
            this.stack.Push(4);
            this.stack.Push(5);
            this.stack.Push(6);
            this.stack.Push(7);
            this.stack.Push(8);
            this.stack.Push(9);
            this.stack.Push(10);
            this.stack.Push(11);

            Assert.AreEqual(11, this.stack.Count);
        }

        [TestMethod]
        public void TestCtor_CustomStackCreatedWithCollection_StackCreated()
        {
            CustomLinkedStack<int> customStack = new CustomLinkedStack<int>(new[] { 1, 2, 3, 4, 5, 6 });

            Assert.AreEqual(6, customStack.Count);
        }

        [TestMethod]
        public void PushPop_EmptyStack_ShouldWorkCorrectly()
        {
            Assert.AreEqual(0, stack.Count);

            var element = 10;

            stack.Push(element);

            Assert.AreEqual(1, this.stack.Count);

            var poppedElement = this.stack.Pop();

            Assert.AreEqual(element, poppedElement);

            Assert.AreEqual(0, this.stack.Count);
        }

        [TestMethod]
        public void PushPop1000Elements_EmptyStack_ShouldWorkCorrectly()
        {
            var stringStack = new CustomLinkedStack<string>();

            Assert.AreEqual(0, stringStack.Count);

            for (int i = 1; i <= 1000; i++)
            {
                stringStack.Push(i.ToString());

                Assert.AreEqual(i, stringStack.Count);
            }

            for (int i = 1000; i > 0; i--)
            {
                var poppedElement = stringStack.Pop();

                Assert.AreEqual(i.ToString(), poppedElement);

                Assert.AreEqual(i - 1, stringStack.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopElement_EmptyStack_ShouldThrow()
        {
            this.stack.Pop();
        }

        [TestMethod]
        public void PushedToArray_EmptyStack_ShouldWorkCorrectly()
        {
            this.stack.Push(3);
            this.stack.Push(5);
            this.stack.Push(-2);
            this.stack.Push(7);

            var array = this.stack.ToArray();

            CollectionAssert.AreEqual(new int[] { 7, -2, 5, 3 }, array);
        }

        [TestMethod]
        public void ToArray_EmptyStack_ShouldReturnEmptyArray()
        {
            var dateStack = new CustomLinkedStack<DateTime>();
            var resultArray = dateStack.ToArray();

            CollectionAssert.AreEqual(new DateTime[] { }, resultArray);
        }
    }
}