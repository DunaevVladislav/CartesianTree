using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartesianTree
{
    /// <summary>
    /// Перечислитель возможный изменния в декартовом дереве
    /// </summary>
    enum LogTreapEvent
    {
        /// <summary>
        /// Добавление узла
        /// </summary>
        AddedNode,
        /// <summary>
        /// Добавление ребра
        /// </summary>
        AddedEdge,
        /// <summary>
        /// Удаление ребра
        /// </summary>
        RemoveEdge,
        /// <summary>
        /// Удаление узла
        /// </summary>
        RemoveNode,
        /// <summary>
        /// Старт слияния деревеьев
        /// </summary>
        StartMerge,
        /// <summary>
        /// Старт разделения дерева
        /// </summary>
        StartSplite,
        /// <summary>
        /// Конец слияния деревеьев
        /// </summary>
        EndMerge,
        /// <summary>
        /// Конец разделения дерева
        /// </summary>
        EndSplite,
    }

    /// <summary>
    /// Информация об измениниях произошедших в декартовом дереве
    /// </summary>
    /// <typeparam name="TNode">Информация хранящайся в узле дерева</typeparam>
    /// <typeparam name="TValue">Тип данных который хранится в декартовом дереве</typeparam>
    class LogTreap<TNode, TValue>
        where TValue : IComparable
        where TNode : NodeInfo<TValue>, new()
    {
        /// <summary>
        /// Произошедшее событие
        /// </summary>
        public LogTreapEvent LogEvent { get; private set; }

        /// <summary>
        /// Список узлов, которые участвовали в событии
        /// </summary>
        public List<NodeOfCartesianTree<TNode, TValue>> Nodes { get; private set; } = new List<NodeOfCartesianTree<TNode, TValue>>();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logEvent">Произошедшее событие</param>
        public LogTreap(LogTreapEvent logEvent) => LogEvent = logEvent;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logEvent">Произошедшее событие</param>
        /// <param name="node">Узел учавствующий в дереве</param>
        public LogTreap(LogTreapEvent logEvent, NodeOfCartesianTree<TNode, TValue> node) : this(logEvent) => Nodes.Add(node);

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logEvent">Произошедшее событие</param>
        /// <param name="node1">Узел учавствующий в дереве</param>
        /// <param name="node2">Узел учавствующий в дереве</param>
        public LogTreap(LogTreapEvent logEvent, NodeOfCartesianTree<TNode, TValue> node1, NodeOfCartesianTree<TNode, TValue> node2) 
            : this(logEvent, node1) => Nodes.Add(node2);

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logEvent">Произошедшее событие</param>
        /// <param name="nodes">Список участвоваших узлов</param>
        public LogTreap(LogTreapEvent logEvent, List<NodeOfCartesianTree<TNode, TValue>> nodes) : this(logEvent) => Nodes = nodes;
    }
}
