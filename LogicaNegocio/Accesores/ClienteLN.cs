using Entidades;
using System;

namespace LogicaNegocio.Accesores
{
    public class ClienteLN
    {
        public void AgregarCliente(Cliente ingresarClientes)
        {
            try
            {
                //var clienteActuales = ClienteAD.ListarCliente();
                //if (clienteActuales.Where(clien => clien != null && clien.IdCedula == ingresarClientes.IdCedula).Count() == 0)
                //    ClienteAD.AgregarCliente(ingresarClientes);
                //else
                //    throw new Exception("Ya existe un cliente con ese Id");
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
                //return ClienteAD.ListarCliente();
                return new Cliente[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}