using System;
using Entidades;
using AccesoDatos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LogicaNegocio
{
    public class PlatoRestaurante2LG
    {


        public void AgregarPlatoRestaurante2(PlatoRestaurante platoRestaurante)
        {
            try
            {
                PlatoRestaurante2AD.AgregarPlatoRestaurante2(platoRestaurante);

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        public PlatoRestaurante[] ListarPlatoRestaurante2()
        {

            try
            {
                return PlatoRestaurante2AD.ListarPlatoRestaurante2();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



    }
}
