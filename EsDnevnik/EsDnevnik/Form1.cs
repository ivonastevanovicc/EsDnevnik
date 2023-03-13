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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void skolskaGodinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forme d = new Forme("Skolska_godina");
            d.Text = "Skolska Godina";
            d.ShowDialog();
        }

        private void smerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forme d = new Forme("Smer");
            d.Text = "Smer";
            d.ShowDialog();
        }

        private void predmetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forme d = new Forme("Predmet");
            d.Text = "Predmet";
            d.ShowDialog();
        }

        private void upisnicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Upisnica d = new Upisnica();
            d.ShowDialog();
        }

        private void raspodelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Raspodela d = new Raspodela();
            d.ShowDialog();
        }

        private void oceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ocene d = new Ocene();
            d.ShowDialog();
        }
    }
}
