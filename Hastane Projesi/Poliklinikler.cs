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
    public partial class Poliklinikler : Form
    {
        public Poliklinikler()
        {
            InitializeComponent();
        }

        //SqlConnection conn1 = new SqlConnection("Server=DESKTOP-MO5HLC4;Database=HastaneProje;Integrated Security=true");
        SqlConnection conn1 = new SqlConnection("Server=.;Database=Hastane_Projesi;Integrated Security=true");
        SqlCommand komut = new SqlCommand();

        public void Goster()
        {
            SqlCommand komut = new SqlCommand(); //Sql Komutları yazdırabilir.
            komut.Connection = conn1;
            komut.CommandType = CommandType.StoredProcedure;
            komut.CommandText = "PoliklinikGoruntule"; //Prosedürün adını bulur.

            SqlDataAdapter dp = new SqlDataAdapter(komut); //Sql ile Form Aracısı ve düzensiz şekilde alır.
            DataTable dt = new DataTable();
            dp.Fill(dt); //Gelen düzensiz verileri düzenli hale getir.
            dataGridView1.DataSource = dt; //Veri Kaynağımda Göster.
        }
        public void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox7.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                textBox1.Text = null;
                conn1.Open();
                komut.Connection = conn1;
                komut.CommandType = CommandType.StoredProcedure;
                komut.CommandText = "PoliklinikEkle";
                komut.Parameters.AddWithValue("poliklinik_adi", textBox2.Text);
                komut.Parameters.AddWithValue("poliklinik_uzmansayisi", textBox3.Text);
                komut.Parameters.AddWithValue("poliklinik_baskani", textBox7.Text);
                komut.Parameters.AddWithValue("poliklinik_bashemsire", textBox5.Text);
                komut.Parameters.AddWithValue("poliklinik_yataksayisi", textBox4.Text);
                komut.ExecuteNonQuery();
                conn1.Close();
                Goster();
                Temizle();
            }
            else
            {
                MessageBox.Show("En az Poliklinik Adı girilmelidir...", "BİLGİ");
            }
        } //EKLE BUTONU

        private void Poliklinikler_Load(object sender, EventArgs e)
        {
            Goster();
        } //FORM YÜKLENMESİ

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                conn1.Open();
                komut.Connection = conn1;
                komut.CommandType = CommandType.StoredProcedure;
                komut.CommandText = "PoliklinikGuncelle";
                komut.Parameters.AddWithValue("poliklinik_no",textBox1.Text);
                komut.Parameters.AddWithValue("poliklinik_adi", textBox2.Text);
                komut.Parameters.AddWithValue("poliklinik_uzmansayisi", textBox3.Text);
                komut.Parameters.AddWithValue("poliklinik_baskani", textBox7.Text);
                komut.Parameters.AddWithValue("poliklinik_bashemsire", textBox5.Text);
                komut.Parameters.AddWithValue("poliklinik_yataksayisi", textBox4.Text);
                komut.ExecuteNonQuery();
                conn1.Close();
                Goster();
                Temizle();
            }
            else
            {
                MessageBox.Show("Liste üzerinden Poliklinik seçiniz...", "BİLGİ");
            }
        } //GÜNCELLEME BUTONUs

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //TABLO KODLARI
        {
            int sectim = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            textBox8.Text = dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[sectim].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[sectim].Cells[2].Value.ToString();
            textBox7.Text = dataGridView1.Rows[sectim].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[sectim].Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.Rows[sectim].Cells[5].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e) //SİLME BUTONU
        {
            DialogResult Result1 = MessageBox.Show("Polikliniği Silmek İstediğinize Emin Misiniz ?", "UYARI", MessageBoxButtons.YesNo);
            if (Result1 == DialogResult.Yes)
            {
                if (textBox8 != null)
                {
                    conn1.Open();
                    SqlCommand komut = new SqlCommand();
                    komut.Connection = conn1;
                    komut.CommandType = CommandType.StoredProcedure;
                    komut.CommandText = "PoliklinikSil";
                    komut.Parameters.AddWithValue("poliklinik_no", textBox1.Text);
                    komut.ExecuteNonQuery();
                    conn1.Close();
                    Goster();
                    Temizle();
                    textBox8.Clear();
                }
                else
                {
                    MessageBox.Show("Poliklinik Numarası Bölümü Boş Bırakılamaz...", "UYARI");
                }
            }
            else if (Result1 == DialogResult.No)
            {

            }
        }

        private void button5_Click(object sender, EventArgs e) //GÖSTER BUTONU
        {
            dataGridView1.Show();
        }

        private void button8_Click(object sender, EventArgs e) //GİZLE BUTONU
        {
            dataGridView1.Hide();
        }

        private void button10_Click(object sender, EventArgs e) //A-Z SIRALAMA
        {
            conn1.Open();
            SqlCommand komut = new SqlCommand(); //Sql Komutları yazdırabilir.
            komut.Connection = conn1;
            komut.CommandType = CommandType.StoredProcedure;
            komut.CommandText = "PoliklinikGoruntuleAZ"; //Prosedürün adını bulur.

            SqlDataAdapter dp = new SqlDataAdapter(komut); //Sql ile Form Aracısı ve düzensiz şekilde alır.
            DataTable dt = new DataTable();
            dp.Fill(dt); //Gelen düzensiz verileri düzenli hale getir.
            dataGridView1.DataSource = dt; //Veri Kaynağımda Göster.
            conn1.Close();
        }

        private void button9_Click(object sender, EventArgs e) //Z-A SIRALAMA
        {
            conn1.Open();
            SqlCommand komut = new SqlCommand(); //Sql Komutları yazdırabilir.
            komut.Connection = conn1;
            komut.CommandType = CommandType.StoredProcedure;
            komut.CommandText = "PoliklinikGoruntuleZA"; //Prosedürün adını bulur.

            SqlDataAdapter dp = new SqlDataAdapter(komut); //Sql ile Form Aracısı ve düzensiz şekilde alır.
            DataTable dt = new DataTable();
            dp.Fill(dt); //Gelen düzensiz verileri düzenli hale getir.
            dataGridView1.DataSource = dt; //Veri Kaynağımda Göster.
            conn1.Close();
        }

        private void button3_Click(object sender, EventArgs e) //ARAMA BUTONU
        {
            if (textBox10.Text != "")
            {
                conn1.Open();
                SqlCommand komut = new SqlCommand(); //Sql Komutları yazdırabilir.
                komut.Connection = conn1;
                komut.CommandType = CommandType.StoredProcedure;
                komut.CommandText = "PoliklinikNoArama"; //Prosedürün adını bulur.
                komut.Parameters.AddWithValue("poliklinik_no", textBox10.Text);

                SqlDataAdapter dp = new SqlDataAdapter(komut); //Sql ile Form Aracısı ve düzensiz şekilde alır.
                DataTable dt = new DataTable();
                dp.Fill(dt); //Gelen düzensiz verileri düzenli hale getir.
                dataGridView1.DataSource = dt; //Veri Kaynağımda Göster.
                conn1.Close();
                textBox10.Clear();
            }
            if (textBox11.Text != "")
            {
                conn1.Open();
                SqlCommand komut = new SqlCommand(); //Sql Komutları yazdırabilir.
                komut.Connection = conn1;
                komut.CommandType = CommandType.StoredProcedure;
                komut.CommandText = "PoliklinikIsimArama"; //Prosedürün adını bulur.
                komut.Parameters.AddWithValue("poliklinik_adi", "%" + textBox11.Text + "%");

                SqlDataAdapter dp = new SqlDataAdapter(komut); //Sql ile Form Aracısı ve düzensiz şekilde alır.
                DataTable dt = new DataTable();
                dp.Fill(dt); //Gelen düzensiz verileri düzenli hale getir.
                dataGridView1.DataSource = dt; //Veri Kaynağımda Göster.
                conn1.Close();
                textBox11.Clear();
            }
        }

        private void button11_Click(object sender, EventArgs e) //TÜMÜNÜ GÖSTER
        {
            Goster();
        }
    }
}
