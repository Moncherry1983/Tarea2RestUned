using System;
using Entidades;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AccesoDatos
{
    public class ClienteAD
    {
        public static void AgregarRestaurante(Cliente ingresoCliente )
        {
            string query = $"INSERT INTO Cliente(IdCliente, Nombre, PrimerApellido, SegundoApellido, FechaNacimiento, Genero) VALUES(@IdCliente, @Nombre, @PrimerApellido, @SegundoApellido, @FechaNacimiento, @Genero)";
            try
            {
                if (ConexionDB.Conectar())
                {
                    SqlCommand command = new SqlCommand(query, ConexionDB.ObtenerConexion())
                    {
                        CommandType = CommandType.Text
                    };
                    command.Parameters.AddWithValue("@IdCliente", ingresoCliente.IdCedula);
                    command.Parameters.AddWithValue("@Nombre", ingresoCliente.Nombre);
                    command.Parameters.AddWithValue("@PrimerApellido", ingresoCliente.PApellido);
                    command.Parameters.AddWithValue("@SegundoApellido", ingresoCliente.SApellido);
                    command.Parameters.AddWithValue("@FechaNacimiento", ingresoCliente.FNacimiento);
                    command.Parameters.AddWithValue("@Genero", ingresoCliente.Genero);                  
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
        }

        public static List<Cliente> ListarCliente()
        {
            List<Cliente> ListaCliente = new List<Cliente>();
            string query = $"SELECT IdCliente, Nombre, PrimerApellido, SegundoApellido, FechaNacimiento, Genero FROM Cliente ";

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
                            Cliente cliente = new Cliente(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5)[0]);
                            ListaCliente.Add(cliente);
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

            return ListaCliente;
        }

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