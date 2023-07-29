using Entidades;
using LogicaNegocio;
using LogicaNegocio.Enumeradores;
using Presentacion.Miscelaneas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class ConsultaPlato : Form
    {
        readonly string nombreMaquinaCliente;
        PantallaEspera pantallaEspera = new PantallaEspera();
        AdministradorTCP tcpClient;

        public ConsultaPlato(string nombreMaquinaCliente)
        {
            InitializeComponent();
            this.nombreMaquinaCliente = nombreMaquinaCliente;
            dgvConsultaPlatos.ReadOnly = true;
            InicializarDataGridView();

        }

        void InicializarDataGridView()
        {
            dgvConsultaPlatos.ReadOnly = true;
            dgvConsultaPlatos.AutoGenerateColumns = false;

            dgvConsultaPlatos.Columns.Add("IdPlato", "IdPlato");
            dgvConsultaPlatos.Columns.Add("IdCategoria", "Categoria Plato");
            dgvConsultaPlatos.Columns.Add("NombrePlato", "Nombre Plato");
            dgvConsultaPlatos.Columns.Add("Precio", "Precio");

            dgvConsultaPlatos.Columns["IdPlato"].DataPropertyName = "IdPlato";
            dgvConsultaPlatos.Columns["IdPlato"].Width = 50;

            dgvConsultaPlatos.Columns["IdCategoria"].DataPropertyName = "IdCategoria";
            dgvConsultaPlatos.Columns["IdCategoria"].Width = 120;

            dgvConsultaPlatos.Columns["NombrePlato"].DataPropertyName = "NombrePlato";
            dgvConsultaPlatos.Columns["NombrePlato"].Width = 120;

            dgvConsultaPlatos.Columns["Precio"].DataPropertyName = "Precio";
            dgvConsultaPlatos.Columns["Precio"].Width = 60;

        }
        private void ConsultaPlato_Load(object sender, EventArgs e)
        {
            tcpClient = new AdministradorTCP();
            tcpClient.TcpClient.DataReceived += Client_DataReceived;
            SolicitarDatosAlServidor();
        }

        private void descripcion_TextChanged(object sender, EventArgs e)
        {
        }


        private void button1_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        //private void Conectar_Click(object sender, EventArgs e)
        //{
        //    dgvConsultaPlatos.DataSource = plato.ListarPlato();
        //    dgvConsultaPlatos.Refresh();
        //}

        private CategoriaPlato[] ObtenerCategoriasDisponibles()
        {
            //return categorias.ListarCategoriaPlatoCombo();
            return null;
        }

        private void dgvConsultaPlatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvConsultaPlatos == null || dgvConsultaPlatos.Columns.Count <= e.ColumnIndex)
                return;

            DataGridViewColumn col = dgvConsultaPlatos.Columns[e.ColumnIndex];

            try
            {
                if (col.Name == "IdCategoria")
                {
                    if (e.Value != null)
                    {
                        int idCategoria;
                        if (int.TryParse(e.Value.ToString(), out idCategoria))
                        {
                            var categoria = ObtenerCategoriasDisponibles()
                                .FirstOrDefault(cp => cp.IdCategoria == idCategoria);

                            if (categoria != null)
                                e.Value = categoria.Descripcion;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valorRecibido = e.MessageString.TrimEnd('\u0013');
            Paquete<List<Plato>> informacionCategoriaPlatos = AdmistradorPaquetes.DeserializePackage(valorRecibido);

            if (informacionCategoriaPlatos != null)
            {
                List<Plato> listaCategoriaPlatos = (List<Plato>)informacionCategoriaPlatos.ListaInstaciasGenericas[0];
                CargarDatos(listaCategoriaPlatos);
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
                    CategoriaPlato categoriaPlato = new CategoriaPlato(0, "", true);
                    Plato plato = new Plato(0, "", 0,categoriaPlato);
                    var paquete = new Paquete<Plato>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Listar,
                        ListaInstaciasGenericas = new System.Collections.ArrayList(){ plato }
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
        private void CargarDatos(List<Plato> lista)
        {
            dgvConsultaPlatos.Invoke((MethodInvoker)delegate ()
            {
                dgvConsultaPlatos.DataSource = lista;
                dgvConsultaPlatos.Refresh();
                pantallaEspera.Hide();
            });
        }

    }
}