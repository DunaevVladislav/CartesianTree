using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CartesianTree
{
    public partial class Form1 : Form
    {
        private Treap<NodeWithHeightAndSize, HeightAndSizeInfo> treap = new Treap<NodeWithHeightAndSize, HeightAndSizeInfo>();
        public Form1()
        {
            InitializeComponent();
            Random rnd = new Random();
            do
            {
                treap = new Treap<NodeWithHeightAndSize, HeightAndSizeInfo>();
                for (int i = 0; i < 31; ++i)
                {
                    treap.Add(new HeightAndSizeInfo(rnd.Next(10000)));
                }
            } while (treap.Root.Info.Value.Height > 6);

            int asdasd = 0;
            var qweq = new NodeDrawer()
            {
                Size = 92;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = panel1.CreateGraphics();
            Pen pen = new Pen(Color.Black, 2);
            graphics.DrawRectangle(pen, 100, 100, 60, 60);
            graphics.DrawString("Значение: 45\nПриоритет:12", new Font("Microsoft Sans Serif", 10), pen.Brush, 100, 100);
        }
    }
}
