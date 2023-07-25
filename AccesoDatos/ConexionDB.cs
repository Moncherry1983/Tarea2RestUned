using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public static class ConexionDB
    {
        //
        static readonly string serverName = "JOSEANDRES\\SQLEXPRESS";
        //agrega su base de datos del profesor....
        //static SqlConnection conectar = new SqlConnection($"Data Source={***Nombre servidor***};Initial Catalog=RESTUNED;Integrated Security=True");

        //base de datos estudiante.
        static SqlConnection conectar = new SqlConnection($"Data Source=JOSEANDRES\\SQLEXPRESS;Initial Catalog=RESTUNED;Integrated Security=True");


        //Este método intenta abrir una conexión con la base de datos utilizando la información de conexión predefinida.
        //Si la conexión es exitosa, devuelve true, lo que significa que la conexión se realizó correctamente.
        //Si ocurre algún error al conectar, devuelve false.
        public static bool Conectar()
        {
            try
            {
                conectar.Open();
                //MessageBox.Show("Conexion exitosa a la base de datos RESTUNED...");
                return true;
            }
            catch (SqlException ex)
            {
                conectar = null;
                new Exception("No es posible conectar a la base de datos:\n" + ex.Message);
            }

            return false;
        }

        // Este método se encarga de cerrar la conexión a la base de datos. 
        public static void CerrarConeccion()
        {
            conectar?.Close();
        }

        //Este método devuelve el objeto de conexión actual, que se puede utilizar para ejecutar consultas y operaciones en la base de datos.
        public static SqlConnection ObtenerConexión()
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




    
