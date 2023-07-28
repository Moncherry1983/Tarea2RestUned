using System;
using Entidades;
using System.Data;
using System.Linq;
using System.Text;
using LogicaNegocio;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Presentacion.Miscelaneas;

namespace Presentacion
{
    public partial class ConsultarRestaurante : Form
    {

        readonly string nombreMaquinaCliente;
        PantallaEspera pantallaEspera = new PantallaEspera();
        AdministradorTCP tcpClient;
        public ConsultarRestaurante(string nombreMaquinaCliente)
        {
            InitializeComponent();
            this.nombreMaquinaCliente = nombreMaquinaCliente;
            InitializeDataGridView();
            dgvConsultaRestaurante.ReadOnly = true;
        }

        void InitializeDataGridView()
        {
            //se bloquea para no se pueda manipular permite columnas personalizadas
            dgvConsultaRestaurante.ReadOnly = true;
            dgvConsultaRestaurante.AutoGenerateColumns = false;

            dgvConsultaRestaurante.Columns.Add("IdRestaurante", "IdRestaurante");
            dgvConsultaRestaurante.Columns.Add("NombreRestaurante", "NombreRestaurante");
            dgvConsultaRestaurante.Columns.Add("Direccion", "Direccion");
            dgvConsultaRestaurante.Columns.Add("Estado", "Estado");
            dgvConsultaRestaurante.Columns.Add("Telefono", "Telefono");

            dgvConsultaRestaurante.Columns["IdRestaurante"].DataPropertyName = "IdRestaurante";
            dgvConsultaRestaurante.Columns["IdRestaurante"].Width = 80;

            dgvConsultaRestaurante.Columns["NombreRestaurante"].DataPropertyName = "NombreRestaurante";
            dgvConsultaRestaurante.Columns["NombreRestaurante"].Width = 150;

            dgvConsultaRestaurante.Columns["Direccion"].DataPropertyName = "Direccion";
            dgvConsultaRestaurante.Columns["Direccion"].Width = 180;

            dgvConsultaRestaurante.Columns["Estado"].DataPropertyName = "Estado";
            dgvConsultaRestaurante.Columns["Estado"].Width = 60;

            dgvConsultaRestaurante.Columns["Telefono"].DataPropertyName = "Telefono";
            dgvConsultaRestaurante.Columns["Telefono"].Width = 70;

        }
        private void ConsultarRestaurante_Load(object sender, EventArgs e)
        {
            tcpClient = new AdministradorTCP();
            tcpClient.TcpClient.DataReceived += Client_DataReceived;
            SolicitarDatosAlServidor();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();

        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dgvConsultaRestaurante.Columns[e.ColumnIndex];

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

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valorRecibido = e.MessageString.TrimEnd('\u0013');
            Paquete<List<Restaurante>> informacionRestaurante = AdmistradorPaquetes.DeserializePackage(valorRecibido);

            if (informacionRestaurante != null)
            {
                List<Restaurante> listaRestaurantes = informacionRestaurante.InstaciaGenerica;
                CargarDatos(listaRestaurantes);
            }
            else
            {
                MessageBox.Show("La categoría de plato no existe");
            }
        }

        private void SolicitarDatosAlServidor()
        {
            try
            {
                if (tcpClient.ConectarTCP())
                {
                    pantallaEspera.Show();
                    Restaurante restaurante = new Restaurante(0, "", "", true, "");
                    var paquete = new Paquete<Restaurante>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Listar,
                        InstaciaGenerica = restaurante
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

        private void CargarDatos(List<Restaurante> lista)
        {
            dgvConsultaRestaurante.Invoke((MethodInvoker)delegate ()
            {
                dgvConsultaRestaurante.DataSource = lista;
                dgvConsultaRestaurante.Refresh();
                pantallaEspera.Hide();
            });
        }

    }
}
