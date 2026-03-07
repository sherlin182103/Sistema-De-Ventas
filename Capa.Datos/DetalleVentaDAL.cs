using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Capa.Datos
{
    public class DetalleVentaDAL
    {
        private string cadena = ConfigurationManager
                                .ConnectionStrings["cn"]
                                .ConnectionString;

        // INSERTAR DETALLE
        public void InsertarDetalleVenta(int idVenta, int idProducto, int cantidad, decimal precio)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand("SP_InsertarDetalle", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Venta", idVenta);
                cmd.Parameters.AddWithValue("@Id_producto", idProducto);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Precio_venta", precio);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // 🔥 OBTENER DETALLE PARA REPORTE
        public DataTable ObtenerDetalleReporte(int idVenta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand("SP_ObtenerDetalleReporte", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_venta", idVenta);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }
    }
}
