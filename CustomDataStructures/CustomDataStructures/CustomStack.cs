namespace CustomDataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CustomStack<T> : IEnumerable<T>
    {
        private const int DefaultCapacity = 8;

        private T[] stackElements;

        private int capacity;

        private int count;

        public CustomStack(int capacity = DefaultCapacity)
        {
            this.capacity = capacity;
            this.stackElements = new T[this.capacity];
            this.Count = 0;
        }

        public CustomStack(IEnumerable<T> collection)
            : this()
        {
            foreach (var item in collection)
            {
                this.Push(item);
            }
        } 

        public int Count
        {
            get
            {
                return this.count;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Count cannot be negative!");
                }

                this.count = value;
            }
        }

        public bool IsEmpty => this.count == 0;

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                var element = this.stackElements[i];

                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Push(T element)
        {
            if (this.Count == this.capacity)
            {
                this.Resize();
            }

            this.stackElements[this.Count++] = element;
        }

        public T Pop()
        {
            this.CheckIfEmpty();

            T element = this.stackElements[--this.Count];

            this.stackElements[this.Count] = default(T);

            return element;
        }

        public T Peek()
        {
            this.CheckIfEmpty();

            T element = this.stackElements[this.Count - 1];

            return element;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < this.count; i++)
            {
                if (this.stackElements.Equals(item))
                {
                    return true;
                }                
            }

            return false;
        }

        public void Clear()
        {
            Array.Clear(this.stackElements, 0, this.count);

            this.Count = 0;
        }

        private void Resize()
        {
            this.capacity *= 2;

            T[] extendedArray = new T[this.capacity];

            Array.Copy(this.stackElements, extendedArray, this.Count);

            this.stackElements = extendedArray;
        }

        private void CheckIfEmpty()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException("Empty collection!");
            }
        }
    }
}
