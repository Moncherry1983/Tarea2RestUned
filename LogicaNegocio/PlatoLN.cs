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
    public class PlatoLN
    {

        public void AgregarPlato(RegistrarPlato ingresarPlato)
        {
            try
            {
                PlatoAD.AgregarPlato(ingresarPlato);

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        public RegistrarPlato[] ListarPlato()
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