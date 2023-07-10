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
        List<Plato> platosSeleccionados = new List<Plato>();

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

            dgvAsociacionesRestaurantes.Columns.Add("RestauranteAsignado", "Id Restaurante");
            dgvAsociacionesRestaurantes.Columns.Add("NombreRestaurante", "Nombre del Restaurante");
            dgvAsociacionesRestaurantes.Columns.Add("Direccion", "Direccion");

            /////////////////////////////////////////////////////////////////////////////////////////////

            dgvAsociacionesPlatos.Columns.Add("IdAsignacion", "Id Plato");
            dgvAsociacionesPlatos.Columns.Add("NombrePlato", "Nombre Plato");
            dgvAsociacionesPlatos.Columns.Add("Precio", "Precio");


            ////////////////////////////////////////////////////////////////////////////////////////////
            dgvAsociacionesRestaurantes.Columns["RestauranteAsignado"].DataPropertyName = "RestauranteAsignado";
            dgvAsociacionesRestaurantes.Columns["RestauranteAsignado"].Width = 80;

            dgvAsociacionesRestaurantes.Columns["NombreRestaurante"].DataPropertyName = "NombreRestaurante";
            dgvAsociacionesRestaurantes.Columns["NombreRestaurante"].Width = 180;

            dgvAsociacionesRestaurantes.Columns["Direccion"].DataPropertyName = "Direccion";
            dgvAsociacionesRestaurantes.Columns["Direccion"].Width = 180;

            ///////////////////////////////////////////////////////////////////////////////////////////
            dgvAsociacionesPlatos.Columns["IdAsignacion"].DataPropertyName = "IdAsignacion";
            dgvAsociacionesPlatos.Columns["IdAsignacion"].Width = 50;

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
            /* dgvAsociacionesRestaurantes.DataSource = restaurante.ListarRestaurantesActivos();*/
            
            dgvAsociacionesRestaurantes.DataSource = cmbRestaurantesDisponibles.SelectedItem;
            dgvAsociacionesRestaurantes.Refresh();


            dgvAsociacionesPlatos.DataSource = platosSeleccionados;

            /*dgvAsociacionesPlatos.DataSource = plato.ListarPlato()*/;
            dgvAsociacionesRestaurantes.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

               
                DateTime fechaAfiliacion = dtpFechaAfiliacion.Value;
                int restauranteAsignado=(int)cmbRestaurantesDisponibles.SelectedValue;
               

                DateTime fechaActual = DateTime.Now;
                if (fechaAfiliacion > fechaActual)
                {
                    // El usuario ha seleccionado una fecha posterior a la fecha actual.
                    // Realiza acciones de validación o muestra un mensaje de error al usuario.
                    MessageBox.Show("La fecha seleccionada no tiene un orden cronologico...");
                }
                else
                {
                    if (cmbRestaurantesDisponibles.SelectedIndex == -1)
                    {

                        MessageBox.Show("No deje campos vacios por favor...");


                    }
                    else
                    {

                        PlatoRestauranteLN platoRestauranteLN = new PlatoRestauranteLN();
                        PlatoRestaurante platoRestaurante = new PlatoRestaurante(1, 1,fechaAfiliacion);
                        dgvAsociacionesRestaurantes.Refresh();
                        dgvAsociacionesPlatos.Refresh();
                    }


                }
                
                cmbRestaurantesDisponibles.SelectedIndex = -1;
                dtpFechaAfiliacion.ResetText();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\tHa sucedido un error y no podido registrar el restaurante\n");
            }















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
            this.Hide();
            using (ListaPlatos platosDialog = new ListaPlatos())
            {
                DialogResult result = platosDialog.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    var listaIdsPlatosSeleccionados = platosDialog.idPlatosSeleccionados;
                    //Consultar Logica de negocios -> AccesoDatos mi lista de Ids Seleccionadas contra la lista existente, que retorne la lista de platos 
                    platosSeleccionados = platoLn.ListarPlatosSeleccionados(listaIdsPlatosSeleccionados);
                }
            }
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

        private void dgvAsociacionesRestaurantes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
