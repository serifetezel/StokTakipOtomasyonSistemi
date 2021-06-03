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
    public partial class frmÜrünListele : Form
    {
        public frmÜrünListele()
        {
            InitializeComponent();
        }
        MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=Stok_Takip;Uid=root;Pwd=98765Ab1.");
        DataSet daset = new DataSet();
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
        private void frmÜrünListele_Load(object sender, EventArgs e)
        {
            ÜrünListele();
            kategorigetir();
        }

        private void ÜrünListele()
        {
            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select *from product", baglanti);
            adtr.Fill(daset, "product");
            dataGridView1.DataSource = daset.Tables["product"];
            baglanti.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BarkodNotxt.Text = dataGridView1.CurrentRow.Cells["barcodeno"].Value.ToString();
            Kategoritxt.Text = dataGridView1.CurrentRow.Cells["category"].Value.ToString();
            Markatxt.Text = dataGridView1.CurrentRow.Cells["brand"].Value.ToString();
            ÜrünAdıtxt.Text = dataGridView1.CurrentRow.Cells["productname"].Value.ToString();
            Miktarıtxt.Text = dataGridView1.CurrentRow.Cells["amount"].Value.ToString();
            AlışFiyatıtxt.Text = dataGridView1.CurrentRow.Cells["purchaseprice"].Value.ToString();
            SatışFiyatıtxt.Text = dataGridView1.CurrentRow.Cells["saleprice"].Value.ToString();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("update product set productname=@productname,amount=@amount,purchaseprice=@purchaseprice,saleprice=@saleprice where barcodeno=@barcodeno",baglanti);
            komut.Parameters.AddWithValue("@barcodeno", BarkodNotxt.Text);
            komut.Parameters.AddWithValue("@productname", ÜrünAdıtxt.Text);
            komut.Parameters.AddWithValue("@amount", int.Parse(Miktarıtxt.Text));
            komut.Parameters.AddWithValue("@purchaseprice", double.Parse(AlışFiyatıtxt.Text));
            komut.Parameters.AddWithValue("@saleprice",double.Parse(SatışFiyatıtxt.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["product"].Clear();
            ÜrünListele();
            MessageBox.Show("Was updated.");
            foreach(Control item in this.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnMarkaGuncelle_Click(object sender, EventArgs e)
        {
            if (BarkodNotxt.Text != "")
            {
                baglanti.Open();
                MySqlCommand komut = new MySqlCommand("update product set category=@category,brand=@brand where barcodeno=@barcodeno", baglanti);
                komut.Parameters.AddWithValue("@barcodeno", BarkodNotxt.Text);
                komut.Parameters.AddWithValue("@category", comboKategori.Text);
                komut.Parameters.AddWithValue("@brand", comboMarka.Text);

                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Was updated.");
                daset.Tables["product"].Clear();
                ÜrünListele();
            }
            else
            {
                MessageBox.Show("BarcodeNo not written.");
            }
            
            foreach (Control item in this.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void comboKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboMarka.Items.Clear();
            comboMarka.Text = "";
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select *from brandinformation where category='" + comboKategori.SelectedItem + "'", baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMarka.Items.Add(read["brand"].ToString());
            }
            baglanti.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("delete from product where barcodeno='" + dataGridView1.CurrentRow.Cells["barcodeno"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["product"].Clear();
            ÜrünListele();
            MessageBox.Show("Registration deleted.");
        }

        private void txtBarkodNoAra_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select *from product where barcodeno like '%" + txtBarkodNoAra.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

       private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.satıış_listele;
        }
       
    }
}
