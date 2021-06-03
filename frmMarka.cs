using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql;

namespace Stok_Takip_Otomasyonu
{
    public partial class frmMarka : Form
    {
        public frmMarka()
        {
            InitializeComponent();
        }
        MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=Stok_Takip;Uid=root;Pwd=98765Ab1.");
        bool durum;
        private void markakontrol()
        {
            durum = true;
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select *from brandinformation", baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (comboBox1.Text==read["category"].ToString() && textBox1.Text == read["brand"].ToString() || comboBox1.Text == ""|| textBox1.Text == "")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            markakontrol();
            if (durum == true)
            {
                baglanti.Open();
                MySqlCommand komut = new MySqlCommand("insert into brandinformation(category,brand) values('" + comboBox1.Text + "','" + textBox1.Text + "')", baglanti);
                komut.ExecuteNonQuery(); 
                baglanti.Close();
                MessageBox.Show("Brand added.");
            }
            else
            {
                MessageBox.Show("There is such a category and brand.", "Warning");
            }
            textBox1.Text = "";
            comboBox1.Text = "";
            
        }

        private void frmMarka_Load(object sender, EventArgs e)
        {
            kategorigetir();
        }

        private void kategorigetir()
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select *from categoryinformation", baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["category"].ToString());
            }
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.marka;
        }
    }
}
