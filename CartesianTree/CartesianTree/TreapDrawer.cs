using System;
using System.Collections.Generic;
using System.Drawing;

namespace CartesianTree
{
    /// <summary>
    /// Класс для рисования декартового дерева
    /// </summary>
    class TreapDrawer<TNode, TValue>
        where TValue : IComparable,IDetailsToString
        where TNode : NodeInfo<TValue>, new()
    {
        /// <summary>
        /// Объект Graphics, на котором происходит отрисовка дерева
        /// </summary>
        public Graphics Graphics { get; set; }

        /// <summary>
        /// Корень дерева
        /// </summary>
        public NodeDrawer Root { get; set; }

        /// <summary>
        /// Список всех узлов дерева(для отрисовки)
        /// </summary>
        public List<NodeDrawer> NodeDrawers { get; private set; } = new List<NodeDrawer>();


        /// <summary>
        /// Список всех ребер дерева(для отрисовки)
        /// </summary>
        public List<ArrowDrawer> ArrowDrawers { get; private set; } = new List<ArrowDrawer>();

        /// <summary>
        /// Список всех узлов дерева
        /// </summary>
        public List<NodeOfCartesianTree<TNode, TValue>> Nodes { get; private set; } = new List<NodeOfCartesianTree<TNode, TValue>>();

        /// <summary>
        /// Декартовое дерево, которое отрисовывается на Graphics
        /// </summary>
        public Treap<TNode, TValue> Treap { get; private set; }

        /// <summary>
        /// Расстояние между уровнями в дереве
        /// </summary>
        public int DistanseHeight { get; set; } = 50;

        /// <summary>
        /// Размер узла дерева
        /// </summary>
        public int Size { get; set; } = 45;

        /// <summary>
        /// Цвет границ узлов
        /// </summary>
        public Pen PenBorder { get; set; } = new Pen(Color.Black, 2);

        /// <summary>
        /// Цвет стрелок
        /// </summary>
        public Pen PenArrow { get; set; } = new Pen(Color.Blue, 1.5f);

        /// <summary>
        /// Шрифт для отображения информации внутри узла
        /// </summary>
        public Font Font { get; set; } = new Font("Calibri", 9);

        /// <summary>
        /// Цвет шрифта
        /// </summary>
        public Brush BrushFont { get; set; } = new SolidBrush(Color.Black);

        /// <summary>
        /// Максимальная ширина области для рисования
        /// </summary>
        public int MaxWidth { get; set; }

        /// <summary>
        /// Максимальная высота области для рисования
        /// </summary>
        public int MaxHeight { get; set; }

        /// <summary>
        /// Отступ сверху
        /// </summary>
        public int Backslash { get; set; } = 15;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="treap">Декартовое дерево, которое надо нарисовать</param>
        public TreapDrawer(Treap<TNode, TValue> treap, Graphics graphics, int maxWidth, int maxHeight)
        {
            Treap = treap;
            Graphics = graphics;
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
            AddedAllNodes();
        }

        /// <summary>
        /// Добавляет все узлы декартового дерева в списки
        /// </summary>
        private void AddedAllNodes()
        {
            if (!Treap.IsEmpty())
            {
                Nodes.Add(Treap.Root);
                NodeDrawers.Add(GetNodeDrawer(Treap.Root, Backslash, 0, MaxWidth, Size));
            }
            for (int i = 0; i < Nodes.Count; ++i)
            {
                int mid = (NodeDrawers[i].EndX + NodeDrawers[i].StartX) / 2;
                if (Nodes[i].Left != null)
                {
                    Nodes.Add(Nodes[i].Left);
                    NodeDrawers.Add(GetNodeDrawer(Nodes[i].Left, NodeDrawers[i].StartY + NodeDrawers[i].Size + DistanseHeight, NodeDrawers[i].StartX, mid, Size));
                    ArrowDrawers.Add(
                        new ArrowDrawer(mid, NodeDrawers[i].StartY + NodeDrawers[i].Size + 2, (NodeDrawers[i].StartX + mid) / 2, NodeDrawers[i].StartY + NodeDrawers[i].Size + DistanseHeight - 2)
                        {
                            Pen = PenArrow,
                            LengthPointer = NodeDrawers[i].Size / 3,
                        });
                }
                if (Nodes[i].Right != null)
                {
                    Nodes.Add(Nodes[i].Right);
                    NodeDrawers.Add(GetNodeDrawer(Nodes[i].Right, NodeDrawers[i].StartY + NodeDrawers[i].Size + DistanseHeight, mid + 1, NodeDrawers[i].EndX, Size));
                    ArrowDrawers.Add(
                        new ArrowDrawer(mid, NodeDrawers[i].StartY + NodeDrawers[i].Size + 2, (NodeDrawers[i].EndX + mid) / 2, NodeDrawers[i].StartY + NodeDrawers[i].Size + DistanseHeight - 2)
                        {
                            Pen = PenArrow,
                            LengthPointer = NodeDrawers[i].Size /3,
                        });
                }
            }
        }

        /// <summary>
        /// Отрисовывает декартовое дерево
        /// </summary>
        public void Draw()
        {
            Graphics.Clear(Color.White);
            foreach (NodeDrawer node in NodeDrawers)
            {
                if (node.StartY + node.Size > MaxHeight) continue;
                Graphics.DrawRectangle(PenBorder, node.GetRectangle());
                if (node.Size == Size)
                {
                    Graphics.DrawString(node.Text, Font, BrushFont, (node.StartX + node.EndX - Size) / 2, node.StartY);
                }
            }
            foreach (ArrowDrawer arrow in ArrowDrawers)
            {
                if (arrow.End.Y > MaxHeight) continue;
                arrow.Draw(Graphics);
            }
        }

        /// <summary>
        /// Ковертирует узел декартового дерева в узел для отрисовки на форме
        /// </summary>
        /// <param name="node">Узел декартового дерева</param>
        /// <param name="startY">Высота рисования узла</param>
        /// <param name="startX">Начало области рисования узла</param>
        /// <param name="endX">Конец области рисования узла</param>
        /// <returns></returns>
        private static NodeDrawer GetNodeDrawer(NodeOfCartesianTree<TNode, TValue> node, int startY, int startX, int endX, int size)
        {
            NodeDrawer nodeDrawer = new NodeDrawer()
            {
                StartY = startY,
                StartX = startX,
                EndX = endX,
                Text = node.ToString(),
                Size = size,
            };
            return nodeDrawer;
        }

        /// <summary>
        /// Возвращает подробную информацию об узел, которому принадлежит точка point
        /// Если такой найдется
        /// </summary>
        /// <param name="point">Точка, по которой ищется узел</param>
        /// <returns>Подробную информацию об узле(если такой найдется)</returns>
        public string GetDetailInfo(Point point)
        {
            for(int i = 0; i < NodeDrawers.Count; ++i)
            {
                if (NodeDrawers[i].Inside(point))
                {
                    return Nodes[i].ToString(true);
                }
            }
            return "";
        }


    }
}