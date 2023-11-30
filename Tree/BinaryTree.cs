using System;
using System.Collections;
using System.Collections.Generic;

namespace Tree
{
    public class BinaryTreeNode<T> : IComparable<BinaryTreeNode<T>> where T : IComparable<T>
    {
        public T Data { get; set; }
        public BinaryTreeNode<T>? Left { get; set; }
        public BinaryTreeNode<T>? Right { get; set; }

        public BinaryTreeNode(T data)
        {
            Data = data;
        }

        public int CompareTo(BinaryTreeNode<T>? other)
        {
            if (other == null)
            {
                return 1;
            }
            return Data.CompareTo(other.Data);
        }
    }

    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public BinaryTreeNode<T>? Root { get; private set; }

        public BinaryTree()
        {
            Root = null;
        }

        public void Insert(T data)
        {
            Root = InsertRec(Root, data);
        }

        public void Remove(T data)
        {
            Root = RemoveRec(Root, data);
        }

        private BinaryTreeNode<T> InsertRec(BinaryTreeNode<T>? root, T data)
        {
            if (root == null)
                return new BinaryTreeNode<T>(data);
            int comparisonResult = data.CompareTo(root.Data);
            if (comparisonResult < 0)
                root.Left = InsertRec(root.Left, data);
            else if (comparisonResult > 0)
                root.Right = InsertRec(root.Right, data);
            return root;
        }

        private BinaryTreeNode<T> RemoveRec(BinaryTreeNode<T>? root, T data)
        {
            if (root == null)
                return root;

            int comparisonResult = data.CompareTo(root.Data);

            if (comparisonResult < 0)
            {
                root.Left = RemoveRec(root.Left, data);
            }
            else if (comparisonResult > 0)
            {
                root.Right = RemoveRec(root.Right, data);
            }
            else
            {
                if (root.Left == null)
                    return root.Right;
                else if (root.Right == null)
                    return root.Left;

                root.Data = MinValue(root.Right);

                root.Right = RemoveRec(root.Right, root.Data);
            }

            return root;
        }

        private T MinValue(BinaryTreeNode<T> root)
        {
            T minValue = root.Data;
            while (root.Left != null)
            {
                minValue = root.Left.Data;
                root = root.Left;
            }
            return minValue;
        }

        public IEnumerable<T> Preorder()
        {
            return Preorder(Root);
        }

        private IEnumerable<T> Preorder(BinaryTreeNode<T> node)
        {
            if (node == null)
                yield break;

            yield return node.Data;

            foreach (var left in Preorder(node.Left))
            {
                yield return left;
            }

            foreach (var right in Preorder(node.Right))
            {
                yield return right;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Preorder().GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class StudentEnumerator<T> : IEnumerator<T> where T : IComparable<T>
    {
        private BinaryTreeNode<T>? _root;
        private Stack<BinaryTreeNode<T>> _stack;

        public StudentEnumerator(BinaryTreeNode<T>? node)
        {
            _root = node;
            _stack = new Stack<BinaryTreeNode<T>>();
            if (_root != null)
            {
                _stack.Push(_root);
            }
        }

        public T Current => _stack.Peek().Data;

        object System.Collections.IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public bool MoveNext()
        {
            if (_stack.Count > 0)
            {
                BinaryTreeNode<T> current = _stack.Pop();

                if (current.Right != null)
                {
                    _stack.Push(current.Right);
                }

                if (current.Left != null)
                {
                    _stack.Push(current.Left);
                }

                if (_stack.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            _stack.Clear();
            if (_root != null)
            {
                _stack.Push(_root);
            }
        }

        public void Dispose()
        {
            _stack.Clear();
        }
    }
}
