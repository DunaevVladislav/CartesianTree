using System;

namespace CartesianTree
{
    /// <summary>
    /// Узел декратового дерева, хранящий размер и высоту поддерева
    /// </summary>
    class NodeWithHeightAndSize : NodeInfo<HeightAndSizeInfo>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="value">Значение узла</param>
        public NodeWithHeightAndSize(HeightAndSizeInfo value) : base(value)
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public NodeWithHeightAndSize()
        {
        }

        /// <summary>
        /// Пересчет размера и высоты поддерева
        /// </summary>
        /// <param name="left">Левое поддерево</param>
        /// <param name="right">Правое поддерево</param>
        public override void Update(NodeInfo<HeightAndSizeInfo> left, NodeInfo<HeightAndSizeInfo> right)
        {

            Value.Size = (left == null ? 0 : left.Value.Size) + (right == null ? 0 : right.Value.Size) + 1;
            Value.Height = Math.Max((left == null ? 0 : left.Value.Height), (right == null ? 0 : right.Value.Height)) + 1;
        }
    }
}
