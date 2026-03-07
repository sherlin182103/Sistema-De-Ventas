using Capa.Datos;
using Capa.Entidades;
using System.Collections.Generic;

namespace Capa.Negocio
{
    public class ClienteBL
    {
        private ClienteDAL dal = new ClienteDAL();

        // Listar clientes
        public List<Cliente> Listar()
        {
            return dal.Listar();
        }

        // Guardar cliente (insertar o actualizar)

        public int Guardar(Cliente c)
        {
            if (c.Id_cliente == 0)
            {
                return dal.Insertar(c);   // inserta y devuelve 1
            }
            else
            {
                return dal.Actualizar(c); // actualiza y devuelve filas afectadas
            }
        }
        // Eliminar cliente (pone Estado_cliente = 'Inactivo')
        public void Eliminar(int id)
        {
            dal.Eliminar(id);
        }

        }
    }


