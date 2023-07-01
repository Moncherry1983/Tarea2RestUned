using System;
using Entidades;
using AccesoDatos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LogicaNegocio
{
    public class ClienteLN
    {

        public void AgregarCliente(Cliente ingresarClientes)
        {
            try
            {
                ClienteAD.AgregarCliente(ingresarClientes);

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
