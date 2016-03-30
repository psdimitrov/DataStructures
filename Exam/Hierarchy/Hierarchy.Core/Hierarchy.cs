namespace Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private class Node<T>
        {
            public Node(T value)
            {
                this.Value = value;
                this.Children = new List<Node<T>>();
            }

            public Node(T value, Node<T> parent)
                : this(value)
            {
                this.Parent = parent;
            }

            public T Value { get; set; }

            public Node<T> Parent { get; set; }

            public List<Node<T>> Children { get; set; } 
        }

        private Node<T> root;

        private Dictionary<T, Node<T>> nodes; 

        public Hierarchy(T rootValue)
        {
            this.root = new Node<T>(rootValue);
            this.nodes = new Dictionary<T, Node<T>>();           
            this.nodes.Add(rootValue, this.root);
        }

        public int Count => this.nodes.Count;

        public void Add(T element, T child)
        {
            this.ValidateElementExist(element);

            if (this.nodes.ContainsKey(child))
            {
                throw new ArgumentException("The hierarchy already contains the child");
            }

            Node<T> parent = this.nodes[element];
            var childNode = new Node<T>(child, parent);
            this.nodes.Add(child, childNode);
            parent.Children.Add(childNode);
        }

        public void Remove(T element)
        {
            this.ValidateElementExist(element);

            var nodeElement = this.nodes[element];
            if (nodeElement.Parent == null)
            {
                throw new InvalidOperationException("Cannot remove root node");
            }

            nodeElement.Parent.Children.Remove(nodeElement);
            nodeElement.Parent.Children.AddRange(nodeElement.Children);

            foreach (var child in nodeElement.Children)
            {
                child.Parent = nodeElement.Parent;
            }

            this.nodes.Remove(element);
        }

        public IEnumerable<T> GetChildren(T element)
        {
            this.ValidateElementExist(element);

            var children = this.nodes[element].Children;

            return children.Select(child => child.Value);
        }

        public T GetParent(T element)
        {
            this.ValidateElementExist(element);

            var node = this.nodes[element];
            var parent = node.Parent;
            
            return parent != null ? parent.Value : default(T);
        }

        public bool Contains(T element) => this.nodes.ContainsKey(element);
        
        public IEnumerable<T> GetCommonElements(IHierarchy<T> other)
        {
            var elements = this.nodes.Keys;

            return elements.Where(other.Contains);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<Node<T>>();
            queue.Enqueue(this.root);

            while (queue.Count > 0)
            {
                var element = queue.Dequeue();

                yield return element.Value;

                foreach (var child in element.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ValidateElementExist(T element)
        {
            if (!this.nodes.ContainsKey(element))
            {
                throw new ArgumentException("Element does not exist in the hierarchy");
            }
        }
    }
}
