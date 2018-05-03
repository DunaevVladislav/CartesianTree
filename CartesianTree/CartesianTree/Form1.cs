using System;
using System.Collections;
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

        private TreapDrawer<NodeWithHeightAndSize, HeightAndSizeInfo> drawer = null;

        public Form1()
        {
            InitializeComponent();
            NodeOfCartesianTree<NodeWithHeightAndSize, HeightAndSizeInfo>.MinPriority = 0;
            NodeOfCartesianTree<NodeWithHeightAndSize, HeightAndSizeInfo>.MaxPriority = 10009;
            Random rnd = new Random();
            int num = 0;
            do
            {
                num++;
                treap = new Treap<NodeWithHeightAndSize, HeightAndSizeInfo>(true);
                for (int i = 0; i < 25; ++i)
                {
                    treap.Add(new HeightAndSizeInfo(rnd. Next(1000)));
                }
            } while (treap.Root.Info.Value.Height > 6);
            treap.Logs?.Clear();
            drawer = new TreapDrawer<NodeWithHeightAndSize, HeightAndSizeInfo>(treap, panel1.CreateGraphics(), panel1.Size.Width, panel1.Size.Height);
            drawer.Logs = treap.Logs;
            drawer.ResetLogs();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            drawer.Draw();
        }


        ToolTip t = new ToolTip()
        {
            AutomaticDelay = 0,
            InitialDelay = 0,
            AutoPopDelay = 100000,
            UseAnimation = false,
            UseFading = false,
        };
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            string info = drawer.GetDetailInfo(e.Location);
            if (info.Length != 0)
            {
                t.Hide(panel1);
                t.SetToolTip(panel1, info);
            }
        }

        Treap<NodeWithHeightAndSize, HeightAndSizeInfo> right = null;

        private void button1_Click(object sender, EventArgs e)
        {
            right = treap.Split(Convert.ToInt32(numericUpDown1.Value));
            button1.Enabled = false;
            button3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            drawer.NextEventInLog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            treap.Merge(right);
            button1.Enabled = true;
            button3.Enabled = false;
        }
    }
}
