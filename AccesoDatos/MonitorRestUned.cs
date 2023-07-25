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


        //Lo que hace es mostrar un mensaje en el cuadro de texto que dice que el servidor se ha iniciado.
        //Luego, crea una variable que guarda la dirección IP que se escribió en otro cuadro de texto.
        //Finalmente, llama al método Start del objeto server, pasándole la dirección IP y el número de puerto
        //que también se escribió en otro cuadro de texto. Este método sirve para iniciar la conexión con el servidor.
        private void btnEncender_click(object sender, EventArgs e)
        {

            txtEstado.Text += " ...Servidor se ha iniciado..." + Environment.NewLine;
            IPAddress ipAddress = IPAddress.Parse(txtDireccion.Text);
            server.Start(ipAddress, Convert.ToInt32(txtPuerto.Text));
        }

        //  Crea un nuevo servidor TCP simple que usa el formato UTF8 para enviar y recibir mensajes de texto.
        //  El servidor usa el carácter 0x13 como separador de mensajes. El servidor también tiene un evento
        //  que se activa cuando recibe datos de algún cliente.
        private void Monitor_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer
            {
                StringEncoder = Encoding.UTF8,
                Delimiter = 0x13
            };
            server.DataReceived += Server_DataReceived;

        }

        //Cuando el servidor recibe datos de un cliente, este método es llamado. En primer lugar, deserializa los datos recibidos en un objeto "paqueteRecibido". Luego,
        //se verifica qué tipo de acción debe realizar el servidor, según la información recibida. Si la acción es "ObtenerObjetoEspecifico", busca un cliente en una base de datos
        //utilizando la información proporcionada en el paquete. Si encuentra al cliente, lo empaqueta y lo envía de vuelta al cliente. En caso de no encontrar al cliente, envía un mensaje
        //indicando que no se encontró.
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
                                    respuesta = $"Respuesta enviada...{Environment.NewLine}";
                                }
                                else
                                {
                                    respuesta = $"Cliente no existe... {Environment.NewLine}";
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

        //Este método se llama cuando se hace clic en el botón Desconectar.
        //Lo que hace es agregar un mensaje al cuadro de texto Estado que dice
        //que la desconexión fue exitosa. Luego, detiene el servidor para terminar la comunicación.
        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            txtEstado.Text += "  ...Desconeccion exitosa... " + Environment.NewLine;
            server.Stop();
        }


         //Método que se llama cuando se hace clic en el botón Salir. Lo que hace es mostrar un mensaje en pantalla
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Estas saliendo Monitor Rest-Uned...");
            Application.Exit();
        }

    }
}
