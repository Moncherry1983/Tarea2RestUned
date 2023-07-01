using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AccesoDatos
{
    public static class  PlatoAD
    {
        private static Entidades.RegistrarPlato[] ingresarPlato = new Entidades.RegistrarPlato[20];

        public static void AgregarPlato(Entidades.RegistrarPlato ingresarPlatos)
        {

            int contador = 0;
            bool revision = true;

            for (int i = 0; i < ingresarPlato.Count(); i++)
            {

                if (ingresarPlato[i] == null)
                {
                    contador = i;
                    revision = false;
                    break;
                }
            }

            if (!revision)
            {
                ingresarPlato[contador] = ingresarPlatos;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }


        }

        public static Entidades.RegistrarPlato[] ListarPlatos()
        {
            try
            {
                return ingresarPlato;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static Entidades.RegistrarPlato ObtenerPlato(int idPlato)
        {

            return ingresarPlato.Where(x => x.IdPlato == idPlato).FirstOrDefault();

        }




    }
}