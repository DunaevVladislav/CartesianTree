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
        /// Цвет границ узлов
        /// </summary>
        public Pen PenBorder { get; set; } = new Pen(Color.Black, 2);

        /// <summary>
        /// Шрифт для отображения информации внутри узла
        /// </summary>
        public Font Font { get; set; } = new Font("Calibri", 9);

        /// <summary>
        /// Цвет шрифта
        /// </summary>
        public Brush BrushFont { get; set; } = new SolidBrush(Color.Black);

        /// <summary>
        /// Прямоугольник, который представляет узел
        /// </summary>
        public Rectangle Rect => new Rectangle((EndX + StartX - Size) / 2, StartY, Size, Size);

        /// <summary>
        /// Минимальный размер при котором происходит отбражение текста
        /// </summary>
        public int MinSizeForOutText { get; set; } = 45;

        /// <summary>
        /// Стрелка которая входит в узел
        /// </summary>
        public ArrowDrawer Arrow { get; set; }

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
            return Rect;
        }

        /// <summary>
        /// Проверяет принадлежит ли точка прямоугольнику
        /// </summary>
        /// <param name="x">Координата x точки</param>
        /// <param name="y">Координата y точки </param>
        /// <returns>Приндлежит ли точка прямоугольнику</returns>
        public bool Inside(int x, int y)
        {
            int leftX = (EndX + StartX - Size) / 2;
            return leftX <= x && x <= leftX + size && StartY <= y && y <= StartY + size;
        }

        /// <summary>
        /// Проверяет принадлежит ли точка прямоугольнику
        /// </summary>
        /// <param name="point">Проверяемая точка</param>
        /// <returns>Приндлежит ли точка прямоугольнику</returns>
        public bool Inside(Point point) => Inside(point.X, point.Y);

        /// <summary>
        /// Рисует узел дерева на graphics
        /// </summary>
        /// <param name="graphics">Объект, где рисует узел дерева</param>
        public void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(PenBorder, Rect);
            if (Size >= MinSizeForOutText)
            {
                graphics.DrawString(Text, Font, BrushFont, (StartX + EndX - Size) / 2, StartY);
            }
            Arrow?.Draw(graphics);
        }


    }
}
