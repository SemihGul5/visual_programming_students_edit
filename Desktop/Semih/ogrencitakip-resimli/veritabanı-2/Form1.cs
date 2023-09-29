using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb; //Acces bağlantı dosyaları

namespace veritabanı_2
{
    public partial class Form1 : Form
    {
        //Veri Tabanı Değişkenlerini Tanımlama Bölümü
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=okul.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet ds = new DataSet();


        public Form1()
        {
            InitializeComponent();
        }

        //DataGridWiev de kayıtları listeleme bölümü
        void listele()
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * from ogrenci", baglanti);
            adtr.Fill(ds, "ogrenci");
            dataGridView1.DataSource = ds.Tables["ogrenci"];
            adtr.Dispose();
            baglanti.Close();
        }

        //DataGridWiev de kayıtları listeleme bölümü
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        //Resim Seçme Bölümü
        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                oresim.Text = openFileDialog1.FileName;
            }
        }

        //Kayıt Ekleme Bölümü
        private void button1_Click(object sender, EventArgs e)
        {
            
            oresim.Text = pictureBox1.ImageLocation;
            if (tbno.Text != "" && tbadi.Text != "" && tbsoyadi.Text != "" && tbtel.Text != "" && oresim.Text!= "")
            {
                komut.Connection = baglanti;
                komut.CommandText = "Insert Into ogrenci(ogr_no,ogr_ad,ogr_soyad,ogr_tel,resim) Values ('" + tbno.Text + "','" + tbadi.Text + "','" + tbsoyadi.Text + "','" + tbtel.Text + "','" + oresim.Text + "')";
                baglanti.Open();
                komut.ExecuteNonQuery();
                komut.Dispose();
                baglanti.Close();
                MessageBox.Show("Kayıt Tamamlandı!");
                ds.Clear();
                listele();
            }
            else //BOŞ ALAN VARSA 
            {
                MessageBox.Show("Boş alan geçmeyiniz!");
            }
        }

        //Kayıt Silem Bölümü
        private void button2_Click(object sender, EventArgs e)
        {
            
            DialogResult c;
            c = MessageBox.Show("Silmek istediğinizden emin misiniz?","Uyarı!",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (c == DialogResult.Yes)
            {
                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "Delete from ogrenci where ogr_no=" + textBox1.Text + "";
                komut.ExecuteNonQuery();
                komut.Dispose();
                baglanti.Close();
                ds.Clear();
                listele();
            }
        }

        //Kayıtları DataGridden Textboxlara yazdırma bölümü
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            tbno.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tbadi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            tbsoyadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            tbtel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            oresim.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        //Kayıt Arama Bölümü
        private void button4_Click(object sender, EventArgs e)
        {
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=okul.accdb");
            adtr = new OleDbDataAdapter("SElect *from ogrenci where ogr_ad like '" + textBox2.Text + "%'", baglanti);
            ds = new DataSet();
            baglanti.Open();
            adtr.Fill(ds, "ogrenci");
            dataGridView1.DataSource = ds.Tables["ogrenci"];
            baglanti.Close();
        }

        //Kayıt Güncelleme Bölümü 
        private void button3_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "update ogrenci set ogr_ad='" + tbadi.Text + "', ogr_soyad='" + tbsoyadi.Text + "', ogr_tel='" + tbtel.Text + "', resim='" + oresim.Text + "' where ogr_no=" + tbno.Text + "";
            komut.ExecuteNonQuery();
            baglanti.Close();
            ds.Clear();
            listele();
        }
    }
}
