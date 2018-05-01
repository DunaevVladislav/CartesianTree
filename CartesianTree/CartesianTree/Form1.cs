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
            for(int i= 0; i < 1000; ++i)
            {
                treap.Add(new HeightAndSizeInfo(rnd.Next()));
            }
            var r = treap.Split(new HeightAndSizeInfo(1000000));
            int asdasd = 0;
        }
    }
}
