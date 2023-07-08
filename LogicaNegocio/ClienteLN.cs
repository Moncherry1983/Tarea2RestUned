using System;
using Entidades;
using AccesoDatos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using static LogicaNegocio.PlatoRestauranteLN;

namespace LogicaNegocio
{
    public class ClienteLN
    {

        public void AgregarCliente(Cliente ingresarClientes)
        {
           
            try
            {
                var clienteActuales = ClienteAD.ListarCliente();
                if (clienteActuales.Where(clien => clien != null && clien.IdPersona == ingresarClientes.IdPersona).Count() == 0)
                    ClienteAD.AgregarCliente(ingresarClientes);
                else
                    throw new Exception("Ya existe un cliente con ese Id");

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public Cliente[] ListarCliente()
        {

            try
            {
                return ClienteAD.ListarCliente();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
