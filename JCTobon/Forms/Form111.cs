//using Microsoft.Office.Interop.Excel;
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
using System.Data.Sql;
using JCTobon.Clases;
using System.Security.Policy;
using System.Data.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using Azure.Core;
using System.Drawing.Printing;
using iTextSharp.text.pdf.parser;

namespace JCTobon.Forms
{
    public partial class Form111 : Form
    {
        public Form111()
        {
            InitializeComponent();
            GeneraCodigo();
          
        }

        string usuario;

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");
        ConexionBD conex = new ConexionBD();

        string razon, direccion;

        public static StringBuilder line = new StringBuilder();

        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += printDocument1_PrintPage;
            //printDocument1.Print();
            //printDocument1.Print();
            //printDocument1.Print();

            //actualizarStock();

            DateTime date = dateTimePicker1.Value;
            date.ToString();




            txtTipo.Text = txtNombre.Text = txtTalla.Text = txtPrecioVenta.Text = txtCantidad.Text = txtExistencia.Text = "";

            SqlCommand guardar = new SqlCommand("INSERT INTO Ventas(Tipo, Nombre, Talla, PrecioVenta, CantidadPiezas, Existencia, Total,Folio,Marca,IdCodigoBarra,Utilidad,Fecha,utilidadjctobon) Values (@Tipo, @Nombre, @Talla, @PrecioVenta, @CantidadPiezas, @Existencia, @Total,@Folio,@Marca,@IdCodigoBarra,@Utilidad,@Fecha,@utilidadjctobon)", con);
            con.Open();

            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    guardar.Parameters.Clear();

                    guardar.Parameters.AddWithValue("@Tipo", Convert.ToString(row.Cells["Column1"].Value));
                    guardar.Parameters.AddWithValue("@Nombre", Convert.ToString(row.Cells["Column2"].Value));
                    guardar.Parameters.AddWithValue("@Talla", Convert.ToString(row.Cells["Column3"].Value));
                    guardar.Parameters.AddWithValue("@PrecioVenta", Convert.ToString(row.Cells["Column4"].Value));
                    guardar.Parameters.AddWithValue("@CantidadPiezas", Convert.ToString(row.Cells["Column5"].Value));
                    guardar.Parameters.AddWithValue("@Existencia", Convert.ToString(row.Cells["Column6"].Value));
                    guardar.Parameters.AddWithValue("@Total", Convert.ToString(row.Cells["Column7"].Value));
                    guardar.Parameters.AddWithValue("@Folio", Convert.ToString(row.Cells["Column8"].Value));
                    guardar.Parameters.AddWithValue("@IdCodigoBarra", Convert.ToString(row.Cells["Column9"].Value));
                    guardar.Parameters.AddWithValue("@Marca", Convert.ToString(row.Cells["Column10"].Value));
                    guardar.Parameters.AddWithValue("@Utilidad", Convert.ToString(row.Cells["Column11"].Value));
                    guardar.Parameters.AddWithValue("@utilidadjctobon", Convert.ToString(row.Cells["Column13"].Value));
                    guardar.Parameters.AddWithValue("@Fecha",date);

