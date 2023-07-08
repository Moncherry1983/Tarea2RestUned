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
    public partial class MenuPlatoRestaurante : Form
    {
        
        RestauranteLN restauranteLn = new RestauranteLN();
        RestauranteLN restaurante;

        PlatoLN platoLn = new PlatoLN();
        PlatoLN plato;

        public MenuPlatoRestaurante()
        {
            InitializeComponent();
            dgvAsociacionesRestaurantes.ReadOnly = true;
            dgvAsociacionesPlatos.ReadOnly = true;
            plato = new PlatoLN();
            restaurante = new RestauranteLN();
            ObtenerInformacionRestaurantes();
            InitializeDataGridView();
            CargarDatos();
        }

        void InitializeDataGridView()
        {
            dgvAsociacionesRestaurantes.ReadOnly = true;
            dgvAsociacionesRestaurantes.AutoGenerateColumns = false;

            dgvAsociacionesPlatos.ReadOnly = true;
            dgvAsociacionesPlatos.AutoGenerateColumns = false;

            dgvAsociacionesRestaurantes.Columns.Add("IdRestaurante", "Id Restaurante");
            dgvAsociacionesRestaurantes.Columns.Add("NombreRestaurante", "Nombre del Restaurante");
            dgvAsociacionesRestaurantes.Columns.Add("Direccion", "Direccion");

            /////////////////////////////////////////////////////////////////////////////////////////////

            dgvAsociacionesPlatos.Columns.Add("IdPlato", "Id Plato");
            dgvAsociacionesPlatos.Columns.Add("NombrePlato", "Nombre Plato");
            dgvAsociacionesPlatos.Columns.Add("Precio", "Precio");


            ////////////////////////////////////////////////////////////////////////////////////////////
            dgvAsociacionesRestaurantes.Columns["IdRestaurante"].DataPropertyName = "IdRestaurante";
            dgvAsociacionesRestaurantes.Columns["IdRestaurante"].Width = 80;

            dgvAsociacionesRestaurantes.Columns["NombreRestaurante"].DataPropertyName = "NombreRestaurante";
            dgvAsociacionesRestaurantes.Columns["NombreRestaurante"].Width = 180;

            dgvAsociacionesRestaurantes.Columns["Direccion"].DataPropertyName = "Direccion";
            dgvAsociacionesRestaurantes.Columns["Direccion"].Width = 180;

            ///////////////////////////////////////////////////////////////////////////////////////////
            dgvAsociacionesPlatos.Columns["IdPlato"].DataPropertyName = "IdPlato";
            dgvAsociacionesPlatos.Columns["IdPlato"].Width = 50;

            dgvAsociacionesPlatos.Columns["NombrePlato"].DataPropertyName = "NombrePlato";
            dgvAsociacionesPlatos.Columns["NombrePlato"].Width = 120;

            dgvAsociacionesPlatos.Columns["Precio"].DataPropertyName = "Precio";
            dgvAsociacionesPlatos.Columns["Precio"].Width = 60;

            CargarDatos();
        }

            private void ObtenerInformacionRestaurantes()
        {
            //llamar metodo listar Categorias desde la logica de negocio

            //cargar combox
            cmbRestaurantesDisponibles.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRestaurantesDisponibles.DisplayMember = "NombreRestaurante";
            cmbRestaurantesDisponibles.ValueMember = "IdRestaurante";
            cmbRestaurantesDisponibles.DataSource = ObtenerInformacionRestaurantesDisponibles();
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }


        void CargarDatos()
        {
            dgvAsociacionesRestaurantes.DataSource = restaurante.ListarRestaurantesActivos();
            dgvAsociacionesRestaurantes.Refresh();

            dgvAsociacionesPlatos.DataSource = plato.ListarPlato();
            dgvAsociacionesRestaurantes.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
















        }

        private void txtIdAsociacionRestaurantes_TextChanged(object sender, EventArgs e)
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

        private void txtIdAsociacionPlatos_TextChanged(object sender, EventArgs e)
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

        private void btnPLatos_Click(object sender, EventArgs e)
        {
            new ListaPlatos().Show();
            this.Hide();
        }

        private void cmbRestaurantesDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvAsociacionesCompletadas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dgvAsociacionesRestaurantes.Columns[e.ColumnIndex];

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

        }


        private Restaurante[] ObtenerInformacionRestaurantesDisponibles()
        {
            return restaurante.ListarRestaurantesActivos();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
    }

}
