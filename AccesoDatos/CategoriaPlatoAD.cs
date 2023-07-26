using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public static class CategoriaPlatoAD
    {
        public static void AgregarCategoria(CategoriaPlato categoria)
        {
            string query = $"INSERT INTO Categoria(IdCategoria, Descripcion, Estado) VALUES(@IdCategoria, @Descripcion, @Estado)";
            try
            {
                if (ConexionDB.Conectar())
                {
                    SqlCommand command = new SqlCommand(query, ConexionDB.ObtenerConexion())
                    {
                        CommandType = CommandType.Text
                    };
                    command.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoria);
                    command.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                    command.Parameters.AddWithValue("@Estado", categoria.Estado);
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

        public static List<CategoriaPlato> ListarCategoriaPlato()
        {
            List<CategoriaPlato> listaCategoriaPlato = new List<CategoriaPlato>();
            string query = $"SELECT IdCategoria, Descripcion, Estado FROM CategoriaPlato WHERE Estado = 1";
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
                            CategoriaPlato categoriaPlato = new CategoriaPlato(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2));
                            listaCategoriaPlato.Add(categoriaPlato);
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

            return listaCategoriaPlato;
        }

        public static CategoriaPlato ObtenerCategoriaPlato(int idCategoria)
        {
            CategoriaPlato categoriaPlato = null;
            string query = $"SELECT IdCategoria, Descripcion, Estado FROM CategoriaPlato WHERE IdCliente ={idCategoria}";
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
                            categoriaPlato = new CategoriaPlato(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2));
                            return categoriaPlato;
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

            return categoriaPlato;
        }
    }
}