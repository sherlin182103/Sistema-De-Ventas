using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa.Datos;
using Capa.Entidades;

namespace Capa.Negocio
{
    public class ProductoBL
    {
        private ProductoDAL dal = new ProductoDAL();

        public List<Producto> Listar()
        {
            return dal.Listar();
        }

        public void Guardar(Producto p)
        {
            if (p.ID_producto == 0)
                dal.Insertar(p);
            else
                dal.Actualizar(p);
        }

        public void Eliminar(int id)
        {
            dal.Eliminar(id);
        }
    }
}
