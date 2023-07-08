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
    public partial class MenuExtra : Form
    {
        CategoriaPlatoLN categorias = new CategoriaPlatoLN();
        ExtraLN extras;
        public MenuExtra()
        {          
            InitializeComponent();
            dgvExtra.ReadOnly = true;
            extras = new ExtraLN();
            ObtenerInformacionCategorias();
            InitializeDataGridView();
            CargarDatos();
        }

        void InitializeDataGridView()
        {
            dgvExtra.ReadOnly = true;
            dgvExtra.AutoGenerateColumns = false;

            dgvExtra.Columns.Add("IdExtra", "Id");
            dgvExtra.Columns.Add("Descripcion", "Nombre del plato extra");
            dgvExtra.Columns.Add("Estado", "Estado");
            dgvExtra.Columns.Add("Precio", "Precio");
            dgvExtra.Columns.Add("IdCategoriaExtra", "nombre de categoria extra");

            dgvExtra.Columns["IdExtra"].DataPropertyName = "IdExtra";
            dgvExtra.Columns["IdExtra"].Width = 50;

            dgvExtra.Columns["Descripcion"].DataPropertyName = "Descripcion";
            dgvExtra.Columns["Descripcion"].Width = 120;

            dgvExtra.Columns["Estado"].DataPropertyName = "Estado";
            dgvExtra.Columns["Estado"].Width = 60;

            dgvExtra.Columns["Precio"].DataPropertyName = "Precio";
            dgvExtra.Columns["Precio"].Width = 80;

            dgvExtra.Columns["IdCategoriaExtra"].DataPropertyName = "IdCategoriaExtra";
            dgvExtra.Columns["IdCategoriaExtra"].Width = 120;

            CargarDatos();

        }



        private void ObtenerInformacionCategorias()
        {
            //llamar metodo listar Categorias desde la logica de negocio

            //cargar combox
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.DisplayMember = "Descripcion";
            cmbCategoria.ValueMember = "IdCategoria";
            cmbCategoria.DataSource = ObtenerCategoriasDisponibles();



        }

        private void MenuExtra_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        void CargarDatos()
        {
            dgvExtra.DataSource = extras.ListarExtra();
            dgvExtra.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                int idExtra = int.Parse(txtIdextra.Text);
                string descripcion = txtDescripcion.Text;
                bool estado = cmbEstado.SelectedIndex == 0;
                int idCategoriaExtra = (int)cmbCategoria.SelectedValue;
                int precio = int.Parse(txtPrecio.Text);


                if (String.IsNullOrEmpty(txtIdextra.Text) || String.IsNullOrEmpty(txtDescripcion.Text) || String.IsNullOrEmpty(txtPrecio.Text))
                {

                    MessageBox.Show("No deje campos vacios por favor...");

                }
                else if (cmbEstado.SelectedIndex == -1 && cmbCategoria.SelectedIndex == -1)
                {

                    MessageBox.Show("No deje campos vacios por favor...");


                }
                else
                {

                    ExtraLN extraLN = new ExtraLN();
                    Extra registrarExtra = new Extra(int.Parse(txtIdextra.Text), txtDescripcion.Text, cmbEstado.SelectedIndex == 0, int.Parse(txtPrecio.Text), (int)cmbCategoria.SelectedValue);
                    extraLN.AgregarExtra(registrarExtra);
                    dgvExtra.DataSource = extraLN.ListarExtra();
                    dgvExtra.Refresh();



                }

                txtIdextra.Text = " ";
                txtDescripcion.Text=" ";
                cmbEstado.SelectedIndex = -1;
                cmbCategoria.SelectedValue = " ";
                txtPrecio.Text = " ";


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "\n\tHa sucedido un error y no podido registrar el restaurante\n", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtIdextra_TextChanged(object sender, EventArgs e)
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

        private void txtIdcategoriaPlato_TextChanged(object sender, EventArgs e)
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

        private void txtPrecioExtra_TextChanged(object sender, EventArgs e)
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


        private void dgvExtra_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dgvExtra.Columns[e.ColumnIndex];

            try
            {
                if (col.Name == "IdCategoriaExtra")
                {
                    if (e.Value != null)
                        e.Value = ObtenerCategoriasDisponibles()
                            .Where(cp => cp.IdCategoria == (int)e.Value).FirstOrDefault().Descripcion;
                }
            }
            catch (Exception ex)
            {

            }

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
                e.Value = "DESCONOCIDO";
            }
        }

        private CategoriaPlato[] ObtenerCategoriasDisponibles()
        {
            return categorias.ListarCategoriaPlatoCombo();
        }

    }

}
