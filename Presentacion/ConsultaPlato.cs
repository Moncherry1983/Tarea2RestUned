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
    public partial class ConsultaPlato : Form
    {
        
        CategoriaPlatoLN categorias = new CategoriaPlatoLN();
        PlatoLN plato;
        public ConsultaPlato()
        {
            InitializeComponent();
            dgvConsultaPlatos.ReadOnly = true;
            plato = new PlatoLN();
            InitializeDataGridView();
            CargarDatos();
        }

        void InitializeDataGridView()
        {
            dgvConsultaPlatos.ReadOnly = true;
            dgvConsultaPlatos.AutoGenerateColumns = false;

            dgvConsultaPlatos.Columns.Add("IdPlato", "IdPlato");
            dgvConsultaPlatos.Columns.Add("IdCategoria", "Categoria Plato");
            dgvConsultaPlatos.Columns.Add("NombrePlato", "Nombre Plato");
            dgvConsultaPlatos.Columns.Add("Precio", "Precio");

            dgvConsultaPlatos.Columns["IdPlato"].DataPropertyName = "IdPlato";
            dgvConsultaPlatos.Columns["IdPlato"].Width = 50;

            dgvConsultaPlatos.Columns["IdCategoria"].DataPropertyName = "IdCategoria";
            dgvConsultaPlatos.Columns["IdCategoria"].Width = 120;

            dgvConsultaPlatos.Columns["NombrePlato"].DataPropertyName = "NombrePlato";
            dgvConsultaPlatos.Columns["NombrePlato"].Width = 120;

            dgvConsultaPlatos.Columns["Precio"].DataPropertyName = "Precio";
            dgvConsultaPlatos.Columns["Precio"].Width = 60;

            CargarDatos();

        }


        private void descripcion_TextChanged(object sender, EventArgs e)
        {

        }
        void CargarDatos()
        {
            dgvConsultaPlatos.DataSource = plato.ListarPlato();
            dgvConsultaPlatos.Refresh();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        //private void button2_Click(object sender, EventArgs e)
        //{

        //    dgvConsultaPlatos.DataSource = plato.ListarPlato();
        //    dgvConsultaPlatos.Refresh();
        //}

        private CategoriaPlato[] ObtenerCategoriasDisponibles()
        {
            return categorias.ListarCategoriaPlatoCombo();
        }

     

        private void dgvConsultaPlatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvConsultaPlatos == null || dgvConsultaPlatos.Columns.Count <= e.ColumnIndex)
                return;

            DataGridViewColumn col = dgvConsultaPlatos.Columns[e.ColumnIndex];

            try
            {
                if (col.Name == "IdCategoria")
                {
                    if (e.Value != null)
                    {
                        int idCategoria;
                        if (int.TryParse(e.Value.ToString(), out idCategoria))
                        {
                            var categoria = ObtenerCategoriasDisponibles()
                                .FirstOrDefault(cp => cp.IdCategoria == idCategoria);

                            if (categoria != null)
                                e.Value = categoria.Descripcion;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}
