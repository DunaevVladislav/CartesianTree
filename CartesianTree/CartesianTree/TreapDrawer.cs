using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CartesianTree
{
    /// <summary>
    /// Класс для рисования декартового дерева
    /// </summary>
    /// <typeparam name="TNode">Информация хранящайся в узле дерева</typeparam>
    /// <typeparam name="TValue">Тип данных который хранится в декартовом дереве</typeparam>
    class TreapDrawer<TNode, TValue>
        where TValue : IComparable, IDetailsToString
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
                NodeDrawers.Add(GetNodeDrawer(Treap.Root, Backslash, 0, MaxWidth));
            }
            for (int i = 0; i < Nodes.Count; ++i)
            {
                int mid = (NodeDrawers[i].EndX + NodeDrawers[i].StartX) / 2;
                if (Nodes[i].Left != null)
                {
                    Nodes.Add(Nodes[i].Left);
                    NodeDrawers.Add(GetNodeDrawer(Nodes[i].Left, NodeDrawers[i].StartY + NodeDrawers[i].Size + DistanseHeight, NodeDrawers[i].StartX, mid));
                    NodeDrawers.Last().Arrow =
                        new ArrowDrawer(mid, NodeDrawers[i].StartY + NodeDrawers[i].Size + 2, (NodeDrawers[i].StartX + mid) / 2, NodeDrawers[i].StartY + NodeDrawers[i].Size + DistanseHeight - 2)
                        {
                            Pen = (Pen)PenArrow.Clone(),
                            LengthPointer = NodeDrawers[i].Size / 3,
                        };
                }
                if (Nodes[i].Right != null)
                {
                    Nodes.Add(Nodes[i].Right);
                    NodeDrawers.Add(GetNodeDrawer(Nodes[i].Right, NodeDrawers[i].StartY + NodeDrawers[i].Size + DistanseHeight, mid + 1, NodeDrawers[i].EndX));
                    NodeDrawers.Last().Arrow =
                        new ArrowDrawer(mid, NodeDrawers[i].StartY + NodeDrawers[i].Size + 2, (NodeDrawers[i].EndX + mid) / 2, NodeDrawers[i].StartY + NodeDrawers[i].Size + DistanseHeight - 2)
                        {
                            Pen = (Pen)PenArrow.Clone(),
                            LengthPointer = NodeDrawers[i].Size / 3,
                        };
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
                node.Draw(Graphics);
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
        private NodeDrawer GetNodeDrawer(NodeOfCartesianTree<TNode, TValue> node, int startY, int startX, int endX)
        {
            NodeDrawer nodeDrawer = new NodeDrawer()
            {
                StartY = startY,
                StartX = startX,
                EndX = endX,
                Text = node.ToString(),
                Size = Size,
                PenBorder = (Pen)PenBorder.Clone(),
                BrushFont = (Brush)BrushFont.Clone(),
                MinSizeForOutText = Size,
                Font = Font
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
            for (int i = 0; i < NodeDrawers.Count; ++i)
            {
                if (NodeDrawers[i].Inside(point))
                {
                    return Nodes[i].ToString(true);
                }
            }
            return "";
        }

        /// <summary>
        /// Измения декартового дерева
        /// </summary>
        public List<LogTreap<TNode, TValue>> Logs { get; set; }

        /// <summary>
        /// Индекс последнего просмотренного действия в логах
        /// </summary>
        private int indexLogs = 0;

        /// <summary>
        /// Очищает лог изменений
        /// </summary>
        public void ResetLogs()
        {
            Logs.Clear();
            indexLogs = 0;
        }

        /// <summary>
        /// Ищет объект NodeDrawer по узлу Декартового дерева
        /// </summary>
        /// <param name="node">Узел Декартового дерева</param>
        /// <returns>Найденный NodeDrawer</returns>
        public NodeDrawer FindNodeDrawer(NodeOfCartesianTree<TNode, TValue> node)
        {
            for (int i = 0; i < Nodes.Count; ++i)
            {
                if (Nodes[i] == node) return NodeDrawers[i];
            }
            return null;
        }

        /// <summary>
        /// Очередь узлов, которые ждут заверешения рекурсивного вызова
        /// </summary>
        private Queue<NodeDrawer> prevActive = new Queue<NodeDrawer>();

        /// <summary>
        /// Цвет узлов, которые ждут заверешения рекурсивного вызова
        /// </summary>
        private Color waitColor = Color.Blue;

        /// <summary>
        /// Цвет узлов, которые активны в данный момент
        /// </summary>
        private Color activeColor = Color.Red;

        /// <summary>
        /// Очищает очередь prevActive
        /// Красит все узлы в очереди в цвет waitColor
        /// </summary>
        private void ClearActive()
        {
            while (prevActive.Count > 0)
            {
                prevActive.Dequeue().PenBorder.Color = waitColor;
            }
        }

        /// <summary>
        /// Отрисовывает следующее изменение
        /// </summary>
        public void NextEventInLog()
        {
            if (indexLogs >= Logs.Count) return;
            else
            {
                switch (Logs[indexLogs].LogEvent)
                {
                    case LogTreapEvent.StartSplite:
                        {
                            ClearActive();
                            NodeDrawer nodeDrawer = FindNodeDrawer(Logs[indexLogs].Nodes[0]);
                            nodeDrawer.PenBorder.Color = activeColor;
                            prevActive.Enqueue(nodeDrawer);
                            break;
                        }

                    case LogTreapEvent.EndSplite:
                        {
                            prevActive.Clear();
                            NodeDrawer nodeDrawer = FindNodeDrawer(Logs[indexLogs].Nodes[0]);
                            nodeDrawer.PenBorder.Color = PenBorder.Color;
                            break;
                        }
                    case LogTreapEvent.RemoveEdge:
                        {
                            NodeDrawer nodeDrawer = FindNodeDrawer(Logs[indexLogs].Nodes[0]);
                            nodeDrawer.Arrow = null;
                            break;
                        }

                    case LogTreapEvent.AddedEdge:
                        {
                            NodeDrawer nodeFrom = FindNodeDrawer(Logs[indexLogs].Nodes[0]);
                            NodeDrawer nodeTo = FindNodeDrawer(Logs[indexLogs].Nodes[1]);
                            nodeFrom.PenBorder.Color = activeColor;
                            nodeTo.Arrow = new ArrowDrawer(nodeFrom.Rect, nodeTo.Rect)
                            {
                                Pen = (Pen)PenArrow.Clone(),
                                LengthPointer = nodeTo.Size / 3,
                            };
                            break;
                        }

                    case LogTreapEvent.StartMerge:
                        {
                            ClearActive();
                            NodeDrawer nodeLeft = FindNodeDrawer(Logs[indexLogs].Nodes[0]);
                            NodeDrawer nodeRight = FindNodeDrawer(Logs[indexLogs].Nodes[1]);
                            nodeLeft.PenBorder.Color = activeColor;
                            nodeRight.PenBorder.Color = activeColor;
                            prevActive.Enqueue(nodeLeft);
                            prevActive.Enqueue(nodeRight);
                            break;
                        }
                    case LogTreapEvent.EndMerge:
                        {
                            prevActive.Clear();
                            NodeDrawer nodeLeft = FindNodeDrawer(Logs[indexLogs].Nodes[0]);
                            NodeDrawer nodeRight = FindNodeDrawer(Logs[indexLogs].Nodes[1]);
                            nodeLeft.PenBorder.Color = PenBorder.Color;
                            nodeRight.PenBorder.Color = PenBorder.Color;
                            break;
                        }

                }
                Draw();
                indexLogs++;
            }
        }



    }
}