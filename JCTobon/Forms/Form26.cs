
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

namespace JCTobon.Forms
{
    public partial class Form26 : Form
    {
        public Form26()
        {
            InitializeComponent();
            cargarData();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            Form27 abrir = new Form27();
            abrir.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void cargarData()
        {
            SqlDataAdapter sa = new SqlDataAdapter("Select * from ventasValidadas ", con);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtfolio.Text);
            SqlDataAdapter sa = new SqlDataAdapter("buscarfolio", con);
            sa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sa.SelectCommand.Parameters.Add("@Folio", SqlDbType.Int).Value = id;
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;



        }

        private void AgVenta_UpdateEventHandler(object sender, Form28.UpdateEventArgs args)
        {
            cargarData();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            cargarData();
            txtfolio.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form28 abrir = new Form28(this);
            abrir.UpdateEventHandler += AgVenta_UpdateEventHandler;
            abrir.Show();
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlDataAdapter sa = new SqlDataAdapter("BuscarValidacionesFecha", con);
            sa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sa.SelectCommand.Parameters.Add("@fechainicio", SqlDbType.DateTime).Value = inicio.Text;
            sa.SelectCommand.Parameters.Add("@fechafin", SqlDbType.DateTime).Value = fin.Text;
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }
    }
}
