using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Entidades
{
    public class Producto
    {
        public int ID_producto { get; set; }
        public string Nombre_producto { get; set; }
        public decimal Precio_producto { get; set; }
        public int Stock { get; set; }
        public int ID_categoria { get; set; }
        public bool Estado { get; set; }

    }
}


