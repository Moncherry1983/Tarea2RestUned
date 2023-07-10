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
using System.Security.Cryptography;

namespace Presentacion
{
    public partial class ListaPlatos : Form
    {
        private PlatoLN platoLn = new PlatoLN();
        PlatoLN plato;
        private readonly Plato[] platos = new Plato[10];
        public List<int> idPlatosSeleccionados = new List<int>();

        public ListaPlatos()
        {
            InitializeComponent();
            dgvPlatosDisponibles.ReadOnly = true;
            plato = new PlatoLN();
            InitializeDataGridView();
            CargarDatos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MenuPlatoRestaurante().Show();
            this.Hide();
        }
        void InitializeDataGridView()
        {
            dgvPlatosDisponibles.ReadOnly = true;
            dgvPlatosDisponibles.AutoGenerateColumns = false;
            dgvPlatosDisponibles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            /////////////////////////////////////////////////////////////////////////////////////////////

            dgvPlatosDisponibles.Columns.Add("IdPlato", "IdPlato");
            dgvPlatosDisponibles.Columns.Add("NombrePlato", "Nombre Plato");
            dgvPlatosDisponibles.Columns.Add("Precio", "Precio");

            ///////////////////////////////////////////////////////////////////////////////////////////
            dgvPlatosDisponibles.Columns["IdPlato"].DataPropertyName = "IdPlato";
            dgvPlatosDisponibles.Columns["IdPlato"].Width = 50;

            dgvPlatosDisponibles.Columns["NombrePlato"].DataPropertyName = "NombrePlato";
            dgvPlatosDisponibles.Columns["NombrePlato"].Width = 120;

            dgvPlatosDisponibles.Columns["Precio"].DataPropertyName = "Precio";
            dgvPlatosDisponibles.Columns["Precio"].Width = 60;


            dgvPlatosDisponibles.Columns["IdPlato"].DataPropertyName = "IdPlato";
            dgvPlatosDisponibles.Columns["IdPlato"].Width = 50;


            CargarDatos();
        }

        void CargarDatos()
        {

            dgvPlatosDisponibles.DataSource = plato.ListarPlato();
            dgvPlatosDisponibles.Refresh();
        }


        //Este código corresponde a una clase llamada ListaPlatos que hereda de la clase Form y sirve para mostrar una lista de platos disponible
        //    y seleccionar los platos del usuario.El código seleccionado hace referencia a la implementación del evento button1_Click, que se dispara
        //    al hacer clic en el botón "Agregar plato" y agrega los platos seleccionados a una lista idPlatosSeleccionados.Luego muestra una ventana
        //    emergente(MessageBox) preguntando si el usuario desea seguir agregando más platos, si el usuario dice que no se muestra una nueva ventana
        //    de la clase MenuPlatoRestaurante y se oculta la ventana actual.
        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewRow[] selectedRows = dgvPlatosDisponibles.SelectedRows
           .OfType<DataGridViewRow>()
           .Where(row => !row.IsNewRow)
           .ToArray();

            foreach (var lista in selectedRows)
                idPlatosSeleccionados.Add(int.Parse(lista.Cells[0].Value.ToString()));

            string msg = "Desea seguir agredando mas platos?";
            var pregunta = MessageBox.Show(msg, "Guardar Platos", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (pregunta == DialogResult.No)
            {
                new MenuPlatoRestaurante().Show();
                this.Hide();
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
