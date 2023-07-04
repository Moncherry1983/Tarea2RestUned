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
        public MenuRestaurante()
        {
            InitializeComponent();
            dgvRestaurantes.ReadOnly = true;
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

                int idRestaurante = int.Parse(txtidRestaurante.Text);
                string nombreRestaurante = txtNombre.Text;
                String direccion = txtDireccion.Text;
                bool estado = cmbEstado.SelectedIndex == 0;
                string telefono = txtTelefono.Text;



                if (String.IsNullOrEmpty(txtidRestaurante.Text) || String.IsNullOrEmpty(txtNombre.Text) || String.IsNullOrEmpty(txtTelefono.Text) || String.IsNullOrEmpty(txtDireccion.Text))
                {

                    MessageBox.Show("No deje campos vacios por favor...");

                }
                else if (cmbEstado.SelectedIndex == -1)
                {

                    MessageBox.Show("No deje campos vacios por favor...");


                }
                else
                {
                    
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

    }
}


