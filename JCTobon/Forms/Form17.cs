using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;


namespace JCTobon.Forms
{
    public partial class Form17 : Form
    {
        public Form17(Form productos)
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");
        public delegate void updateDelegate(object sender, UpdateEventArgs args);
        public event updateDelegate UpdateEventHandler;

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }

        protected void Agregar()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        public void mostradata(string id, string tipo, string nombre,string existencia, string precioMaquila, string utilidad, string precioVenta, string descripcion, string talla, string marca, string modelo, string color, string temporada, string codigobarra)
        {
            txtid.Text = id;    
            combox.Text = tipo;
            txtnombre.Text = nombre;
            txtexistencia.Text = existencia;
            txtpmaquila.Text = precioMaquila;
            txtutilidad.Text = utilidad;
            txtventa.Text = precioVenta;
            txtdescripcion.Text= descripcion;
            txttalla.Text = talla;
            txtmarca.Text = marca;
            txtcolor.Text = color;
            txtmodelo.Text = modelo;
            txttemporada.Text = temporada;
            txtcodigobarras.Text = codigobarra;

            con.Open();
            SqlCommand query1 = new SqlCommand("select * from Productos where ID = '" + id + "'", con);
            SqlDataReader registro = query1.ExecuteReader();


            if (registro.HasRows)
            {

                registro.Read();


                MemoryStream ms = new MemoryStream((byte[])registro["Img"]);
                Bitmap bm = new Bitmap(ms);
                cargarimagen.Image = bm;
            }
            con.Close();
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
             OpenFileDialog abrirImagen = new OpenFileDialog();
            // cargamos el explorador de archivos
            if (abrirImagen.ShowDialog() == DialogResult.OK)
            {
                cargarimagen.ImageLocation = abrirImagen.FileName;
                cargarimagen.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int flag = 0;
            MemoryStream ms = new MemoryStream();
            cargarimagen.Image.Save(ms, ImageFormat.Png);
            byte[] aByte = ms.ToArray();

            con.Open();
            SqlCommand query = new SqlCommand("Update Productos set Tipo = @tipo, Nombre = @nombre, Existencia = @existencia, Descripcion = @descripcion, Talla = @talla, Marca = @marca, Modelo = @modelo, Color = @color,Temporada = @temporada, Img = @img Where CodigoBarra = '" + txtcodigobarras.Text+ "' ", con);
            query.Parameters.AddWithValue("@tipo", combox.Text);
            query.Parameters.AddWithValue("@nombre", txtnombre.Text);
            query.Parameters.AddWithValue("@existencia", txtexistencia.Text);
            query.Parameters.AddWithValue("@descripcion", txtdescripcion.Text);
            query.Parameters.AddWithValue("@talla", txttalla.Text);
            query.Parameters.AddWithValue("@marca", txtmarca.Text);
            query.Parameters.AddWithValue("@modelo", txtmodelo.Text);
            query.Parameters.AddWithValue("@color", txtmodelo.Text);
            query.Parameters.AddWithValue("@temporada", txttemporada.Text);
            query.Parameters.AddWithValue("@img", aByte).Value = ms.GetBuffer();

            flag = query.ExecuteNonQuery();


            if (flag == 1)
            {
                MessageBox.Show("Dato Actualizado en el Sistema");
                Form8 abrir = new Form8();
                con.Close();
                Limpiar();
                Agregar();
                this.Close();
               
            }
            else
            {
                MessageBox.Show("Dato no Actualizado en el Sistema");
            }
           



        }


        public void Limpiar()
        {
            combox.Text = null;
            txtnombre.Text = null;
            txtexistencia.Text = null;
            txttalla.Text = null;
            txtmarca.Text = null;
            txtmodelo.Text = null;
            txtcolor.Text = null;
            txttemporada.Text = null;
            txtpmaquila.Text = null;
            txtutilidad.Text = null;
            txtventa.Text = null;
            txtcodigobarras.Text = null;
            txtdescripcion.Text = null;
            cargarimagen.Image = null;
            
           
        }

       
        private void button4_Click(object sender, EventArgs e)
        {
        
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
