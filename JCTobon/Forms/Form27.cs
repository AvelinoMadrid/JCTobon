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
using System.IO;

namespace JCTobon.Forms
{
    public partial class Form27 : Form
    {
        public Form27()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                String sql = "Select * from Productos where CodigoBarra ='" + txtcodigobarra.Text + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    txtcodigobarra.Text = reader[13].ToString();
                    txtstock.Text = reader[3].ToString();

                    MemoryStream ms = new MemoryStream((byte[])reader["Img"]);
                    Bitmap bm = new Bitmap(ms);
                    cargarimagen.Image = bm;

                }
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            actualizarStock();
            MessageBox.Show("Stock actualizado", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            txtcodigobarra.Text = null;
            txtcantidad.Text = null;
            txtstock.Text = null;
            cargarimagen.Image = null;


        }


        public void actualizarStock()
        {
            string codigo = txtcodigobarra.Text;
            int stock = int.Parse(txtstock.Text);
            int cantidad = int.Parse(txtcantidad.Text);
            int nuevostock = stock - cantidad;

            con.Open();
            SqlCommand query = new SqlCommand("Update Productos set Existencia = @Existencia where CodigoBarra = '" + codigo + "'", con);
            query.Parameters.AddWithValue("@Existencia", nuevostock);
            query.ExecuteNonQuery();
            con.Close();
        }
    }
}
