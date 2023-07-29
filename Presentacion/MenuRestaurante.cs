using Entidades;
using LogicaNegocio;
using LogicaNegocio.Enumeradores;
using Presentacion.Miscelaneas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MenuRestaurante : Form
    {
        readonly string nombreMaquinaCliente;
        PantallaEspera pantallaEspera = new PantallaEspera();
        AdministradorTCP tcpClient;

        public MenuRestaurante(string nombreMaquinaCliente)
        {
            InitializeComponent();            
            this.nombreMaquinaCliente = nombreMaquinaCliente;
            dgvRestaurantes.ReadOnly = true;
            InicializarDataGridView();
        }

        void InicializarDataGridView()
        {
            //se bloquea para no se pueda manipular permite columnas personalizadas
            dgvRestaurantes.ReadOnly = true;
            dgvRestaurantes.AutoGenerateColumns = false;
            // se crean los cascarones para rellenar el grid
            dgvRestaurantes.Columns.Add("IdRestaurante", "IdRestaurante");
            dgvRestaurantes.Columns.Add("NombreRestaurante", "NombreRestaurante");
            dgvRestaurantes.Columns.Add("Direccion", "Direccion");
            dgvRestaurantes.Columns.Add("Estado", "Estado");
            dgvRestaurantes.Columns.Add("Telefono", "Telefono");

            dgvRestaurantes.Columns["IdRestaurante"].DataPropertyName = "IdRestaurante";
            dgvRestaurantes.Columns["IdRestaurante"].Width = 80;

            dgvRestaurantes.Columns["NombreRestaurante"].DataPropertyName = "NombreRestaurante";
            dgvRestaurantes.Columns["NombreRestaurante"].Width = 150;

            dgvRestaurantes.Columns["Direccion"].DataPropertyName = "Direccion";
            dgvRestaurantes.Columns["Direccion"].Width = 180;

            dgvRestaurantes.Columns["Estado"].DataPropertyName = "Estado";
            dgvRestaurantes.Columns["Estado"].Width = 60;

            dgvRestaurantes.Columns["Telefono"].DataPropertyName = "Telefono";
            dgvRestaurantes.Columns["Telefono"].Width = 70;            
        }

        private void MenuRestaurante_Load(object sender, EventArgs e)
        {
            tcpClient = new AdministradorTCP();
            tcpClient.TcpClient.DataReceived += Client_DataReceived;
            cmbEstado.SelectedIndex = 0;
            SolicitarDatosAlServidor();
            
        }

        private void SolicitarDatosAlServidor()
        {
            try
            {
                if (tcpClient.ConectarTCP())
                {
                    pantallaEspera.Show();
                    Restaurante restaurante = new Restaurante(0, "","", true, "");
                    var paquete = new Paquete<Restaurante>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Listar,
                        ListaInstaciasGenericas = new ArrayList() { restaurante }
                    };

                    string PaqueteSerializado = AdmistradorPaquetes.SerializePackage(paquete);
                    tcpClient.TcpClient.WriteLineAndGetReply(PaqueteSerializado, TimeSpan.FromSeconds(3));
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
                        List<Restaurante> listaRestaurantes = informacionPaquete.ListaInstaciasGenericas[0];
                        CargarDatos(listaRestaurantes);
                        break;

                    case TiposAccion.ObtenerObjetoEspecifico:

                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("La categoría de plato no existe");
            }
        }                
        
        private void idCategoria_TextChanged(object sender, EventArgs e)
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
        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //inicializa las variables
                int idRestaurante = int.Parse(txtidRestaurante.Text);
                string nombreRestaurante = txtNombre.Text;
                String direccion = txtDireccion.Text;
                string telefono = txtTelefono.Text;

                if (String.IsNullOrEmpty(txtidRestaurante.Text) || String.IsNullOrEmpty(txtNombre.Text) || String.IsNullOrEmpty(txtTelefono.Text) || String.IsNullOrEmpty(txtDireccion.Text))
                {
                    MessageBox.Show("No deje campos vacios por favor...");
                }
                else if (cmbEstado.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar el estado del restaurante...");
                }
                else
                {
                    bool estado = cmbEstado.SelectedIndex == 0;
                    Restaurante restaurante = new Restaurante(int.Parse(txtidRestaurante.Text), txtNombre.Text, txtDireccion.Text, cmbEstado.SelectedItem.ToString() == "Activo", txtTelefono.Text);
                    GuardarRestaurante(restaurante);
                    SolicitarDatosAlServidor();
                }

                txtidRestaurante.Text = " ";
                txtNombre.Text = " ";
                txtDireccion.Text = " ";
                cmbEstado.SelectedIndex = -1;
                txtTelefono.Text = " ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "\n\tHa sucedido un error y no podido registrar el restaurante\n", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedIndex != -1)
            {
                // El usuario ha seleccionado un elemento del combobox
            }
            else
            {
                // El usuario no ha seleccionado ningún elemento del combobox
            }
        }

        //permite solo numero a agregar al telefono y no se puede pasar de 8 digitos
        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            if (txtTelefono.Text.Length > 8)
            {
                txtTelefono.Text = txtTelefono.Text.Substring(0, 8);
                txtTelefono.SelectionStart = txtTelefono.Text.Length;
            }

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

        // metod para cambiar true y el false por activo y inactivo
        private void dgvRestaurantes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dgvRestaurantes.Columns[e.ColumnIndex];

            try
            {
                if (col.Name == "Estado")
                {
                    if (e.Value != null)
                        e.Value = Convert.ToBoolean(e.Value) ? "Activo" : "Inactivo";
                }
            }
            catch (Exception ex)
            {
                e.Value = "Unknown";
            }
        }

        private void GuardarRestaurante(Restaurante restaurante)
        {
            try
            {
                
                if (tcpClient.ConectarTCP())
                {
                    var paquete = new Paquete<Restaurante>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Agregar,
                        ListaInstaciasGenericas = new ArrayList() { restaurante }
                    };

                    string CategoriaPlatoSerializada = AdmistradorPaquetes.SerializePackage(paquete);
                    tcpClient.TcpClient.WriteLineAndGetReply(CategoriaPlatoSerializada, TimeSpan.FromSeconds(3));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con el servidor: " + ex.Message);
            }
        }

        private void ReiniciarPantalla()
        {
            txtidRestaurante.Invoke((MethodInvoker)delegate ()
            {
                SolicitarDatosAlServidor();
                txtidRestaurante.Focus();
                cmbEstado.SelectedIndex = 1;
            });
        }

        private void CargarDatos(List<Restaurante> lista)
        {
            dgvRestaurantes.Invoke((MethodInvoker)delegate ()
            {
                pantallaEspera.Hide();
                dgvRestaurantes.DataSource = lista;
                dgvRestaurantes.Refresh();
            });
        }
    }
}