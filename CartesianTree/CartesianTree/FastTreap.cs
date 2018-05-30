using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartesianTree
{
    public class FastTreap
    {
        public int Val;

        /// <summary>
        /// Приоритет узла
        /// </summary>
        public int Priority { get; private set; }

        public FastTreap Left;

        public FastTreap Right;

        public static Random rnd = new Random();

        private static int maxRnd = 1000000007;

        public FastTreap(int val)
        {
            Val = val;
            Priority = rnd.Next() % maxRnd;
        }

        public static FastTreap Merge(FastTreap L, FastTreap R)
        {
            if (L == null) return R;
            if (R == null) return L;
            if (L.Priority > R.Priority)
            {
                L.Right = Merge(L.Right, R);
                return L;
            }
            else
            {
                R.Left =  Merge(L, R.Left);
                return R;
            }
        }

        public static void Split(FastTreap tree, ref int val, out FastTreap L, out FastTreap R)
        {
            if (tree.Val <= val)
            {
                if (tree.Right == null) R = null;
                else Split(tree.Right, ref val, out tree.Right, out R);
                L = tree;
            }
            else
            {
                if (tree.Left == null) L = null;
                else Split(tree.Left, ref val, out L, out tree.Left);
                R = tree;
            }
        }

        public static void Add(ref FastTreap tree, int val)
        {
            if (tree == null)
            {
                tree = new FastTreap(val);
                return;
            }
            if (Contains(tree, val)) return;
            Split(tree, ref val, out tree, out FastTreap R);
            FastTreap node = new FastTreap(val);
            tree = Merge(tree, node);
            tree = Merge(tree, R);
        }

        public static bool Contains(FastTreap tree, int val)
        {
            while(tree != null)
            {
                if (tree.Val == val) return true;
                tree = val < tree.Val? tree.Left : tree.Right;
            }
            return false;
        }

        public static void Remove(ref FastTreap tree, int val)
        {
            if (tree == null || !Contains(tree, val)) return;

            Split(tree, ref val, out tree, out FastTreap R);
            if (tree.Val == val)
            {
                tree = Merge(tree.Left, R);
                return;
            }
            else
            {
                FastTreap deleted = tree;
                while (deleted.Right.Val != val) deleted = deleted.Right;
                deleted.Right = null;

                tree = Merge(tree, R);
            }
        }

    }
}
