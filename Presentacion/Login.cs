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

        private void OcultarLogin()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { OcultarLogin(); }));
                return;
            }
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Estas saliendo del programa Rest-Uned...");
            Application.Exit();
        }


        //Inicializa la coneccion con el servidor
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
        //Validacion por si no hay respuesta con el servidor..
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
