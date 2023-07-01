using System;
using Entidades;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LogicaNegocio
{
    public class Arrays
    {

       
       
       
       ;
        PlatoRestaurante[] platos = new PlatoRestaurante[10];
        PlatoRestaurante[] platoRestaurante = new PlatoRestaurante[20];
       


      

        public void GuardaringresarClient(Cliente ingresarClientes)
        {
            int contador = 0;
            bool revision = true;

            for (int i = 0; i < ingresarCliente.Count(); i++)
            {

                if (ingresarCliente[i] == null)
                {
                    contador = i;
                    revision = false;
                    break;
                }
            }

            if (!revision)
            {
                ingresarCliente[contador] = ingresarClientes;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }


        }


        public void Guardarplatos(PlatoRestaurante plato)
        {
            int contador = 0;
            bool revision = true;

            for (int i = 0; i < platos.Count(); i++)
            {

                if (platos[i] == null)
                {
                    contador = i;
                    revision = false;
                    break;
                }
            }

            if (!revision)
            {
                platos[contador] = plato;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }

        }

        public void Guardarplatotatal(PlatoRestaurante platoRestaurantes)
        {
            int contador = 0;
            bool revision = true;

            for (int i = 0; i < platoRestaurante.Count(); i++)
            {

                if (platoRestaurante[i] == null)
                {
                    contador = i;
                    revision = false;
                    break;
                }
            }

            if (!revision)
            {
                platoRestaurante[contador] = platoRestaurantes;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }

        }

    }
}
