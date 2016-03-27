namespace CustomDataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CustomQueue<T> : IEnumerable<T>
    {
        private const int DefaultCapacity = 10;

        private T[] queueElements = new T[DefaultCapacity];

        private int capacity = DefaultCapacity;

        private int count;

        public CustomQueue()
        {
        }

        public CustomQueue(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Enqueue(item);
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
                    throw new ArgumentOutOfRangeException("Count cannot be negative");
                }

                this.count = value;
            }
        }

        public void Enqueue(T item)
        {
            if (this.Count == this.capacity)
            {
                this.Resize();
            }

            this.queueElements[this.Count++] = item;
        }

        public T Dequeue()
        {
            this.CheckIfEmpty();

            T dequeuedElement = this.queueElements[0];

            for (int i = 0; i < this.Count - 1; i++)
            {
                this.queueElements[i] = this.queueElements[i + 1];
            }

            this.queueElements[--this.count] = default(T);            

            return dequeuedElement;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                var element = this.queueElements[i];

                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Clear()
        {
            Array.Clear(this.queueElements, 0, this.count);

            this.Count = 0;
        }

        private void Resize()
        {
            this.capacity *= 2;

            T[] extendedArray = new T[this.capacity];

            Array.Copy(this.queueElements, extendedArray, this.Count);

            this.queueElements = extendedArray;
        }

        private void CheckIfEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Empty collection!");
            }
        }
    }
}
