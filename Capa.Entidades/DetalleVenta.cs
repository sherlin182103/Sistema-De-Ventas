using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Entidades
{
    public class DetalleVenta
    {

   
        public int Id_producto { get; set; }
        public string NombreCliente { get; set; }
        public string NombreProducto { get; set; }

        public int Cant { get; set; }
        public decimal Precio { get; set; }

        public decimal Subtotal { get; set; }

    }
}
