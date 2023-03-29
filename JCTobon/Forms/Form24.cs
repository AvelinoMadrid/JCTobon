using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JCTobon.Forms
{
    public partial class Form24 : Form
    {
        public Form24()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form22 abrir = new Form22();
            abrir.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form23 abrir = new Form23();
            abrir.Show();
        }
    }
}
