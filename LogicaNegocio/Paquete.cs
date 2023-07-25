using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LogicaNegocio
{
    public class Paquete<T> where T : class
    {
        public string ClienteId { get; set; }
        public TiposAccion TiposAccion { get; set; }
        public T InstaciaGenerica { get; set; }
    }
}

