using System;
using SimpleTCP;
using Entidades;
using System.Net;
using System.Data;
using System.Linq;
using System.Text;
using LogicaNegocio;
using System.Drawing;
using System.Threading;
using System.Text.Json;
using System.Net.Sockets;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Security.Policy;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Presentacion
{
    public partial class Login : Form
    {
        String direccion = "127.0.0.1";
        String puerto = "14100";
        SimpleTcpClient tcpClient;
        String nombreMaquinaCliente = String.Empty;


        public Login()
        {
            InitializeComponent();
        }

        //Este método se ejecuta cuando se carga el formulario de inicio de sesión.
        //Crea un cliente TCP que usa el formato UTF-8 y un delimitador especial para enviar y recibir datos.
        //El cliente TCP tiene un evento que se activa cuando recibe datos. Luego, genera un nombre aleatorio
        //para la máquina del cliente y lo muestra en una etiqueta.
        private void Login_Load(object sender, EventArgs e)
        {
            tcpClient = new SimpleTcpClient
            {
                StringEncoder = System.Text.Encoding.UTF8,
                Delimiter = 0x13
            };
            tcpClient.DataReceived += Client_DataReceived;
            Random valorAleatorio = new Random();
            nombreMaquinaCliente = "Cliente" + valorAleatorio.Next(1,10);
            lblNombreMaquina.Text = nombreMaquinaCliente;
        }

        //Este método se ejecuta cuando se hace clic en el botón de conexión.Tambien este método se ejecuta cuando el cliente recibe un mensaje del servidor.
        //El mensaje contiene información sobre el cliente, como su nombre, su saldo, etc. El método convierte el mensaje en un objeto de tipo Paquete<Cliente>
        //usando una clase llamada LogicaNegocio.AdmistradorPaquetes. Luego, verifica si el objeto es nulo o no. Si no es nulo, significa que el cliente existe y
        //puede acceder al menú principal de la aplicación. El método crea una ventana de tipo MenuPrincipal y le pasa el objeto Paquete<Cliente> como parámetro.
        //Después, oculta la ventana de inicio de sesión. Si el objeto es nulo, significa que el cliente no existe y muestra un mensaje de error.

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valorRecibido = e.MessageString.TrimEnd('\u0013');
            Paquete<Cliente> informacionCliente = LogicaNegocio.AdmistradorPaquetes.DeserializePackage(valorRecibido);

            if (informacionCliente != null)
            {
                MenuPrincipal menu = new MenuPrincipal(informacionCliente.InstaciaGenerica);
                this.Invoke(new MethodInvoker(delegate { menu.Show(); }));
                OcultarLogin();
            }
            else
            {
                MessageBox.Show("El cliente no existe");
            }
        }

       // Este método sirve para ocultar la ventana de inicio de sesión cuando no se necesita.Primero,
       // comprueba si el método se está ejecutando en el mismo hilo que el formulario.Si no es así,
       // llama al método Invoke, que ejecuta el método en el hilo del formulario.Después, usa el método Hide
       // para hacer invisible la ventana.

        private void OcultarLogin()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { OcultarLogin(); }));
                return;
            }
            this.Hide();
        }

        //Este método se ejecuta cuando se hace clic en el botón de salida. Muestra un mensaje de confirmación
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Estas saliendo del programa Rest-Uned...");
            Application.Exit();
        }


        //verifica si un cuadro de texto llamado "txtCedula" está vacío. Si lo está, muestra un mensaje pidiendo que se ingrese un usuario.
        //Si el cuadro de texto no está vacío, el método intenta conectarse a un servidor mediante la función "ConnectToServer()".
        //Si la conexión tiene éxito, crea un objeto "Cliente" con la información del usuario ingresada en el cuadro de texto.
        //Luego, prepara un paquete de datos que contiene la información del cliente y lo envía al servidor utilizando una conexión de red.
        private void Conectar_Click(object sender, EventArgs e)
        {
           

            if (string.IsNullOrEmpty(txtCedula.Text))
            {
                MessageBox.Show("Por favor ingresa un usuario...");
            }
            else if (ConnectToServer())
            {
                Cliente cliente = new Cliente(txtCedula.Text, string.Empty, string.Empty, string.Empty, new DateTime(), 'M');
                var paquete = new Paquete<Cliente>()
                {
                    ClienteId = nombreMaquinaCliente,
                    TiposAccion = TiposAccion.ObtenerObjetoEspecifico,
                    InstaciaGenerica = cliente

                };

                string clienteSerializado = AdmistradorPaquetes.SerializePackage(paquete);
                tcpClient.WriteLineAndGetReply(clienteSerializado, TimeSpan.FromSeconds(3));
            }          

        }
        //Este método intenta establecer una comunicación con el servidor usando una dirección y un puerto.
        //Si lo logra, devuelve verdadero. Si no, muestra un mensaje de error y devuelve falso.
        private bool ConnectToServer()
        {
            try
            {
                tcpClient.Connect(direccion, int.Parse(puerto));
                return true;
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Error al conectar con el servidor: " + ex.Message);
                return false;
            }
        }

        private void txtCedula_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)sender;
            string texto = textBox.Text;

            // Verificar si el texto contiene caracteres no numéricos
            foreach (char c in texto)
            {
                if (!char.IsDigit(c))
                {
                    textBox.Text = texto.Remove(texto.IndexOf(c), 1);
                    textBox.Select(texto.Length, 0);
                    break;


                }
            }
        }
    }
}
