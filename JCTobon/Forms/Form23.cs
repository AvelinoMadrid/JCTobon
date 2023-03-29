
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
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System.IO;

namespace JCTobon.Forms
{
    public partial class Form23 : Form
    {
        public Form23()
        {
            InitializeComponent();
            mostrarConfiguracion();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            cargarData();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");

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


                label2.Text = registro["RazonSocial"].ToString();
                label3.Text = registro["Direccion"].ToString();
            }

            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlDataAdapter sa = new SqlDataAdapter("buscarVentaMostrador", con);
            sa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sa.SelectCommand.Parameters.Add("@fechainicio", SqlDbType.DateTime).Value = inicio.Text;
            sa.SelectCommand.Parameters.Add("@fechafinal", SqlDbType.DateTime).Value = fin.Text;
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = comboBox1.Text;
            SqlDataAdapter sa = new SqlDataAdapter("buscarTipoMostrador", con);
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

        private void Form23_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand query = new SqlCommand("SELECT Tipo FROM Catalogos", con);
           
            SqlDataReader registro = query.ExecuteReader();

            comboBox1.Items.Add("Todos");
            while (registro.Read())
            {
                comboBox1.Items.Add(registro["Tipo"].ToString());

            }
            con.Close();

            //filtro 2
           
            SqlCommand querys = new SqlCommand("SELECT Marca FROM Catalogos", con);
            con.Open();
            SqlDataReader leer = querys.ExecuteReader();
            comboBox2.Items.Add("Todos");
            while (leer.Read())
            {
                comboBox2.Items.Add(leer["Marca"].ToString());

            }
            con.Close();




        }

        public DataGridView getData()
        {
            return dataGridView1;
        }

        // metodo de pdf



        public void exportarPDF()
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = "Reporte ventas " + ".pdf";

            string formato = Properties.Resources.plantilla2.ToString();

            if (guardar.ShowDialog() == DialogResult.OK)
            {

                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    pdfDoc.Add(new Phrase(""));

                    using (StringReader sr = new StringReader(formato))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);

                        if (dataGridView1.Rows.Count > 0)
                        {
                            PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn col in dataGridView1.Columns)
                            {
                                PdfPCell pcell = new PdfPCell(new Phrase(col.HeaderText));
                                pTable.AddCell(pcell);
                            }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell dcell in row.Cells)
                                {
                                    //pTable.AddCell(dcell.Value.ToString());
                                    pTable.AddCell(dcell.Value.ToString()); 


                                }

                            }
                            pdfDoc.Add(pTable);

                        }


                    }

                   int acumulaodr = 0;
                   int datos = 0;
                   int valores = 0;
                   int acumulador = 0;
                    

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                       datos = int.Parse(row.Cells[3].Value.ToString());
                       acumulaodr = acumulaodr + datos;
                       valores = int.Parse(row.Cells[7].Value.ToString());
                        acumulador = acumulador + valores;


                    }


                    Paragraph p1 = new Paragraph();
                    p1.Alignment = Element.ALIGN_LEFT;
                    p1.Add("Total de Ventas : $ " + acumulaodr.ToString());

                    Paragraph p2 = new Paragraph();
                    p2.Add("Total de Utilidades : $ " + acumulador.ToString());
                    pdfDoc.Add(p1);
                    pdfDoc.Add(p2);


                    //pdfDoc.Add(new Phrase("Total de Ventas" + acumulaodr.ToString()));
                    //pdfDoc.Add(new Phrase("Total de Utilidades" + acumulador.ToString()));


                    pdfDoc.Close();
                    stream.Close();
                }


            }


        }


        public void cargarData()
        {
            SqlDataAdapter sa = new SqlDataAdapter("select Tipo,Nombre,Talla,PrecioVenta,CantidadPiezas,Marca,Total,Fecha from Ventas ", con);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            exportarPDF();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = comboBox2.Text;
            SqlDataAdapter sa = new SqlDataAdapter("buscarVentaMostradores", con);
            sa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sa.SelectCommand.Parameters.Add("@marca", SqlDbType.NVarChar, 150).Value = opcion;
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;

            if (opcion.Equals("Todos"))
            {
                cargarData();
            }
        }
    } // fin names pace
}
