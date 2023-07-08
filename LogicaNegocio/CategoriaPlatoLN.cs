using System;
using Entidades;
using AccesoDatos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using static LogicaNegocio.PlatoRestauranteLN;

namespace LogicaNegocio
{

    //se consulta el acceso de datos y se proceda la informacion nesesaria pen caso de manipular la informacion.
    public class CategoriaPlatoLN
    {

        public void AgregarCategoriaPlato(CategoriaPlato categoria)
        {
          
            try
            {
                var categoriaPlatoActuales = CategoriaPlatoAD.ListarCategoriaPlato();
                if (categoriaPlatoActuales.Where(categoPlato => categoPlato != null && categoPlato.IdCategoria == categoria.IdCategoria).Count()==0)
                    CategoriaPlatoAD.AgregarCategoria(categoria);
                else
                    throw new Exception("Ya el id de la categoria existe ");

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public CategoriaPlato[] ListarCategoriaPlato()
        {

            try
            {
                return CategoriaPlatoAD.ListarCategoriaPlato();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public CategoriaPlato[] ListarCategoriaPlatoCombo()
        {

            try
            {
                return CategoriaPlatoAD.ListarCategoriaPlato().Where( cat => cat != null && cat.Estado == true).ToArray();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}


