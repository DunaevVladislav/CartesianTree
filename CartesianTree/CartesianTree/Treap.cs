using System;

namespace CartesianTree
{
    /// <summary>
    /// Декартовое дерево
    /// </summary>
    class Treap<TNode, TValue> 
        where TValue : IComparable
        where TNode : NodeInfo<TValue>, new()
    {
        /// <summary>
        /// Корень дерева
        /// </summary>
        public NodeOfCartesianTree<TNode, TValue> Root { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="value">Информация, которая хранится в корне</param>
        public Treap(TValue value)
        {
            Root = new NodeOfCartesianTree<TNode, TValue>(value);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="rootInfo">Информация, которая хранится в корне</param>
        public Treap(TNode rootInfo)
        {
            Root = new NodeOfCartesianTree<TNode, TValue>(rootInfo);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="rootNode">Корень дерева</param>
        public Treap(NodeOfCartesianTree<TNode, TValue> rootNode = null)
        {
            Root = rootNode;
        }

        /// <summary>
        /// Операция слияния декартового дерева с subTree
        /// Ключи subTree не меньше, чем ключи текущего дерева
        /// </summary>
        /// <param name="subTree">Поддерево, с которым происходит слияние</param>
        public void Merge(Treap<TNode, TValue> subTree)
        {
            Root = NodeOfCartesianTree<TNode, TValue>.Merge(Root, subTree.Root);
        }

        /// <summary>
        /// Разделяет текущее декратовое дерева на два по ключу x
        /// Все элементы с ключом не превосходящим х окажутся в левом поддереве
        /// Остальные в правом
        /// </summary>
        /// <param name="x">Ключ, по которому происходит разделение</param>
        /// <param name="left">Левое поддерево</param>
        /// <param name="right">Правое поддерево</param>
        public void Split(TValue x, out Treap<TNode, TValue> left, out Treap<TNode, TValue> right)
        {
            NodeOfCartesianTree<TNode, TValue> l, r;
            Root.Split(x, out l, out r);
            left = new Treap<TNode, TValue>(l);
            right = new Treap<TNode, TValue>(r);
        }

        /// <summary>
        /// Разделяет текущее декратовое дерева на два по ключу x
        /// Все элементы с ключом меньшем х окажутся в левом поддереве
        /// Остальные в правом
        /// </summary>
        /// <param name="x">Ключ, по которому происходит разделение</param>
        /// <param name="left">Левое поддерево</param>
        /// <param name="right">Правое поддерево</param>
        public void SplitLeft(TValue x, out Treap<TNode, TValue> left, out Treap<TNode, TValue> right)
        {
            NodeOfCartesianTree<TNode, TValue> l, r;
            Root.SplitLeft(x, out l, out r);
            left = new Treap<TNode, TValue>(l);
            right = new Treap<TNode, TValue>(r);
        }

        /// <summary>
        /// Разделяет текущее декратовое дерева на два по ключу x
        /// Все элементы с ключом не превосходящим х останутся в текущем дерева
        /// Остальные окажутся в поддереве, которое возвращает функция
        /// </summary>
        /// <param name="x">Ключ, по которому происходит разделение</param>
        /// <returns>Поддерево, в котором ключи больше x</returns>
        public Treap<TNode, TValue> Split(TValue x)
        {
            NodeOfCartesianTree<TNode, TValue> l = null, r = null;
            Root?.Split(x, out l, out r);
            Root = l;
            return new Treap<TNode, TValue>(r);
        }

        /// <summary>
        /// Разделяет текущее декратовое дерева на два по ключу x
        /// Все элементы с ключом меньшем х останутся в текущем дерева
        /// Остальные окажутся в поддереве, которое возвращает функция
        /// </summary>
        /// <param name="x">Ключ, по которому происходит разделение</param>
        /// <returns>Поддерево, в котором ключи больше x</returns>
        public Treap<TNode, TValue> SplitLeft(TValue x)
        {
            NodeOfCartesianTree<TNode, TValue> l, r;
            Root.SplitLeft(x, out l, out r);
            Root = l;
            return new Treap<TNode, TValue>(r);
        }

        /// <summary>
        /// Добавляет элемент со значением x в дерево
        /// </summary>
        /// <param name="x">Добавляемой значение</param>
        public void Add(TValue x)
        {
            var r = Split(x);
            Merge(new Treap<TNode, TValue>(x));
            Merge(r);
        }

        /// <summary>
        /// Удаляет элемент со значением x
        /// </summary>
        /// <param name="x">Удаляемыей элемент</param>
        /// <returns>Был ли удален элемент</returns>
        public bool Delete(TValue x)
        {
            var r = Split(x);
            var deletedTree = SplitLeft(x);
            Merge(r);
            return !deletedTree.IsEmpty();
        }

        /// <summary>
        /// Возвращает является ли дерево пустым
        /// </summary>
        /// <returns>Является ли дерево пустым</returns>
        public bool IsEmpty()
        {
            return Root == null;
        }
    }
}
