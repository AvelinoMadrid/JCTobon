
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System.IO;

namespace JCTobon.Forms
{
    public partial class Form19 : Form
    {
        public Form19()
        {
            InitializeComponent();
            mostrarConfiguracion();
            cargarData();

            comboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            combomarca.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");


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

        public void cargarData()
        {
            SqlDataAdapter sa = new SqlDataAdapter("select Tipo,Nombre,Talla,PrecioVenta,CantidadPiezas,Marca,Fecha,Utilidad,UtilidadJCTobon,Total from Ventas ", con);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void Form19_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand query = new SqlCommand("SELECT Tipo FROM Catalogos", con);

            SqlDataReader registro = query.ExecuteReader();

            comboTipo.Items.Add("Todos");
            while (registro.Read())
            {
                comboTipo.Items.Add(registro["Tipo"].ToString());

            }
            con.Close();

            //filtro 2

            SqlCommand querys = new SqlCommand("SELECT Marca FROM Catalogos", con);
            con.Open();
            SqlDataReader leer = querys.ExecuteReader();
            combomarca.Items.Add("Todos");
            while (leer.Read())
            {
                combomarca.Items.Add(leer["Marca"].ToString());

            }
            con.Close();
        }

        private void comboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = comboTipo.Text;
            SqlDataAdapter sa = new SqlDataAdapter("buscarVentaMostradoresTipo", con);
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

        private void combomarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = combomarca.Text;
            SqlDataAdapter sa = new SqlDataAdapter("buscarVentaEscuelaMarca", con);
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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sa = new SqlDataAdapter("BuscarVentaMostradoresFecha", con);
            sa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sa.SelectCommand.Parameters.Add("@fechainicio", SqlDbType.DateTime).Value = inicio.Text;
            sa.SelectCommand.Parameters.Add("@fechafin", SqlDbType.DateTime).Value = fin.Text;
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            exportarPDF();
            MessageBox.Show("PDF exportado con éxito");
        }

        // exportarPDF

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

                    // acumuladores
                    int acumuladadorUtilidadesJCTobon = 0;
                    int acumuladadorVentastotales = 0;
                    int acumuladadorUtilidades = 0;

                    int utilidades = 0;
                    int ventas = 0;
                    int jctobon = 0;
                     


                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                       ventas = int.Parse(row.Cells[9].Value.ToString());
                       utilidades = int.Parse(row.Cells[7].Value.ToString());
                       jctobon = int.Parse(row.Cells[8].Value.ToString());
                       
                       //ventas totales  
                       acumuladadorVentastotales = acumuladadorVentastotales + ventas;

                        // ventas utilidades
                        acumuladadorUtilidades = acumuladadorUtilidades + utilidades;

                        // ventas jctobon 
                        acumuladadorUtilidadesJCTobon = acumuladadorUtilidadesJCTobon + jctobon;
                    }


                    Paragraph p1 = new Paragraph();
                    p1.Alignment = Element.ALIGN_LEFT;
                    p1.Add("Total de Ventas : $ " + acumuladadorVentastotales.ToString());

                    Paragraph p2 = new Paragraph();
                    p2.Alignment = Element.ALIGN_LEFT;
                    p2.Add("Total de Utilidades Escuela : $ " + acumuladadorUtilidades.ToString());

                    Paragraph p3 = new Paragraph();
                    p3.Alignment = Element.ALIGN_LEFT;
                    p3.Add("Total de Utilidades JCTobon : $ " + acumuladadorUtilidadesJCTobon.ToString());


                    pdfDoc.Add(p1);
                    pdfDoc.Add(p2);
                    pdfDoc.Add(p3);
                    //pdfDoc.Add(new Phrase("Total de Ventas" + acumulaodr.ToString()));
                    //pdfDoc.Add(new Phrase("Total de Utilidades" + acumulador.ToString()));


                    pdfDoc.Close();
                    stream.Close();
                }


            }


        }












    }
}
