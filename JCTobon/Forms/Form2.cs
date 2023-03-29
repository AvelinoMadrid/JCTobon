using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace JCTobon.Forms
{
    public partial class Form2 : Form
    {
        public Form2(string nombre)
        {
            InitializeComponent();
            label1.Text = "Usuario " + nombre;

        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport ("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void btnmenu_Click(object sender, EventArgs e)
        {
            if(menuvertical.Width == 250)
            {
                menuvertical.Width = 70;
            }

            else
            {
                menuvertical.Width = 250;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            abrirPanel(new Form111());    
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            abrirPanel(new Form4());
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        // metodo para abrir frame 

        private void abrirPanel(object Formhijo)
        {
            if (this.panelContenedorr.Controls.Count > 0)
                this.panelContenedorr.Controls.RemoveAt(0);
            Form fh = Formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedorr.Controls.Add(fh);
            this.panelContenedorr.Tag = fh;
            fh.Show();
        }

        private void btnproductos_Click(object sender, EventArgs e)
        {
            abrirPanel(new Form8());
        }

        private void btncatalogos_Click(object sender, EventArgs e)
        {
            abrirPanel(new Form21());
        }

        private void btnetiquetas_Click(object sender, EventArgs e)
        {
            abrirPanel(new Form7());
        }

        private void menuvertical_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnconfiguraciones_Click(object sender, EventArgs e)
        {
            abrirPanel(new Form12());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblhora.Text = DateTime.Now.ToLongTimeString();
            lblfecha.Text = DateTime.Now.ToLongDateString();

        }

        private void btnreportes_Click(object sender, EventArgs e)
        {
            abrirPanel(new Form20());   
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Form1 sesion = new Form1();
            sesion.Show();
            this.SetVisibleCore(false);
        }
    }
}
