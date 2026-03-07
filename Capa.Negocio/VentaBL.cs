using Capa.Datos;
using Capa.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa.Negocio
{
    public class VentaBL
    {
        private VentaDAL dal = new VentaDAL();

        // ✅ Guardar venta completa (cabecera + detalles)

        public int Insertar(Venta venta)
        {

            if (venta.Id_cliente <= 0)
                throw new Exception("Debe seleccionar un cliente.");

            if (venta.Detalles == null || venta.Detalles.Count == 0)
                throw new Exception("Debe agregar al menos un producto a la venta.");

            if (venta.Total_general <= 0)
                throw new Exception("El total de la venta no puede ser 0.");

            // Si pasa validaciones → guarda
            dal.Insertar(venta);
            return dal.Insertar(venta);
        }

        public List<Venta> Listar()
        {
            return dal.Listar();
        }

        // ✅ Actualizar venta


        // ✅ Obtener detalles para reporte
        public System.Data.DataTable ObtenerDetalleReporte(int idVenta)
        {
            return dal.ObtenerDetalleReporte(idVenta);
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
