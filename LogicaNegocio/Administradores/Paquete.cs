using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using LogicaNegocio.Enumeradores;
using System.Collections;

namespace LogicaNegocio
{
    public class Paquete<T> where T : class
    {
        public Paquete() {
            ListaInstaciasGenericas = new ArrayList();
        }

        public string ClienteId { get; set; }
        public TiposAccion TiposAccion { get; set; }
        public ArrayList ListaInstaciasGenericas { get; set; }
    }
}

