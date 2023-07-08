using System;
using Entidades;
using System.Linq;
using LogicaNegocio;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MenuPlatos : Form
    {
        CategoriaPlatoLN categorias = new CategoriaPlatoLN();
        PlatoLN plato;
        public MenuPlatos()
        {
            InitializeComponent();
            dgvPlato.ReadOnly = true;
            plato = new PlatoLN();
            ObtenerInformacionCategorias();
            InitializeDataGridView();
            CargarDatos();

        }

        void InitializeDataGridView()
        {
            dgvPlato.ReadOnly = true;
            dgvPlato.AutoGenerateColumns = false;

            dgvPlato.Columns.Add("IdPlato", "IdPlato");
            dgvPlato.Columns.Add("IdCategoria", "Categoria Plato");
            dgvPlato.Columns.Add("NombrePlato", "Nombre Plato");
            dgvPlato.Columns.Add("Precio", "Precio");

            dgvPlato.Columns["IdPlato"].DataPropertyName = "IdPlato";
            dgvPlato.Columns["IdPlato"].Width = 50;

            dgvPlato.Columns["IdCategoria"].DataPropertyName = "IdCategoria";
            dgvPlato.Columns["IdCategoria"].Width = 120;

            dgvPlato.Columns["NombrePlato"].DataPropertyName = "NombrePlato";
            dgvPlato.Columns["NombrePlato"].Width = 120;

            dgvPlato.Columns["Precio"].DataPropertyName = "Precio";
            dgvPlato.Columns["Precio"].Width = 60;

            CargarDatos();

        }

        private void ObtenerInformacionCategorias()
        {
            //llamar metodo listar Categorias desde la logica de negocio

            //cargar combox
            cmbPlatos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlatos.DisplayMember = "Descripcion";
            cmbPlatos.ValueMember = "IdCategoria";

            cmbPlatos.DataSource = ObtenerCategoriasDisponibles();



        }

        private void descripcion_TextChanged(object sender, EventArgs e)
        {

        }
        void CargarDatos()
        {
            dgvPlato.DataSource = plato.ListarPlato();
            dgvPlato.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int idPlato = int.Parse(txtIdPlato.Text);
                string nombrePlato = txtNombrePlato.Text;
                int idCategoria = (int)cmbPlatos.SelectedValue;
                int precio = int.Parse(txtPrecioPlato.Text);

                if (String.IsNullOrEmpty(txtIdPlato.Text) || String.IsNullOrEmpty(txtNombrePlato.Text) || String.IsNullOrEmpty(txtPrecioPlato.Text))
                {

                    MessageBox.Show("No deje campos vacios por favor...");

                }
                else if (cmbPlatos.SelectedIndex == -1)
                {

                    MessageBox.Show("No deje campos vacios por favor...");


                }
                else
                {

                    PlatoLN platoLn = new PlatoLN();
                    Plato registrarPlato = new Plato(int.Parse(txtIdPlato.Text), txtNombrePlato.Text, int.Parse(txtPrecioPlato.Text), (int)cmbPlatos.SelectedValue);
                    platoLn.AgregarPlato(registrarPlato);
                    dgvPlato.DataSource = plato.ListarPlato();
                    dgvPlato.Refresh();

                }

                txtIdPlato.Text = " ";
                txtNombrePlato.Text = " ";
                cmbPlatos.SelectedValue = -1;
                txtPrecioPlato.Text = " ";

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "\n\tHa sucedido un error y no se registrar el plato\n", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        private void txtIdPlato_TextChanged(object sender, EventArgs e)
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

        private void txtPrecioPlato_TextChanged(object sender, EventArgs e)
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

        private void txtIdValidar_TextChanged(object sender, EventArgs e)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlatos.SelectedIndex != -1)
            {
                // El usuario ha seleccionado un elemento del combobox
            }
            else
            {
                // El usuario no ha seleccionado ningún elemento del combobox
            }
        }

        private void dgvPlato_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dgvPlato.Columns[e.ColumnIndex];

            try
            {
                if (col.Name == "IdCategoria")
                {
                    if (e.Value != null)
                        e.Value = ObtenerCategoriasDisponibles()
                            .Where(cp => cp.IdCategoria == (int)e.Value).FirstOrDefault().Descripcion;
                }
            }
            catch (Exception ex)
            {

            }
        }


        private CategoriaPlato[] ObtenerCategoriasDisponibles()
        {
            return categorias.ListarCategoriaPlatoCombo();
        }
    }
}
