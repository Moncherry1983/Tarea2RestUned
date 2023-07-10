using System;
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
    public partial class ConsultarCategoriaPlato : Form
    {
        CategoriaPlatoLN categoria;
        public ConsultarCategoriaPlato()
        {
            InitializeComponent();
            dvgConsultaCategoriaPlato.ReadOnly = true;
            categoria = new CategoriaPlatoLN();
            InitializeDataGridView();
            CargarDatos();
        }


        
     
        void InitializeDataGridView()
        {
            dvgConsultaCategoriaPlato.ReadOnly = true;
            dvgConsultaCategoriaPlato.AutoGenerateColumns = false;

            dvgConsultaCategoriaPlato.Columns.Add("IdCategoria", "idCategoria");
            dvgConsultaCategoriaPlato.Columns.Add("Descripcion", "descripcion");
            dvgConsultaCategoriaPlato.Columns.Add("Estado", "estado");


            dvgConsultaCategoriaPlato.Columns["IdCategoria"].DataPropertyName = "IdCategoria";
            dvgConsultaCategoriaPlato.Columns["IdCategoria"].Width = 90;

            dvgConsultaCategoriaPlato.Columns["Descripcion"].DataPropertyName = "Descripcion";
            dvgConsultaCategoriaPlato.Columns["Descripcion"].Width = 120;

            dvgConsultaCategoriaPlato.Columns["Estado"].DataPropertyName = "Estado";
            dvgConsultaCategoriaPlato.Columns["Estado"].Width = 120;


            CargarDatos();


        }

        private void CargarDatos()
        {
            dvgConsultaCategoriaPlato.DataSource = categoria.ListarCategoriaPlato();
            dvgConsultaCategoriaPlato.Refresh();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dvgConsultaCategoriaPlato.Columns[e.ColumnIndex];

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
