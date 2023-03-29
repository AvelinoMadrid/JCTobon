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
    public partial class Form28 : Form
    {
        public Form28(Form26 actualizar)
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

        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);
        public event UpdateDelegate UpdateEventHandler;

        public class UpdateEventArgs: EventArgs
        {
            public string Data { get; set; }
        }

        protected void Agregar()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);

        }




        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string folio = txtvalidacion.Text;
            string tipo;
            string nombre;
            int talla;
            int precioventa;
            int cantidadpzas;
            int existencia;
            int total;
            DateTime fecha = DateTime.Now;

            con.Open();
            SqlCommand query = new SqlCommand("Select ID,Tipo,Nombre,Talla,PrecioVenta,CantidadPiezas,Total,Folio,Fecha from Ventas where Folio = "+folio+"",con);
            SqlDataReader registro = query.ExecuteReader();

            if (registro.HasRows)
            {
                registro.Read();
                tipo = registro["Tipo"].ToString();
                nombre = registro["Nombre"].ToString();
                talla = int.Parse(registro["Talla"].ToString());
                precioventa = int.Parse(registro["PrecioVenta"].ToString());
                cantidadpzas = int.Parse(registro["CantidadPiezas"].ToString());
                //existencia = int.Parse(registro["Existencia"].ToString());
                total = int.Parse(registro["Total"].ToString());
               


                con.Close();


                con.Open();
                SqlCommand querys = new SqlCommand("Insert into ventasValidadas (Folio,Tipo,Nombre,Talla,PrecioVenta,CantidadPiezas,Total,Fecha) values(@folio,@tipo,@nombre,@talla,@precioventa,@cantidadpzas,@total,@Fecha)", con);
                querys.Parameters.AddWithValue("@folio", folio);
                querys.Parameters.AddWithValue("@tipo", tipo);
                querys.Parameters.AddWithValue("@nombre", nombre);
                querys.Parameters.AddWithValue("@talla", talla);
                querys.Parameters.AddWithValue("@precioventa", precioventa);
                querys.Parameters.AddWithValue("@cantidadpzas", cantidadpzas);
                querys.Parameters.AddWithValue("@total", total);
                querys.Parameters.AddWithValue("@Fecha", fecha);
                querys.ExecuteNonQuery();

                Agregar();
                MessageBox.Show("Venta encontrada dentro del sistema");

                Form29 abrir = new Form29();
                abrir.mostrarFolio(folio);
                abrir.Show();


                this.Close();

            }

            else
            {
                MessageBox.Show("Venta NO encontrada dentro del sistema");
            }

            con.Close();

        }
    }
}
