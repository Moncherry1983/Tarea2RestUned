using Entidades;
using LogicaNegocio;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MenuCategoríaPlato : Form
    {
        public MenuCategoríaPlato()
        {
            InitializeComponent();
            dgvCategoriaPlato.ReadOnly = true;
        }

        private void aceptar_Click(object sender, System.EventArgs e)
        {






            CategoriaPlatoLN categoriaLN = new CategoriaPlatoLN();
            CategoriaPlato categoriaPlato = new CategoriaPlato(int.Parse(txtidCategoria.Text), txtdescripcion.Text, cmbEstado.SelectedIndex == 0);
            categoriaLN.AgregarCategoriaPlato(categoriaPlato);
            dgvCategoriaPlato.DataSource = categoriaLN.ListarCategoriaPlato();
            dgvCategoriaPlato.Refresh();





        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }
    }
}
