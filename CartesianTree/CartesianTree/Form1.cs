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
        Treap<int> treap = new Treap<int>(5);
        public Form1()
        {
            InitializeComponent();
            var q = new Treap<int>(8);
            treap.Merge(q);
            treap.Split(5, treap, out q);
            int asdasd = 0;
        }
    }
}
