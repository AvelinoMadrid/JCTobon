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
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.Design.AxImporter;
using System.IO;

namespace JCTobon.Forms
{
    public partial class Form25 : Form
    {
        public Form25(Form8 productos)
        {
            InitializeComponent();
            GeneraCodigo();
            combox.DropDownStyle = ComboBoxStyle.DropDownList;
            combotemporada.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        static string nombre;

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");

        // uso de la POE ( Programacion Orientada a Eventos) 
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
            string op;

            MemoryStream ms = new MemoryStream();
            cargarimagen.Image.Save(ms, ImageFormat.Png);
            byte[] aByte = ms.ToArray();

            string Tipo = combox.Text;
            string Nombre = txtnombre.Text;
            int Existencia = int.Parse(txtexistencia.Text);
            int PrecioMaquila = int.Parse(txtpmaquila.Text);
            int Utilidad = int.Parse(txtutilidad.Text);
            int PrecioVenta = int.Parse(txtventa.Text);
            string Descripcion = txtdescripcion.Text;
            string Talla = txttalla.Text;
            string Marca = txtmarca.Text;
            string Color = txtcolor.Text;
            string Modelo = txtmodelo.Text;
            string Temporada = combotemporada.Text;
            string CodigoBarra = (txtcodigobarras.Text);
            string status = combostatus.Text;

            

            if (status.Equals("Activo")) {
                op = "1";
            }
            else
            {
                op = "2";
            }



            //string date = fecha.Value.ToString("dd/MM/yyyy");
            DateTime date = fecha.Value;
            date.ToString();

            con.Open();
            SqlCommand query = new SqlCommand("Insert into Productos (Tipo,Nombre,Existencia,UtilidadJCTobon,Utilidad,PrecioVenta,Descripcion,Talla,Marca,Modelo,Color,Temporada,CodigoBarra,Fecha,Status,Img) values (@Tipo,@Nombre,@Existencia,@PrecioMaquila,@Utilidad,@PrecioVenta,@Descripcion,@Talla,@Marca,@Modelo,@Color,@Temporada,@CodigoBarra,@Fecha,@Status,@Img)", con);
       
            query.Parameters.AddWithValue("@Tipo", Tipo);
            query.Parameters.AddWithValue("@Nombre", Nombre);
            query.Parameters.AddWithValue("@Existencia", Existencia);
            query.Parameters.AddWithValue("@PrecioMaquila", PrecioMaquila);
            query.Parameters.AddWithValue("@Utilidad", Utilidad);
            query.Parameters.AddWithValue("@PrecioVenta", PrecioVenta);
            query.Parameters.AddWithValue("@Descripcion", Descripcion);
            query.Parameters.AddWithValue("@Talla", Talla);
            query.Parameters.AddWithValue("@Marca", Marca);
            query.Parameters.AddWithValue("@Modelo", Modelo);
            query.Parameters.AddWithValue("@Color", Color);
            query.Parameters.AddWithValue("@Temporada", Temporada);
            query.Parameters.AddWithValue("@CodigoBarra", CodigoBarra);
            query.Parameters.AddWithValue("@Fecha", date);
            query.Parameters.AddWithValue("@Status", op);
            query.Parameters.AddWithValue("@Img", aByte).Value = ms.GetBuffer();
            query.ExecuteNonQuery();

            limpiar();
            this.Close();
            MessageBox.Show("Registro Guardado");
            Agregar();
            InsertEtiqueta(Nombre, Modelo, CodigoBarra, date,Talla);
            con.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int cantidad = int.Parse(txtexistencia.Text);
            string codigo = txtcodigobarras.Text;
            string nombre = txtnombre.Text;
            string talla = txttalla.Text;



            Form16 abrir = new Form16();
            abrir.muestraCodigo(codigo,nombre,talla);
            abrir.n_impresiones(cantidad);
            abrir.Show();
        }


        public void limpiar()
        {
            combox.Text = null;
            txtnombre.Text = null;
            txtexistencia.Text = null;
            txtpmaquila.Text = null;
            txtutilidad.Text = null;
            txtventa.Text = null;
            txtdescripcion.Text = null;
            txttalla.Text = null;
            txtmarca.Text = null;
            txtmodelo.Text = null;
            txtcolor.Text = null;
            combotemporada.Text = null;
            txtcodigobarras.Text = null;

            cargarimagen.Image = null;
            combostatus.Text = null;
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // Metodo para generar el codigo de barra en automatico 

        public void GeneraCodigo()
        {
            Random rnd = new Random();
            txtcodigobarras.Text = rnd.Next().ToString();
        }

        private void Form25_Load(object sender, EventArgs e)
        {
            con.Open();
            button3.Enabled = false;
            button2.Enabled = false;
            
            SqlCommand query = new SqlCommand("SELECT Tipo FROM Catalogos", con);
         
            SqlDataReader registro = query.ExecuteReader();

            while (registro.Read())
            {
                combox.Items.Add(registro["Tipo"].ToString());

            }
            con.Close();
        }

        private void combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validarcampos();
            mostrarValores();

        }

