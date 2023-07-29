using Entidades;
using LogicaNegocio;
using LogicaNegocio.Enumeradores;
using Presentacion.Miscelaneas;
using System;
using System.Collections;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Login : Form
    {
        String nombreMaquinaCliente = String.Empty;
        readonly AdministradorTCP administradorTCP = new AdministradorTCP();
        PantallaEspera pantallaEspera = new PantallaEspera();

        public Login()
        {
            InitializeComponent();
            //txtCedula.Text = "111111111";
        }

        //Este método se ejecuta cuando se carga el formulario de inicio de sesión.
        //Crea un cliente TCP que usa el formato UTF-8 y un delimitador especial para enviar y recibir datos.
        //El cliente TCP tiene un evento que se activa cuando recibe datos. Luego, genera un nombre aleatorio
        //para la máquina del cliente y lo muestra en una etiqueta.
        private void Login_Load(object sender, EventArgs e)
        {            
            administradorTCP.TcpClient.DataReceived += Client_DataReceived;
            Random valorAleatorio = new Random();
            lblNombreMaquina.Text = nombreMaquinaCliente;
            nombreMaquinaCliente = "Cliente" + valorAleatorio.Next(1, 10);
        }

        //Este método se ejecuta cuando se hace clic en el botón de conexión.Tambien este método se ejecuta cuando el cliente recibe un mensaje del servidor.
        //El mensaje contiene información sobre el cliente, como su nombre, su saldo, etc. El método convierte el mensaje en un objeto de tipo Paquete<Cliente>
        //usando una clase llamada LogicaNegocio.AdmistradorPaquetes. Luego, verifica si el objeto es nulo o no. Si no es nulo, significa que el cliente existe y
        //puede acceder al menú principal de la aplicación. El método crea una ventana de tipo MenuPrincipal y le pasa el objeto Paquete<Cliente> como parámetro.
        //Después, oculta la ventana de inicio de sesión. Si el objeto es nulo, significa que el cliente no existe y muestra un mensaje de error.

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string trimCharacter = e.MessageString.TrimEnd('\u0013');
            Paquete<Cliente> informacionCliente = AdmistradorPaquetes.DeserializePackage(trimCharacter);

            if (informacionCliente != null)
            {
                MenuPrincipal menu = new MenuPrincipal((Cliente)informacionCliente.ListaInstaciasGenericas[0], nombreMaquinaCliente);
                this.Invoke(new MethodInvoker(delegate
                {
                    pantallaEspera.Hide();
                    menu.Show();
                }));
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
            try
            {
                if (string.IsNullOrEmpty(txtCedula.Text))
                {
                    MessageBox.Show("Por favor ingresa un usuario...");
                }
                else if (administradorTCP.ConectarTCP())
                {
                    pantallaEspera.Show();
                    Cliente cliente = new Cliente(txtCedula.Text, string.Empty, string.Empty, string.Empty, new DateTime(), 'M');
                    var paquete = new Paquete<Cliente>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.ObtenerObjetoEspecifico,
                        ListaInstaciasGenericas = new ArrayList() { cliente }
                    };

                    string clienteSerializado = AdmistradorPaquetes.SerializePackage(paquete);
                    administradorTCP.TcpClient.WriteLineAndGetReply(clienteSerializado, TimeSpan.FromSeconds(3));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con el servidor: " + ex.Message);
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