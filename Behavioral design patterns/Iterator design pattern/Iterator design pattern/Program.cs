using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator_design_pattern
{
    internal class Program
    {
        // Iteration (traversal) is a core functionality of of various data structures
        // An iterator is a class that facilitates the traversal (Keeps reference to the current element, knows how to move to the next element)
        // Iterator is an implicit construct

        static void Main(string[] args)
        {
            var root = new Node<int>(1, new Node<int>(2), new Node<int>(3));
        }
    }
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;
            Left.Parent = this;
            Right.Parent = this;
        }
    }
    public class InOrderIterator<T>
    {
        private readonly Node<T> root;
        public Node<T> Current;
        private bool yieldedStart;
        public InOrderIterator(Node<T> root)
        {
            this.root = root;
            Current = root;
            while(Current.Left != null)
            {
                Current = Current.Left;
            }
        }
        public bool MoveNext()
        {
            if(!yieldedStart)
            {
                yieldedStart = true;
                return true;
            }
            if(Current.Right != null)
            {
                Current = Current.Right;
                while(Current.Left != null)
                {
                    Current = Current.Left;
                }
                return true;
            }
            else
            {
                var p = Current.Parent;
                while (p != null && Current == p.Right)
                {
                    Current = p;
                    p = p.Parent;
                }
                Current = p;
                return Current != null;
            }
        }
        public void Reset()
        {
            Current = root;
            yieldedStart = false;
        }
    }
    public class BinaryTree<T> // Making the traversing recursive (Iterator method video)
    {
        Node<T> root;
        public BinaryTree(Node<T> root)
        {
            this.root = root;
        }
        public IEnumerable<Node<T>> InOrder
        {
            get
            {
                IEnumerable<Node<T>> Traverse(Node<T> current)
                {
                    if(current.Left != null)
                    {
                        foreach(var left in Traverse(current.Left))
                        {
                            yield return left;
                        }
                    }
                    yield return current;
                    if(current.Right != null)
                    {
                        foreach (var right in Traverse(current.Right))
                        {
                            yield return right;
                        }
                    }
                }
                foreach(var node in Traverse(root))
                {
                    yield return node;
                }
            }
        }
    }
}
