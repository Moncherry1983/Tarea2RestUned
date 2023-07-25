using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LogicaNegocio
{
    public class PlatoRestauranteLN
    {
        //se consulta el acceso de datos y se proceda la informacion nesesaria pen caso de manipular la informacion.


        public void AgregarPlatoRestaurante(PlatoRestaurante plato)
        {
            try
            {
                //PlatosRestauranteAD.AgregarPlatoRestaurante(plato);

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        public PlatoRestaurante[] ListarPlatoRestaurantes()
        {

            try
            {
                //return PlatosRestauranteAD.ListarPlatoRestaurante();
                return new PlatoRestaurante[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        //Este método devuelve el primer plato de un restaurante dado su identificador.Para ello, llama a otro método que lista todos los platos de todos los restaurantes
        //y luego filtra los que pertenecen al restaurante buscado. Si ocurre algún error, lo lanza como una excepción.
        public PlatoRestaurante ObtenerPlatosRestaurante(int id)
        {

            try
            {                
                //return PlatosRestauranteAD.ListarPlatoRestaurante().Where(rest => rest!= null && rest.RestauranteAsignado.IdRestaurante == id).FirstOrDefault();
                return new PlatoRestaurante[0].FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
