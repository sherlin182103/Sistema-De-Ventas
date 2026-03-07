using Capa.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Capa.Datos
{
    public class ProductoDAL
    {
        private string cadena =
            ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

        
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_listar_producto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Producto
                    {
                        ID_producto = Convert.ToInt32(dr["ID_producto"]),
                        Nombre_producto = dr["Nombre_producto"].ToString(),
                        Precio_producto = Convert.ToDecimal(dr["Precio_producto"]),
                        Stock = Convert.ToInt32(dr["Stock"]),
                        ID_categoria = Convert.ToInt32(dr["ID_categoria"]),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    });
                }
            }

            return lista;
        }


        public void Insertar(Producto p)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_guardar_producto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = p.Nombre_producto;

                cmd.Parameters.Add("@precio", SqlDbType.Decimal).Value = p.Precio_producto;
                cmd.Parameters["@precio"].Precision = 10;
                cmd.Parameters["@precio"].Scale = 2;

                cmd.Parameters.Add("@stock", SqlDbType.Int).Value = p.Stock;
                cmd.Parameters.Add("@id_categoria", SqlDbType.Int).Value = p.ID_categoria;

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Guardar(Producto p)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand("sp_guardar_producto", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", p.Nombre_producto);
                cmd.Parameters.AddWithValue("@precio", p.Precio_producto);
                cmd.Parameters.AddWithValue("@stock", p.Stock);
                cmd.Parameters.AddWithValue("@id_categoria", p.ID_categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void Actualizar(Producto p)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_actualizar_producto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_producto", p.ID_producto);
                cmd.Parameters.AddWithValue("@nombre", p.Nombre_producto);
                cmd.Parameters.AddWithValue("@precio", p.Precio_producto);
                cmd.Parameters.AddWithValue("@stock", p.Stock);
                cmd.Parameters.AddWithValue("@id_categoria", p.ID_categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

   
        public void Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_eliminar_producto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
