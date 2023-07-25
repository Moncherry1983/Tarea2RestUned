using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AccesoDatos
{
    public static class PlatoAD
    {
        private static Plato[] platos = new Plato[20];

        public static void AgregarPlato(Plato ingresarPlatos)
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
                platos[contador] = ingresarPlatos;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }


        }

        public static Entidades.Plato[] ListarPlatos()
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
     
        public static Entidades.Plato ObtenerPlato(int idPlato)
        {

            return platos.Where(x => x.IdPlato == idPlato).FirstOrDefault();

        }

    }
}
