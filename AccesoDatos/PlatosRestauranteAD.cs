using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AccesoDatos
{
    public static class PlatosRestauranteAD
    {

       
        private static PlatoRestaurante[] platoRestaurante = new PlatoRestaurante[20];


        public static void AgregarPlatoRestaurante(PlatoRestaurante plato)
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
                platoRestaurante[contador] = plato;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }


        }

        public static PlatoRestaurante[] ListarPlatoRestaurante()
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


    }
}




