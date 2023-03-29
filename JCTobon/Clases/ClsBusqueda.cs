//using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace JCTobon.Clases
{
    internal class ClsBusqueda
    {


        public void buscarCodigo(TextBox txtCodigoBarra, TextBox txtTipo, TextBox txtNombre, TextBox txtTalla, TextBox txtPrecioVenta)
        {
            ConexionBD objecto = new ConexionBD();

            string cadenaConexion = "Data Source=DESKTOP-GD5MVN2;Initial Catalog=PuntoVenta;Integrated Security=True";

            SqlConnection conex = objecto.establecerConexion();

            try
            {
                conex.Open();

                String sql = "Select Tipo, Nombre, Talla, PrecioVenta,Existencia from Productos where CodigoBarra ='" + txtCodigoBarra.Text + "'";

                SqlCommand cmd = new SqlCommand(sql, conex);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())

                {
                    txtTipo.Text = reader[0].ToString();
                    txtNombre.Text = reader[1].ToString();
                    txtTalla.Text = reader[2].ToString();
                    txtPrecioVenta.Text = reader[3].ToString();
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se logro realizar la busqueda, error:" + ex.ToString());
            }
        }
    }
}
