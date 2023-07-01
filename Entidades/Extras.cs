using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Entidades
{
    public class Extras
    {
        #region "Atributos"
        private int idExtra;
        private string descripcion;
        private bool estado;
        private int precio;
        

        // Constructor de la clase
        public Extras(int idExtra,string descripcion,bool estado,int precio)
        {
            this.idExtra = idExtra;
            this.descripcion = descripcion;
            this.estado = estado;
            this.precio = precio;     
        }

        // Métodos de acceso (getters y setters) de los atributos
        public int IdExtra
        {
            get { return IdExtra; }
            set { IdExtra = value; }
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


        #endregion

    }
}
