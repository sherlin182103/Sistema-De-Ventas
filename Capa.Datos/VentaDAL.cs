using Capa.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace Capa.Datos
{
    public class VentaDAL
    {
        // Tomamos la cadena del App.config
        private string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

        public int Insertar(Venta venta)
        {

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {

                    SqlCommand cmdVenta = new SqlCommand("sp_InsertarVenta", cn, transaction);
                    cmdVenta.CommandType = CommandType.StoredProcedure;

                    cmdVenta.Parameters.AddWithValue("@IdCliente", venta.Id_cliente);
                    cmdVenta.Parameters.AddWithValue("@Total", venta.Total_general);


                    int idVentaGenerado = Convert.ToInt32(cmdVenta.ExecuteScalar());


                    foreach (var item in venta.Detalles)
                    {
                        decimal subtotal = item.Cant * item.Precio;

                        SqlCommand cmdDetalle = new SqlCommand("sp_InsertarDetalleVenta", cn, transaction);
                        cmdDetalle.CommandType = CommandType.StoredProcedure;

                        cmdDetalle.Parameters.AddWithValue("@IdVenta", idVentaGenerado);
                        cmdDetalle.Parameters.AddWithValue("@IdProducto", item.Id_producto);
                        cmdDetalle.Parameters.AddWithValue("@Cant", item.Cant);
                        cmdDetalle.Parameters.AddWithValue("@Precio", item.Precio);
                        cmdDetalle.Parameters.AddWithValue("@Subtotal", subtotal);


                        cmdDetalle.ExecuteNonQuery();

                    }

                    transaction.Commit();
                    return idVentaGenerado;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // LISTAR VENTAS
        public List<Venta> Listar()
        {
            List<Venta> lista = new List<Venta>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_ListarVentas", cn);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Venta()
                    {
                        Id_Venta = Convert.ToInt32(dr["Id_Venta"]),
                        Fecha_venta = Convert.ToDateTime(dr["Fecha_venta"]),
                        Id_cliente = Convert.ToInt32(dr["Id_cliente"]),
                        Total_general = Convert.ToDecimal(dr["Total_general"]),


                    });
                }
            }

            return lista;
        }

        // ACTUALIZAR VENTA
 

        // OBTENER DETALLE PARA REPORTE
        public DataTable ObtenerDetalleReporte(int idVenta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand("SP_ReporteVenta", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdVenta", idVenta);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable MostrarFactura(int idVenta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_Mostrar_Factura", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Venta", idVenta);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }
    }
}
