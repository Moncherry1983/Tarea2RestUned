using System;
using Entidades;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class ClienteAD
    {
       

        public Cliente ObtenerClientePorId(string id)
        {
            Cliente cliente = null;
            string query = $"SELECT IdCliente, Nombre, PrimerApellido, SegundoApellido, FechaNacimiento, Genero FROM Cliente WHERE IdCliente ={id}";

            SqlDataReader reader = null;
            try
            {
                if (ConexionDB.Conectar())
                {
                    SqlCommand comand = new SqlCommand(query, ConexionDB.ObtenerConexion());
                    reader = comand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            cliente = new Cliente(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5).ToCharArray()[0]);

                            return cliente;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:\n No se puede acceder a la base de datos.\n" + ex.Message);
            }
            finally
            {
                try
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                        ConexionDB.CerrarConexion();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return cliente;
        }
    }
}