using System;
using Entidades;
using AccesoDatos;
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
                PlatosRestauranteAD.AgregarPlatoRestaurante(plato);

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
                return PlatosRestauranteAD.ListarPlatoRestaurante();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public PlatoRestaurante ObtenerPlatosRestaurante(int id)
        {

            try
            {                
                return PlatosRestauranteAD.ListarPlatoRestaurante().Where(rest => rest!= null && rest.RestauranteAsignado.IdRestaurante == id).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }




    }
}
