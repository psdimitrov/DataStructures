namespace CustomDataStructuresTests
{
    using CustomDataStructures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CustomListTests
    {
        private CustomList<int> list = new CustomList<int>();

        [TestMethod]
        public void InsertTest()
        {
            this.list = new CustomList<int>(new[] { 1, 2, 3, 4, 5 });

            this.list.Insert(3, 100);

            Assert.AreEqual(100, this.list[3]);
        }

        [TestMethod]
        public void AddTest()
        {
            this.list.Add(1);
            this.list.Add(2);
            this.list.Add(3);
            this.list.Add(4);
            this.list.Add(5);

            Assert.AreEqual(5, this.list.Count);
        }

        [TestMethod]
        public void CustomListTest()
        {
            this.list = new CustomList<int>(new[] { 1, 2, 3, 4, 5, 6 });

            Assert.AreEqual(6, this.list.Count);
        }

        [TestMethod]
        public void CustomListTest_CreatedCustomListWithCapacityFourAddedFiveElements_ListResized()
        {
            this.list = new CustomList<int>(4);

            this.list.Add(1);
            this.list.Add(2);
            this.list.Add(3);
            this.list.Add(4);
            this.list.Add(5);

            Assert.AreEqual(5, this.list.Count);
        }

        [TestMethod]
        public void ClearTest_CreatedAndCleared_ListCleared()
        {
            this.list = new CustomList<int>(new[] { 10, 20, 30, 40, 50 });

            this.list.Clear();

            Assert.AreEqual(0, this.list.Count);
        }

        [TestMethod]
        public void RemoveAtTest_RemoveElement_ElementRemoved()
        {
            this.list = new CustomList<int>(new[] { 10, 20, 30, 40, 50 });

            this.list.RemoveAt(3);

            Assert.AreEqual(4, this.list.Count);
        }

        [TestMethod]
        public void RemoveAtTest_RemoveElementAtIndex3_ElementThatWasIndex4NowIsAtIndex3()
        {
            this.list = new CustomList<int>(new[] { 10, 20, 30, 40, 50 });

            this.list.RemoveAt(3);

            Assert.AreEqual(50, this.list[3]);
        }

        [TestMethod]
        public void RemoveAtTest_RemoveElementAtIndex0_ElementThatWasIndex1NowIsAtIndex0()
        {
            this.list = new CustomList<int>(new[] { 10, 20, 30, 40, 50 });

            this.list.RemoveAt(0);

            Assert.AreEqual(20, this.list[0]);
        }

        [TestMethod]
        public void RemoveAtTest_RemoveElementAtLastIndex_ElementsAreOneLess()
        {
            this.list = new CustomList<int>(new[] { 10, 20, 30, 40, 50 });

            this.list.RemoveAt(this.list.Count - 1);

            Assert.AreEqual(4, this.list.Count);
        }

        [TestMethod]
        public void RemoveTest_CreatedListRemovedElement_ElementRemoved()
        {
            this.list = new CustomList<int>(new[] { 10, 20, 30, 40, 50 });

            this.list.Remove(10);

            Assert.AreEqual(20, this.list[0]);
        }

        [TestMethod]
        public void RemoveTest_CreatedListRemoveElement_ElementRemovedThirdElementSecond()
        {
            this.list = new CustomList<int>(new[] { 10, 20, 30, 40, 50 });

            this.list.Remove(10);

            Assert.AreEqual(30, this.list[1]);
        }

        [TestMethod]
        public void CopyToTest_CopySixElementsFromIndex4_FirstElementIsAtIndexFour()
        {
            this.list.Add(10);
            this.list.Add(20);
            this.list.Add(30);
            this.list.Add(40);
            this.list.Add(50);
            this.list.Add(60);

            int[] array = new int[10];
            this.list.CopyTo(array, 4);

            Assert.AreEqual(10, array[4]);
        }

        [TestMethod]
        public void CopyToTest_CopySixElementsFromIndex4_LastElementIsAtIndexNine()
        {
            this.list.Add(10);
            this.list.Add(20);
            this.list.Add(30);
            this.list.Add(40);
            this.list.Add(50);
            this.list.Add(60);

            int[] array = new int[10];
            this.list.CopyTo(array, 4);

            Assert.AreEqual(60, array[9]);
        }
    }
}