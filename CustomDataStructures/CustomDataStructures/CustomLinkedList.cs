namespace CustomDataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CustomLinkedList<T> : IList<T>
    {
        private Node head;

        private Node tail;

        private int count;

        public CustomLinkedList()
        {
            this.head = null;
            this.tail = null;
            this.count = 0;
        }

        public CustomLinkedList(IEnumerable<T> collection)
            : this()
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        } 

        public int Count => this.count;

        public bool IsReadOnly { get; }

        public T this[int index]
        {
            get
            {
                this.CheckIndex(index);

                var nodeByIndex = this.FindNodeByIndex(index);

                return nodeByIndex.Value;
            }

            set
            {
                this.CheckIndex(index);

                var nodeByIndex = this.FindNodeByIndex(index);

                nodeByIndex.Value = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            if (this.head == null)
            {
                Node newNode = new Node(item);
                this.head = newNode;
                this.tail = this.head;
            }
            else
            {
                Node newNode = new Node(item, this.tail);
                this.tail = newNode;
            }

            this.count++;
        }

        public void Clear()
        {
            this.head = null;
            this.tail = this.head;
            this.count = 0;
        }

        public bool Contains(T item)
        {
            var isFound = this.IndexOf(item) != -1;

            return isFound;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex + this.count > array.Length || arrayIndex < 0)
            {
                throw new IndexOutOfRangeException();
            }

            var currentNode = this.head;
            while (currentNode != null)
            {
                array[arrayIndex] = currentNode.Value;
                currentNode = currentNode.NextNode;
                arrayIndex++;
            }
        }

        public bool Remove(T item)
        {
            if (this.head == null)
            {
                return false;
            }
            else if (this.head.Value.Equals(item))
            {
                this.head = this.head.NextNode;
                this.count--;
                return true;
            }
            else if (this.tail.Value.Equals(item))
            {
                this.tail = this.FindNodeByIndex(this.count - 2);
                this.tail.NextNode = null;
                return true;

            }
            else {
                var currentNode = this.head.NextNode;
                Node previousNode = this.head;
                while (currentNode != null)
                {
                    if (currentNode.Value.Equals(item))
                    {
                        previousNode.NextNode = currentNode.NextNode;
                        this.count--;
                        return true;
                    }

                    previousNode = currentNode;
                    currentNode = currentNode.NextNode;
                }
            }

            return false;
        }

        public int IndexOf(T item)
        {
            var currentNode = this.head;
            var currentIndex = 0;

            while (currentNode != null)
            {
                if (currentNode.Value.Equals(item))
                {
                    return currentIndex;
                }

                currentNode = currentNode.NextNode;
                currentIndex++;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.CheckIndex(index);

            if (index == 0)
            {
                var nodeToInsert = new Node(item);
                nodeToInsert.NextNode = this.head;
                this.head = nodeToInsert;
                this.count++;
            }
            else
            {
                var currentIndex = 0;
                var currentNode = this.head;
                Node previousNode = null;
                while (currentIndex != index)
                {
                    previousNode = currentNode;
                    currentNode = currentNode.NextNode;
                    currentIndex++;
                }

                var nodeToInsert = new Node(item, previousNode);
                nodeToInsert.NextNode = currentNode;
                this.count++;
            }
        }

        public void RemoveAt(int index)
        {
            this.CheckIndex(index);

            if (index == 0)
            {
                this.head = this.head.NextNode;
                this.count--;
            }
            else
            {
                var previousNode = this.FindNodeByIndex(index - 1);
                previousNode.NextNode = previousNode.NextNode.NextNode;
                this.count--;
            }
        }

        private Node FindNodeByIndex(int index)
        {
            var currentIndex = 0;
            var currentNode = this.head;
            while (currentIndex < index)
            {
                currentNode = currentNode.NextNode;
                currentIndex++;
            }

            return currentNode;
        }

        private void CheckIndex(int index)
        {
            if (index >= this.count || index < 0)
            {
                throw new IndexOutOfRangeException("Invalid index: " + index);
            }
        }

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.NextNode = null;
            }

            public Node(T value, Node prevNode)
            {
                this.Value = value;
                prevNode.NextNode = this;
            }

            public T Value { get; set; }

            public Node NextNode { get; set; }
        }
    }
}
