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
    public partial class Form8 : Form
    {

        string tipo, nombre, existencia, precioMaquila, utilidad, precioVenta, descripcion, talla, marca, modelo, color, temporada, codigobarra;
        string id;

        public Form8()
        {
            InitializeComponent();
            cargarData();
            cargatipos.DropDownStyle = ComboBoxStyle.DropDownList;
            cargarmarca.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            exportarPDF();
            
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True");

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click_1(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("SP_DELETE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", 1);
            cmd.Parameters.AddWithValue("@Tipo", "");
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@Existencia", "");
            cmd.Parameters.AddWithValue("@PrecioMaquila", "");
            cmd.Parameters.AddWithValue("@Utilidad", "");
            cmd.Parameters.AddWithValue("@PrecioVenta", "");
            cmd.Parameters.AddWithValue("@Descripcion", "");
            cmd.Parameters.AddWithValue("@Talla", "");
            cmd.Parameters.AddWithValue("@Marca", "");
            cmd.Parameters.AddWithValue("@Modelo", "");
            cmd.Parameters.AddWithValue("@Color", "");
            cmd.Parameters.AddWithValue("@Temporada", "");
            cmd.Parameters.AddWithValue("@CodigoBarra", "");
            cmd.Parameters.AddWithValue("@StatementType", "DELETE");

            if (MessageBox.Show("El producto " + nombre + " se eliminará", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AgPrd_UpdateEventHandler(object sender, Form25.UpdateEventArgs agrs)
        {
            cargarData();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tipo = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            nombre = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            existencia = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            precioMaquila = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            utilidad = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            precioVenta = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            descripcion = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            talla = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            marca = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            modelo = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            color = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            temporada = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            codigobarra = dataGridView1.CurrentRow.Cells[13].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tipo = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            nombre = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            existencia = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            precioMaquila = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            utilidad = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            precioVenta = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            descripcion = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            talla = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            marca = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            modelo = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            color = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            temporada = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            codigobarra = dataGridView1.CurrentRow.Cells[13].Value.ToString();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Existencia")
            {
                if(Convert.ToInt32(e.Value) <= 5)
                {
                    e.CellStyle.ForeColor = Color.Black;
                    e.CellStyle.BackColor = Color.Red;
                    
                }

                if(Convert.ToInt32(e.Value) >= 6)
                {
                    e.CellStyle.ForeColor = Color.Black;
                    e.CellStyle.BackColor = Color.GreenYellow;
                }


            }
        }

        private void cargarmarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cargarmarca.Text;
            SqlDataAdapter sa = new SqlDataAdapter("buscarMarca", con);
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
            Form25 abrir = new Form25(this);
            abrir.UpdateEventHandler += AgPrd_UpdateEventHandler;
            abrir.Show();

        }

        public void cargarData()
        {
            SqlDataAdapter sa = new SqlDataAdapter("Select ID,Tipo,Nombre,Existencia,Utilidad,UtilidadJCTobon,PrecioVenta,Descripcion,Talla,Marca,Modelo,Color,Temporada,CodigoBarra,Fecha from Productos ", con);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void Form8_Load(object sender, EventArgs e)
        {

            SqlCommand query = new SqlCommand("SELECT Tipo FROM Catalogos", con);
            con.Open();
            SqlDataReader registro = query.ExecuteReader();

            cargatipos.Items.Add("Todos");

            while (registro.Read())
            {
                cargatipos.Items.Add(registro["Tipo"].ToString());

            }
            con.Close();

            con.Open();
            SqlCommand querys = new SqlCommand("SELECT Marca FROM Catalogos", con);
            SqlDataReader leer = querys.ExecuteReader();
            cargarmarca.Items.Add("Todos");

            while (leer.Read())
            {
                cargarmarca.Items.Add(leer["Marca"].ToString());
            }

            con.Close();


            
        }

        // busqueda por tipo 
        private void cargatipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cargatipos.Text;
            SqlDataAdapter sa = new SqlDataAdapter("buscarTipoProductos", con);
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

        // busqueda por fecha 
        private void button5_Click_1(object sender, EventArgs e)
        {
            SqlDataAdapter sa = new SqlDataAdapter("buscarP", con);
            sa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sa.SelectCommand.Parameters.Add("@fechainicio", SqlDbType.DateTime).Value = inicio.Text;
            sa.SelectCommand.Parameters.Add("@fechafinal", SqlDbType.DateTime).Value = fin.Text;
            DataTable dt = new DataTable();
            sa.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tipo = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            nombre = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            existencia = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            precioMaquila = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            utilidad = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            precioVenta = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            descripcion = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            talla = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            marca = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            modelo = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            color = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            temporada = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            codigobarra = dataGridView1.CurrentRow.Cells[13].Value.ToString();
     

        }

        private void UpdateEventHandler(object sender, Form17.UpdateEventArgs agrs)
        {
            cargarData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form17 leer = new Form17(this);
            leer.mostradata(id,tipo, nombre, existencia, precioMaquila, utilidad, precioVenta, descripcion, talla, marca, modelo, color, temporada, codigobarra);
            leer.UpdateEventHandler += UpdateEventHandler;
            leer.Show();

        }
            

        public void ExportarExcel(DataGridView data)
        {
            Microsoft.Office.Interop.Excel.Application excelexport = new Microsoft.Office.Interop.Excel.Application();
            excelexport.Application.Workbooks.Add(true);

            int indicecolumnas = 0;

            foreach (DataGridViewColumn columna in data.Columns)
            {
                indicecolumnas++;
                excelexport.Cells[1, indicecolumnas] = columna.Name;
            }

            int indicefilas = 0;

            foreach (DataGridViewRow fila in data.Rows)
            {
                indicefilas++;
                indicecolumnas = 0;

                foreach (DataGridViewColumn columna in data.Columns)
                {
                    indicecolumnas++;
                    excelexport.Cells[indicefilas + 1, indicecolumnas] = fila.Cells[columna.Name].Value;
                }
            }
            excelexport.Visible = true;
        }

        public void exportarPDF()
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = "Reporte Stock Productos " + ".pdf";
           
            string formato = Properties.Resources.plantilla.ToString();
        
            if(guardar.ShowDialog() == DialogResult.OK)
            {
                
                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4.Rotate(), 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    pdfDoc.Add(new Phrase(""));

                    using (StringReader sr = new StringReader(formato))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer,pdfDoc,sr);

                        if(dataGridView1.Rows.Count>0)
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

        public DataGridView getData()
        {
            return dataGridView1;
        }


        public void PDF(DataGridView data)
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














    }

}
