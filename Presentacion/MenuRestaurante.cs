using System;
using Entidades;
using LogicaNegocio;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentacion
{
    public partial class MenuRestaurante : Form
    {

        RestauranteLN restaLN = new RestauranteLN();

        public MenuRestaurante()
        {
            InitializeComponent();
            dgvRestaurantes.ReadOnly = true;
            InitializeDataGridView();
            CargarDatos();
        }


        void InitializeDataGridView()
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

            CargarDatos();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void activo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void descripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        //actualiza el datagrid y hace un un refresh de la lista.
        void CargarDatos()
        {
            dgvRestaurantes.DataSource = restaLN.ListarRestaurantes();
            dgvRestaurantes.Refresh();
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

        private void inactivo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
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
                    RestauranteLN restauranteLn = new RestauranteLN();
                    Restaurante registrarRestaurante = new Restaurante(int.Parse(txtidRestaurante.Text), txtNombre.Text, txtDireccion.Text, cmbEstado.SelectedIndex == 0, txtTelefono.Text);
                    restauranteLn.AgregarRestaurante(registrarRestaurante);
                    dgvRestaurantes.DataSource = restauranteLn.ListarRestaurantes();
                    dgvRestaurantes.Refresh();

                    

                }

                txtidRestaurante.Text = " ";
                txtNombre.Text = " ";
                txtDireccion.Text = " ";
                cmbEstado.SelectedIndex = -1;
                txtTelefono.Text = " ";


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "\n\tHa sucedido un error y no podido registrar el restaurante\n",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void MenuRestaurante_Load(object sender, EventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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
    }
}


