using System;
using System.Drawing;

namespace CartesianTree
{
    /// <summary>
    /// Класс, позволяющий рисовать узлы деревьев
    /// </summary>
    class NodeDrawer
    {

        /// <summary>
        /// Высота на которой будет рисоваться узел
        /// </summary>
        public int StartY { get; set; } = 0;

        /// <summary>
        /// Размер узла дерева
        /// </summary>
        private int size = 60;

        /// <summary>
        /// Размер узла дерева
        /// </summary>
        public int Size
        {
            get => size;
            set
            {
                size = Math.Min(value, EndX - StartX - 2);
            }
        }

        /// <summary>
        /// Начало области, в которой можно отображать узел (по ширине)
        /// </summary>
        public int StartX { get; set; } = 0;

        /// <summary>
        /// Конец области, в которой можно отображать узел (по ширине)
        /// </summary>
        public int EndX { get; set; } = 1000;

        /// <summary>
        /// Текст, который отображается внутри узла
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public NodeDrawer() { }

        /// <summary>
        /// Возвращает Rectangle, который предстваляет узел дерева
        /// </summary>
        /// <returns>Rectangle, который предстваляет узел дерева</returns>
        public Rectangle GetRectangle()
        {
            return new Rectangle((EndX + StartX - Size) / 2, StartY, Size, Size);
        }

    }
}
