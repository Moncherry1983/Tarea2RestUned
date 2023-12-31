﻿using Entidades;
using LogicaNegocio;
using LogicaNegocio.Accesores;
using LogicaNegocio.Enumeradores;
using Presentacion.Miscelaneas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MenuClientes : Form
    {
        //inicializan los objetos de la capa de negocio
        readonly string nombreMaquinaCliente;
        readonly PantallaEspera pantallaEspera = new PantallaEspera();
        AdministradorTCP tcpClient;
        List<CategoriaPlato> listaCategoriaPlatos = new List<CategoriaPlato>();

        
        private readonly IDictionary<char, string> generos = new Dictionary<char, string>();

        public MenuClientes(string nombreMaquinaCliente)
        {
            //inicializa los componentes del formulario
            InitializeComponent();
            this.nombreMaquinaCliente = nombreMaquinaCliente;
            dgvCliente.ReadOnly = true;
            InicializarDataGridView();
            InicializarComboBox();
        }

        //metodo para inicializar el datagridview
        void InicializarDataGridView()
        {
            dgvCliente.ReadOnly = true;
            dgvCliente.AutoGenerateColumns = false;

            dgvCliente.Columns.Add("IdCedula", "Cedula");
            dgvCliente.Columns.Add("Nombre", "Nombre");
            dgvCliente.Columns.Add("PApellido", "Primer apellido");
            dgvCliente.Columns.Add("SApellido", "Segundo apellido");
            dgvCliente.Columns.Add("FNacimiento", "Fecha nacimiento");
            dgvCliente.Columns.Add("Genero", "Genero");

            dgvCliente.Columns["IdCedula"].DataPropertyName = "IdCedula";
            dgvCliente.Columns["IdCedula"].Width = 80;

            dgvCliente.Columns["Nombre"].DataPropertyName = "Nombre";
            dgvCliente.Columns["Nombre"].Width = 120;

            dgvCliente.Columns["PApellido"].DataPropertyName = "PApellido";
            dgvCliente.Columns["PApellido"].Width = 120;

            dgvCliente.Columns["SApellido"].DataPropertyName = "SApellido";
            dgvCliente.Columns["SApellido"].Width = 120;

            dgvCliente.Columns["FNacimiento"].DataPropertyName = "FNacimiento";
            dgvCliente.Columns["FNacimiento"].Width = 120;

            dgvCliente.Columns["Genero"].DataPropertyName = "Genero";
            dgvCliente.Columns["Genero"].Width = 80;

        }

        private void InicializarComboBox()
        {
            generos.Add('M', "Masculino");
            generos.Add('F', "Femenino");
            
            cmbGenero.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGenero.DisplayMember = "Value";
            cmbGenero.ValueMember = "Key";

            cmbGenero.DataSource = generos.ToList();

        }
        private void MenuClientes_Load(object sender, EventArgs e)
        {
            tcpClient = new AdministradorTCP();
            tcpClient.TcpClient.DataReceived += Client_DataReceived;
            SolicitarDatosAlServidor();
        }
 
        //Este método carga los datos de los clientes en el datagridview.
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string idPersona = txtIdPersona.Text;
                string nombre = txtNombrePersona.Text;
                String pApellido = txtPrimerApellido.Text;
                String sApellido = txtSegundoApellido.Text;
                DateTime fNacimiento = dtpFNacimiento.Value;

                DateTime fechaActual = DateTime.Now;
                if (fNacimiento > fechaActual)
                {
                    // El usuario ha seleccionado una fecha posterior a la fecha actual.
                    // Realiza acciones de validación o muestra un mensaje de error al usuario.
                    MessageBox.Show("La fecha seleccionada no tiene un orden cronologico...");
                }
                else
                {
                    //validaciones para que no se dejen campos vacios
                    if (String.IsNullOrEmpty(txtIdPersona.Text) || String.IsNullOrEmpty(txtNombrePersona.Text) || String.IsNullOrEmpty(txtPrimerApellido.Text) || String.IsNullOrEmpty(txtSegundoApellido.Text))
                    {
                        MessageBox.Show("No deje campos vacios por favor...");
                    }
                    else if (cmbGenero.SelectedIndex == -1)
                    {
                        MessageBox.Show("No deje campos vacios por favor...");
                    }
                    else
                    {
                        char genero = char.Parse(cmbGenero.SelectedValue.ToString());
                        ClienteLN clienteLN = new ClienteLN();
                        Cliente cliente = new Cliente(txtIdPersona.Text, txtNombrePersona.Text, txtPrimerApellido.Text, txtSegundoApellido.Text, dtpFNacimiento.Value, char.Parse(cmbGenero.SelectedValue.ToString()));
                        GuardarCambios(cliente);
                        SolicitarDatosAlServidor();
                    }
                }
                txtIdPersona.Text = " ";
                txtNombrePersona.Text = " ";
                txtPrimerApellido.Text = " ";
                txtSegundoApellido.Text = " ";
                cmbGenero.SelectedIndex = -1;
                dtpFNacimiento.ResetText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\tHa sucedido un error y no podido registrar el cliente\n");
            }
        }

        private void txtNombrePersona_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        //Este código define un método que se ejecuta cuando se formatea una celda del control DataGridView llamado dgvCliente.
        //El método recibe dos parámetros: el objeto que disparó el evento y los argumentos del evento. El método verifica si la columna
        //de la celda es la que tiene el nombre "FNacimiento", que almacena la fecha de nacimiento de los clientes. Si es así, intenta convertir
        //el valor de la celda a un tipo DateTime y lo muestra en un formato específico. Si el valor no es válido o nulo, muestra un mensaje de error.
        //Si ocurre alguna excepción durante el proceso, muestra otro mensaje de error.
        private void dgvCliente_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dgvCliente.Columns[e.ColumnIndex];

            try
            {
                if (col.Name == "FNacimiento")
                {
                    if (e.Value != null && e.Value != DBNull.Value)
                    {
                        if (DateTime.TryParse(e.Value.ToString(), out DateTime dateValue))
                        {
                            e.Value = dateValue.ToString(" dd / MMMM / yyyy");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "Fecha inválida";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                e.Value = "Desconocido";
            }
        }

        //Este código es una función que se ejecuta cuando el usuario cambia el texto de un cuadro de texto llamado txtCedula.
        //El propósito de esta función es validar que el texto solo contenga números y que no tenga más de 9 caracteres.
        //Para hacer esto, el código recorre cada carácter del texto y lo elimina si no es un número. Luego, verifica si el texto
        //tiene más de 9 caracteres y lo recorta si es así. Finalmente, ajusta la posición del cursor al final del texto.
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

            if (txtIdPersona.Text.Length > 9)
            {
                txtIdPersona.Text = txtIdPersona.Text.Substring(0, 9);
                txtIdPersona.SelectionStart = txtIdPersona.Text.Length;
            }
        }

        private void cmbGenero_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGenero.SelectedIndex != -1)
            {
                // El usuario ha seleccionado un elemento del combobox
            }
            else
            {
                // El usuario no ha seleccionado ningún elemento del combobox
            }
        }

        private void GuardarCambios(Cliente cliente)
        {
            try
            {
                if (tcpClient.ConectarTCP())
                {
                    var paquete = new Paquete<Cliente>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Agregar,
                        ListaInstaciasGenericas = new ArrayList() { cliente }
                    };

                    string paqueteSerializado = AdmistradorPaquetes.SerializePackage(paquete);
                    tcpClient.TcpClient.WriteLineAndGetReply(paqueteSerializado, TimeSpan.FromSeconds(3));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con el servidor: " + ex.Message);
            }
        }

        private void SolicitarDatosAlServidor()
        {
            try
            {
                if (tcpClient.ConectarTCP())
                {
                    pantallaEspera.Show();
                    Cliente cliente = new Cliente("", "", "", "",new DateTime(),'M');
                    var paquete = new Paquete<Cliente>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Listar,
                        ListaInstaciasGenericas = new ArrayList() { cliente }
                    };

                    string paqueteSerializado = AdmistradorPaquetes.SerializePackage(paquete);
                    tcpClient.TcpClient.WriteLineAndGetReply(paqueteSerializado, TimeSpan.FromSeconds(3));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con el servidor: " + ex.Message);
            }
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valorRecibido = e.MessageString.TrimEnd('\u0013');
            var informacionPaquete = AdmistradorPaquetes.DeserializePackage(valorRecibido);

            if (informacionPaquete != null)
            {
                switch (informacionPaquete.TiposAccion)
                {
                    case TiposAccion.Agregar:
                        ReiniciarPantalla();
                        break;

                    case TiposAccion.Listar:
                        List<Cliente> listaCliente = informacionPaquete.ListaInstaciasGenericas[0];
                        CargarDatos(listaCliente);
                        break;

                    case TiposAccion.ObtenerObjetoEspecifico:

                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("El cliente no existe");
            }
        }

        private void ReiniciarPantalla()
        {
            txtIdPersona.Invoke((MethodInvoker)delegate ()
            {
                SolicitarDatosAlServidor();
                txtIdPersona.Focus();
                cmbGenero.SelectedIndex = 0;
            });
        }

        private void CargarDatos( List<Cliente> listaClientes)
        {
            dgvCliente.Invoke((MethodInvoker)delegate ()
            {
                dgvCliente.DataSource = listaClientes;
                dgvCliente.Refresh();

                pantallaEspera.Hide();
            });
        }

    }
}