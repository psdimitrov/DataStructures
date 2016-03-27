namespace CustomDataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class CustomList<T> : IList<T>
    {
        private const int DefaultCapacity = 16;

        private int count;

        private T[] listElements;

        public CustomList()
        {
            this.listElements = new T[DefaultCapacity];
            this.count = 0;
        }

        public CustomList(int capacity)
        {
            this.listElements = new T[capacity];
            this.count = 0;
        }

        public CustomList(IEnumerable<T> collection)
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
                return this.listElements[index];
            }

            set
            {
                this.CheckIndex(index);
                this.listElements[index] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.count; i++)
            {
                yield return this.listElements[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            this.Insert(this.count, item);
        }

        public void Clear()
        {
            Array.Clear(this.listElements, 0, this.count);
            this.count = 0;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < this.count; i++)
            {
                if (this.listElements[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex + this.count > array.Length || arrayIndex < 0)
            {
                throw new IndexOutOfRangeException();
            }

            Array.Copy(this.listElements, 0, array, arrayIndex, this.count);
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.count; i++)
            {
                if (this.listElements[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index > this.count || index < 0)
            {
                throw new IndexOutOfRangeException("Invalid index: " + index);
            }

            T[] extendedArray = this.listElements;
            if (this.count + 1 == this.listElements.Length)
            {
                extendedArray = new T[this.listElements.Length * 2];
            }

            Array.Copy(this.listElements, extendedArray, index);
            Array.Copy(this.listElements, index, extendedArray, index + 1, this.count - index);
            extendedArray[index] = item;
            this.count++;
            this.listElements = extendedArray;
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < this.count; i++)
            {
                if (!this.listElements[i].Equals(item))
                {
                    continue;
                }

                for (int j = i; j < this.count - 1; j++)
                {
                    this.listElements[j] = this.listElements[j + 1];
                }

                this.count--;

                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            this.CheckIndex(index);

            for (int i = index; i < this.count; i++)
            {
                this.listElements[i] = this.listElements[i + 1];
            }

            this.count--;
        }

        public override string ToString()
        {
            if (this.count == 0)
            {
                return "[]";
            }

            StringBuilder sb = new StringBuilder(this.count);
            sb.Append('[');
            for (int i = 0; i < this.count - 1; i++)
            {
                sb.AppendFormat("{0}, ", this.listElements[i]);
            }

            sb.AppendFormat("{0}]", this.listElements[this.count - 1]);
            return sb.ToString();
        }

        private void CheckIndex(int index)
        {
            if (index >= this.count || index < 0)
            {
                throw new IndexOutOfRangeException("Invalid index: " + index);
            }
        }
    }
}
