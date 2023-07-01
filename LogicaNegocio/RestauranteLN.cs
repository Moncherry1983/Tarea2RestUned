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
      public class RestauranteLN
    {

        public void AgregarRestaurante(Restaurante restaurante)
        {
			try
			{
				RestauranteAD.AgregarRestaurante(restaurante);

			}
			catch (Exception ex)
			{

				throw ex;
			}


        }


		public Restaurante[] ListarRestaurantes()
		{

			try
			{
              return  RestauranteAD.ListarRestaurante();
            }
			catch (Exception ex)
			{

				throw ex;
			}

		}



















    }
}
