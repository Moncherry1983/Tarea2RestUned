using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Entidades
{
    public class Restaurante
    {

        #region "Atributos"
        // Atributos de la clase
        private int idRestaurante; // Identificador único del restaurante
        private string nombreRestaurante; // Nombre del restaurante
        private string direccion; // Dirección del restaurante
        private bool estado; // Estado del restaurante (activo o inactivo)
        private string telefono; // Teléfono del restaurante

        public Restaurante(int idRestaurante)
        {
            this.idRestaurante = idRestaurante;
        }

        //Constructor de la clase
        [JsonConstructor]
        public Restaurante(int idRestaurante, string nombreRestaurante, string direccion, bool estado, string telefono)
        {
            this.idRestaurante = idRestaurante;
            this.nombreRestaurante = nombreRestaurante;
            this.direccion = direccion;
            this.estado = estado;
            this.telefono = telefono;
        }

        // Métodos de acceso (getters y setters) de los atributos
        public int IdRestaurante
        {
            get { return idRestaurante; }
            set { idRestaurante = value; }
        }

        public string NombreRestaurante
        {
            get { return nombreRestaurante; }
            set { nombreRestaurante = value; }
        }

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        #endregion
    }
}

