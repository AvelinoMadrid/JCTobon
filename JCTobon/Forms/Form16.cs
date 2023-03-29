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
using System.Data.SqlClient;

namespace JCTobon.Forms
{
    public partial class Form16 : Form
    {
        public Form16()
        {
            InitializeComponent();
          
        }

        string nombre;

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void button1_Click(object sender, EventArgs e)
        {
            //printPreviewDialog1.Show();

            int n_impresione = int.Parse(textBox1.Text);

            for (int i = 0; i < n_impresione; i++)
            {
                printDocument1.Print();

            }

         
        }

        public void muestraCodigo(string codigo,string nombre,string talla)
        {
            BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
            Codigo.IncludeLabel = true;
            label3.Text= nombre ;
            label4.Text = talla;
            panel3.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE39, codigo, Color.Black, Color.White, 200, 50); /*agregamos el codigo de barras, el tipo, color de fondo, ancho y alto*/
            
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Imprimir(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //e.Graphics.DrawImage(panel3.BackgroundImage, 50, 20);
            Font font = new Font("Arial", 10);
            Font font2 = new Font("Arial", 12);

        


            e.Graphics.DrawImage(panel3.BackgroundImage, 30, 40,130,35);


            e.Graphics.DrawString(label3.Text, font, Brushes.Black, 73, 90);
            //e.Graphics.DrawString(name, font, Brushes.Black, 50, 55);

            e.Graphics.DrawString(label4.Text, font2, Brushes.Black, 68, 102);
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Form16_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        public void n_impresiones(int numero)
        {
            textBox1.Text = numero.ToString();
        }


        private void button1_TextChanged(object sender, EventArgs e)
        {
           

        }

     
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingrese carácteres numericos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var vr = !string.IsNullOrEmpty(textBox1.Text);
            button1.Enabled = vr;


        }
    }
}
