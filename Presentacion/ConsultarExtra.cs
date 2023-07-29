using Entidades;
using LogicaNegocio.Accesores;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class ConsultarExtra : Form
    {
        CategoriaPlatoLN categorias = new CategoriaPlatoLN();
        ExtraLN extras;

        public ConsultarExtra()
        {
            InitializeComponent();
            dvgConsultaExtra.ReadOnly = true;
            extras = new ExtraLN();
            InicializarDataGridView();
            CargarDatos();
        }

        void InicializarDataGridView()
        {
            dvgConsultaExtra.ReadOnly = true;
            dvgConsultaExtra.AutoGenerateColumns = false;

            dvgConsultaExtra.Columns.Add("IdExtra", "Id");
            dvgConsultaExtra.Columns.Add("Descripcion", "Nombre del plato extra");
            dvgConsultaExtra.Columns.Add("Estado", "Estado");
            dvgConsultaExtra.Columns.Add("Precio", "Precio");
            dvgConsultaExtra.Columns.Add("IdCategoriaExtra", "nombre de categoria extra");

            dvgConsultaExtra.Columns["IdExtra"].DataPropertyName = "IdExtra";
            dvgConsultaExtra.Columns["IdExtra"].Width = 70;

            dvgConsultaExtra.Columns["Descripcion"].DataPropertyName = "Descripcion";
            dvgConsultaExtra.Columns["Descripcion"].Width = 120;

            dvgConsultaExtra.Columns["Estado"].DataPropertyName = "Estado";
            dvgConsultaExtra.Columns["Estado"].Width = 60;

            dvgConsultaExtra.Columns["Precio"].DataPropertyName = "Precio";
            dvgConsultaExtra.Columns["Precio"].Width = 80;

            dvgConsultaExtra.Columns["IdCategoriaExtra"].DataPropertyName = "IdCategoriaExtra";
            dvgConsultaExtra.Columns["IdCategoriaExtra"].Width = 120;

            CargarDatos();
        }

        void CargarDatos()
        {
            dvgConsultaExtra.DataSource = extras.ListarExtra();
            dvgConsultaExtra.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private CategoriaPlato[] ObtenerCategoriasDisponibles()
        {
            return categorias.ListarCategoriaPlatoCombo();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dvgConsultaExtra.Columns[e.ColumnIndex];

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
    }
}