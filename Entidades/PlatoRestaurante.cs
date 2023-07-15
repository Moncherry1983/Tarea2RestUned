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
        private Plato[] platos = new Plato[10];

        public PlatoRestaurante(Restaurante restauranteAsignado, Plato[] platos, DateTime fechaAfiliacion)
        {
            this.restauranteAsignado = restauranteAsignado;
            this.fechaAfiliacion = fechaAfiliacion;
            this.platos = platos;


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


        public int GetIdRestaurante
        {
            get { return restauranteAsignado.IdRestaurante; }            

        }

        public string GetNombreRestaurante
        {
            get { return restauranteAsignado.NombreRestaurante; }

        }

        public string GetDireccionRestaurante
        {
            get { return restauranteAsignado.Direccion; }

        }

        public Plato[] Platos
        {
            get { return platos; }
            set { platos = value; }

        }

        #endregion
    }
}
