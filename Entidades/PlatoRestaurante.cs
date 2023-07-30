using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Entidades
{
    public class PlatoRestaurante
    {
        // Atributos de la clase
        #region "Atributos"
        private DateTime fechaAfiliacion;
        private Restaurante restauranteAsignado;
        private int idAsignacion;
        private List<Plato> platosAsociados;
        private int idPlatoAsociado;

        [JsonConstructor]
        public PlatoRestaurante( int idAsignacion, Restaurante restauranteAsignado, List<Plato> platoAsociado, DateTime fechaAfiliacion)
        {
            this.idAsignacion = idAsignacion;
            this.restauranteAsignado = restauranteAsignado;
            this.idPlatoAsociado = -1;
            this.platosAsociados = platoAsociado;
            this.fechaAfiliacion = fechaAfiliacion;
        }

        public PlatoRestaurante(int idAsignacion, int restauranteAsignado, int idPlatoAsociado, DateTime fechaAfiliacion)
        {
            this.idAsignacion = idAsignacion;
            this.restauranteAsignado = new Restaurante(restauranteAsignado);
            this.idPlatoAsociado = idPlatoAsociado;
            this.platosAsociados =  null;
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

        public List<Plato> ListaPlatosAsociados
        {
            get { return platosAsociados; }
            set { platosAsociados = value; }

        }

        public int IdPlatoAsociado
        {
            get { return idPlatoAsociado; }
            set { idPlatoAsociado = value; }
        }


        public int GetIdRestaurante
        {
            get { return restauranteAsignado!= null ? restauranteAsignado.IdRestaurante : -1; }

        }

        public string GetNombreRestaurante
        {
            get { return restauranteAsignado != null ? restauranteAsignado.NombreRestaurante : ""; }

        }

        public string GetDireccionRestaurante
        {
            get { return restauranteAsignado != null ? restauranteAsignado.Direccion: ""; }

        }



        #endregion
    }
}
