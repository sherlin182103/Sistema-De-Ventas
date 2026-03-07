using System.Data.SqlClient;
using System.Configuration;

namespace Capa.Datos
{
    public class Conexion
    {
        private static string cadena = @"Server=DESKTOP-MV85JFF;Database=SistemaVentas;Trusted_Connection=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadena);
        }
    }
}
