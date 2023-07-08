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
    public class PlatoLN
    {

        public void AgregarPlato(Plato ingresarPlato)
        {
           
            try
            {
                var PlatoActuales = PlatoAD.ListarPlatos();
                if (PlatoActuales.Where(plato => plato != null && plato.IdPlato == ingresarPlato.IdPlato).Count() == 0)
                    PlatoAD.AgregarPlato(ingresarPlato);
                else
                    throw new Exception("Ya existe el id de plato");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Plato[] ListarPlato()
        {

            try
            {
                return PlatoAD.ListarPlatos();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}