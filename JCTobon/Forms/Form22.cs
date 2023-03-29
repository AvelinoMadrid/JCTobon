
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
using System.IO;

namespace JCTobon.Forms
{
    public partial class Form22 : Form
    {
        public Form22()
        {
            InitializeComponent();
            cargarData();
            mostrarConfiguracion();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sa = new SqlDataAdapter("buscarStockUserEscuelaFecha", con);
            sa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sa.SelectCommand.Parameters.Add("@fechainicio", SqlDbType.DateTime).Value = inicio.Text;
            sa.SelectCommand.Parameters.Add("@fechafin", SqlDbType.DateTime).Value = fin.Text;
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;



        }


        public void cargarData()
        {
            SqlDataAdapter sa = new SqlDataAdapter("Select Tipo,Nombre,Existencia,PrecioVenta,Descripcion,Talla,Marca,Modelo,Color,Temporada,CodigoBarra,Fecha,Img,Status from Productos", con);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void comboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = comboTipo.Text;
            SqlDataAdapter sa = new SqlDataAdapter("buscarStockUserEscuela", con);
            sa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sa.SelectCommand.Parameters.Add("@tipo", SqlDbType.NVarChar, 150).Value = opcion;
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void Form22_Load(object sender, EventArgs e)
        {
            SqlCommand query = new SqlCommand("SELECT Tipo FROM Catalogos ", con);
            con.Open();
            SqlDataReader registro = query.ExecuteReader();

            while (registro.Read())
            {
                comboTipo.Items.Add(registro["Tipo"].ToString());

            }

            con.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void mostrarConfiguracion()
        {


            con.Open();

            SqlCommand query1 = new SqlCommand("select * from Configuracion where ID = 7", con);
            SqlDataReader registro = query1.ExecuteReader();

            if (registro.HasRows)
            {
                registro.Read();

                MemoryStream ms = new MemoryStream((byte[])registro["Imagen"]);
                Bitmap bm = new Bitmap(ms);
                pictureBox3.Image = bm;

                label5.Text = registro["RazonSocial"].ToString();
                label6.Text = registro["Direccion"].ToString();
            }

            con.Close();

        }



    }
}
