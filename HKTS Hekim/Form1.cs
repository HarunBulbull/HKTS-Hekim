using Guna.UI2.WinForms;
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

namespace HKTS_Hekim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void kapat_MouseHover(object sender, EventArgs e)
        {
            kapat2.Stop();
            kapat1.Start();
        }

        private void kapat_MouseLeave(object sender, EventArgs e)
        {
            kapat1.Stop();
            kapat2.Start();
        }

        private void kapat1_Tick(object sender, EventArgs e)
        {
            int x = int.Parse(kapat.BackColor.ToString().Split('=')[2].Split(',')[0]);
            if (x < 255)
            {
                x += 10;
                kapat.BackColor = Color.FromArgb(x, 25, 25);
            }
            else
            {
                kapat1.Stop();
            }
        }

        private void kapat2_Tick(object sender, EventArgs e)
        {
            int x = int.Parse(kapat.BackColor.ToString().Split('=')[2].Split(',')[0]);
            if (x > 25)
            {
                x -= 10;
                kapat.BackColor = Color.FromArgb(x, 25, 25);
            }
            else
            {
                kapat2.Stop();
            }
        }

        private void kapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kucult_MouseHover(object sender, EventArgs e)
        {
            kucult2.Stop();
            kucult1.Start();
        }

        private void kucult_MouseLeave(object sender, EventArgs e)
        {
            kucult1.Stop();
            kucult2.Start();
        }

        private void kucult1_Tick(object sender, EventArgs e)
        {
            int x = int.Parse(kucult.BackColor.ToString().Split('=')[4].Split(']')[0]);
            if (x < 255)
            {
                x += 10;
                kucult.BackColor = Color.FromArgb(25, 25, x);
            }
            else
            {
                kucult1.Stop();
            }
        }

        private void kucult2_Tick(object sender, EventArgs e)
        {
            int x = int.Parse(kucult.BackColor.ToString().Split('=')[4].Split(']')[0]);
            if (x > 25)
            {
                x -= 10;
                kucult.BackColor = Color.FromArgb(25, 25, x);
            }
            else
            {
                kucult2.Stop();
            }
        }

        private void kucult_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        string ver = "1.0";
        public string host = "localhost";
        public string user = "root";
        public string pwd = "root";

        MySqlConnection con;
        MySqlDataReader dr;
        MySqlCommand cmd;

        string checkver;

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new MySqlConnection("server=" + host + ";user=" + user + ";password=" + pwd + ";database=HKTS");
            con.Open();
            string sorgu = "SELECT * FROM Ayarlar where id='" + 1 + "'";
            cmd = new MySqlCommand(sorgu, con);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                checkver = dr["ver"].ToString();
            }
            con.Close();
            if (ver != checkver)
            {
                Form2 fr = new Form2();
                fr.formmod = 1;
                fr.baslik = "HATA!";
                fr.str = "Geçersiz ya da eski bir sürüm kullanıyorsunuz! Lütfen programı aşağıdaki link üzerinden güncelleyerek tekrar çalıştırmayı deneyin.\n\nhttps://www.harunbulbull.com/hkts/";
                fr.ShowDialog();
                Application.Exit();
            }
            tc.Focus();
            rnd_filtre.Focus();
            aml_filtre.Focus();
            tc.Focus();
            kod.Focus();
            if (adult != "" && doktor != "")
            {
                veri();
                timer1.Start();
            }
        }

        private void tc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        DataTable dt1, dt2;

        public string doktor, dtc, adult;


        private void ft1()
        {
            if (rndf_ad.Checked == true)
            {
                dt1.DefaultView.RowFilter = string.Format("AD like '%" + rnd_filtre.Text + "%'");
            }
            if (rndf_soyad.Checked == true)
            {
                dt1.DefaultView.RowFilter = string.Format("SOYAD like '%" + rnd_filtre.Text + "%'");
            }
            if (rndf_tc.Checked == true)
            {
                dt1.DefaultView.RowFilter = string.Format("TC_NO like '%" + rnd_filtre.Text + "%'");
            }
        }

        private void ft2()
        {
            if (amlf_ad.Checked == true)
            {
                dt2.DefaultView.RowFilter = string.Format("AD like '%" + aml_filtre.Text + "%'");
            }
            if (amlf_soyad.Checked == true)
            {
                dt2.DefaultView.RowFilter = string.Format("SOYAD like '%" + aml_filtre.Text + "%'");
            }
            if (amlf_tc.Checked == true)
            {
                dt2.DefaultView.RowFilter = string.Format("TC_NO like '%" + aml_filtre.Text + "%'");
            }
        }

        private void aml_filtre_TextChanged(object sender, EventArgs e)
        {
            ft2();
        }

        string id;

        private void rnd_sil_Click(object sender, EventArgs e)
        {
            Form2 fr = new Form2();
            fr.baslik = "DİKKAT!";
            fr.str = "Seçilen veriyi silmek istediğinize emin misiniz?";
            fr.formmod = 2;
            if (fr.ShowDialog() == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in randevu.SelectedRows)
                {
                    id = row.Cells[0].Value.ToString();
                }
                con.Open();
                cmd = new MySqlCommand("Delete from randevular where id='" + id + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                veri();
            }
            check();
        }

        private void check()
        {
            if(randevu.Rows.Count > 0)
            {
                rnd_sil.Enabled = true;
            }
            else
            {
                rnd_sil.Enabled = false;
            }
            if (ameliyat.Rows.Count > 0)
            {
                aml_sil.Enabled = true;
            }
            else
            {
                aml_sil.Enabled = false;
            }
        }

        private void aml_sil_Click(object sender, EventArgs e)
        {
            Form2 fr = new Form2();
            fr.baslik = "DİKKAT!";
            fr.str = "Seçilen veriyi silmek istediğinize emin misiniz?";
            fr.formmod = 2;
            if (fr.ShowDialog() == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in ameliyat.SelectedRows)
                {
                    id = row.Cells[0].Value.ToString();
                }
                con.Open();
                cmd = new MySqlCommand("Delete from ameliyatlar where id='" + id + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                veri();
            }
            check();
        }

        int r1, a1;

        private void timer1_Tick(object sender, EventArgs e)
        {
            con.Open();
            string komut = "select * from randevular where Adult='" + adult + "' AND DOKTOR='" + doktor + "'";
            MySqlDataAdapter da = new MySqlDataAdapter(komut, con);
            DataTable dtt1 = new DataTable();
            da.Fill(dtt1);
            r1 = dtt1.Rows.Count;
            con.Close();
            con.Open();
            komut = "select * from ameliyatlar where Adult='" + adult + "' AND DOKTOR='" + doktor + "'";
            da = new MySqlDataAdapter(komut, con);
            dtt1 = new DataTable();
            da.Fill(dtt1);
            a1 = dtt1.Rows.Count;
            con.Close();
            if (r1 > r || a1 > a)
            {
                veri();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form3 fr = new Form3();
            fr.adult = adult;
            fr.doktor = doktor;
            fr.Show();
            this.Hide();
        }

        private void rnd_filtre_TextChanged(object sender, EventArgs e)
        {
            ft1();
        }

        private void veri()
        {
            con.Open();
            string komut = "select * from randevular where Adult='" + adult + "' AND DOKTOR='" + doktor + "'";
            MySqlDataAdapter da = new MySqlDataAdapter(komut, con);
            dt1 = new DataTable();
            da.Fill(dt1);
            con.Close();
            dt1.Columns.RemoveAt(1);
            dt1.Columns.RemoveAt(6);
            dt1.Columns.RemoveAt(4);
            randevu.DataSource = dt1;
            randevu.Columns[1].HeaderText = "TC";
            randevu.Sort(this.randevu.Columns["TARIH"], ListSortDirection.Descending);
            randevu.Columns[0].Visible = false;
            con.Open();
            komut = "select * from ameliyatlar where Adult='" + adult + "' AND DOKTOR='" + doktor + "'";
            MySqlDataAdapter da1 = new MySqlDataAdapter(komut, con);
            dt2 = new DataTable();
            da1.Fill(dt2);
            con.Close();
            dt2.Columns.RemoveAt(1);
            dt2.Columns.RemoveAt(6);
            dt2.Columns.RemoveAt(4);
            ameliyat.DataSource = dt2;
            ameliyat.Columns[1].HeaderText = "TC";
            ameliyat.Sort(this.ameliyat.Columns["TARIH"], ListSortDirection.Descending);
            ameliyat.Columns[0].Visible = false;
        }

        int r, a;

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            dtc = tc.Text;
            adult = kod.Text;
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Select * from doktorlar where Adult='" + adult + "' AND TC_NO='" + dtc + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                con.Open();
                cmd = new MySqlCommand("Select * from doktorlar where TC_NO='" + dtc + "'", con);
                cmd.ExecuteNonQuery();
                DataTable data = new DataTable();
                MySqlDataAdapter dadp = new MySqlDataAdapter(cmd);
                dadp.Fill(data);
                foreach (DataRow dr in data.Rows)
                {
                    doktor = dr["AD"].ToString() + " " + dr["SOYAD"].ToString() + " (" + dr["ALAN"].ToString() + ")";
                }
                con.Close();
                veri();
                timer1.Start();
                r = randevu.Rows.Count;
                a = ameliyat.Rows.Count;
                kod.Text = "";
                tc.Text = "";
            }
            else
            {
                Form2 fr = new Form2();
                fr.baslik = "HATA!";
                fr.formmod = 1;
                fr.str = "Geçersiz bir kod veya tc girdiniz!";
                fr.ShowDialog();
            }
            con.Close();
            check();
        }
    }
}
