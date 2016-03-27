namespace CustomDataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CustomLinkedStack<T> : IEnumerable<T>
    {
        private Node<T> firstNode;

        public CustomLinkedStack()
        {            
        }

        public CustomLinkedStack(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Push(item);
            }
        }

        public int Count { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.firstNode;

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

        public void Push(T element)
        {
            this.firstNode = new Node<T>(element, this.firstNode);
            this.Count++;
        }

        public T Pop()
        {
            this.CheckIfEmpty();

            var result = this.firstNode.Value;
            this.firstNode = this.firstNode.NextNode;
            this.Count--;

            return result;
        }

        public T Peek()
        {
            this.CheckIfEmpty();

            var result = this.firstNode.Value;

            return result;
        }

        public bool Contains(T value)
        {
            var currentNode = this.firstNode;

            while (currentNode != null)
            {
                if (currentNode.Value.Equals(value))
                {
                    return true;
                }

                currentNode = currentNode.NextNode;
            }

            return false;
        }

        public void Clear()
        {
            this.firstNode = null;
            this.Count = 0;
        }

        public T[] ToArray()
        {
            var currentNode = this.firstNode;
            var currentIndex = 0;

            var resultArray = new T[this.Count];
            while (currentNode != null)
            {
                resultArray[currentIndex++] = currentNode.Value;
                currentNode = currentNode.NextNode;
            }

            return resultArray;
        }

        private void CheckIfEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Empty collection");
            }
        }

        private class Node<T>
        {
            public Node(T value, Node<T> nextNode = null)
            {
                this.Value = value;
                this.NextNode = nextNode;
            }

            public Node<T> NextNode { get; private set; }

            public T Value { get; private set; }
        }
    }
}
