using JCTobon.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data;
using System.Data.SqlClient;


namespace JCTobon.Forms
{
    public partial class Form4 : Form
    {


       
        public Form4()
        {
            InitializeComponent();
            cargarData();
            combostatus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

           
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        
            string usuario = txtusuario.Text;
            string password = txtpassword.Text;
            string rol = comboBox1.Text;
            string status = combostatus.Text;
            string correo = txtcorreo.Text;
            string opcion;

            if (status.Equals("Activo"))
            {
                opcion = "1";
            }

            else
            {
                opcion = "0";
            }


            con.Open();
            
            SqlCommand query = new SqlCommand ("insert into Usuarios (Nombre,Password,Rol,Status,Correo) values (@Nombre,@Password,@Rol,@status,@correo)", con);
            query.Parameters.AddWithValue("@Nombre", usuario);
            query.Parameters.AddWithValue("@Password", password);
            query.Parameters.AddWithValue("@Rol", rol);
            query.Parameters.AddWithValue("@status", opcion);
            query.Parameters.AddWithValue("@correo", correo);
            query.ExecuteNonQuery();
            cargarData();

            con.Close();
            Limpiar();
        }
        public void cargarData()
        {
            SqlDataAdapter sa = new SqlDataAdapter("Select ID, Nombre, Password, Rol from Usuarios", con);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;

        }

        public void Limpiar()
        {
            txtusuario.Text = null;
            txtpassword.Text = null;
            txtnombre.Text = null;
            //txtrol.Text = null;
            comboBox1.Text = null;
            combostatus.Text = null;
        }

        public void validar()
        {
            var vr = !string.IsNullOrEmpty(txtusuario.Text) &&
                     !string.IsNullOrEmpty(txtnombre.Text) &&
                     !string.IsNullOrEmpty(txtpassword.Text) &&
                     !string.IsNullOrEmpty(comboBox1.Text) &&
                     !string.IsNullOrEmpty(combostatus.Text) && 
                     !string.IsNullOrEmpty(txtcorreo.Text);

            button2.Enabled = vr;


        }

        private void Form4_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }

        private void txtusuario_TextChanged(object sender, EventArgs e)
        {
            validar();
        }

        private void txtnombre_TextChanged(object sender, EventArgs e)
        {
            validar();
        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {
            validar();
        }


        private void combostatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            validar();
        }

        string nombre;

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            nombre = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_Borrarusuarios", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", 1);
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@Password", "");
            cmd.Parameters.AddWithValue("@Rol", "");
            cmd.Parameters.AddWithValue("@Status", "");
            cmd.Parameters.AddWithValue("@IdUsuarios", "");
            cmd.Parameters.AddWithValue("@StatementType", "DELETE");

            if (MessageBox.Show(" El Usuario " + nombre + " se eliminará", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Se ha eliminado el registro");
                    cargarData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("No se pudo eliminar usuario");
                    throw;
                }
            }
            //conectar.mostrar("Productos", dataGridView1);
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            nombre = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void txtcorreo_TextChanged(object sender, EventArgs e)
        {
            validar();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            validar();
        }
    }
}
