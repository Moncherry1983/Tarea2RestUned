using SimpleTCP;
using System;
using System.Net.Sockets;

namespace Presentacion
{
    public class AdministradorTCP
    {
        readonly string direccion = "127.0.0.1";
        readonly string puerto = "14100";
        public SimpleTcpClient TcpClient { get; set; }

        public AdministradorTCP()
        {
            TcpClient = new SimpleTcpClient
            {
                StringEncoder = System.Text.Encoding.UTF8,
                Delimiter = 0x13
            };
        }

        public bool ConectarTCP()
        {
            try
            {
                TcpClient.Connect(direccion, int.Parse(puerto));
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DesconectarTCP()
        {
            try
            {
                TcpClient.Disconnect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
}