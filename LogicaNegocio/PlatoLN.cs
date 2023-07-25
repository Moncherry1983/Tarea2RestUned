using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Collections;
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
                //var PlatoActuales = PlatoAD.ListarPlatos();
                //if (PlatoActuales.Where(plato => plato != null && plato.IdPlato == ingresarPlato.IdPlato).Count() == 0)
                //    PlatoAD.AgregarPlato(ingresarPlato);
                //else
                //    throw new Exception("Ya existe el id de plato");
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
                //return PlatoAD.ListarPlatos();
                return new Plato[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        //Este método recibe una lista de identificadores de platos seleccionados y devuelve una lista de objetos Plato que corresponden
        //a esos identificadores. Primero, obtiene todos los platos de la base de datos usando el objeto PlatoAD. Luego, verifica si alguna
        //de las listas es nula y, en ese caso, devuelve una lista vacía. Después, filtra los platos que no son nulos y que están en la lista
        //de identificadores usando el método Where. Finalmente, verifica si el resultado del filtro es nulo o vacío y, en ese caso, devuelve
        //una lista vacía. De lo contrario, devuelve el resultado convertido a una lista.
        public List<Plato> ListarPlatosSeleccionados(List<int> listaIdsPlatosSeleccionados)
        {
            //var allPlatos = PlatoAD.ListarPlatos();
            //if (allPlatos == null || listaIdsPlatosSeleccionados == null)
            //{
            //    return new List<Plato>();
            //}

            //var resultado = allPlatos.Where(plat => plat != null && listaIdsPlatosSeleccionados.Contains(plat.IdPlato));

            //if (resultado == null || resultado.Count() < 1)
            //{
            //    return new List<Plato>();
            //}

            //return resultado.ToList();
            return new List<Plato>();
        }

    }
}




