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
using System.Windows.Forms.VisualStyles;
using System.Web;

namespace JCTobon.Forms
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            cargarData();

        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");



        private void button1_Click(object sender, EventArgs e)
        {

            Form6 abrir = new Form6();
            abrir.Show();


        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void cargarData()
        {
            SqlDataAdapter sa = new SqlDataAdapter("Select * from Catalogos", con);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;

        }


        public void Recibir(double maquila, double proveedor, double utilidad, double escuela)
        {
            MessageBox.Show("hola desde form 5 Precio Maquila" + maquila);
            MessageBox.Show("hola desde form 5 Precio proveedor" + proveedor);
            MessageBox.Show("hola desde form 5 Precio utilidad" + utilidad);
            MessageBox.Show("hola desde form 5 Precio utilidad" + escuela);

            string tipo = txttipo.Text;
            string marca = txtmarca.Text;
            string color = txtcolor.Text;
            string temporada = txttemporada.Text;
            string status = combostatus.Text;
            int modelo = int.Parse(textBoxmodelo.Text);
            int talla = int.Parse(textBoxtalla.Text);


            MessageBox.Show("Valor tipo " + tipo);
            MessageBox.Show("Valor de marca " + marca);
            MessageBox.Show("valor de color " + color);
            MessageBox.Show("Valor de temporada" + temporada);
            MessageBox.Show("valor de status" + status);
            MessageBox.Show("valor de modelo" + modelo);
            MessageBox.Show("valor de talla" + talla);
            


            //con.Open();


            //SqlCommand query = new SqlCommand("Insert into Catalogo(Tipo,Modelo,Marca,Talla,Color,Temporada,PrecioMaquila,PrecioProveedor,Utilidad,PrecioEscuela,Status) values (@Tipo,@Modelo,@Marca,@Talla,@Color,@Temporada,@PrecioMaquila,@PrecioProveedor,@Utilidad,@PrecioMaquila,@Status)", con);
            //query.Parameters.AddWithValue("@Tipo", tipo);
            //query.Parameters.AddWithValue("@Modelo", modelo);
            //query.Parameters.AddWithValue("@Marca", marca);
            //query.Parameters.AddWithValue("@Talla", talla);
            //query.Parameters.AddWithValue("@Color", color);
            //query.Parameters.AddWithValue("@Temporada", temporada);
            //query.Parameters.AddWithValue("@PrecioMaquila", maquila);
            //query.Parameters.AddWithValue("@PrecioProveedor", proveedor);
            //query.Parameters.AddWithValue("@Utilidad", utilidad);
            //query.Parameters.AddWithValue("@PrecioEscuela", escuela);
            //query.Parameters.AddWithValue("@Status", status);
            //query.ExecuteNonQuery();
            //cargarData();
            //con.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Form6 ob = new Form6();

            //string tipo = txttipo.Text;
            //int modelo = int.Parse(txtmodelo.Text);
            //string marca = txtmarca.Text;
            //int talla = int.Parse(txttalla.Text);
            //string color = txtcolor.Text;
            //string temporada = txttemporada.Text;
            //string status = combostatus.Text;

            ////double precioMaquila = ob.getPrecioMaquila();
            ////double precioProveedor = ob.getPrecioProveedor();
            ////double precioEscuela = ob.getprecioEscuela();
            ////double precioutilidad = ob.getPrecioUtilidad();

            //con.Open();


            //SqlCommand query = new SqlCommand("Insert into Catalogo(Tipo,Modelo,Marca,Talla,Color,Temporada,PrecioMaquila,PrecioProveedor,Utilidad,PrecioEscuela,Status) values (@Tipo,@Modelo,@Marca,@Talla,@Color,@Temporada,@PrecioMaquila,@PrecioProveedor,@Utilidad,@PrecioMaquila,@Status)", con);
            //query.Parameters.AddWithValue("@Tipo", tipo);
            //query.Parameters.AddWithValue("@Modelo", modelo);
            //query.Parameters.AddWithValue("@Marca", marca);
            //query.Parameters.AddWithValue("@Talla", talla);
            //query.Parameters.AddWithValue("@Color", color);
            //query.Parameters.AddWithValue("@Temporada", temporada);
            //query.Parameters.AddWithValue("@PrecioMaquila", precioM);
            //query.Parameters.AddWithValue("@PrecioProveedor", precioP);
            //query.Parameters.AddWithValue("@Utilidad", precioU);
            //query.Parameters.AddWithValue("@PrecioEscuela", precioE);
            //query.Parameters.AddWithValue("@Status", status);
            //query.ExecuteNonQuery();
            //cargarData();
            //con.Close();
            cargarData();


        }


    }
}
