using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AccesoDatos
{
    public static class PlatoRestaurante2AD
    {
        private static PlatoRestaurante[] platoRestaurante = new PlatoRestaurante[20];

        public static void AgregarPlatoRestaurante2(PlatoRestaurante platoRestaurantes)
        {

            int contador = 0;
            bool revision = true;

            for (int i = 0; i < platoRestaurante.Count(); i++)
            {

                if (platoRestaurante[i] == null)
                {
                    contador = i;
                    revision = false;
                    break;
                }
            }

            if (!revision)
            {
                platoRestaurante[contador] = platoRestaurantes;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }


        }

        public static PlatoRestaurante[] ListarPlatoRestaurante2()
        {
            try
            {
                return platoRestaurante;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static PlatoRestaurante ObtenerAsociacionPlato(int idAsignacion)
        {

            return platoRestaurante.Where(x => x.IdAsignacion == idAsignacion).FirstOrDefault();

        }

        public static PlatoRestaurante ObtenerAsociacionRestaurante(int restauranteAsignado)
        {

            return platoRestaurante.Where(x => x.RestauranteAsignado == restauranteAsignado).FirstOrDefault();

        }





        //int idAsignacion, int restauranteAsignado,


    }
}
