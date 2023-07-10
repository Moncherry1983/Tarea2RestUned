using System;
using Entidades;
using LogicaNegocio;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MenuCategoríaPlato : Form
    {
        CategoriaPlatoLN categoria;
        public MenuCategoríaPlato()
        {
            InitializeComponent();
            dgvCategoriaPlato.ReadOnly = true;
            categoria = new CategoriaPlatoLN();
            InitializeDataGridView();
            CargarDatos();

        }
        void InitializeDataGridView()
        {
            dgvCategoriaPlato.ReadOnly = true;
            dgvCategoriaPlato.AutoGenerateColumns = false;

            dgvCategoriaPlato.Columns.Add("IdCategoria", "idCategoria");
            dgvCategoriaPlato.Columns.Add("Descripcion", "descripcion");
            dgvCategoriaPlato.Columns.Add("Estado", "estado");


            dgvCategoriaPlato.Columns["IdCategoria"].DataPropertyName = "IdCategoria";
            dgvCategoriaPlato.Columns["IdCategoria"].Width = 70;

            dgvCategoriaPlato.Columns["Descripcion"].DataPropertyName = "Descripcion";
            dgvCategoriaPlato.Columns["Descripcion"].Width = 120;

            dgvCategoriaPlato.Columns["Estado"].DataPropertyName = "Estado";
            dgvCategoriaPlato.Columns["Estado"].Width = 120;


            CargarDatos();


        }

        private void CargarDatos()
        {
            dgvCategoriaPlato.DataSource = categoria.ListarCategoriaPlato();
            dgvCategoriaPlato.Refresh();
        }

        private void aceptar_Click(object sender, System.EventArgs e)
        {
            try
            {
                int idCategoria = int.Parse(txtidCategoria.Text);
                string descripcion = txtdescripcion.Text;
                bool estado = cmbEstado.SelectedIndex == 0;
 
                if (String.IsNullOrEmpty(txtidCategoria.Text) || String.IsNullOrEmpty(txtdescripcion.Text))
                {
                    MessageBox.Show("No deje campos vacios por favor...");

                }
                else if (cmbEstado.SelectedIndex == -1)
                {
                    MessageBox.Show("No deje campos vacios por favor...");

                }
                else
                {
                    CategoriaPlatoLN categoriaLN = new CategoriaPlatoLN();
                    CategoriaPlato categoriaPlato = new CategoriaPlato(int.Parse(txtidCategoria.Text), txtdescripcion.Text, cmbEstado.SelectedIndex == 0);
                    categoriaLN.AgregarCategoriaPlato(categoriaPlato);
                    dgvCategoriaPlato.DataSource = categoriaLN.ListarCategoriaPlato();
                    dgvCategoriaPlato.Refresh();
                }

                txtdescripcion.Text = "";
                txtidCategoria.Text = "";
                cmbEstado.SelectedIndex = -1;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "\n\tHa sucedido un error y no podido registrar el restaurante\n", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void txtidCategoria_TextChanged(object sender, System.EventArgs e)
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

        private void cmbEstado_SelectedIndexChanged(object sender, System.EventArgs e)
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

        private void dgvCategoriaPlato_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dgvCategoriaPlato.Columns[e.ColumnIndex];

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
