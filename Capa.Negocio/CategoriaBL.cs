using Capa.Datos;
using Capa.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Negocio
{
    public class CategoriaBL
    {
            private CategoriaDAL dal = new CategoriaDAL();

            public List<Categoria> Listar()
            {
                return dal.Listar();
            }

            public void Guardar(Categoria categoria)
            {
                if (categoria.ID_categoria == 0)
                    dal.Insertar(categoria);
                else
                    dal.Actualizar(categoria);
            }

            public void Eliminar(int id)
            {
                dal.Eliminar(id);
            }
        }
    }
