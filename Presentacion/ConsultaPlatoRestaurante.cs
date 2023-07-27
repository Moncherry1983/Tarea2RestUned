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

namespace Presentacion
{
    public partial class ConsultaPlatoRestaurante : Form
    {

        RestauranteLN restauranteLn = new RestauranteLN();
        RestauranteLN restaurante;
        PlatoLN platoLn = new PlatoLN();
        PlatoRestauranteLN platoRestauranteLN = new PlatoRestauranteLN();
        List<Plato> platosSeleccionados = new List<Plato>();

        public ConsultaPlatoRestaurante()
        {
            InitializeComponent();
            dgvConsultaRestaurante.ReadOnly = true;
            dgvConsultaPlatos.ReadOnly = true;
            restaurante = new RestauranteLN();
            InitializeDataGridView();
            CargarDatos();
        }


        void InitializeDataGridView()
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

            CargarDatos();
        }



        void CargarDatos()
        {

            dgvConsultaRestaurante.DataSource = platoRestauranteLN.ListarPlatoRestaurantes();
            dgvConsultaRestaurante.Refresh();

            dgvConsultaPlatos.DataSource = platosSeleccionados;
            dgvConsultaRestaurante.Refresh();
        }


     

        private Restaurante[] ObtenerInformacionRestaurantesDisponibles()
        {
            return restaurante.ListarRestaurantesActivos();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }


 

        private void ActualizarListaPlatos(int idRestaurante)
        {
            PlatoRestaurante platosRes = platoRestauranteLN.ObtenerPlatosRestaurante(idRestaurante);
            dgvConsultaPlatos.DataSource = platosRes != null ? new List<Plato>() { platosRes.Platos }.ToList() : new List<Plato>();
            dgvConsultaPlatos.Refresh();
            dgvConsultaPlatos.Refresh();
            
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
                if (col.Name == "IdRestaurante")
                {
                    if (e.Value != null)
                        e.Value = ObtenerInformacionRestaurantesDisponibles()
                            .Where(cp => cp.IdRestaurante == (int)e.Value).FirstOrDefault().IdRestaurante;
                }
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
    }
}
