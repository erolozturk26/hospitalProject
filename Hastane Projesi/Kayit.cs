using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hastane_Projesi
{
    public partial class Kayit : Form
    {
        public Kayit()
        {
            InitializeComponent();
        }

        //SqlConnection conn1 = new SqlConnection("Server=DESKTOP-MO5HLC4;Database=HastaneProje;Integrated Security=true");
        SqlConnection conn1 = new SqlConnection("Server=.;Database=Hastane_Projesi;Integrated Security=true");

        private void label5_Click(object sender, EventArgs e)
        {
            kayitol_panel.Visible = false;
            girisyap_panel.Visible = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            kayitol_panel.Visible = true;
            girisyap_panel.Visible = false;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e) //ÜYE OL TUŞU
        {
            if (textBox4.Text == "" || textBox2.Text == "" || textBox5.Text == "" || maskedTextBox1.Text == "(   )    -")
            {
                MessageBox.Show("Lütfen Alanları Boş Bırakmayınız...");
            }
            else
            {
                conn1.Open();
                SqlCommand komut = new SqlCommand();
                komut.Connection = conn1;
                komut.CommandType = CommandType.StoredProcedure;
                komut.CommandText = "KayitEkle";
                komut.Parameters.AddWithValue("kullanici_adi", textBox4.Text);
                komut.Parameters.AddWithValue("sifre", textBox2.Text);
                komut.Parameters.AddWithValue("mail_adresi", textBox5.Text);
                komut.Parameters.AddWithValue("telefon_numarasi", maskedTextBox1.Text);
                komut.ExecuteNonQuery();
                conn1.Close();
                textBox3.Text = textBox4.Text;
                Temizle();
                MessageBox.Show("KAYIT BAŞARILI", "BİLGİ");
                kayitol_panel.Visible = false;
                girisyap_panel.Visible = true;
            }
        }
        public void Temizle()
        {
            textBox4.Clear();
            textBox2.Clear();
            textBox5.Clear();
            maskedTextBox1.Clear();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            kayitol_panel.Visible = true;
            girisyap_panel.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
