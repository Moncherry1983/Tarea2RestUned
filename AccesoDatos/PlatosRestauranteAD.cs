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

        private static PlatoRestaurante[] platos = new PlatoRestaurante[10];
        

        public static void AgregarPlatoRestaurante(PlatoRestaurante plato)
        {

            int contador = 0;
            bool revision = true;

            for (int i = 0; i < platos.Count(); i++)
            {

                if (platos[i] == null)
                {
                    contador = i;
                    revision = false;
                    break;
                }
            }

            if (!revision)
            {
                platos[contador] = plato;
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
                return platos;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static PlatoRestaurante ObtenerPlatoRestaurante(int idAsignacion)
        {

            return platos.Where(x => x.IdAsignacion == idAsignacion).FirstOrDefault();

        }

    }
}




