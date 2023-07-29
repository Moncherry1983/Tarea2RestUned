using AccesoDatos.Accesores;
using Entidades;
using LogicaNegocio;
using LogicaNegocio.Enumeradores;
using SimpleTCP;
using System;
using System.Collections;
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

        //Este método se llama cuando se hace clic en el botón Desconectar.
        //Lo que hace es agregar un mensaje al cuadro de texto Estado que dice
        //que la desconexión fue exitosa. Luego, detiene el servidor para terminar la comunicación.
        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            txtEstado.Text += "  ...Desconeccion exitosa... " + Environment.NewLine;
            server.Stop();
        }

        //Método que se llama cuando se hace clic en el botón Salir. Lo que hace es mostrar un mensaje en pantalla
        private void btnSalir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Estas saliendo Monitor Rest-Uned...");
            Application.Exit();
        }
        private void Server_DataReceived(object sender, SimpleTCP.Message msg)
        {
            var paqueteRecibido = AdmistradorPaquetes.DeserializePackage(msg.MessageString);            
            string textoSolicitud = $"Solicitud recibida desde:";
            switch (paqueteRecibido)
            {
                case Paquete<CategoriaPlato> paqueteCategoriaPlato:                    
                    ProcesarCategoriaPlato(textoSolicitud,paqueteCategoriaPlato, msg);
                    break;

                case Paquete<Cliente> paqueteCliente:                    
                    ProcesarCliente(textoSolicitud, paqueteCliente, msg);
                    break;
                case Paquete<Plato> paquetePlato:
                    ProcesarPlato(textoSolicitud, paquetePlato, msg);
                    break;
                case Paquete<Restaurante> paqueteRestaurante:                    
                    ProcesarRestaurante(textoSolicitud, paqueteRestaurante, msg);
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

                        if (CategoriaPlatoAD.AgregarCategoria((CategoriaPlato)paqueteCategoriaPlato.ListaInstaciasGenericas[0]))
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

                        var categoriasPlatos = CategoriaPlatoAD.ListarCategoriasPlatos();

                        if (categoriasPlatos != null)
                        {
                            Paquete<List<CategoriaPlato>> paqueteLista = new Paquete<List<CategoriaPlato>>();
                            paqueteLista.ClienteId = paqueteCategoriaPlato.ClienteId;
                            paqueteLista.TiposAccion = paqueteCategoriaPlato.TiposAccion;
                            paqueteLista.ListaInstaciasGenericas = new ArrayList() { categoriasPlatos };                            
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
                    var categoriasPlato = CategoriaPlatoAD.ObtenerCategoriaPlato(((CategoriaPlato)paqueteCategoriaPlato.ListaInstaciasGenericas[0]).IdCategoria);

                    if (categoriasPlato != null)
                    {
                        paqueteCategoriaPlato.ListaInstaciasGenericas[0] = categoriasPlato;
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

                        if (ClienteAD.AgregarCliente((Cliente)paqueteCliente.ListaInstaciasGenericas[0]))
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
                            Paquete<List<Cliente>> paqueteLista = new Paquete<List<Cliente>>
                            {
                                ClienteId = paqueteCliente.ClienteId,
                                TiposAccion = paqueteCliente.TiposAccion,
                                ListaInstaciasGenericas = new ArrayList() { clientes }
                            };
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
                        
                        var cliente = ClienteAD.ObtenerClientePorId(((Cliente)paqueteCliente.ListaInstaciasGenericas[0]).IdCedula);

                        if (cliente != null)
                        {
                            paqueteCliente.ListaInstaciasGenericas = new ArrayList() { cliente };                            
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
        private void ProcesarPlato(string textoSolicitud, Paquete<Plato> paquetePlato, SimpleTCP.Message msg)
        {
            string respuesta = string.Empty;
            switch (paquetePlato.TiposAccion)
            {
                case TiposAccion.Agregar:
                    txtEstado.Invoke((MethodInvoker)delegate
                    {
                        txtEstado.Text += $"{textoSolicitud} {paquetePlato.ClienteId}, procesando la solicitud: {TiposAccion.Agregar}...{Environment.NewLine}";

                        if (paquetePlato.ListaInstaciasGenericas.Count > 0 && PlatoAD.AgregarPlato((Plato)paquetePlato.ListaInstaciasGenericas[0]))
                        {
                            string serializedResult = AdmistradorPaquetes.SerializePackage(paquetePlato);
                            msg.ReplyLine(serializedResult);
                            respuesta = $"Respuesta enviada a: {paquetePlato.ClienteId}...{Environment.NewLine}";
                        }
                        else
                        {
                            msg.ReplyLine(null);
                            respuesta = $"El plato no se pudo agregar... {Environment.NewLine}";
                        }

                        txtEstado.Text += respuesta;
                    });
                    break;

                case TiposAccion.Listar:
                    txtEstado.Invoke((MethodInvoker)delegate
                    {
                        txtEstado.Text += $"{textoSolicitud} {paquetePlato.ClienteId}, procesando la solicitud: {TiposAccion.Listar}...{Environment.NewLine}";

                        ArrayList listaObjetos = new ArrayList();

                        foreach (var objeto in paquetePlato.ListaInstaciasGenericas)
                        {
                            var resultados = ObtenerListadoGenerico(objeto);
                            listaObjetos.Add(resultados);
                        }


                        if (listaObjetos.Count > 0)
                        {
                            Paquete<List<Plato>> paqueteLista = new Paquete<List<Plato>>
                            {
                                ClienteId = paquetePlato.ClienteId,
                                TiposAccion = paquetePlato.TiposAccion,
                                ListaInstaciasGenericas = listaObjetos
                            };
                            string serializedResult = AdmistradorPaquetes.SerializePackage(paqueteLista);
                            msg.ReplyLine(serializedResult);
                            respuesta = $"Respuesta enviada a: {paquetePlato.ClienteId}...{Environment.NewLine}";
                        }
                        else
                        {
                            respuesta = $"El plato no se pudo obtener... {Environment.NewLine}";
                        }

                        txtEstado.Text += respuesta;
                    });
                    break;

                case TiposAccion.ObtenerObjetoEspecifico:

                    txtEstado.Invoke((MethodInvoker)delegate
                    {
                        txtEstado.Text += $"{textoSolicitud} {paquetePlato.ClienteId}, procesando la solicitud: {TiposAccion.ObtenerObjetoEspecifico}...{Environment.NewLine}";

                        var cliente = RestauranteAD.ObtenerRestaurante(((Restaurante)paquetePlato.ListaInstaciasGenericas[0]).IdRestaurante);

                        if (cliente != null)
                        {
                            paquetePlato.ListaInstaciasGenericas = new ArrayList() { cliente };
                            string serializedResult = AdmistradorPaquetes.SerializePackage(paquetePlato);
                            msg.ReplyLine(serializedResult);
                            respuesta = $"Respuesta enviada...{Environment.NewLine}";
                        }
                        else
                        {
                            respuesta = $"El Restaurante no existe... {Environment.NewLine}";
                        }

                        txtEstado.Text += respuesta;
                    });

                    break;

                default:
                    break;
            }
        }

        private void ProcesarRestaurante(string textoSolicitud, Paquete<Restaurante> paqueteRestaurante, SimpleTCP.Message msg)
        {
            string respuesta = string.Empty;
            switch (paqueteRestaurante.TiposAccion)
            {
                case TiposAccion.Agregar:
                    txtEstado.Invoke((MethodInvoker)delegate
                    {
                        txtEstado.Text += $"{textoSolicitud} {paqueteRestaurante.ClienteId}, procesando la solicitud: {TiposAccion.Agregar}...{Environment.NewLine}";

                        if (paqueteRestaurante.ListaInstaciasGenericas.Count > 0 &&  RestauranteAD.AgregarRestaurante((Restaurante)paqueteRestaurante.ListaInstaciasGenericas[0]))
                        {
                            string serializedResult = AdmistradorPaquetes.SerializePackage(paqueteRestaurante);
                            msg.ReplyLine(serializedResult);
                            respuesta = $"Respuesta enviada a: {paqueteRestaurante.ClienteId}...{Environment.NewLine}";
                        }
                        else
                        {
                            msg.ReplyLine(null);
                            respuesta = $"El restaurante no se pudo agregar... {Environment.NewLine}";
                        }

                        txtEstado.Text += respuesta;
                    });
                    break;

                case TiposAccion.Listar:
                    txtEstado.Invoke((MethodInvoker)delegate
                    {
                        txtEstado.Text += $"{textoSolicitud} {paqueteRestaurante.ClienteId}, procesando la solicitud: {TiposAccion.Listar}...{Environment.NewLine}";

                        ArrayList listaObjetos = new ArrayList();

                        foreach (var objeto in paqueteRestaurante.ListaInstaciasGenericas)
                        {
                            var resultados = ObtenerListadoGenerico(objeto);
                            listaObjetos.Add(resultados);
                        }
                        

                        if (listaObjetos.Count > 0)
                        {
                            Paquete<List<Restaurante>> paqueteLista = new Paquete<List<Restaurante>>
                            {
                                ClienteId = paqueteRestaurante.ClienteId,
                                TiposAccion = paqueteRestaurante.TiposAccion,
                                ListaInstaciasGenericas = listaObjetos
                            };
                            string serializedResult = AdmistradorPaquetes.SerializePackage(paqueteLista);
                            msg.ReplyLine(serializedResult);
                            respuesta = $"Respuesta enviada a: {paqueteRestaurante.ClienteId}...{Environment.NewLine}";
                        }
                        else
                        {
                            respuesta = $"El restaurante no se pudo obtener... {Environment.NewLine}";
                        }

                        txtEstado.Text += respuesta;
                    });
                    break;

                case TiposAccion.ObtenerObjetoEspecifico:

                    txtEstado.Invoke((MethodInvoker)delegate
                    {
                        txtEstado.Text += $"{textoSolicitud} {paqueteRestaurante.ClienteId}, procesando la solicitud: {TiposAccion.ObtenerObjetoEspecifico}...{Environment.NewLine}";
                        
                        var cliente = RestauranteAD.ObtenerRestaurante(((Restaurante)paqueteRestaurante.ListaInstaciasGenericas[0]).IdRestaurante);

                        if (cliente != null)
                        {
                            paqueteRestaurante.ListaInstaciasGenericas = new ArrayList() { cliente };
                            string serializedResult = AdmistradorPaquetes.SerializePackage(paqueteRestaurante);
                            msg.ReplyLine(serializedResult);
                            respuesta = $"Respuesta enviada...{Environment.NewLine}";
                        }
                        else
                        {
                            respuesta = $"El Restaurante no existe... {Environment.NewLine}";
                        }

                        txtEstado.Text += respuesta;
                    });

                    break;

                default:
                    break;
            }
        }

        private dynamic ObtenerListadoGenerico(object objeto)
        {
            if (objeto.GetType() == typeof(CategoriaPlato))
            {
                return CategoriaPlatoAD.ListarCategoriasPlatos();
            }
            if (objeto.GetType() == typeof(Cliente))
            {
                return ClienteAD.ListarClientes();
            }
            if (objeto.GetType() == typeof(Extra))
            {
                return ExtraAD.ListarExtra();
            }

            if (objeto.GetType() == typeof(Plato))
            {
                return PlatoAD.ListarPlatos();
            }

            if (objeto.GetType() == typeof(PlatoRestaurante))
            {
                return PlatosRestauranteAD.ListarPlatoRestaurante();
            }

            if (objeto.GetType() == typeof(Restaurante))
            {                
                return RestauranteAD.ListarRestaurante();
            }

            return null;
        }
    }
}