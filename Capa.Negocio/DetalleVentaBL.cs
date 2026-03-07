using Capa.Datos;

namespace Capa.Negocio
{
    public class DetalleVentaBL
    {
        private DetalleVentaDAL dal = new DetalleVentaDAL();

        public void InsertarDetalleVenta(int idVenta, int idProducto, int cantidad, decimal precio)
        {
            dal.InsertarDetalleVenta(idVenta, idProducto, cantidad, precio);
        }
    }
}
