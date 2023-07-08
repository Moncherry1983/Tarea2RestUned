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
                var restaurantesActuales= RestauranteAD.ListarRestaurante();                
                if (restaurantesActuales.Where(rest => rest != null && rest.IdRestaurante == restaurante.IdRestaurante).Count() == 0)
                    RestauranteAD.AgregarRestaurante(restaurante);
                else
                    throw new Exception("Ya existe un restaurate con ese Id");

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
                return RestauranteAD.ListarRestaurante();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Restaurante[] ListarRestaurantesActivos()
        {

            try
            {
                return RestauranteAD.ListarRestaurante().Where(cat => cat != null && cat.Estado == true).ToArray();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
