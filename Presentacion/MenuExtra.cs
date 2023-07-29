using Entidades;
using LogicaNegocio.Accesores;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MenuExtra : Form
    {
        //inicializa los arrays
        CategoriaPlatoLN categorias = new CategoriaPlatoLN();

        ExtraLN extras;

        public MenuExtra()
        {    //inicializa los componentes de la ventana
            InitializeComponent();
            dgvExtra.ReadOnly = true;
            extras = new ExtraLN();
            ObtenerInformacionCategorias();
            InicializarDataGridView();
            CargarDatos();
        }

        //crear los datagrid para que se muestren los datos
        void InicializarDataGridView()
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

        //metodo para obtener las categorias disponibles
        private void ObtenerInformacionCategorias()
        {
            //llamar metodo listar Categorias desde la logica de negocio

            //carga el combobox con las categorías disponibles que se obtienen de una función.
            //Establece el estilo del combobox como una lista desplegable, el campo que se muestra
            //como la descripción de la categoría y el campo que se usa como el valor de la categoría.
            //Asigna el resultado de la función ObtenerCategoriasDisponibles() como la fuente de datos del combobox.
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
                    bool estado = cmbEstado.SelectedIndex == 0;
                    ExtraLN extraLN = new ExtraLN();
                    Extra registrarExtra = new Extra(int.Parse(txtIdextra.Text), txtDescripcion.Text, int.Parse(txtPrecio.Text), cmbEstado.SelectedIndex == 0, (int)cmbCategoria.SelectedValue);
                    dgvExtra.DataSource = extraLN.ListarExtra();
                    dgvExtra.Refresh();
                }

                txtIdextra.Text = " ";
                txtDescripcion.Text = " ";
                cmbEstado.SelectedIndex = -1;
                cmbCategoria.SelectedValue = " ";
                txtPrecio.Text = " ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "\n\tHa sucedido un error y no podido registrar el extra \n", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //Este método se ejecuta cuando el usuario cambia el texto de un cuadro de texto que representa el identificador de una categoría de plato.
        //El método recibe como parámetro el cuadro de texto y el evento que lo desencadena. El método obtiene el texto del cuadro de texto y lo almacena
        //en una variable llamada texto.Luego, recorre cada carácter del texto y verifica si es un dígito o no.Si encuentra un carácter que no es un dígito,
        //lo elimina del texto y mueve el cursor al final del texto. De esta forma, el método asegura que el texto solo contenga números.
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

        //Este método se llama cuando el usuario cambia el texto del campo de precio extra.
        //Recibe como parámetro el objeto que representa el campo de texto. Luego, asigna el texto del campo a una variable llamada texto.
        //Después, recorre cada carácter del texto y verifica si es un número o no. Si no es un número, lo elimina del texto y mueve el cursor
        //al final del texto. Así, se asegura de que el campo solo contenga números.
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

        //validacion por si el usuario no escoje en el combo box
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

        //Este método se encarga de dar formato a las celdas de una tabla que muestra información sobre categorías extras.
        //Para cada celda, verifica el nombre de la columna y el valor que contiene. Si la columna es "IdCategoriaExtra",
        //busca la descripción de la categoría correspondiente en una lista de categorías disponibles y la muestra en la celda.
        //Si la columna es "Estado", convierte el valor a un texto que indica si la categoría está activa o inactiva. Si ocurre algún error,
        //muestra un mensaje de "DESCONOCIDO" en la celda.
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

        //llama a la array de categorias disponibles y la muestra en el combo box de categorias
        private CategoriaPlato[] ObtenerCategoriasDisponibles()
        {
            return categorias.ListarCategoriaPlatoCombo();
        }
    }
}