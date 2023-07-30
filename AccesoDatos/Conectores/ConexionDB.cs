using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public static class ConexionDB
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["restUnedConnString"].ConnectionString;
        static SqlConnection conectar = new SqlConnection(connectionString);


        //Este método intenta abrir una conexión con la base de datos utilizando la información de conexión predefinida.
        //Si la conexión es exitosa, devuelve true, lo que significa que la conexión se realizó correctamente.
        //Si ocurre algún error al conectar, devuelve false.
        public static bool Conectar()
        {
            try
            {
                if (conectar == null)
                {
                    throw new Exception("La conexión no ha sido inicializada.");
                }
                conectar.Open();
                return true;
            }
            catch (SqlException ex)
            {
                conectar = null;
                throw new Exception("No es posible conectar a la base de datos:\n" + ex.Message);
            }
        }


        // Este método se encarga de cerrar la conexión a la base de datos.
        public static void CerrarConexion()
        {
            conectar?.Close();
        }

        //Este método devuelve el objeto de conexión actual, que se puede utilizar para ejecutar consultas y operaciones en la base de datos.
        public static SqlConnection ObtenerConexion()
        {
            return conectar;
        }

        //Este método verifica si la conexión a la base de datos se ha perdido o está cerrada.
        //Si la conexión es nula o está en un estado de conexión rota o cerrada, devuelve true,
        //indicando que se ha perdido la conexión. De lo contrario, devuelve false.
        public static bool ComprobarConexiónPerdida()
        {
            return conectar == null || (conectar != null && conectar.State == ConnectionState.Broken || conectar.State == ConnectionState.Closed);
        }
    }
}