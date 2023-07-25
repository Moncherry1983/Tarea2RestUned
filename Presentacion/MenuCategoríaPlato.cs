using System;
using Entidades;
using LogicaNegocio;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MenuCategoríaPlato : Form
    {
        //inicializacion de arrays que se van a utilizar en la ventana
        CategoriaPlatoLN categoria;
        public MenuCategoríaPlato()
        {
            //inicializacion de componentes de la ventana
            InitializeComponent();
            dgvCategoriaPlato.ReadOnly = true;
            categoria = new CategoriaPlatoLN();
            InitializeDataGridView();
            CargarDatos();

        }
        //metodo para inicializar el datagridview
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
        //metodo para cargar los datos en el datagridview
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
                
                
                //validaciones para que el usuario no deje campos vacios
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
                    bool estado = cmbEstado.SelectedIndex == 0;
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
        //Este método se llama cuando se formatea una celda del DataGridView que muestra las categorías de platos.
        //Se obtiene la columna que corresponde a la celda y se verifica si su nombre es "Estado". Si es así, se convierte
        //el valor de la celda a un valor booleano y se muestra "Activo" o "Inactivo" según sea verdadero o falso. Si ocurre
        //algún error en el proceso, se muestra "Desconocido".
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
                e.Value = "Desconocido";
            }
        }
    }
}
