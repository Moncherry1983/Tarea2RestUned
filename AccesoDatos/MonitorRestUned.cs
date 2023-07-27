using Entidades;
using LogicaNegocio;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;

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
            this.StartPosition = FormStartPosition.CenterScreen;

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
            string textoSolicitud = "Solicitud recibida desde: ";
            switch (paqueteRecibido)
            {
                case Paquete<CategoriaPlato> paqueteCategoriaPlato:
                    ProcesarCategoriaPlato(textoSolicitud,paqueteCategoriaPlato, msg);
                    break;

                case Paquete<Cliente> paqueteCliente:
                    ProcesarCliente(textoSolicitud, paqueteCliente, msg);
                    break;

                default:
                    break;
            }
        }


        private void ProcesarCategoriaPlato(string textoSolicitud, Paquete<CategoriaPlato> paqueteCategoriaPlato, SimpleTCP.Message msg)
        {
            string respuesta = string.Empty;
            switch (paqueteCategoriaPlato.TiposAccion)
            {
                case TiposAccion.Agregar:
                    txtEstado.Invoke((MethodInvoker)delegate
                    {
                        txtEstado.Text += $"{textoSolicitud} {paqueteCategoriaPlato.ClienteId}, procesando la solicitud: {TiposAccion.Agregar}...{Environment.NewLine}";                        

                        if (CategoriaPlatoAD.AgregarCategoria(paqueteCategoriaPlato.InstaciaGenerica))
                        {                            
                            string serializedResult = AdmistradorPaquetes.SerializePackage(paqueteCategoriaPlato);
                            msg.ReplyLine(serializedResult);
                            respuesta = $"Respuesta enviada a: {paqueteCategoriaPlato.ClienteId}...{Environment.NewLine}";
                        }
                        else
                        {
                            respuesta = $"La Categoría Plato no se pudo agregar... {Environment.NewLine}";
                        }

                        txtEstado.Text += respuesta;
                    });
                    break;

                case TiposAccion.Listar:
                    txtEstado.Invoke((MethodInvoker)delegate
                    {
                        txtEstado.Text += $"{textoSolicitud} {paqueteCategoriaPlato.ClienteId}, procesando la solicitud: {TiposAccion.Listar}...{Environment.NewLine}";

                        var categoriasPlatos = CategoriaPlatoAD.ListarCategoriaPlato();

                        if (categoriasPlatos != null)
                        {
                            Paquete<List<CategoriaPlato>> paqueteLista = new Paquete<List<CategoriaPlato>>();
                            paqueteLista.ClienteId = paqueteCategoriaPlato.ClienteId;
                            paqueteLista.TiposAccion = paqueteCategoriaPlato.TiposAccion;
                            paqueteLista.InstaciaGenerica = categoriasPlatos;                            
                            string serializedResult = AdmistradorPaquetes.SerializePackage(paqueteLista);
                            msg.ReplyLine(serializedResult);
                            respuesta = $"Respuesta enviada a: {paqueteCategoriaPlato.ClienteId}...{Environment.NewLine}";
                        }
                        else
                        {
                            respuesta = $"La lista de categorías de platos no se pudo obtener... {Environment.NewLine}";
                        }

                        txtEstado.Text += respuesta;
                    });
                    break;

                case TiposAccion.ObtenerObjetoEspecifico:

                    txtEstado.Text += $"{textoSolicitud} {paqueteCategoriaPlato.ClienteId}, procesando la solicitud: {TiposAccion.ObtenerObjetoEspecifico}...{Environment.NewLine}";                    
                    var categoriasPlato = CategoriaPlatoAD.ObtenerCategoriaPlato(paqueteCategoriaPlato.InstaciaGenerica.IdCategoria);

                    if (categoriasPlato != null)
                    {
                        paqueteCategoriaPlato.InstaciaGenerica = categoriasPlato;
                        string serializedResult = AdmistradorPaquetes.SerializePackage(paqueteCategoriaPlato);
                        msg.ReplyLine(serializedResult);
                        respuesta = $"Respuesta enviada a: {paqueteCategoriaPlato.ClienteId}...{Environment.NewLine}";
                    }
                    else
                    {
                        respuesta = $"La categorías de platos no se pudo obtener... {Environment.NewLine}";
                    }

                    txtEstado.Text += respuesta;

                    break;

                default:
                    break;
            }
        }
        private void ProcesarCliente(string textoSolicitud, Paquete<Cliente> paqueteCliente, SimpleTCP.Message msg)
        {
            string respuesta = string.Empty;
            switch (paqueteCliente.TiposAccion)
            {
                case TiposAccion.Agregar:
                    txtEstado.Invoke((MethodInvoker)delegate
                    {
                        txtEstado.Text += $"{textoSolicitud} {paqueteCliente.ClienteId}, procesando la solicitud: {TiposAccion.Agregar}...{Environment.NewLine}";

                        if (ClienteAD.AgregarCliente(paqueteCliente.InstaciaGenerica))
                        {
                            string serializedResult = AdmistradorPaquetes.SerializePackage(paqueteCliente);
                            msg.ReplyLine(serializedResult);
                            respuesta = $"Respuesta enviada a: {paqueteCliente.ClienteId}...{Environment.NewLine}";
                        }
                        else
                        {
                            respuesta = $"El Cliente no se pudo agregar... {Environment.NewLine}";
                        }

                        txtEstado.Text += respuesta;
                    });
                    break;

                case TiposAccion.Listar:
                    txtEstado.Invoke((MethodInvoker)delegate
                    {
                        txtEstado.Text += $"{textoSolicitud} {paqueteCliente.ClienteId}, procesando la solicitud: {TiposAccion.Listar}...{Environment.NewLine}";

                        var clientes = ClienteAD.ListarClientes();

                        if (clientes != null)
                        {
                            Paquete<List<Cliente>> paqueteLista = new Paquete<List<Cliente>>();
                            paqueteLista.ClienteId = paqueteCliente.ClienteId;
                            paqueteLista.TiposAccion = paqueteCliente.TiposAccion;
                            paqueteLista.InstaciaGenerica = clientes;
                            string serializedResult = AdmistradorPaquetes.SerializePackage(paqueteLista);
                            msg.ReplyLine(serializedResult);
                            respuesta = $"Respuesta enviada a: {paqueteCliente.ClienteId}...{Environment.NewLine}";
                        }
                        else
                        {
                            respuesta = $"La lista de categorías de platos no se pudo obtener... {Environment.NewLine}";
                        }

                        txtEstado.Text += respuesta;
                    });
                    break;

                case TiposAccion.ObtenerObjetoEspecifico:

                    txtEstado.Invoke((MethodInvoker)delegate
                    {
                        txtEstado.Text += $"{textoSolicitud} {paqueteCliente.ClienteId}, procesando la solicitud: {TiposAccion.ObtenerObjetoEspecifico}...{Environment.NewLine}";

                        var clienteAD = new ClienteAD();
                        var cliente = clienteAD.ObtenerClientePorId(paqueteCliente.InstaciaGenerica.IdCedula);

                        if (cliente != null)
                        {
                            paqueteCliente.InstaciaGenerica = cliente;                            
                            string serializedResult = AdmistradorPaquetes.SerializePackage(paqueteCliente);
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