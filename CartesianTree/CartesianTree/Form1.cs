using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        private void TestRebBlackTree(int[] vals)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                dataGridView1.Rows.Add();
            }
            else
            {
                dataGridView1.Rows[0].Cells[0].Value = "";
                dataGridView1.Rows[0].Cells[1].Value = "";
                dataGridView1.Rows[0].Cells[2].Value = "";
                dataGridView1.Rows[0].Cells[3].Value = "";
            }

            dataGridView1.Rows[0].Cells[0].Value = "Красно-черное дерево";

            Stopwatch stopWatch = new Stopwatch();
            SortedSet<int> test = new SortedSet<int>();

            stopWatch.Restart();
            for (int i = 0; i < vals.Length; i++)
            {
                test.Add(vals[i]);
            }
            stopWatch.Stop();
            dataGridView1.Rows[0].Cells[1].Value = stopWatch.ElapsedTicks.ToString();

            stopWatch.Restart();
            for (int i = 0; i < vals.Length; i++)
            {
                test.Contains(vals[i]);
            }
            stopWatch.Stop();
            dataGridView1.Rows[0].Cells[2].Value = stopWatch.ElapsedTicks.ToString();

            stopWatch.Restart();
            for (int i = 0; i < vals.Length; i++)
            {
                test.Remove(vals[i]);
            }
            stopWatch.Stop();
            dataGridView1.Rows[0].Cells[3].Value = stopWatch.ElapsedTicks.ToString();
        }

        private void TestCartesianTree(int[] vals)
        {
            if (dataGridView1.Rows.Count < 2)
            {
                dataGridView1.Rows.Add();
            }
            else
            {
                dataGridView1.Rows[1].Cells[0].Value = "";
                dataGridView1.Rows[1].Cells[1].Value = "";
                dataGridView1.Rows[1].Cells[2].Value = "";
                dataGridView1.Rows[1].Cells[3].Value = "";
            }

            dataGridView1.Rows[1].Cells[0].Value = "Декартовое дерево";

            Stopwatch stopWatch = new Stopwatch();
            FastTreap test = null;

            stopWatch.Restart();
            for (int i = 0; i < vals.Length; i++)
            {
                FastTreap.Add(ref test, vals[i]);
            }
            stopWatch.Stop();
            dataGridView1.Rows[1].Cells[1].Value = stopWatch.ElapsedTicks.ToString();

            stopWatch.Restart();
            for (int i = 0; i < vals.Length; i++)
            {
                FastTreap.Contains(test, vals[i]);
            }
            stopWatch.Stop();
            dataGridView1.Rows[1].Cells[2].Value = stopWatch.ElapsedTicks.ToString();

            stopWatch.Restart();
            for (int i = 0; i < vals.Length; i++)
            {
                FastTreap.Remove(ref test, vals[i]);
            }
            stopWatch.Stop();
            dataGridView1.Rows[1].Cells[3].Value = stopWatch.ElapsedTicks.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int sz = (int)numericUpDown2.Value;
            int[] vals = new int[sz];
            Random rnd = new Random();
            for (int i = 0; i < sz; ++i)
            {
                vals[i] = rnd.Next() % (2 * sz);
            }

            TestRebBlackTree(vals);
            TestCartesianTree(vals);
            


        }
    }
}
