﻿using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Entidades
{
    public class Plato
    {

        #region "Atributos"
        // Atributos de la clase
        private int idPlato; // Identificador único de la categoría
        private string nombrePlato; // Nombre del plato
        private int precio; //precio del plato
        private CategoriaPlato categoriaPlato;



        // Constructor de la clase
        [JsonConstructor]
        public Plato(int idPlato, string nombrePlato, int precio, CategoriaPlato categoriaPlato)
        {
            this.idPlato = idPlato;
            this.nombrePlato = nombrePlato;
            this.precio = precio;
            this.categoriaPlato = categoriaPlato;

        }

        public Plato(int idPlato) { 
            this.idPlato=idPlato;
            this.nombrePlato = "";
            this.precio = 0;
            this.categoriaPlato = new CategoriaPlato(0,"",true);
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
        public CategoriaPlato CategoriaPlato
        {
            get { return categoriaPlato; }
            set { categoriaPlato = value; }
        }

        public int IdCategoria
        {
            get { return categoriaPlato !=  null ?  categoriaPlato.IdCategoria: 0 ; }            
        }
        


        #endregion
    }
}
