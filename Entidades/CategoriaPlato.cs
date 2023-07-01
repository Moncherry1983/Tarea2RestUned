using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Entidades
{
    public class CategoriaPlato
    {

        #region "Atributos"
        // Atributos de la clase
        private int idCategoria; // Identificador único de la categoría
        private string descripcion; // Descripción de la categoría
        private bool estado; // Estado de la categoría (activo o inactivo)

        // Constructor de la clase
        public CategoriaPlato(int idCategoria, string descripcion, bool estado)
        {
            this.idCategoria = idCategoria;
            this.descripcion = descripcion;
            this.estado = estado;
        }

        // Métodos de acceso (getters y setters) de los atributos
        public int IdCategoria
        {
            get { return idCategoria; }
            set { idCategoria = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }


        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        #endregion
    }
}
