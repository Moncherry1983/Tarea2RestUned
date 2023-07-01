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

        // Inicializar el arreglo con 20 posiciones
        Restaurante[] restaurantes = new Restaurante[20];
        CategoriaPlato[] categorias = new CategoriaPlato[20];
        RegistrarPlato[] ingresarPlato = new RegistrarPlato[20];
        Cliente[] ingresarCliente = new Cliente[20];
        PlatoRestaurante[] platos = new PlatoRestaurante[10];
        PlatoRestaurante[] platoRestaurante = new PlatoRestaurante[20];
       


        public void GuardarRestaurante(Restaurante restaurante)
        {



        }



        public void GuardarCategoria(CategoriaPlato categoria)
        {
            int contador = 0;
            bool revision = true;

            for (int i = 0; i < categorias.Count(); i++)
            {

                if (categorias[i] == null)
                {
                    contador = i;
                    revision = false;
                    break;
                }
            }

            if (!revision)
            {
                categorias[contador] = categoria;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }

        }

        public void GuardarIngresarPlato(RegistrarPlato ingresarPlatos)
        {
            int contador = 0;
            bool revision = true;

            for (int i = 0; i < ingresarPlato.Count(); i++)
            {

                if (ingresarPlato[i] == null)
                {
                    contador = i;
                    revision = false;
                    break;
                }
            }

            if (!revision)
            {
                ingresarPlato[contador] = ingresarPlatos;
            }
            else
            {
                throw new Exception("La lista se encuentra llena.");
            }

        }

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
