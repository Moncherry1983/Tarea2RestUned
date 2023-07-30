using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Data;
using System.CodeDom;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AccesoDatos.Accesores
{
    public static class ExtraAD
    {
        public static bool AgregarExtra(Extra ingresarExtras) 
        {
            string query = $"INSERT INTO Extra(IdExtra, Descripcion, IdCategoria, Estado, Precio ) VALUES(@IdExtra, @Descripcion, @IdCategoria, @Estado, @Precio)";
            try
            {
                if (ConexionDB.Conectar())
                {
                    SqlCommand command = new SqlCommand(query, ConexionDB.ObtenerConexion())
                    {
                        CommandType = CommandType.Text
                    };
                    command.Parameters.AddWithValue("@IdExtra", ingresarExtras.IdCategoriaextra);
                    command.Parameters.AddWithValue("@Descripcion", ingresarExtras.Descripcion);
                    command.Parameters.AddWithValue("@IdCategoria", ingresarExtras.IdCategoriaextra);
                    command.Parameters.AddWithValue("@Estado", ingresarExtras.Estado);
                    command.Parameters.AddWithValue("@Estado", ingresarExtras.Precio);
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

        public static List<Extra> ListarExtra()
        {
            List<Extra> ingresarExtras = new List<Extra>();
            string query = $"SELECT e.IdExtra, e.Descripcion,e.IdCategoria, e.Estado, e.Precio, c.Descripcion, c.Estado FROM Extra as e INNER JOIN  CategoriaPlato as c ON e.IdExtra = c.IdCategoria WHERE e.Estado = 1";

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
                            CategoriaPlato categoriaPlato = new CategoriaPlato(reader.GetInt32(2), reader.GetString(5), reader.GetBoolean(6));
                            Extra extra = new Extra(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(4), reader.GetBoolean(3), reader.GetInt32(2));

                            ingresarExtras.Add(extra);
                           
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

            return ingresarExtras;
        }

        public static Extra ObtenerExtra(int idExtra)
        {
            Extra ingresarExtras = null;
            string query = $"\"SELECT e.IdExtra, e.Descripcion,e.IdCategoria, e.Estado, e.Precio, c.IdCategoria, c.Descripcion, c.Estado FROM Extra as e INNER JOIN  CategoriaPlato as c ON e.IdExtra = c.IdCategoria WHERE e.IdExtra ={idExtra}";
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
                            CategoriaPlato categoriaPlato = new CategoriaPlato(reader.GetInt32(5), reader.GetString(6), reader.GetBoolean(71));
                            Extra extra = new Extra(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetBoolean(3), reader.GetInt32(4));
                            return ingresarExtras;
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

            return ingresarExtras;
        }

    }
}
