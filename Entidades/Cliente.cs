﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Entidades
{
    public class Cliente
    {
        #region "Atributos"
        // Atributos de la clase
        private string idCedula;
        private string nombre;
        private string pApellido;
        private string sApellido;
        private DateTime fNacimiento;
        private char genero;

        // Constructor de la clase
        public Cliente(string idCedula, string nombre, string pApellido, string sApellido, DateTime fNacimiento, char genero)
        {
            this.idCedula = idCedula;
            this.nombre = nombre;
            this.pApellido = pApellido;
            this.sApellido = sApellido;
            this.fNacimiento = fNacimiento;
            this.genero = genero;
        }

        // Métodos de acceso (getters y setters) de los atributos
        public string IdCedula
        {
            get { return idCedula; }
            set { idCedula = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string PApellido
        {
            get { return pApellido; }
            set { pApellido = value; }
        }

        public string SApellido
        {
            get { return sApellido; }
            set { sApellido = value; }
        }

        public DateTime FNacimiento
        {
            get { return fNacimiento; }
            set { fNacimiento = value; }
        }

        public char Genero
        {
            get { return genero; }
            set { genero = value; }
        }

        #endregion
    }
}
