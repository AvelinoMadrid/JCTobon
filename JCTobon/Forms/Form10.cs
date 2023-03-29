using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JCTobon.Forms
{
    public partial class Form10 : Form
    {
        public Form10(string nombre)
        {
            InitializeComponent();
            label1.Text = " Usuario : " + nombre;
            
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (menuvertical.Width == 250)
            {
                menuvertical.Width = 70;
            }

            else
            {
                menuvertical.Width = 250;
            }
        }


        // metodo para abrir frame 

        private void abrirPanel(object Formhijo)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = Formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();
        }






        private void Form10_Load(object sender, EventArgs e)
        {

        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            abrirPanel(new Form12());
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            abrirPanel(new Form111());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblfecha.Text = DateTime.Now.ToLongDateString();
            lblhora.Text = DateTime.Now.ToLongTimeString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            abrirPanel(new Form23());
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form1 sesion = new Form1();
            sesion.Show();
            this.SetVisibleCore(false);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            abrirPanel(new Form14());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            abrirPanel(new Form26());
        }
    }
}
