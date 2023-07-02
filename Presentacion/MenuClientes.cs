using System;
using Entidades;
using LogicaNegocio;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MenuClientes : Form
    {
        public MenuClientes()
        {
            InitializeComponent();
            dgvCliente.ReadOnly = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteLN clienteLN = new ClienteLN();
                Cliente cliente = new Cliente(txtNombrePersona.Text,txtCedula.Text,txtPrimerApellido.Text,txtSegundoApellido.Text,dtpFNacimiento.Value,char.Parse(cmbGenero.Text));
                clienteLN.AgregarCliente(cliente);
                dgvCliente.DataSource = clienteLN.ListarCliente();
                dgvCliente.Refresh();



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\tHa sucedido un error y no podido registrar el restaurante\n");
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtNombrePersona_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }
    }
}
