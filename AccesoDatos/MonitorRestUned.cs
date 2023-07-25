using System;
using SimpleTCP;
using Entidades;
using System.Net;
using System.Data;
using System.Linq;
using System.Text;
using LogicaNegocio;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AccesoDatos
{
    public partial class MonitorRestUned : Form
    {
        SimpleTcpServer server;

        public MonitorRestUned()
        {
            InitializeComponent();
            txtDireccion.Text = "127.0.0.1";
            txtPuerto.Text = "14100";
        }


        //Se inicializa la coneccion al servidor.
        private void btnEncender_click(object sender, EventArgs e)
        {

            txtEstado.Text += " ...Servidor se ha iniciado..." + Environment.NewLine;
            IPAddress ipAddress = IPAddress.Parse(txtDireccion.Text);
            server.Start(ipAddress, Convert.ToInt32(txtPuerto.Text));
        }

        // todo el contenido lo transforme en UTF8 LETRAS Y NUMEROS 
        private void Monitor_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer
            {
                StringEncoder = Encoding.UTF8,
                Delimiter = 0x13
            };
            server.DataReceived += Server_DataReceived;

        }

        private void Server_DataReceived(object sender, SimpleTCP.Message msg)
        {
            var paqueteRecibido = AdmistradorPaquetes.DeserializePackage(msg.MessageString);            
            //ResponseHandler(paqueteRecibido);            

            switch (paqueteRecibido)
            {
                case Paquete<Cliente> paquete:
                    switch (paquete.TiposAccion)
                    {
                        case TiposAccion.Agregar:
                            break;

                        case TiposAccion.Listar:
                            break;

                        case TiposAccion.ObtenerObjetoEspecifico:

                            txtEstado.Invoke((MethodInvoker)delegate
                            {
                                txtEstado.Text += $"Solicitud del cliente Recibida, tramitando la solicitud...{Environment.NewLine}";
                                string respuesta = string.Empty;

                                var clienteAD = new ClienteAD();
                                var cliente = clienteAD.ObtenerClientePorId(paquete.InstaciaGenerica.IdCedula);

                                if (cliente != null)
                                {
                                    paquete.InstaciaGenerica = cliente;
                                    //Serialize and package Object gotten
                                    string serializedResult = AdmistradorPaquetes.SerializePackage(paquete);
                                    msg.ReplyLine(serializedResult);
                                    respuesta = $"Response sent...{Environment.NewLine}";
                                }
                                else
                                {
                                    respuesta = $"Client not found {Environment.NewLine}";
                                }

                                txtEstado.Text += respuesta;
                            });

                            break;

                        default:
                            break;
                    }

                    break;

                default:
                    break;
            }
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            txtEstado.Text += "  ...Desconeccion exitosa... " + Environment.NewLine;
            server.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Estas saliendo Monitor Rest-Uned...");
            Application.Exit();
        }

    }
}
