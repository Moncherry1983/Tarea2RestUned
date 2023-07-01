using System;
using Entidades;
using System.Linq;
using LogicaNegocio;
using LogicaNegocios;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
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
                RestauranteLN restauranteLn = new RestauranteLN();
                Restaurante registrarRestaurante = new Restaurante(int.Parse(txtidRestaurante.Text), txtNombre.Text, txtDireccion.Text, cmbEstado.SelectedIndex == 0, txtTelefono.Text);
                restauranteLn.AgregarRestaurante(registrarRestaurante);
                dgvRestaurantes.DataSource =restauranteLn.ListarRestaurantes();
                dgvRestaurantes.Refresh();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\tHa sucedido un error y no podido registrar el restaurante\n");
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
            this.Close();

        }
    }
}


