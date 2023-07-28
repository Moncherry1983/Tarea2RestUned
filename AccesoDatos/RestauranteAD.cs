
//La capa de acceso a datos es una parte importante de cualquier aplicación que necesita trabajar con una base de datos o un servicio externo.
//Su objetivo es simplificar la conexión, consulta y modificación de los datos, para que el resto de la aplicación los pueda usar de forma fácil y eficaz. 
//La capa de acceso a datos también se ocupa de las transacciones, los errores y la seguridad de los datos.
using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Security.Policy;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace AccesoDatos
{
    public static class RestauranteAD
    {

        public static bool AgregarRestaurante(Restaurante restaurante)
        {
            string query = $"INSERT INTO Restaurante(IdRestaurante, Nombre, Direccion, Estado,Telefono) VALUES(@IdRestaurante, @Nombre, @Direccion, @Estado, @Telefono)";
            try
            {
                if (ConexionDB.Conectar())
                {
                    SqlCommand command = new SqlCommand(query, ConexionDB.ObtenerConexion())
                    {
                        CommandType = CommandType.Text
                    };
                    command.Parameters.AddWithValue("@IdRestaurante", restaurante.IdRestaurante);
                    command.Parameters.AddWithValue("@Nombre", restaurante.NombreRestaurante);
                    command.Parameters.AddWithValue("@Direccion", restaurante.Direccion);
                    command.Parameters.AddWithValue("@Estado", restaurante.Estado);
                    command.Parameters.AddWithValue("@Telefono", restaurante.Telefono);
                    command.ExecuteNonQuery();

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
                    ConexionDB.CerrarConexion();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return true;
        }

        public static List<Restaurante> ListarRestaurante()
        {
            List<Restaurante> ListaRestaurantes = new List<Restaurante>();
            string query = $"SELECT IdRestaurante, Nombre, Direccion, Estado, Telefono FROM Restaurante WHERE Estado = 1";

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
                            Restaurante restaurante = new Restaurante(
                                reader.GetInt32(0), 
                                reader.GetString(1), 
                                reader.GetString(2), 
                                reader.GetBoolean(3), 
                                reader.GetString(4)
                            );
                            ListaRestaurantes.Add(restaurante);
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

            return ListaRestaurantes;
        }

        public static Restaurante ObtenerRestaurante(int idRestaurante)
        {
            Restaurante restaurante = null;
            string query = $"SELECT IdRestaurante, Nombre, Direccion, Estado, Telefono FROM Restaurante WHERE IdCliente ={idRestaurante}";
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
                            restaurante = new Restaurante(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3), reader.GetString(4));
                            return restaurante;
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

            return restaurante;
        }

    }
}