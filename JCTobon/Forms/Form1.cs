using JCTobon.Clases;
using JCTobon.Forms;
using JCTobon.Properties;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;


namespace JCTobon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string usuario;


        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         



        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            string usuario = txtuser.Text;
            string contraseña = txtpassword.Text;

            ConexionBD con = new ConexionBD();
            con.establecerConexion();

            logear(usuario, contraseña);
            Form111 usuo = new Form111();
       
            
            
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        public void logear(string user, string password)
        {
            
            try
            {
                con.Open();
                SqlCommand query = new SqlCommand("SELECT * FROM Usuarios WHERE Nombre = @usuario AND Password=@pass", con );
                query.Parameters.AddWithValue("usuario",user);
                query.Parameters.AddWithValue("pass", password);
                query.ExecuteNonQuery();
                SqlDataAdapter sql = new SqlDataAdapter(query);
                DataTable dt = new DataTable();
                sql.Fill(dt);


                if (dt.Rows.Count == 1) {

                    if (dt.Rows[0][3].ToString().Equals("admin") )
                    {
                        Form2 abrir = new Form2(user);
                        abrir.Show();
                        this.SetVisibleCore(false);
                    }

                    else  if (dt.Rows[0][3].ToString().Equals("mostrador"))
                    {                                         
                       
                        Form10 abrir = new Form10(user);
                        abrir.Show();
                        this.SetVisibleCore(false);
                    }

                    else
                    {
                        Form13 abrir = new Form13(user);
                        abrir.Show();
                        this.SetVisibleCore(false);
                    }

                }

                else {
                    MessageBox.Show("Usuario y contraseña incorrecta");
                    txtuser.Text = null;
                    txtpassword.Text = null;
                }

                con.Close();

            }
            catch (SqlException ex)
            {
                
            }
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string usuario = txtuser.Text;
            string contraseña = txtpassword.Text;

            ConexionBD con = new ConexionBD();
            //con.establecerConexion();

            logear(usuario, contraseña);
            
        }


        public string getUser()
        {
            return usuario = txtuser.Text;
        }

    }
}