using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using System.Drawing.Imaging;
using System.IO;

namespace JCTobon.Forms
{
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrirImagen = new OpenFileDialog();
            // cargamos el explorador de archivos
            if(abrirImagen.ShowDialog() == DialogResult.OK) 
            {
                cargarimagen.ImageLocation = abrirImagen.FileName;
                cargarimagen.SizeMode = PictureBoxSizeMode.Zoom;

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string razon = txtRazon.Text;
            string direccion = txtDireccion.Text;

            MemoryStream ms = new MemoryStream();
            cargarimagen.Image.Save(ms, ImageFormat.Png);
            byte[] aByte = ms.ToArray();


            con.Open();
            // falta checar como insertar la imagen 
            //SqlCommand query = new SqlCommand("Insert into Configuracion (RazonSocial,Direccion) values (@RazonSocial,@direccion)", con);
            SqlCommand query = new SqlCommand("Update Configuracion  set RazonSocial = @RazonSocial, Direccion = @direccion, Imagen= @img Where ID = 7", con);
            query.Parameters.AddWithValue("@RazonSocial", razon);
            query.Parameters.AddWithValue("@direccion", direccion);
            query.Parameters.AddWithValue("@img", aByte).Value = ms.GetBuffer();


            query.ExecuteNonQuery();
            limpiar();
            MessageBox.Show(" Configuracion Guardada");
            con.Close();

        }

        public void limpiar()
        {
            txtRazon.Text = null;
            txtDireccion.Text = null;
            cargarimagen.Image = null;
        }



    }
}
