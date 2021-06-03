using MySql.Data.MySqlClient;
using MySql;
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
    public partial class FrmMüşteriListele : Form
    {
        public FrmMüşteriListele()
        {
            InitializeComponent();
        }
        MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=Stok_Takip;Uid=root;Pwd=98765Ab1.");
        DataSet daset = new DataSet();
        private void FrmMüşteriListele_Load(object sender, EventArgs e)
        {
            Kayıt_Göster();
        }

        private void Kayıt_Göster()
        {
            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select *from customer", baglanti);
            adtr.Fill(daset, "customer");
            dataGridView1.DataSource = daset.Tables["customer"];
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells["namesurname"].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells["telephone"].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells["address"].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("update customer set namesurname=@namesurname,telephone=@telephone,address=@address,email=@email where tc=@tc",baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@namesurname", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@telephone", txtTelefon.Text);
            komut.Parameters.AddWithValue("@address", txtAdres.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["customer"].Clear();
            Kayıt_Göster();
            MessageBox.Show("Customer record updated.");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("delete from customer where tc='"+dataGridView1.CurrentRow.Cells["tc"].Value.ToString()+"'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["customer"].Clear();
            Kayıt_Göster();
            MessageBox.Show("Record deleted.");
        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select *from customer where tc like '%"+txtTcAra.Text+"%'",baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
    }
}
