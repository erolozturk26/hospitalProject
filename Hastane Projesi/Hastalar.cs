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
    public partial class Hastalar : Form
    {
        public Hastalar()
        {
            InitializeComponent();
        }

        //SqlConnection conn1 = new SqlConnection("Server=DESKTOP-MO5HLC4;Database=HastaneProje;Integrated Security=true");
        SqlConnection conn1 = new SqlConnection("Server=.;Database=Hastane_Projesi;Integrated Security=true");

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        public void Goster()
        {
            SqlCommand komut = new SqlCommand(); //Sql Komutları yazdırabilir.
            komut.Connection = conn1;
            komut.CommandType = CommandType.StoredProcedure;
            komut.CommandText = "HastaGoruntule"; //Prosedürün adını bulur.

            SqlDataAdapter dp = new SqlDataAdapter(komut); //Sql ile Form Aracısı ve düzensiz şekilde alır.
            DataTable dt = new DataTable();
            dp.Fill(dt); //Gelen düzensiz verileri düzenli hale getir.
            dataGridView1.DataSource = dt; //Veri Kaynağımda Göster.
        }

        private void button1_Click(object sender, EventArgs e) //EKLE TUŞU
        {
            if (textBox2.Text != "")
            {
                textBox1.Text = null;
                conn1.Open();
                SqlCommand komut = new SqlCommand();
                komut.Connection = conn1;
                komut.CommandType = CommandType.StoredProcedure;
                komut.CommandText = "HastaEkle";
                komut.Parameters.AddWithValue("hasta_adsoyad", textBox2.Text);
                komut.Parameters.AddWithValue("hasta_tc", textBox3.Text);
                komut.Parameters.AddWithValue("hasta_dogumtarihi", dateTimePicker1.Value);
                komut.Parameters.AddWithValue("hasta_boy", textBox7.Text);
                komut.Parameters.AddWithValue("hasta_yas", textBox5.Text);
                komut.Parameters.AddWithValue("hasta_recete", textBox4.Text);
                komut.Parameters.AddWithValue("hasta_rapordurumu", textBox6.Text);
                komut.Parameters.AddWithValue("hasta_randevutarihi", dateTimePicker2.Value);
                komut.Parameters.AddWithValue("doktor_no", comboBox1.SelectedValue);
                komut.ExecuteNonQuery();
                conn1.Close();
                Goster();
                Temizle();
            }
            else
            {
                MessageBox.Show("En az Adsoyad girilmelidir...","BİLGİ");
            }
        }

        public void Hastalar_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hastane_ProjesiDataSet.Doktor' table. You can move, or remove it, as needed.
            this.doktorTableAdapter3.Fill(this.hastane_ProjesiDataSet.Doktor);
            // TODO: This line of code loads data into the 'hastaneProjeDataSet1.doktor' table. You can move, or remove it, as needed.
            //this.doktorTableAdapter1.Fill(this.hastaneProjeDataSet1.doktor);
            // TODO: This line of code loads data into the 'hastaneProjeDataSet.doktor' table. You can move, or remove it, as needed.
            //this.doktorTableAdapter.Fill(this.hastaneProjeDataSet.doktor);

            Goster();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sectim = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            textBox8.Text = dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[sectim].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[sectim].Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[sectim].Cells[3].Value.ToString();
            textBox7.Text = dataGridView1.Rows[sectim].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[sectim].Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.Rows[sectim].Cells[6].Value.ToString();
            textBox6.Text = dataGridView1.Rows[sectim].Cells[7].Value.ToString();
            dateTimePicker2.Text = dataGridView1.Rows[sectim].Cells[8].Value.ToString();
            comboBox1.SelectedValue = dataGridView1.Rows[sectim].Cells[9].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e) //GÜNCELLEME BUTON
        {
            if (textBox1.Text != "")
            {
                conn1.Open();
                SqlCommand komut = new SqlCommand();
                komut.Connection = conn1;
                komut.CommandType = CommandType.StoredProcedure;
                komut.CommandText = "HastaGuncelle";
                komut.Parameters.AddWithValue("hasta_no", textBox1.Text);
                komut.Parameters.AddWithValue("hasta_adsoyad", textBox2.Text);
                komut.Parameters.AddWithValue("hasta_tc", textBox3.Text);
                komut.Parameters.AddWithValue("hasta_dogumtarihi", dateTimePicker1.Value);
                komut.Parameters.AddWithValue("hasta_boy", textBox7.Text);
                komut.Parameters.AddWithValue("hasta_yas", textBox5.Text);
                komut.Parameters.AddWithValue("hasta_recete", textBox4.Text);
                komut.Parameters.AddWithValue("hasta_rapordurumu", textBox6.Text);
                komut.Parameters.AddWithValue("hasta_randevutarihi", dateTimePicker2.Value);
                komut.Parameters.AddWithValue("doktor_no", comboBox1.SelectedValue);
                komut.ExecuteNonQuery();
                conn1.Close();
                Goster();
                Temizle();
            }
            else
            {
                MessageBox.Show("Liste üzerinden hasta seçiniz...","BİLGİ");
            }
        }
        public void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            comboBox1.SelectedValue = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult Result1 = MessageBox.Show("Hastayı Silmek İstediğinize Emin Misiniz ?","UYARI",MessageBoxButtons.YesNo);
            if (Result1 == DialogResult.Yes)
            {
                if (textBox8 != null)
                {
                    conn1.Open();
                    SqlCommand komut = new SqlCommand();
                    komut.Connection = conn1;
                    komut.CommandType = CommandType.StoredProcedure;
                    komut.CommandText = "HastaSil";
                    komut.Parameters.AddWithValue("hasta_no", textBox1.Text);
                    komut.ExecuteNonQuery();
                    conn1.Close();
                    Goster();
                    textBox8.Clear();
                }
                else
                {
                    MessageBox.Show("Hasta Numarası Bölümü Boş Bırakılamaz...", "UYARI");
                }
            }
            else if(Result1 == DialogResult.No)
            {

            }
        } //SİLME TUŞU

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.doktorTableAdapter.FillBy(this.hastaneProjeDataSet.doktor);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
        } //GÖSTER TUŞU

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
        } //GİZLE TUŞU

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox10.Text != "")
            {
                conn1.Open();
                SqlCommand komut = new SqlCommand(); //Sql Komutları yazdırabilir.
                komut.Connection = conn1;
                komut.CommandType = CommandType.StoredProcedure;
                komut.CommandText = "HastaNoArama"; //Prosedürün adını bulur.
                komut.Parameters.AddWithValue("hasta_no", textBox10.Text);

                SqlDataAdapter dp = new SqlDataAdapter(komut); //Sql ile Form Aracısı ve düzensiz şekilde alır.
                DataTable dt = new DataTable();
                dp.Fill(dt); //Gelen düzensiz verileri düzenli hale getir.
                dataGridView1.DataSource = dt; //Veri Kaynağımda Göster.
                conn1.Close();
                textBox10.Clear();
            }
            if (textBox11.Text!="")
            {
                conn1.Open();
                SqlCommand komut = new SqlCommand(); //Sql Komutları yazdırabilir.
                komut.Connection = conn1;
                komut.CommandType = CommandType.StoredProcedure;
                komut.CommandText = "HastaIsimArama"; //Prosedürün adını bulur.
                komut.Parameters.AddWithValue("hasta_adi", "%"+textBox11.Text+"%");

                SqlDataAdapter dp = new SqlDataAdapter(komut); //Sql ile Form Aracısı ve düzensiz şekilde alır.
                DataTable dt = new DataTable();
                dp.Fill(dt); //Gelen düzensiz verileri düzenli hale getir.
                dataGridView1.DataSource = dt; //Veri Kaynağımda Göster.
                conn1.Close();
                textBox11.Clear();
            }
            if (textBox12.Text != "")
            {
                conn1.Open();
                SqlCommand komut = new SqlCommand(); //Sql Komutları yazdırabilir.
                komut.Connection = conn1;
                komut.CommandType = CommandType.StoredProcedure;
                komut.CommandText = "HastaTcArama"; //Prosedürün adını bulur.
                komut.Parameters.AddWithValue("hasta_tc",textBox12.Text);

                SqlDataAdapter dp = new SqlDataAdapter(komut); //Sql ile Form Aracısı ve düzensiz şekilde alır.
                DataTable dt = new DataTable();
                dp.Fill(dt); //Gelen düzensiz verileri düzenli hale getir.
                dataGridView1.DataSource = dt; //Veri Kaynağımda Göster.
                conn1.Close();
                textBox12.Clear();
            }
        } //ARAMA TUŞU

        private void button11_Click(object sender, EventArgs e)
        {
            Goster();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            conn1.Open();
            SqlCommand komut = new SqlCommand(); //Sql Komutları yazdırabilir.
            komut.Connection = conn1;
            komut.CommandType = CommandType.StoredProcedure;
            komut.CommandText = "HastaGoruntuleAZ"; //Prosedürün adını bulur.

            SqlDataAdapter dp = new SqlDataAdapter(komut); //Sql ile Form Aracısı ve düzensiz şekilde alır.
            DataTable dt = new DataTable();
            dp.Fill(dt); //Gelen düzensiz verileri düzenli hale getir.
            dataGridView1.DataSource = dt; //Veri Kaynağımda Göster.
            conn1.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            conn1.Open();
            SqlCommand komut = new SqlCommand(); //Sql Komutları yazdırabilir.
            komut.Connection = conn1;
            komut.CommandType = CommandType.StoredProcedure;
            komut.CommandText = "HastaGoruntuleZA"; //Prosedürün adını bulur.

            SqlDataAdapter dp = new SqlDataAdapter(komut); //Sql ile Form Aracısı ve düzensiz şekilde alır.
            DataTable dt = new DataTable();
            dp.Fill(dt); //Gelen düzensiz verileri düzenli hale getir.
            dataGridView1.DataSource = dt; //Veri Kaynağımda Göster.
            conn1.Close();
        }
    }
}
