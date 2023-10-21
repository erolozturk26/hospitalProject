using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Doktor d1 = new Doktor();
            d1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hastalar h1 = new Hastalar();
            h1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Poliklinikler p1 = new Poliklinikler();
            p1.Show();
            this.Hide();
        }
    }
}
