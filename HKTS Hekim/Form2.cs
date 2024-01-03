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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        public string baslik = string.Empty;
        public string str = string.Empty;
        public int formmod = 1;

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = baslik;
            label2.Text = str;
            this.Size = new Size(370, label2.Height + 100);
            guna2Button1.Location = new Point(16, this.Height - 40);
            guna2Button2.Location = new Point(16, this.Height - 40);
            guna2Button3.Location = new Point(196, this.Height - 40);
            if (formmod == 1)
            {
                guna2Button1.Visible = true;
            }
            else
            {
                guna2Button2.Visible = true;
                guna2Button3.Visible = true;
            }
        }
    }
}
