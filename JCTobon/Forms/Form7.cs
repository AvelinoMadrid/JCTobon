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
using JCTobon.Clases;

namespace JCTobon.Forms
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            cargarData();
            cargarcombobox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        static string nombre,talla;

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");

        string codigobarra,tipo;

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void cargarData()
        {
            SqlDataAdapter sa = new SqlDataAdapter("Select ID,Tipo,Modelo,CodigoBarra,Talla,Fecha from Etiquetas", con);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            SqlCommand query = new SqlCommand("SELECT Tipo FROM Catalogos ", con);
            con.Open();
            SqlDataReader registro = query.ExecuteReader();


            cargarcombobox.Items.Add("Todos");

            while (registro.Read())
            {
                cargarcombobox.Items.Add(registro["Tipo"].ToString());

            }
            con.Close();
        }

        // buscar por fechas
        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sa = new SqlDataAdapter("buscar", con);
            sa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sa.SelectCommand.Parameters.Add("@fechainicio", SqlDbType.DateTime).Value = inicio.Text;
            sa.SelectCommand.Parameters.Add("@fechafinal", SqlDbType.DateTime).Value = fin.Text;
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

         // buscar por lista combobox
        private void cargarcombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cargarcombobox.Text;
            SqlDataAdapter sa = new SqlDataAdapter("buscarTipo", con);
            sa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sa.SelectCommand.Parameters.Add("@tipo", SqlDbType.NVarChar, 150).Value = opcion;
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;

            if (opcion.Equals("Todos"))
            {
                cargarData();
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            codigobarra = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            tipo = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            talla =  dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form16 generar = new Form16();
            generar.muestraCodigo(codigobarra,tipo,talla);
            generar.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            codigobarra = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            tipo = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            nombre = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            talla = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_DELETE_ETIQUETAS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", 1);
            cmd.Parameters.AddWithValue("@Tipo", tipo);
            cmd.Parameters.AddWithValue("@Modelo", "");
            cmd.Parameters.AddWithValue("@CodigoBarra", "");
            cmd.Parameters.AddWithValue("@Fecha", "");
            cmd.Parameters.AddWithValue("@IdCatalogo", "");
            cmd.Parameters.AddWithValue("@StatementType", "DELETE");

            if (MessageBox.Show("El producto " + tipo + " se eliminará", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Se ha eliminado el registro");
                    cargarData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw;
                }
            }
            //conectar.mostrar("Productos", dataGridView1);
            con.Close();


     
        }


        public string getNombre()
        {
            return nombre;
        }



    }
}
