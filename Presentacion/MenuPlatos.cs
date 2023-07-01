using System;
using Entidades;
using LogicaNegocio;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MenuPlatos : Form
    {
        public MenuPlatos()
        {
            InitializeComponent();
            InitializeComponent();
            dgvPlato.ReadOnly = true;
        }

        private void descripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                PlatoLN platoLN = new PlatoLN();
                RegistrarPlato registrarPlato= new RegistrarPlato(int.Parse(txtIdPlato.Text), txtNombrePlato, int.Parse(txtPrecioPlato.Text), int.Parse(txtIdValidar.Text));
                platoLN.AgregarPlato(registrarPlato);
                dgvPlato.DataSource = platoLN.ListarPlato();
                dgvPlato.Refresh();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\tHa sucedido un error y no podido registrar el restaurante\n");
            }




        }
    }
}
