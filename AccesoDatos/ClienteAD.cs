using Entidades;
using System;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class ClienteAD
    {
        //Este método busca un cliente en la base de datos usando su identificador.
        //Primero crea una variable para guardar el cliente y otra para guardar la consulta SQL.
        //Luego intenta conectarse a la base de datos y ejecutar la consulta. Si encuentra algún
        //resultado, crea un objeto cliente con los datos leídos y lo devuelve. Si ocurre algún error,
        //lanza una excepción con un mensaje. Al final, cierra el lector y la conexión a la base de datos.
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