using System;

namespace CartesianTree
{
    /// <summary>
    /// Декартовое дерево
    /// </summary>
    class Treap<TValue> where TValue : IComparable
    {
        /// <summary>
        /// Корень дерева
        /// </summary>
        public NodeOfCartesianTree<TValue> Root { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="rootInfo">Информация, которая хранится в корне</param>
        public Treap(NodeInfo<TValue> rootInfo = null)
        {
            Root = new NodeOfCartesianTree<TValue>(rootInfo);
        }

        /// <summary>
        /// Операция слияния декартового дерева с subTree
        /// Ключи subTree не меньше, чем ключи текущего дерева
        /// </summary>
        /// <param name="subTree">Поддерево, с которым происходит слияние</param>
        public void Merge(Treap<TValue> subTree)
        {
            Root = NodeOfCartesianTree<TValue>.Merge(Root, subTree.Root);
        }

        /// <summary>
        /// Разделяет текущее декратовое дерева на два по ключу x
        /// Все элементы с ключом не превосходящим х окажутся в левом поддереве
        /// Остальные в правом
        /// </summary>
        /// <param name="x">Ключ, по которому происходит разделение</param>
        /// <param name="left">Левое поддерево</param>
        /// <param name="right">Правое поддерево</param>
        public void Split(TValue x, Treap<TValue> left, out Treap<TValue> right)
        {
            NodeOfCartesianTree<TValue> l, r;
            Root.Split(x, out l, out r);
            left = new Treap<TValue>(l);
            right = new Treap<TValue>(r);
        }
    }
}
