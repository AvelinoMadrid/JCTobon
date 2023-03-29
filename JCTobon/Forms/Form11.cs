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

namespace JCTobon.Forms
{
    public partial class Form11 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");
        ConexionBD conex = new ConexionBD();
        
        public Form11()
        {
            InitializeComponent();
            //cargarData();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClsTicket.CreaTicket Ticket1 = new ClsTicket.CreaTicket();

            Ticket1.TextoCentro("Empresa JcTobon "); //imprime una linea de descripcion
            Ticket1.TextoCentro("**********************************");

            Ticket1.TextoIzquierda("Dirc: xxxx");
            Ticket1.TextoIzquierda("Tel:xxxx ");
            Ticket1.TextoIzquierda("Rfc: xxxx");
            Ticket1.TextoIzquierda("");
            Ticket1.TextoCentro("Factura de Venta"); //imprime una linea de descripcion
            Ticket1.TextoIzquierda("No Fac: 1" /*+ ClassBT.clsDetallesVenta.IdVentafk.ToString())*/);
            Ticket1.TextoIzquierda("Fecha:" + DateTime.Now.ToShortDateString() + " Hora:" + DateTime.Now.ToShortTimeString());
            Ticket1.TextoIzquierda("Le Atendio: Lola");
            Ticket1.TextoIzquierda("");
            ClsTicket.CreaTicket.LineasGuion();

            ClsTicket.CreaTicket.EncabezadoVenta();
            ClsTicket.CreaTicket.LineasGuion();
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                // PROD                     //PrECIO                                    CANT                         TOTAL
                Ticket1.AgregaArticulo(r.Cells[1].Value.ToString(), double.Parse(r.Cells[2].Value.ToString()), int.Parse(r.Cells[3].Value.ToString()), double.Parse(r.Cells[4].Value.ToString())); //imprime una linea de descripcion
            }


            ClsTicket.CreaTicket.LineasGuion();
            //Ticket1.AgregaTotales("Sub-Total", double.Parse("000")); // imprime linea con Subtotal
            //Ticket1.AgregaTotales("Menos Descuento", double.Parse("000")); // imprime linea con decuento total
            //Ticket1.AgregaTotales("Mas ITBIS", double.Parse("000")); // imprime linea con ITBis total
            Ticket1.TextoIzquierda(" ");
            Ticket1.AgregaTotales("Total", double.Parse(lblTotal.Text)); // imprime linea con total
            Ticket1.TextoIzquierda(" ");
            Ticket1.AgregaTotales("Efectivo Entregado:", double.Parse(txtEfectivo.Text));
            Ticket1.AgregaTotales("Efectivo Devuelto:", double.Parse(lblDevoluciones.Text));


            // Ticket1.LineasTotales(); // imprime linea 


            Ticket1.TextoIzquierda(" ");
            Ticket1.TextoCentro("**********************************");
            Ticket1.TextoCentro("*     Gracias por preferirnos    *");

            Ticket1.TextoCentro("**********************************");
            Ticket1.TextoIzquierda(" ");
            string impresora = "Microsoft XPS Document Writer";
            Ticket1.ImprimirTiket(impresora);




            //Fila = 0;
            //while (dataGridView1.RowCount > 0)//limpia el dgv
            //{ dataGridView1.Rows.Remove(dataGridView1.CurrentRow); }
            ////LBLIDnuevaFACTURA.Text = ClaseFunciones.ClsFunciones.IDNUEVAFACTURA().ToString();

            //txtIdProducto.Text = lblNombre.Text = txtCantidad.Text = textBox3.Text = "";
            //lblCostoApagar.Text = lbldevolucion.Text = lblPrecio.Text = "0";
            //txtIdProducto.Focus();
            MessageBox.Show("Gracias por preferirnos");

            this.Close();
        }

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    Clases.ClsBusqueda objectoBusqueda = new Clases.ClsBusqueda();
        //    objectoBusqueda.buscarCodigo(txtCodigoBarra, txtTipo, txtNombre, txtPrecioVenta);
        //}

        //private void txtCodigoBarra_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)Keys.Enter)
        //    {
        //        Clases.ClsBusqueda objectoBusqueda = new Clases.ClsBusqueda();
        //        objectoBusqueda.buscarCodigo(txtCodigoBarra, txtTipo, txtNombre, txtPrecioVenta);
        //        txtCodigoBarra.Text = "";
        //    }
        //}

        //private void button3_Click_1(object sender, EventArgs e)
        //{
        //    Clases.ClsBusqueda objectoBusqueda = new Clases.ClsBusqueda();
        //    objectoBusqueda.buscarCodigo(txtCodigoBarra, txtTipo, txtNombre, txtTalla, txtPrecioVenta);
        //}

        private void txtCodigoBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Clases.ClsBusqueda objectoBusqueda = new Clases.ClsBusqueda();
                objectoBusqueda.buscarCodigo(txtCodigoBarra, txtTipo, txtNombre, txtTalla, txtPrecioVenta);
                txtCodigoBarra.Text = "";
                //cargarData();
                
            }
        }

        //public void cargarData()
        //{
        //    SqlDataAdapter sa = new SqlDataAdapter("select Tipo, Nombre, Talla, PrecioVenta from Productos where Productos.CodigoBarra ='" + txtCodigoBarra.Text + "'", con);
        //    DataTable dt = new DataTable();
        //    sa.Fill(dt);
        //    this.dataGridView1.DataSource = dt;
        //    Clases.ClsBusqueda objectoBusqueda = new Clases.ClsBusqueda();
        //    objectoBusqueda.buscarCodigo(txtCodigoBarra, txtTipo, txtNombre, txtTalla, txtPrecioVenta);
        //    txtCodigoBarra.Text = "";

        //}

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewRow file = new DataGridViewRow();
            file.CreateCells(dataGridView1);

            file.Cells[0].Value = txtTipo.Text;
            file.Cells[1].Value = txtNombre.Text;
            file.Cells[2].Value = txtTalla.Text;
            file.Cells[3].Value = txtPrecioVenta.Text;
            file.Cells[4].Value = txtCantidad.Text;
            file.Cells[5].Value = txtCliente.Text;
            file.Cells[6].Value = (float.Parse(txtPrecioVenta.Text) * float.Parse(txtCantidad.Text)).ToString();

            dataGridView1.Rows.Add(file);

            txtTipo.Text = txtNombre.Text = txtTalla.Text = txtPrecioVenta.Text = txtCantidad.Text = txtCliente.Text = "";
            obtenerTotal();
        }

        public void obtenerTotal()
        {
            float costot = 0;
            int contador = 0;

            contador = dataGridView1.Rows.Count;

            for (int i = 0; i < contador; i++)
            {
                costot += float.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
            }

            lblTotal.Text = costot.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rppta = MessageBox.Show("¿Desea ELIMINAR el producto?", "Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(rppta == DialogResult.Yes)
                    {
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                }
            }
            catch { }
            obtenerTotal();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblDevoluciones.Text = (float.Parse(txtEfectivo.Text) - float.Parse(lblTotal.Text)).ToString();
            }
            catch { }
        }

        private void txtCodigoBarra_TextChanged(object sender, EventArgs e)
        {

        }
    }
}