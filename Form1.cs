using System.Data;
using System.Data.SqlClient;

namespace MalzemeKayit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool durum;
        void tekrar()
        {
            baglanti.Open();
            SqlCommand tekrar = new SqlCommand("Select * from MalzemeKayitProjesi WHERE MalzemeKodu=@t1", baglanti);
            tekrar.Parameters.AddWithValue("@t1", textBox1.Text);
            SqlDataReader drr = tekrar.ExecuteReader();
            if (drr.Read())
            {
                durum = false;
            }
            else
            {
                durum = true;
            }
            baglanti.Close();
        }


        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-QGRG0NH;Initial Catalog=SqlDeneme;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {//EKLE BUTONU
            tekrar();
            if (durum == true)
            {

                String t1 = textBox1.Text; // MalzemeKodu
                String t2 = textBox2.Text; // MalzemeAdi
                String t3 = textBox3.Text; // YillikSatis
                String t4 = textBox4.Text; // BirimFiyat
                String t5 = textBox5.Text; // MinStok
                String t6 = textBox6.Text; // TSuresi

                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO MalzemeKayitProjesi(MalzemeKodu, MalzemeAdi, YillikSatis, BirimFiyat, MinStok, TSuresi) VALUES ('" + t1 + "','" + t2 + "','" + t3 + "','" + t4 + "','" + t5 + "','" + t6 + "' )", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                listele();
            }
            else
            {
                MessageBox.Show("Bu Malzeme Kodu Zaten Var !", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            listele();
        }
        private void listele() // Veritabanýndaki kayýtlarýn görüntülenmesi
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from MalzemeKayitProjesi ORDER BY MalzemeKodu", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {//SÝL BUTONU

            String t1 = textBox1.Text; // Seçilen Satýrýn Malzeme kodunu Al
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM MalzemeKayitProjesi WHERE MalzemeKodu=('" + t1 + "')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {//GÜNCELLE BUTONU

            String t1 = textBox1.Text; // MalzemeKodu
            String t2 = textBox2.Text; // MalzemeAdi
            String t3 = textBox3.Text; // YillikSatis
            String t4 = textBox4.Text; // BirimFiyat
            String t5 = textBox5.Text; // MinStok
            String t6 = textBox6.Text; // TSuresi

            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE MalzemeKayitProjesi SET MalzemeKodu='" + t1 + "', MalzemeAdi='" + t2 + "', YillikSatis='" + t3 + "', BirimFiyat='" + t4 + "', MinStok='" + t5 + "', TSuresi='" + t6 + "' WHERE MalzemeKodu='" + t1 + "' ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();

        }
    }
}