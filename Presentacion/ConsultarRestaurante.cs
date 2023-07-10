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
    public partial class ConsultarRestaurante : Form
    {

        RestauranteLN restauranteLn = new RestauranteLN();
        RestauranteLN restaurante;
        public ConsultarRestaurante()
        {
            InitializeComponent();
            InitializeDataGridView();
            CargarDatos();
            dgvConsultaRestaurante.ReadOnly = true;
            restaurante = new RestauranteLN();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();

        }
        void CargarDatos()
        {
            dgvConsultaRestaurante.DataSource = restauranteLn.ListarRestaurantes();
            dgvConsultaRestaurante.Refresh();
        }
        private void button1_Click(object sender, EventArgs e)
        {  

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


           
            dgvConsultaRestaurante.DataSource = restauranteLn.ListarRestaurantes();
            dgvConsultaRestaurante.Refresh();

            CargarDatos();
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
    }
}
