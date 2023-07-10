using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AccesoDatos
{
    public static class RestauranteAD
    {
        private static Restaurante[] restaurantes = new Restaurante[20];


        public static void AgregarRestaurante(Restaurante restaurante)
        {

            int contador = 0;
            bool revision = true;

            for (int i = 0; i < restaurantes.Count(); i++)
            {

                if (restaurantes[i] == null)
                {
                    contador = i;
                    revision = false;
                    break;
                }
            }

            if (!revision)
            {
                restaurantes[contador] = restaurante;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }


        }

        //public static Restaurante[] ListarRestaurante()
        //{
        //    try
        //    {
        //        return restaurantes;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}

        public static Restaurante[] ListarRestaurante()
        {
            try
            {

                Restaurante[] restaurante = new Restaurante[4];
                restaurante[0] = new Restaurante(1, "Restaurante Entre nubes", "100 norte", true, "24866587");
                restaurante[1] = new Restaurante(2, "Restaurante como en casa", "200 sur", true, "57985321");
                restaurante[2] = new Restaurante(1, "Restaurante La cocina", "100 norte", true, "24866587");
                restaurante[3] = new Restaurante(2, "Restaurante mi cuñado", "200 sur", true, "57985321");
                return restaurante;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static Restaurante ObtenerRestaurante(int idRestaurante)
        {

            return restaurantes.Where(x => x.IdRestaurante == idRestaurante).FirstOrDefault();

        }

    }
}
