using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Entidades
{
    public class PlatoRestaurante
    {
        // Atributos de la clase
        #region "Atributos"
        private DateTime fechaAfiliacion;
        private Restaurante restauranteAsignado;
        private int idAsignacion;
        private Plato platoAsociado;


        public PlatoRestaurante( int idAsignacion, Restaurante restauranteAsignado, Plato platoAsociado, DateTime fechaAfiliacion)
        {
            this.idAsignacion = idAsignacion;
            this.restauranteAsignado = restauranteAsignado;
            this.platoAsociado = platoAsociado;
            this.fechaAfiliacion = fechaAfiliacion;
        }

      

        public int IdAsignacion
        {
            get { return idAsignacion; }
            set { idAsignacion = value; }
        }

        public DateTime FechaAfiliacion
        {
            get { return fechaAfiliacion; }
            set { fechaAfiliacion = value; }
        }

        public Restaurante RestauranteAsignado
        {
            get { return restauranteAsignado; }
            set { restauranteAsignado = value; }

        }

        public Plato PlatoAsociado
        {
            get { return platoAsociado; }
            set { platoAsociado = value; }

        }


        //public int GetIdRestaurante
        //{
        //    get { return restauranteAsignado.IdRestaurante; }            

        //}

        //public string GetNombreRestaurante
        //{
        //    get { return restauranteAsignado.NombreRestaurante; }

        //}

        //public string GetDireccionRestaurante
        //{
        //    get { return restauranteAsignado.Direccion; }

        //}

    

        #endregion
    }
}
