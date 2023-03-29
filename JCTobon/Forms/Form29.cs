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
    public partial class Form29 : Form
    {
        public Form29()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        public void mostrarFolio(string folio)
        {
            txtfolio.Text=folio;
                
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void txtfolio_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter sa = new SqlDataAdapter("Select IdCodigoBarra,Tipo,Nombre,Talla,CantidadPiezas,Existencia from Ventas where Folio = '"+txtfolio.Text+"' ", con);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string codigobarra;
            int cantidad, existencia,nuevostock;

            foreach (DataGridViewRow rows in dataGridView1.Rows)
            {
                codigobarra = rows.Cells[0].Value.ToString();
                cantidad = int.Parse(rows.Cells[3].Value.ToString());
                existencia = int.Parse(rows.Cells[4].Value.ToString());

                nuevostock = existencia - cantidad;

                // actualiza el stock en tabla ventas 
                con.Open();
                SqlCommand query = new SqlCommand("Update Productos set Existencia = @existencia where CodigoBarra = '"+codigobarra+"' ", con);
                query.Parameters.AddWithValue("@existencia", nuevostock);
                query.ExecuteNonQuery();
                con.Close();
                // actualizastock en productos
                con.Open();
                SqlCommand querys = new SqlCommand("Update Ventas set Existencia = @existencia where IdCodigoBarra = '" + codigobarra + "' ", con);
                querys.Parameters.AddWithValue("@existencia", nuevostock);
                querys.ExecuteNonQuery();
                con.Close();
            }

            MessageBox.Show("Stock Actualizados");
            this.Close();



        }
    }   
}
