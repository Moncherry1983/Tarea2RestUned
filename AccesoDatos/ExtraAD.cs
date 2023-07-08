using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AccesoDatos
{
    public static class ExtraAD
    {

        private static Extra [] ingresarExtra = new Extra[20];


        public static void AgregarExtra(Extra ingresarExtras)
        {

            int contador = 0;
            bool revision = true;

            for (int i = 0; i < ingresarExtra.Count(); i++)
            {

                if (ingresarExtra[i] == null)
                {
                    contador = i;
                    revision = false;
                    break;
                }
            }

            if (!revision)
            {
                ingresarExtra[contador] = ingresarExtras;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }


        }

        public static Extra[] ListarExtra()
        {
            try
            {
                return ingresarExtra;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static Extra ObtenerExtra(int idExtra)
        {

            return ingresarExtra.Where(x => x.IdExtra == idExtra).FirstOrDefault();

        }

    }
}
