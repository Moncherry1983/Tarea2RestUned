using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AccesoDatos
{
    public static class CategoriaPlatoAD
    {
        private static CategoriaPlato[] categorias = new CategoriaPlato[20];


        public static void AgregarCategoria(CategoriaPlato categoria)
        {

            int contador = 0;
            bool revision = true;

            for (int i = 0; i < categorias.Count(); i++)
            {

                if (categorias[i] == null)
                {
                    contador = i;
                    revision = false;
                    break;
                }
            }

            if (!revision)
            {
                categorias[contador] = categoria;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }


        }

        public static CategoriaPlato[] ListarCategoriaPlato()
        {
            try
            {
                return categorias;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static CategoriaPlato ObtenerCategoriaPlato(int idCategoria)
        {

            return categorias.Where(x => x.IdCategoria == idCategoria).FirstOrDefault();

        }




    }
}
