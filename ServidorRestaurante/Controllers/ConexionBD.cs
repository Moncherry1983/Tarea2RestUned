using System;
using System.Web;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ServidorRestaurante.Controllers
{
    public static class ConexionBD
    {
        static SqlConnection cnn;

        public static bool Conectar()
        {
            try
            {
                string connetionString = @"Data Source=WIN-50GP30FGO75;Initial Catalog=Demodb;User ID=sa;Password=demol23";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                //MessageBox.Show("Conexion abierta !");

                return true;

            }
            catch (Exception)
            {

                throw new Exception("Conexion Fallida");
            }
        }

        public static void CerrarConexion()
        {

            try
            {
                cnn.Close();

            }
            catch (Exception)
            {

                throw new Exception("Error al tratar de desconectar");
            }
        }
    }
}