        // metodo para registrar la etiqueta 
        public void InsertEtiqueta(string nombre, string modelo, string CodigoBarra, DateTime fecha,string talla )
        {
            con.Open();
            SqlCommand query = new SqlCommand("Insert into Etiquetas (Tipo,Modelo,CodigoBarra,Fecha,Talla) Values (@tipo,@modelo,@CodigoBarra,@fecha,@talla)", con);
           
            query.Parameters.AddWithValue("@tipo", nombre);
            query.Parameters.AddWithValue("@modelo", modelo);
            query.Parameters.AddWithValue("@CodigoBarra", CodigoBarra);
            query.Parameters.AddWithValue("@fecha", fecha);
            query.Parameters.AddWithValue("@talla", talla);
            
            query.ExecuteNonQuery();
            con.Close();

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtexistencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47 ) || (e.KeyChar >= 58 && e.KeyChar <=255))
            {
                MessageBox.Show("Ingrese existencia valida (carácteres numericos)", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txttalla_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtmodelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            //{
            //    MessageBox.Show("Ingrese existencia valida (carácteres numericos)", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    e.Handled = true;
            //    return;
            //}
        }


        public void Validarcampos()
        {
            var vr = !string.IsNullOrEmpty(combox.Text) &&
                     !string.IsNullOrEmpty(txtnombre.Text) &&
                     !string.IsNullOrEmpty(txtexistencia.Text) &&
                     !string.IsNullOrEmpty(txtpmaquila.Text) &&
                     !string.IsNullOrEmpty(txtutilidad.Text) &&
                     !string.IsNullOrEmpty(txtventa.Text) &&
                     !string.IsNullOrEmpty(txtdescripcion.Text) &&
                     !string.IsNullOrEmpty(txttalla.Text) &&
                     !string.IsNullOrEmpty(txtmarca.Text) &&
                     !string.IsNullOrEmpty(txtmarca.Text) &&
                     !string.IsNullOrEmpty(txtmodelo.Text) &&
                     !string.IsNullOrEmpty(txtcolor.Text) &&
                     !string.IsNullOrEmpty(combotemporada.Text) &&
                     !string.IsNullOrEmpty(txtcodigobarras.Text) &&
                     !string.IsNullOrEmpty(combostatus.Text);
            button3.Enabled = vr;
            button2.Enabled = vr;
        }

        private void txtnombre_TextChanged(object sender, EventArgs e)
        {
            Validarcampos();
        }

        private void txtexistencia_TextChanged(object sender, EventArgs e)
        {
            Validarcampos();
        }

        private void txttalla_TextChanged(object sender, EventArgs e)
        {
            Validarcampos();
        }

        private void txtmarca_TextChanged(object sender, EventArgs e)
        {
            Validarcampos();
        }

        private void txtmodelo_TextChanged(object sender, EventArgs e)
        {
            Validarcampos();
        }

        private void txtcolor_TextChanged(object sender, EventArgs e)
        {
            Validarcampos();
        }

        private void combotemporada_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validarcampos();
        }

        private void txtpmaquila_TextChanged(object sender, EventArgs e)
        {
            Validarcampos();
        }

        private void txtutilidad_TextChanged(object sender, EventArgs e)
        {
           
            Validarcampos();
           
        }

        private void txtventa_TextChanged(object sender, EventArgs e)
        {
            Validarcampos();
        }

        private void txtcodigobarras_TextChanged(object sender, EventArgs e)
        {
            Validarcampos();
        }

        private void combostatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validarcampos();
        }

        private void txtdescripcion_TextChanged(object sender, EventArgs e)
        {
            Validarcampos();
        }


        private void txtutilidad_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtutilidad_Click(object sender, EventArgs e)
        {
           
        }


        public void mostrarValores()
        {
            if (con.State == ConnectionState.Closed)
            {

                con.Open();
            }


            string opcion = combox.Text;
           
            SqlCommand query1 = new SqlCommand("select * from Catalogos where Tipo = '" + opcion + "'", con);
        
            SqlDataReader registro = query1.ExecuteReader();

            if (registro.HasRows)
            {
                registro.Read();

                txtpmaquila.Text = registro["UtilidadJCTobon"].ToString();
                txtventa.Text = registro["PrecioEscuela"].ToString();
                txtutilidad.Text = registro["Utilidad"].ToString();
            }
            con.Close();
        }

    }
}
