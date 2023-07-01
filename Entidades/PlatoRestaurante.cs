using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Entidades
{
    public class PlatoRestaurante
    {
        #region "Atributos"
        private int idAsignacion;
        private DateTime fechaAfiliacion;
        private int restauranteAsignado;


        public PlatoRestaurante(int idAsignacion, int restauranteAsignado, Restaurante[] restaurantes, PlatoRestaurante[] platos)
        {
            this.idAsignacion = idAsignacion;
            this.restauranteAsignado = restauranteAsignado;


        }


        public PlatoRestaurante(int idAsignacion, int restauranteAsignado, DateTime fechaAfiliacion, Restaurante[] restaurantes, PlatoRestaurante[] platos)
        {
            this.idAsignacion = idAsignacion;
            this.fechaAfiliacion = fechaAfiliacion;


        }

        public int IdAsignacion
        {
            get { return idAsignacion; }
            set { idAsignacion = value; }
        }

        public DateTime FechaAfilicion
        {
            get { return fechaAfiliacion; }
            set { fechaAfiliacion = value; }
        }



        public int RestauranteAsignado
        {
            get { return restauranteAsignado; }
            set { restauranteAsignado = value; }
        }


        #endregion


    }
}
