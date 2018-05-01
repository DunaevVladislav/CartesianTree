using System;

namespace CartesianTree
{
    /// <summary>
    /// Узел декартового дерева
    /// </summary>
    /// <typeparam name="TValue">Информация хранимая в декартовом дереве</typeparam>
    class NodeOfCartesianTree<TValue> where TValue:IComparable
    {
        /// <summary>
        /// Объект для генерации случайных величин
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Информация хранящайся в узле дерева
        /// </summary>
        public NodeInfo<TValue> Info { get; set; }

        /// <summary>
        /// Приоритет узла
        /// </summary>
        public int Priority { get; private set; }

        /// <summary>
        /// Левый сын дерева
        /// </summary>
        public NodeOfCartesianTree<TValue> Left { get; set; }

        /// <summary>
        /// Правый сын дерева
        /// </summary>
        public NodeOfCartesianTree<TValue> Right { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="info">Информация хранящайся в узле дерева</param>
        /// <param name="left">Левый сын</param>
        /// <param name="right">Правый сын</param>
        public NodeOfCartesianTree(NodeInfo<TValue> info = null, NodeOfCartesianTree<TValue> left = null, NodeOfCartesianTree<TValue> right = null)
        {
            Info = info;
            Priority = random.Next(int.MinValue, int.MaxValue);
            Left = left;
            Right = right;
        }
    }
}