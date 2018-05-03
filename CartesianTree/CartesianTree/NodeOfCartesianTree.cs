using System;
using System.Collections.Generic;

namespace CartesianTree
{
    /// <summary>
    /// Узел декартового дерева
    /// </summary>
    /// <typeparam name="TNode">Информация хранящайся в узле дерева</typeparam>
    /// <typeparam name="TValue">Тип данных который хранится в декартовом дереве</typeparam>
    class NodeOfCartesianTree<TNode, TValue>:IDetailsToString
        where TValue : IComparable
        where TNode : NodeInfo<TValue>, new()
    {
        /// <summary>
        /// Объект для генерации случайных величин
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Информация хранящайся в узле дерева
        /// </summary>
        public TNode Info { get; set; }

        /// <summary>
        /// Приоритет узла
        /// </summary>
        public int Priority { get; private set; }

        /// <summary>
        /// Минимальный приоритет узла
        /// </summary>
        public static int MinPriority { get; set; } = int.MinValue;

        /// <summary>
        /// Максимальный приоритет узла
        /// </summary>
        public static int MaxPriority { get; set; } = int.MaxValue;

        /// <summary>
        /// Левый сын дерева
        /// </summary>
        public NodeOfCartesianTree<TNode, TValue> Left { get; set; }

        /// <summary>
        /// Правый сын дерева
        /// </summary>
        public NodeOfCartesianTree<TNode, TValue> Right { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="value">Значение текщуего узла</param>
        public NodeOfCartesianTree(TValue value)
        {
            Info = new TNode
            {
                Value = value
            };
            Priority = random.Next()%(MaxPriority - MinPriority + 1) + MinPriority;
            Left = null;
            Right = null;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="info">Информация хранящайся в узле дерева</param>
        /// <param name="left">Левый сын</param>
        /// <param name="right">Правый сын</param>
        public NodeOfCartesianTree(TNode info = null, NodeOfCartesianTree<TNode, TValue> left = null, NodeOfCartesianTree<TNode, TValue> right = null)
        {
            Info = info;
            Priority = random.Next(int.MinValue, int.MaxValue);
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="info">Информация хранящайся в узле дерева</param>
        /// <param name="prioryty">Приоритет вершины</param>
        /// <param name="left">Левый сын</param>
        /// <param name="right">Правый сын</param>
        public NodeOfCartesianTree(TNode info, int prioryty, NodeOfCartesianTree<TNode, TValue> left = null, NodeOfCartesianTree<TNode, TValue> right = null)
        {
            Info = info;
            Priority = prioryty;
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Обновление информации в узле после совершения операции
        /// </summary>
        public void Update()
        {
            Info.Update(Left?.Info, Right?.Info);
        }

        /// <summary>
        /// Операция слияния двух декартовых деревеьев
        /// Ключи правого поддерева не меньше, чем ключи левого поддерева
        /// </summary>
        /// <param name="left">Левое поддерево</param>
        /// <param name="right">Правое поддерево</param>
        /// <param name="logs">Информация о проведенных операциях</param>
        /// <returns>Результирующее дерево</returns>
        public static NodeOfCartesianTree<TNode, TValue> Merge(
            NodeOfCartesianTree<TNode, TValue> left, NodeOfCartesianTree<TNode, TValue> right, List<LogTreap<TNode, TValue>> logs
            )
        {
            logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.Merging, left, right));
            if (left == null) return right;
            if (right == null) return left;
            if (left.Priority > right.Priority)
            {
                logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.RemoveEdge, left, left.Right));
                var newRight = Merge(left.Right, right, logs);
                logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.AddedEdge, left, newRight));
                left.Right = newRight;
                left.Update();
                return left;
            }
            else
            {
                logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.RemoveEdge, right, right.Left));
                var newLeft = Merge(left, right.Left, logs);
                logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.RemoveEdge, right, newLeft));
                right.Left = newLeft;
                right.Update();
                return right;
            }
        }

        /// <summary>
        /// Разделяет текущее дерево по ключу x (в левом дереве все что меньше или равно x, в правом остальные)
        /// </summary>
        /// <param name="x">Ключ, по которому происходит разделение</param>
        /// <param name="left">Получивщееся левое поддереов</param>
        /// <param name="right">Получившееся правое поддерево</param>
        /// <param name="logs">Информация о проведенных операциях</param>
        public void Split<T>(T x, out NodeOfCartesianTree<TNode, TValue> left, out NodeOfCartesianTree<TNode, TValue> right, List<LogTreap<TNode, TValue>> logs)
        {
            NodeOfCartesianTree<TNode, TValue> newTree = null;
            logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.Splitting, this));
            if (Info.Value.CompareTo(x) <= 0)
            {
                if (Right == null)
                {
                    right = null;
                }
                else
                {
                    logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.RemoveEdge, this, Right));
                    Right.Split(x, out newTree, out right, logs);
                }
                logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.AddedEdge, this, newTree));
                Right = newTree;
                left = this;
            }
            else
            {
                if (Left == null)
                {
                    left = null;
                }
                else
                {
                    logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.RemoveEdge, this, Left));
                    Left.Split(x, out left, out newTree, logs);
                }
                logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.AddedEdge, this, newTree));
                Left = newTree;
                right = this;
            }
            Update();
        }

        /// <summary>
        /// Разделяет текущее дерево по ключу x (в левом дереве все что меньше x, в правом остальные)
        /// </summary>
        /// <param name="x">Ключ, по которому происходит разделение</param>
        /// <param name="left">Получивщееся левое поддереов</param>
        /// <param name="right">Получившееся правое поддерево</param>
        /// <param name="logs">Информация о проведенных операциях</param>
        public void SplitLeft<T>(T x, out NodeOfCartesianTree<TNode, TValue> left, out NodeOfCartesianTree<TNode, TValue> right, List<LogTreap<TNode, TValue>> logs)
        {
            NodeOfCartesianTree<TNode, TValue> newTree = null;
            logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.Splitting, this));
            if (Info.Value.CompareTo(x) < 0)
            {
                if (Right == null)
                {
                    right = null;
                }
                else
                {
                    logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.RemoveEdge, this, Right));
                    Right.SplitLeft(x, out newTree, out right, logs);
                }
                logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.AddedEdge, this, newTree));
                Right = newTree;
                left = this;
            }
            else
            {
                if (Left == null)
                {
                    left = null;
                }
                else
                {
                    logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.RemoveEdge, this, Left));
                    Left.SplitLeft(x, out left, out newTree, logs);
                }
                logs?.Add(new LogTreap<TNode, TValue>(LogTreapEvent.AddedEdge, this, newTree));
                Left = newTree;
                right = this;
            }
            Update();
        }

        /// <summary>
        /// Возвращает строку, представляющую текущий объект 
        /// </summary>
        /// <param name="details">Нужна ли подробная информация</param>
        /// <returns>Строку, представляющую текущий объект </returns>
        public string ToString(bool details = false)
        {
            
            return Info.ToString(details) + "\n" + (details?"Приоритет узла: ":"") + Priority.ToString();
        }
    }
}