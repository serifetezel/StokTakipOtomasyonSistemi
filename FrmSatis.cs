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

namespace Stok_Takip_Otomasyonu
{
    public partial class FrmSatis : Form
    {
        public FrmSatis()
        {
            InitializeComponent();
        }
        MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=Stok_Takip;Uid=root;Pwd=98765Ab1.");
        DataSet daset = new DataSet();
        private void sepetlistele()
        {
            baglanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select *from basket", baglanti);
            adtr.Fill(daset, "basket");
            dataGridView1.DataSource = daset.Tables["basket"];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            baglanti.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmMüsteriEkle ekle = new FrmMüsteriEkle();
            ekle.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmMüşteriListele listele = new FrmMüşteriListele();
            listele.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmÜrünEkle ekle = new frmÜrünEkle();
            ekle.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmKategori kategori = new frmKategori();
            kategori.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmMarka marka = new frmMarka();
            marka.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            frmSatışListele listele = new frmSatışListele();
            listele.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmÜrünListele listele = new frmÜrünListele();
            listele.ShowDialog();

        }
        private void hesapla()
        {
            try
            {
                baglanti.Open();
                MySqlCommand komut = new MySqlCommand("select sum(totalprice) from basket",baglanti);
                lblGenelToplam.Text = komut.ExecuteScalar() + "TL";
                baglanti.Close();
            }
            catch(Exception)
            {
                ;
            }
        }
        
        private void FrmSatis_Load(object sender, EventArgs e)
        {
            sepetlistele();
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            if (txtTc.Text == "")
            {
                txtAdSoyad.Text = "";
                txtTelefon.Text = "";
            }
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select *from customer where tc like '"+txtTc.Text+"'",baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtAdSoyad.Text = read["namesurname"].ToString();
                txtTelefon.Text = read["telephone"].ToString();
            }
            baglanti.Close();
        }
        private void Temizle()
        {
            if (txtBarkodNo.Text == "")
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtMiktar)
                        {
                            item.Text = "";
                        }
                    }
                }
            }
        }
        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            Temizle();
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select *from product where barcodeno like '" + txtBarkodNo.Text + "'", baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtUrunAdi.Text = read["productname"].ToString();
                txtSatışFiyatı.Text = read["saleprice"].ToString();
            }
            baglanti.Close();
        }

        
        bool durum;
        private void barkodkontrol()
        {
            durum = true;
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select *from basket", baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtBarkodNo.Text == read["barcodeno"].ToString())
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            barkodkontrol();
            if(durum == true)
            {
                baglanti.Open();
                MySqlCommand komut = new MySqlCommand("insert into basket(tc,namesurname,telephone,barcodeno,productname,amount,saleprice,totalprice,date) values(@tc,@namesurname,@telephone,@barcodeno,@productname,@amount,@saleprice,@totalprice,@date)", baglanti);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@namesurname", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telephone", txtTelefon.Text);
                komut.Parameters.AddWithValue("@barcodeno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@productname", txtUrunAdi.Text);
                komut.Parameters.AddWithValue("@amount", int.Parse(txtMiktar.Text));
                komut.Parameters.AddWithValue("@saleprice", double.Parse(txtSatışFiyatı.Text));
                komut.Parameters.AddWithValue("@totalprice", double.Parse(txtToplamFiyat.Text));
                komut.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            else
            {
                baglanti.Open();
                MySqlCommand komut2 = new MySqlCommand("update basket set amount=amount+'"+int.Parse(txtMiktar.Text)+ "'where barcodeno='" + txtBarkodNo.Text + "'", baglanti);
                komut2.ExecuteNonQuery();

                MySqlCommand komut3 = new MySqlCommand("update basket set totalprice = amount * saleprice where barcodeno='" + txtBarkodNo.Text + "'", baglanti);
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }
            
            txtMiktar.Text = "1";
            daset.Tables["basket"].Clear();
            sepetlistele();
            hesapla();
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    if (item != txtMiktar)
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void txtMiktar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtToplamFiyat.Text = (double.Parse(txtMiktar.Text) * double.Parse(txtSatışFiyatı.Text)).ToString();
            }
            catch(Exception)
            {
                ;
            }
        }

        private void txtSatisFiyati_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtToplamFiyat.Text = (double.Parse(txtMiktar.Text) * double.Parse(txtSatışFiyatı.Text)).ToString();
            }
            catch(Exception)
            {
                ;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("delete from basket where barcodeno='"+dataGridView1.CurrentRow.Cells["barcodeno"].Value.ToString()+"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("The product removed from the basket.");
            daset.Tables["basket"].Clear();
            sepetlistele();
            hesapla();
        }

        private void btnSatisİptal_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("delete from basket ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("Products removed from the basket.");
            daset.Tables["basket"].Clear();
            sepetlistele();
            hesapla();
        }

        private void btnSatisYap_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                baglanti.Open();
                MySqlCommand komut = new MySqlCommand("insert into sales(tc,namesurname,telephone,barcodeno,productname,amount,saleprice,totalprice,date) values(@tc,@namesurname,@telephone,@barcodeno,@productname,@amount,@saleprice,@totalprice,@date)", baglanti);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@namesurname", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telephone", txtTelefon.Text);
                komut.Parameters.AddWithValue("@barcodeno", dataGridView1.Rows[i].Cells["barcodeno"].Value.ToString());
                komut.Parameters.AddWithValue("@productname", dataGridView1.Rows[i].Cells["productname"].Value.ToString());
                komut.Parameters.AddWithValue("@amount", int.Parse(dataGridView1.Rows[i].Cells["amount"].Value.ToString()));
                komut.Parameters.AddWithValue("@saleprice", double.Parse(dataGridView1.Rows[i].Cells["saleprice"].Value.ToString()));
                komut.Parameters.AddWithValue("@totalprice", double.Parse(dataGridView1.Rows[i].Cells["totalprice"].Value.ToString()));
                komut.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                MySqlCommand komut2 = new MySqlCommand("update product set amount=amount-'" + int.Parse(dataGridView1.Rows[i].Cells["amount"].Value.ToString()) + "' where barcodeno='" + dataGridView1.Rows[i].Cells["barcodeno"].Value.ToString() + "'", baglanti);
                komut2.ExecuteNonQuery();
                baglanti.Close();
               
            }
            baglanti.Open();
            MySqlCommand komut3 = new MySqlCommand("delete from basket ", baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["basket"].Clear();
            sepetlistele();
            hesapla();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
