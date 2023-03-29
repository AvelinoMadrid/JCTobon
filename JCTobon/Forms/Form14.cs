
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
using System.Collections;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System.IO;

namespace JCTobon.Forms
{
    public partial class Form14 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");
        public Form14()
        {
            InitializeComponent();
            cargarData();
            mostrarConfiguracion();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

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
            SqlDataAdapter sa = new SqlDataAdapter("Select Tipo,Nombre,Existencia,PrecioMaquila,Utilidad,PrecioVenta,Descripcion,Talla,Marca,Modelo,Color,Temporada,CodigoBarra,Fecha from Productos order by Existencia asc ", con);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;

        }

        private void Form14_Load(object sender, EventArgs e)
        {
            SqlCommand query = new SqlCommand("SELECT Tipo FROM Catalogos", con);
            con.Open();
            SqlDataReader registro = query.ExecuteReader();

            comboBox1.Items.Add("Todos");

            while (registro.Read())
            {
                comboBox1.Items.Add(registro["Tipo"].ToString());

            }

            con.Close();

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = comboBox1.Text;
            SqlDataAdapter sa = new SqlDataAdapter("buscarStockUserEscuela", con);
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

        private void fin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
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
                    pictureBox2.Image = bm;

                    label2.Text = registro["RazonSocial"].ToString();
                    label3.Text = registro["Direccion"].ToString();


                }
                con.Close();


         
            

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Existencia")
            {
                if (Convert.ToInt32(e.Value) <= 5)
                {
                    e.CellStyle.ForeColor = Color.Black;
                    e.CellStyle.BackColor = Color.Red;

                }

                if (Convert.ToInt32(e.Value) >= 6)
                {
                    e.CellStyle.ForeColor = Color.Black;
                    e.CellStyle.BackColor = Color.GreenYellow;
                }


            }
        }

        public void exportarPDF()
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = "Reporte Stock Productos " + ".pdf";

            string formato = Properties.Resources.plantilla.ToString();

            if (guardar.ShowDialog() == DialogResult.OK)
            {

                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4.Rotate(), 25, 25, 25, 25);
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
                                    pTable.AddCell(dcell.Value.ToString());
                                }

                            }
                            pdfDoc.Add(pTable);

                        }
                    }
                    pdfDoc.Close();
                    stream.Close();
                }


            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            exportarPDF();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = comboBox2.Text;
            SqlDataAdapter sa = new SqlDataAdapter("buscarMarcaMostrador", con);
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
    }
}
