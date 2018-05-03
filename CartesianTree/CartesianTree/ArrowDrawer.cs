using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartesianTree
{
    /// <summary>
    /// Класс для риссования стрелок
    /// </summary>
    class ArrowDrawer
    {
        /// <summary>
        /// Точка начала стрелка
        /// </summary>
        public Point Start { get; set; }

        /// <summary>
        /// Точка конца стрелки
        /// </summary>
        public Point End { get; set; }

        /// <summary>
        /// Объект Pen для рисования стрелки
        /// </summary>
        public Pen Pen { get; set; } = new Pen(Color.Blue, 2);

        /// <summary>
        /// Угол поворот указателя стрелки
        /// </summary>
        public double Alpha { get; set; } = 20 * Math.PI / 180;

        /// <summary>
        /// Длина указателя стрелки
        /// </summary>
        public double LengthPointer { get; set; } = 15.0;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="start">Начало стрелки</param>
        /// <param name="end">Конец стрелки</param>
        public ArrowDrawer(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="startX">Начало стрелки по оси Х</param>
        /// <param name="startY">Начало стрлеки по оси Y</param>
        /// <param name="endX">Конец стрелки на оси X</param>
        /// <param name="endY">Конец стрелки на оси Y</param>
        public ArrowDrawer(int startX, int startY, int endX, int endY) : this(new Point(startX, startY), new Point(endX, endY))
        {

        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="rect1">Прямоугольник от которого рисуется стрелка</param>
        /// <param name="rect2">Прямоугольник к которому рисуется стрелка</param>
        public ArrowDrawer(Rectangle rect1, Rectangle rect2)
        {
            Start = new Point((rect1.Right + rect1.Left) / 2, rect1.Bottom + 2);
            End = new Point((rect2.Right + rect2.Left) / 2, rect2.Top - 2);
        }

        /// <summary>
        /// Рисует стрелку
        /// </summary>
        /// <param name="graphics">Объект Graphics, где будет отрисована стрелка</param>
        public void Draw(Graphics graphics)
        {
            graphics.DrawLine(Pen, Start, End);
            Point vector = new Point(Start.X - End.X, Start.Y - End.Y);
            double coef = LengthPointer / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            Point leftPointer = new Point(
                (int)((vector.X * Math.Cos(Alpha) + vector.Y * Math.Sin(Alpha)) * coef),
                (int)((-vector.X * Math.Sin(Alpha) + vector.Y * Math.Cos(Alpha)) * coef));
            Point rightPointer = new Point(
                (int)((vector.X * Math.Cos(Alpha) - vector.Y * Math.Sin(Alpha)) * coef),
                (int)((+vector.X * Math.Sin(Alpha) + vector.Y * Math.Cos(Alpha)) * coef));
            Point LPointer = new Point(End.X + leftPointer.X, End.Y + leftPointer.Y);
            Point RPointer = new Point(End.X + rightPointer.X, End.Y + rightPointer.Y);
            graphics.DrawLine(Pen, LPointer, End);
            graphics.DrawLine(Pen, RPointer, End);

        }



    }
}
