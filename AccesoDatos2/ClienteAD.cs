using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AccesoDatos
{
    public static class ClienteAD
    {

        private static Cliente[] ingresarCliente = new Cliente[20];


        public static void AgregarCliente(Cliente ingresarClientes)
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

        public static Cliente[] ListarCliente()
        {
            try
            {
                return ingresarCliente;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static Cliente ObtenerRestaurante(string idCliente)
        {

            return ingresarCliente.Where(x => x.IdCedula == idCliente).FirstOrDefault();

        }


    }
}
