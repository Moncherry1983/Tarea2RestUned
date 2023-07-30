using System;
using Entidades;
using System.Data;
using System.Linq;
using LogicaNegocio;
using System.Windows.Forms;
using LogicaNegocio.Accesores;
using Presentacion.Miscelaneas;
using LogicaNegocio.Enumeradores;
using System.Collections.Generic;

namespace Presentacion
{
    public partial class ConsultaPlatoRestaurante : Form
    {
        readonly string nombreMaquinaCliente;
        PantallaEspera pantallaEspera = new PantallaEspera();
        AdministradorTCP tcpClient;
      

        public ConsultaPlatoRestaurante(string nombreMaquinaCliente)
        {
            InitializeComponent();
            dgvConsultaRestaurante.ReadOnly = true;
            dgvConsultaPlatos.ReadOnly = true;
            this.nombreMaquinaCliente = nombreMaquinaCliente;
            InicializarDataGridView();
        }

        void InicializarDataGridView()
        {
            dgvConsultaRestaurante.ReadOnly = true;
            dgvConsultaRestaurante.AutoGenerateColumns = false;
            dgvConsultaRestaurante.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvConsultaPlatos.ReadOnly = true;
            dgvConsultaPlatos.AutoGenerateColumns = false;
            dgvConsultaPlatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvConsultaRestaurante.Columns.Add("RestauranteAsignado", "Id Restaurante");
            dgvConsultaRestaurante.Columns.Add("NombreRestaurante", "Nombre del Restaurante");
            dgvConsultaRestaurante.Columns.Add("Direccion", "Direccion");
            dgvConsultaRestaurante.Columns.Add("FechaAfiliacion", "fecha de asociacion");
            /////////////////////////////////////////////////////////////////////////////////////////////

            dgvConsultaPlatos.Columns.Add("IdPlato", "Id Plato");
            dgvConsultaPlatos.Columns.Add("NombrePlato", "Nombre Plato");
            dgvConsultaPlatos.Columns.Add("Precio", "Precio");

            ////////////////////////////////////////////////////////////////////////////////////////////
            dgvConsultaRestaurante.Columns["RestauranteAsignado"].DataPropertyName = "GetIdRestaurante";
            dgvConsultaRestaurante.Columns["RestauranteAsignado"].Width = 98;

            dgvConsultaRestaurante.Columns["NombreRestaurante"].DataPropertyName = "GetNombreRestaurante";
            dgvConsultaRestaurante.Columns["NombreRestaurante"].Width = 180;

            dgvConsultaRestaurante.Columns["Direccion"].DataPropertyName = "GetDireccionRestaurante";
            dgvConsultaRestaurante.Columns["Direccion"].Width = 180;

            dgvConsultaRestaurante.Columns["FechaAfiliacion"].DataPropertyName = "FechaAfiliacion";
            dgvConsultaRestaurante.Columns["FechaAfiliacion"].Width = 85;

            ///////////////////////////////////////////////////////////////////////////////////////////
            dgvConsultaPlatos.Columns["IdPlato"].DataPropertyName = "IdPlato";
            dgvConsultaPlatos.Columns["IdPlato"].Width = 98;

            dgvConsultaPlatos.Columns["NombrePlato"].DataPropertyName = "NombrePlato";
            dgvConsultaPlatos.Columns["NombrePlato"].Width = 120;

            dgvConsultaPlatos.Columns["Precio"].DataPropertyName = "Precio";
            dgvConsultaPlatos.Columns["Precio"].Width = 60;


        }




        private void ActualizarListaPlatos(int idRestaurante)
        {
            //PlatoRestaurante platosRes = platoRestauranteLN.ObtenerPlatosRestaurante(idRestaurante);
            //dgvConsultaPlatos.DataSource = platosRes != null ? new List<Plato>() { platosRes.PlatoAsociado }.ToList() : new List<Plato>();
            //dgvConsultaPlatos.Refresh();
            //dgvConsultaPlatos.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        private void dgvConsultaRestaurante_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dgvConsultaRestaurante.Columns[e.ColumnIndex];
            try
            {
                //if (col.Name == "IdRestaurante")
                //{
                //    if (e.Value != null)
                //        e.Value = ObtenerInformacionRestaurantesDisponibles()
                //            .Where(cp => cp.IdRestaurante == (int)e.Value).FirstOrDefault().IdRestaurante;
                //}
            }
            catch (Exception ex)
            {
            }

            if (col.Name == "FechaAfiliacion")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    if (DateTime.TryParse(e.Value.ToString(), out DateTime dateValue))
                    {
                        e.Value = dateValue.ToString(" dd/MMMM/yyyy");
                        e.FormattingApplied = true;
                    }
                    else
                    {
                        e.Value = "Fecha inválida";
                    }
                }
            }
        }

        private void dgvConsultaRestaurante_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dgvConsultaRestaurante.CurrentRow;

            if (selectedRow != null)
            {
                if (selectedRow.Cells[0].Value != null)
                    ActualizarListaPlatos(int.Parse(selectedRow.Cells[0].Value.ToString()));
                else
                    ActualizarListaPlatos(-1);
            }
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valorRecibido = e.MessageString.TrimEnd('\u0013');
            Paquete<List<PlatoRestaurante>> informacionCategoriaPlatos = AdmistradorPaquetes.DeserializePackage(valorRecibido);

            if (informacionCategoriaPlatos != null)
            {
                List<PlatoRestaurante> listaPlatosRestaurante = (List<PlatoRestaurante>)informacionCategoriaPlatos.ListaInstaciasGenericas[0];
                CargarDatos(listaPlatosRestaurante);
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
                    Restaurante restaurante = new Restaurante(0, "", "", true, "");
                    CategoriaPlato categoriaPlato = new CategoriaPlato(0, "", true);
                    Plato plato = new Plato(0, "", 0, categoriaPlato);
                    PlatoRestaurante platoRestaurante = new PlatoRestaurante(0,restaurante,plato,new DateTime());
                    var paquete = new Paquete<PlatoRestaurante>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Listar,
                        ListaInstaciasGenericas = new System.Collections.ArrayList() { platoRestaurante }
                    };

                    string PlatoRestauranteSerializada = AdmistradorPaquetes.SerializePackage(paquete);
                    tcpClient.TcpClient.WriteLineAndGetReply(PlatoRestauranteSerializada, TimeSpan.FromSeconds(3));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con el servidor: " + ex.Message);
            }
        }
        private void CargarDatos(List<PlatoRestaurante> lista)
        {
            dgvConsultaRestaurante.Invoke((MethodInvoker)delegate ()
            {
                dgvConsultaRestaurante.DataSource = lista;
                dgvConsultaRestaurante.Refresh();
                pantallaEspera.Hide();
            });
        }
        private void CargarDatos1(List<PlatoRestaurante> lista)
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