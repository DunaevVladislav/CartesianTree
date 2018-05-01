using System.Drawing;

namespace CartesianTree
{
    /// <summary>
    /// Класс, позволяющий рисовать узлы деревьев
    /// </summary>
    class NodeDrawer
    {
        /// <summary>
        /// Расстояние между уровнями в дереве
        /// </summary>
        public int DistanseHeight { get; set; } = 45;

        /// <summary>
        /// Высота на которой будет рисоваться узел
        /// </summary>
        public int StartY { get; set; } = 0;

        /// <summary>
        /// Размер узла дерева
        /// </summary>
        public int Size { get; set; } = 60;

        /// <summary>
        /// Начало области, в которой можно отображать узел (по ширине)
        /// </summary>
        public int StartX { get; set; } = 0;

        /// <summary>
        /// Конец области, в которой можно отображать узел (по ширине)
        /// </summary>
        public int EndX { get; set; } = 1000;

        /// <summary>
        /// Цвет границ узлов
        /// </summary>
        public Pen PenBorder { get; set; } = new Pen(Color.Black, 2);

        /// <summary>
        /// Шрифт для отображения информации внутри узла
        /// </summary>
        public Font Font { get; set; } = new Font("Calibri", 11);

        /// <summary>
        /// Цвет шрифта
        /// </summary>
        public Brush BrushFont { get; set; } = new SolidBrush(Color.Red);

        /// <summary>
        /// Конструктор
        /// </summary>
        public NodeDrawer() { }

    }
}
