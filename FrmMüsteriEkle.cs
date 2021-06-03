using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;

namespace Stok_Takip_Otomasyonu
{
    public partial class FrmMüsteriEkle : Form
    {
        public FrmMüsteriEkle()
        {
            InitializeComponent();
        }
        MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=Stok_Takip;Uid=root;Pwd=98765Ab1.");
        private void FrmMüsteriEkle_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("insert into customer(tc,namesurname,telephone,address,email) values(@tc,@namesurname,@telephone,@address,@email)", baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@namesurname", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@telephone", txtTelefon.Text);
            komut.Parameters.AddWithValue("@address", txtAdres.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Customer record added.");
            foreach(Control item in this.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
    }
}
