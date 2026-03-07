using Capa.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Datos
{
    public class CategoriaDAL
    {
        private string cadena =
            ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

 
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT ID_categoria, Nombre_categoria, Estado FROM categoria WHERE Estado = 1", cn);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Categoria
                    {
                        ID_categoria = Convert.ToInt32(dr["ID_categoria"]),
                        Nombre_categoria = dr["Nombre_categoria"].ToString(),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    });
                }
            }

            return lista;
        }

    
        public void Insertar(Categoria c)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_insertar_categoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", c.Nombre_categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

   
        public void Actualizar(Categoria c)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_actualizar_categoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", c.ID_categoria);
                cmd.Parameters.AddWithValue("@Nombre", c.Nombre_categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

       
        public void Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_eliminar_categoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
