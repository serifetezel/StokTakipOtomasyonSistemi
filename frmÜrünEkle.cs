using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stok_Takip_Otomasyonu
{
    public partial class frmÜrünEkle : Form
    {
        public frmÜrünEkle()
        {
            InitializeComponent();
        }
        MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=Stok_Takip;Uid=root;Pwd=98765Ab1.");
        bool durum;
        private void barkodkontrol()
        {
            durum = true;
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select *from product", baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtBarkodNo.Text==read["barcodeno"].ToString() || txtBarkodNo.Text=="")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        private void kategorigetir()
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select *from categoryinformation", baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboKategori.Items.Add(read["category"].ToString());
            }
            baglanti.Close();
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void frmÜrünEkle_Load(object sender, EventArgs e)
        {
            kategorigetir();
        }

        private void comboKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboMarka.Items.Clear();
            comboMarka.Text = "";
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select *from brandinformation where category='"+comboKategori.SelectedItem+"'", baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMarka.Items.Add(read["brand"].ToString());
            }
            baglanti.Close();
        }

        private void btnYeniEkle_Click(object sender, EventArgs e)
        {
            barkodkontrol();
            if (durum == true)
            {
                baglanti.Open();
                MySqlCommand komut = new MySqlCommand("insert into product(barcodeno,category,brand,productname,amount,purchaseprice,saleprice,date) values(@barcodeno,@category,@brand,@productname,@amount,@purchaseprice,@saleprice,@date)", baglanti);
                komut.Parameters.AddWithValue("@barcodeno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@category", comboKategori.Text);
                komut.Parameters.AddWithValue("@brand", comboMarka.Text);
                komut.Parameters.AddWithValue("@productname", txtÜrünAdı.Text);
                komut.Parameters.AddWithValue("@amount", int.Parse(txtMiktarı.Text));
                komut.Parameters.AddWithValue("@purchasename", double.Parse(txtAlışFiyatı.Text));
                komut.Parameters.AddWithValue("@saleprice", double.Parse(txtSatışFiyatı.Text));
                komut.Parameters.AddWithValue("@date", DateTime.Now.ToString());

                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Product added.");
            }
            else
            {
                MessageBox.Show("There is such a barcodeno.", "Warning");
            }
            comboMarka.Items.Clear();
            foreach(Control item in groupBox1.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void BarkodNotxt_TextChanged(object sender, EventArgs e)
        {
            if (BarkodNotxt.Text == "")
            {
                lblMiktari.Text = "";
                foreach(Control item in groupBox2.Controls)
                {
                    if(item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select *from product where barcodeno like '"+BarkodNotxt.Text+"'", baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                Kategoritxt.Text = read["category"].ToString();
                Markatxt.Text = read["brand"].ToString();
                ÜrünAdıtxt.Text = read["productname"].ToString();
                lblMiktari.Text = read["amount"].ToString();
                AlışFiyatıtxt.Text = read["purchaseprice"].ToString();
                SatışFiyatıtxt.Text = read["saleprice"].ToString();
            }
            baglanti.Close();
        }

        private void btnVarOlanaEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("update product set amount=amount+'"+int.Parse(Miktarıtxt.Text)+"' where barcodeno='"+BarkodNotxt.Text+"'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            MessageBox.Show("Added to existing product.");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
