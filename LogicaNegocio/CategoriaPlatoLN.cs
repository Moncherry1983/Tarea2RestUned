using System;
using Entidades;
using AccesoDatos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LogicaNegocio
{

    //se consulta el acceso de datos y se proceda la informacion nesesaria pen caso de manipular la informacion.
    public class CategoriaPlatoLN
    {

        public void AgregarCategoriaPlato(CategoriaPlato categoria)
        {
            try
            {
                CategoriaPlatoAD.AgregarCategoria(categoria);

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
    }
}


