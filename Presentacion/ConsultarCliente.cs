using System;
using Entidades;
using LogicaNegocio;
using System.Windows.Forms;
using LogicaNegocio.Accesores;
using Presentacion.Miscelaneas;
using LogicaNegocio.Enumeradores;
using System.Collections.Generic;

namespace Presentacion
{
    public partial class ConsultarCliente : Form
    {
        readonly string nombreMaquinaCliente;
        PantallaEspera pantallaEspera = new PantallaEspera();
        AdministradorTCP tcpClient;

        public ConsultarCliente(string nombreMaquinaCliente)
        {
            InitializeComponent();
            dgvConsultaCliente.ReadOnly = true;
            this.nombreMaquinaCliente = nombreMaquinaCliente;
            InicializarDataGridView();

        }

        void InicializarDataGridView()
        {
            dgvConsultaCliente.ReadOnly = true;
            dgvConsultaCliente.AutoGenerateColumns = false;

            dgvConsultaCliente.Columns.Add("IdCedula", "Cedula");
            dgvConsultaCliente.Columns.Add("Nombre", "Nombre");
            dgvConsultaCliente.Columns.Add("PApellido", "Primer apellido");
            dgvConsultaCliente.Columns.Add("SApellido", "Segundo apellido");
            dgvConsultaCliente.Columns.Add("FNacimiento", "Fecha nacimiento");
            dgvConsultaCliente.Columns.Add("Genero", "Genero");

            dgvConsultaCliente.Columns["IdCedula"].DataPropertyName = "IdCedula";
            dgvConsultaCliente.Columns["IdCedula"].Width = 80;

            dgvConsultaCliente.Columns["Nombre"].DataPropertyName = "Nombre";
            dgvConsultaCliente.Columns["Nombre"].Width = 120;

            dgvConsultaCliente.Columns["PApellido"].DataPropertyName = "PApellido";
            dgvConsultaCliente.Columns["PApellido"].Width = 120;

            dgvConsultaCliente.Columns["SApellido"].DataPropertyName = "SApellido";
            dgvConsultaCliente.Columns["SApellido"].Width = 120;

            dgvConsultaCliente.Columns["FNacimiento"].DataPropertyName = "FNacimiento";
            dgvConsultaCliente.Columns["FNacimiento"].Width = 120;

            dgvConsultaCliente.Columns["Genero"].DataPropertyName = "Genero";
            dgvConsultaCliente.Columns["Genero"].Width = 80;

        }



        private void btnRegersar_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }        

        private void dgvConsultaCliente_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dgvConsultaCliente.Columns[e.ColumnIndex];

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


        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valorRecibido = e.MessageString.TrimEnd('\u0013');
            Paquete<List<Cliente>> informacionCliente = AdmistradorPaquetes.DeserializePackage(valorRecibido);

            if (informacionCliente != null)
            {
                List<Cliente> listaCliente = (List<Cliente>)informacionCliente.ListaInstaciasGenericas[0];
                CargarDatos(listaCliente);
            }
            else
            {
                MessageBox.Show("El plato no existe");
            }
        }

        private void SolicitarDatosAlServidor()
        {
            try
            {
                if (tcpClient.ConectarTCP())
                {
                    pantallaEspera.Show();
                    Cliente ingresoCliente = new Cliente("", "", "", "", new DateTime(), 'm');
                    var paquete = new Paquete<Cliente>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Listar,
                        ListaInstaciasGenericas = new System.Collections.ArrayList() { ingresoCliente }
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
        private void CargarDatos(List<Cliente> lista)
        {
            dgvConsultaCliente.Invoke((MethodInvoker)delegate ()
            {
                dgvConsultaCliente.DataSource = lista;
                dgvConsultaCliente.Refresh();
                pantallaEspera.Hide();
            });
        }

        private void ConsultarCliente_Load(object sender, EventArgs e)
        {
            tcpClient = new AdministradorTCP();
            tcpClient.TcpClient.DataReceived += Client_DataReceived;
            SolicitarDatosAlServidor();
        }
    }
}