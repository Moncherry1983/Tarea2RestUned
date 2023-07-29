using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.Accesores
{
    public static class PlatoAD
    {
        public static bool AgregarPlato(Plato ingresarPlatos)
        {
            string query = $"INSERT INTO Plato( IdPlato, Nombre, IdCategoria, Precio ) VALUES(@IdPlato, @Nombre, @IdCategoria, @Precio)";
            try
            {
                if (ConexionDB.Conectar())
                {
                    SqlCommand command = new SqlCommand(query, ConexionDB.ObtenerConexion())
                    {
                        CommandType = CommandType.Text
                    };
                    command.Parameters.AddWithValue("@IdPlato", ingresarPlatos.IdPlato);
                    command.Parameters.AddWithValue("@Nombre", ingresarPlatos.NombrePlato);
                    command.Parameters.AddWithValue("@IdCategoria", ingresarPlatos.CategoriaPlato.IdCategoria);
                    command.Parameters.AddWithValue("@Precio", ingresarPlatos.Precio);
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

        public static List<Plato> ListarPlatos()
        {
            List<Plato> Listaplatos = new List<Plato>();
            string query = $"SELECT p.IdPlato, p.Nombre, p.IdCategoria, p.Precio, c.Descripcion, c.Estado FROM Plato as p INNER JOIN  CategoriaPlato as c ON p.IdCategoria = c.IdCategoria ";

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
                            CategoriaPlato categoriaPlato = new CategoriaPlato(reader.GetInt32(2), reader.GetString(4), reader.GetBoolean(5));
                            Plato plato = new Plato(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(3), categoriaPlato);
                            Listaplatos.Add(plato);
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

            return Listaplatos;
        }

        public static Plato ObtenerPlato(int idPlato)
        {
            Plato plato = null;
            string query = $"SELECT p.IdPlato, p.Nombre, p.IdCategoria, p.Precio, c.Descripcion, c.Estado FROM Plato as p INNER JOIN  CategoriaPlato as c ON p.IdCategoria = c.IdCategoria WHERE p.IdPlato ={idPlato}";
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
                            CategoriaPlato categoriaPlato = new CategoriaPlato(reader.GetInt32(2), reader.GetString(4), reader.GetBoolean(5));
                            plato = new Plato(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), categoriaPlato);
                            return plato;
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

            return plato;
        }
    }
}