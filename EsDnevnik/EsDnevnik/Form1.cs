using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsDnevnik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void odeljenjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Odeljenje odeljenje = new Odeljenje();
            odeljenje.Show();
        }

        private void osobaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Osoba osoba = new Osoba();
            osoba.Show();
        }
    }
}
