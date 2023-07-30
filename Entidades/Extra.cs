using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Entidades
{
    public class Extra
    {
        #region "Atributos"
        // Atributos de la clase
        private int idExtra;
        private string descripcion;
        private bool estado;
        private int precio;
        private int idCategoriaExtra;

        // Constructor de la clase
        public Extra(int idExtra, string descripcion,int precio, bool estado, int idCategoriaExtra)
        {
            this.idExtra = idExtra;
            this.descripcion = descripcion;
            this.estado = estado;
            this.precio = precio;
            this.idCategoriaExtra = idCategoriaExtra;
        }

        // Métodos de acceso (getters y setters) de los atributos
        public int IdExtra
        {
            get { return idExtra; }
            set { idExtra = value; }
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


        public int Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        public int IdCategoriaextra
        {
            get { return idCategoriaExtra; }
            set { idCategoriaExtra = value; }
        }




        #endregion

    }
}
