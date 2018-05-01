﻿using System;
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
        public Form1()
        {
            InitializeComponent();
            NodeOfCartesianTree<NodeWithHeightAndSize, HeightAndSizeInfo>.MinPriority = 0;
            NodeOfCartesianTree<NodeWithHeightAndSize, HeightAndSizeInfo>.MaxPriority = 1000;
            Random rnd = new Random();
            do
            {
                treap = new Treap<NodeWithHeightAndSize, HeightAndSizeInfo>();
                for (int i = 0; i < 25; ++i)
                {
                    treap.Add(new HeightAndSizeInfo(rnd.Next(100)));
                }
            } while (treap.Root.Info.Value.Height > 5);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            TreapDrawer<NodeWithHeightAndSize, HeightAndSizeInfo> drawer = new TreapDrawer<NodeWithHeightAndSize, HeightAndSizeInfo>(treap, panel1.CreateGraphics(), panel1.Size.Width);
            drawer.Draw();
        }
    }
}
