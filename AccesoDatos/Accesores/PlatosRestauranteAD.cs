using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.Accesores
{
    public static class PlatosRestauranteAD
    {
        public static void AgregarPlatoRestaurante(PlatoRestaurante plato)
        {
            string query = $"INSERT INTO PlatoRestaurante(IdAsignacion, IdRestaurante, IdPlato, FechaAsignacion) VALUES(,@IdAsignacion,@IdRestaurante, @IdPlato, @FechaAsignacion)";
            try
            {
                if (ConexionDB.Conectar())
                {
                    SqlCommand command = new SqlCommand(query, ConexionDB.ObtenerConexion())
                    {
                        CommandType = CommandType.Text
                    };
                    command.Parameters.AddWithValue("@IdAsignacion", plato.IdAsignacion);
                    command.Parameters.AddWithValue("@IdRestaurante", plato.RestauranteAsignado);
                    command.Parameters.AddWithValue("@IdPlato", plato.PlatoAsociado);
                    command.Parameters.AddWithValue("@FechaAsignacion", plato.FechaAfiliacion);
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

        public static List<PlatoRestaurante> ListarPlatoRestaurante()
        {
            List<PlatoRestaurante> ListaPlatoRestaurante = new List<PlatoRestaurante>();
            string query = $"SELECT (IdAsignacion, IdRestaurante, IdPlato, FechaAsignacion  FROM PlatoRestaurante";

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
                            Restaurante restauranteAsignado = new Restaurante(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3), reader.GetString(4));
                            CategoriaPlato categoriaPlato = new CategoriaPlato(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2));
                            Plato platos = new Plato(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), categoriaPlato);
                            PlatoRestaurante platoRest = new PlatoRestaurante(reader.GetInt32(0), restauranteAsignado, platos, reader.GetDateTime(3));
                            ListaPlatoRestaurante.Add(platoRest);
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

            return ListaPlatoRestaurante;
        }

        public static PlatoRestaurante ObtenerPlatoRestaurante(int idAsignacion)
        {
            PlatoRestaurante platoRestaurante = null;
            string query = $"SELECT pl.IdAsignacion, p.IdPlato, p.Nombre, p.Precio, r.IdRestaurante, r.Nombre, r.Direccion, pl.FechaAsignacion FROM Plato as p INNER JOIN  Restaurante as r ON p.IdPlato = r.IdRestaurante INNER JOIN PlatoRestaurante as pl ON pl.IdAsignacion = p.IdPlato and pl.IdAsignacion = r.IdRestaurante where pl.IdAsignacion ={idAsignacion}";
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
                            Restaurante restauranteAsignado = new Restaurante(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3), reader.GetString(4));
                            CategoriaPlato categoriaPlato = new CategoriaPlato(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2));
                            Plato platos = new Plato(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), categoriaPlato);
                            PlatoRestaurante platoRest = new PlatoRestaurante(reader.GetInt32(0), restauranteAsignado, platos, reader.GetDateTime(3));
                            return platoRestaurante;
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

            return platoRestaurante;
        }
    }
}