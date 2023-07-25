using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public static class ConexionDB
    {
        static readonly string serverName = "JOSEANDRES\\SQLEXPRESS";
        //static SqlConnection conectar = new SqlConnection($"Data Source={serverName};Initial Catalog=RESTUNED;Integrated Security=True");
        static SqlConnection conectar = new SqlConnection($"Data Source=JOSEANDRES\\SQLEXPRESS;Initial Catalog=RESTUNED;Integrated Security=True");
        //Data Source=LUGOBO-LAPTOP;Initial RESTUNEDE;Integrated Security=True

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

        public static void CerrarConeccion()
        {
            conectar?.Close();
        }

        public static SqlConnection ObtenerConexión()
        {
            return conectar;
        }

        public static bool ComprobarConexiónPerdida()
        {
            return conectar == null || (conectar != null && conectar.State == ConnectionState.Broken || conectar.State == ConnectionState.Closed);
        }


    }
}




    