                    guardar.ExecuteNonQuery();
                }
                MessageBox.Show("Venta Guardada");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL GUARDAR");
            }

            finally
            {
                con.Close();
            }

            dataGridView1.Rows.Clear();
           

           


        }


        private void txtCodigoBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

       
                String sql = "Select Tipo, Nombre, Talla, PrecioVenta,Existencia,Marca,Utilidad,PrecioMaquila from Productos where CodigoBarra ='" + txtCodigoBarra.Text + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())

                {
                    txtTipo.Text = reader[0].ToString();
                    txtNombre.Text = reader[1].ToString();
                    txtTalla.Text = reader[2].ToString();
                    txtPrecioVenta.Text = reader[3].ToString();
                    txtExistencia.Text = reader[4].ToString();
                    txtMarca.Text = reader[5].ToString();
                    txtutilidad.Text = reader[6].ToString();
                    txtproveedor.Text = reader[7].ToString();

                    int existencia = int.Parse(txtExistencia.Text);

                    if(existencia >= 1 && existencia<=5)
                    {
                        MessageBox.Show("Stock bajo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                       
                    }

                    else if (existencia == 0)
                    {
                        MessageBox.Show("No existe stock de producto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Close();
                    }
                  
                    
                   
                }
              
                con.Close();
               


                //txtCodigoBarra.Text = "";
            }
        }

        // Metodo para generar el codigo de barra en automatico 

        public void GeneraCodigo()
        {
            Random rnd = new Random();
            txtfolio.Text = rnd.Next().ToString();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;
            date.ToString();

            DataGridViewRow file = new DataGridViewRow();
            file.CreateCells(dataGridView1);

            file.Cells[0].Value = txtfolio.Text;
            file.Cells[1].Value = txtCodigoBarra.Text;
            file.Cells[2].Value = txtTipo.Text;
            file.Cells[3].Value = txtNombre.Text;
            file.Cells[4].Value = txtTalla.Text;
            file.Cells[5].Value = txtPrecioVenta.Text;
            file.Cells[6].Value = txtCantidad.Text;
            file.Cells[7].Value = txtExistencia.Text;
            file.Cells[8].Value = (float.Parse(txtPrecioVenta.Text) * float.Parse(txtCantidad.Text)).ToString();
            file.Cells[9].Value = txtMarca.Text;
            file.Cells[10].Value = (float.Parse(txtutilidad.Text) * float.Parse(txtCantidad.Text)).ToString(); 
            file.Cells[11].Value = dateTimePicker1.Value.ToString();
            file.Cells[12].Value = (float.Parse(txtproveedor.Text) * float.Parse(txtCantidad.Text)).ToString();

            dataGridView1.Rows.Add(file);           
            obtenerTotal();
            //actualizarStock();
            limpiar();

        }


        public void obtenerTotal()
        {
            float costot = 0;
            int contador = 0;

            contador = dataGridView1.Rows.Count;

            for (int i = 0; i < contador; i++)
            {
                costot += float.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());
            }

            lblTotal.Text = costot.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rppta = MessageBox.Show("¿Desea ELIMINAR el producto?", "Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rppta == DialogResult.Yes)
                {
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                }
            }
            catch { }
            obtenerTotal();
        }

        private void txtCodigoBarra_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public DataGridView getData()
        {
            return dataGridView1;
        }

        // metodo para obtener los datos 
     
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            con.Open();

            SqlCommand query1 = new SqlCommand("select * from Configuracion where ID = 7", con);
            SqlDataReader registro = query1.ExecuteReader();

            if (registro.HasRows)
            {
                registro.Read();

                razon = registro["RazonSocial"].ToString();
                direccion = registro["Direccion"].ToString();
            }

            con.Close();

           
        


            Font font = new Font("Arial",8);
            int ancho = 450;
            int y = 20;

            int posx = 10,posy=10;


            e.Graphics.DrawString(razon , font, Brushes.Black, new RectangleF(0, posy, ancho, 20));
            posy += 15;
            e.Graphics.DrawString(direccion, font, Brushes.Black, new RectangleF(0, posy, ancho, 20));
            posy += 15;
            e.Graphics.DrawString("**********************************", font, Brushes.Black, new RectangleF(0, posy, ancho, 20));
            posy += 15;
            e.Graphics.DrawString("   TICKET DE COMPRA ", font, Brushes.Black, new RectangleF(0, posy, ancho, 20));
            posy += 15;
            e.Graphics.DrawString(" No. de Folio:  " + txtfolio.Text, font, Brushes.Black, new RectangleF(0, posy, ancho, 20));
            posy += 15;
            e.Graphics.DrawString(" Fecha: " + DateTime.Now.ToShortDateString() + " Hora:" + DateTime.Now.ToShortTimeString(), font, Brushes.Black, new RectangleF(0, posy, ancho, 20));
            posy += 15;
            e.Graphics.DrawString(" Cliente: Público en general ", font, Brushes.Black, new RectangleF(0, posy, ancho, 20));
            posy += 15;
            e.Graphics.DrawString("**********************************", font, Brushes.Black, new RectangleF(0, posy, ancho, 20));
            posy += 15;
            e.Graphics.DrawString("  Tipo  " + "   Cantidad  " + "   Precio  " + "   Subtotal  ", font, Brushes.Black, new RectangleF(0, posy, ancho, 30));

            posy += 10;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                // PROD                     //PrECIO                                    CANT                         TOTAL
                /*Ticket1.AgregaArticulo(r.Cells[1].Value.ToString(), double.Parse(r.Cells[2].Value.ToString()), int.Parse(r.Cells[3].Value.ToString()), double.Parse(r.Cells[4].Value.ToString()));*/ //imprime una linea de descripcion
              
                posy += 15;
                e.Graphics.DrawString(r.Cells[2].Value.ToString() + "    " + double.Parse(r.Cells[6].Value.ToString()) + "    " +  int.Parse(r.Cells[5].Value.ToString()) + "    " + double.Parse(r.Cells[8].Value.ToString()), font, Brushes.Black, new RectangleF(0, posy, ancho, 30));


            }

            posy += 15;
            e.Graphics.DrawString("**********************************", font, Brushes.Black, new RectangleF(0, posy, ancho, 20));

            posy += 15;
            e.Graphics.DrawString(" Total: "  + "   "  + " $ " +  lblTotal.Text , font, Brushes.Black, new RectangleF(0, posy, ancho, 20));

            //posy += 15;
            //e.Graphics.DrawString(" Efectivo Entregado : " + "   " + " $ " + txtEfectivo.Text , font, Brushes.Black, new RectangleF(0, posy, ancho, 20));

            //posy += 15;
            //e.Graphics.DrawString(" Efectivo Devuelto : " + "   " +  " $ " + lblDevoluciones.Text, font, Brushes.Black, new RectangleF(0, posy, ancho, 20));

            posy += 15;
            e.Graphics.DrawString("**********************************", font, Brushes.Black, new RectangleF(0, posy, ancho, 20));

            posy += 15;
            e.Graphics.DrawString("     * Gracias por su compra *    ", font, Brushes.Black, new RectangleF(0, posy, ancho, 20));

        }


        public double getTotal()
        {
            return double.Parse(lblTotal.Text);
        }


        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }



        public void actualizarStock()
        {
            string codigo = txtCodigoBarra.Text;
            int stock = int.Parse(txtExistencia.Text);
            int cantidad = int.Parse(txtCantidad.Text);
            int nuevostock = stock - cantidad;

            con.Open();
            SqlCommand query = new SqlCommand("Update Productos set Existencia = @Existencia where CodigoBarra = '" + codigo + "'", con);
            query.Parameters.AddWithValue("@Existencia", nuevostock);
            query.ExecuteNonQuery();
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //alertaStock();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        public void limpiar()
        {
            txtCodigoBarra.Text = null;
            txtExistencia.Text = null;
            txtTipo.Text = null;
            txtNombre.Text = null;
            txtTalla.Text = null; 
            txtPrecioVenta.Text = null;
            txtCantidad.Text = null;
            txtMarca.Text = null; 
            txtutilidad.Text = null;
            txtproveedor.Text = null;

            
        }

        public void validar()
        {
            var vr = !string.IsNullOrEmpty(txtCantidad.Text);
            button2.Enabled = vr;

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            validar();
        }

        private void Form111_Load(object sender, EventArgs e)
        {
            button2.Enabled = false; 
        }

    }
}

