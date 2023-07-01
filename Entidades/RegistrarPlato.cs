using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Entidades
{
    public class RegistrarPlato
    {

        #region "Atributos"
        // Atributos de la clase
        private int idPlato; // Identificador único de la categoría
        private string nombrePlato; // Nombre del plato
        private int precio; //precio del plato
        private int idValidar;



        // Constructor de la clase
        public RegistrarPlato(int idPlato, string nombrePlato, int precio, int idValidar, CategoriaPlato[] categorias)
        {
            this.idPlato = idPlato;
            this.nombrePlato = nombrePlato;
            this.precio = precio;
            this.idValidar = idValidar;

        }

        // Métodos de acceso (getters y setters) de los atributos
        public int IdPlato
        {
            get { return idPlato; }
            set { idPlato = value; }
        }

        public string NombrePlato
        {
            get { return nombrePlato; }
            set { nombrePlato = value; }
        }

        public int Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        public int IdValidar
        {
            get { return idValidar; }
            set { idValidar = value; }
        }
        #endregion
    }
}
