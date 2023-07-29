using Entidades;
using LogicaNegocio.Accesores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class ListaPlatos : Form
    {
        //inicializacion de arrays a utilizar
        private PlatoLN platoLn = new PlatoLN();
        string nombreMaquinaCliente;

        PlatoLN plato;
        private readonly Plato[] platos = new Plato[10];
        public List<int> idPlatosSeleccionados = new List<int>();

        public ListaPlatos(string nombreMaquinaCliente)
        {
            //inicializacion de componentes
            InitializeComponent();
            dgvPlatosDisponibles.ReadOnly = true;
            plato = new PlatoLN();
            InicializarDataGridView();
            CargarDatos();
        }

        //Este código corresponde a una clase llamada ListaPlatos que hereda de la clase Form y sirve para mostrar una lista de platos disponible
        private void button2_Click(object sender, EventArgs e)
        {
            new MenuPlatoRestaurante(this.nombreMaquinaCliente).Show();
            this.Hide();
        }

        //Este código corresponde a una clase llamada ListaPlatos que hereda de la clase Form y sirve para mostrar una lista de platos disponible
        void InicializarDataGridView()
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

        //Carga los datos de los platos al data grid view
        void CargarDatos()
        {
            dgvPlatosDisponibles.DataSource = plato.ListarPlato();
            dgvPlatosDisponibles.Refresh();
        }

        //Este método se ejecuta cuando se hace clic en el botón 1.
        //Obtiene las filas seleccionadas de la tabla de platos disponibles y las guarda en un arreglo.
        //Luego verifica si hay al menos una fila seleccionada y si tiene un valor en la primera celda,
        //que es el id del plato. Agrega los ids de los platos seleccionados a una lista. Después pregunta
        //al usuario si quiere seguir agregando más platos o terminar. Si el usuario dice que no, cierra la ventana.
        //Si no hay ninguna fila seleccionada, muestra un mensaje de error.
        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewRow[] selectedRows = dgvPlatosDisponibles.SelectedRows
            .OfType<DataGridViewRow>()
            .Where(row => !row.IsNewRow)
            .ToArray();
            // se crea este if para evitar que si no se seleccionada no se caiga el programa
            if (selectedRows.Length > 0)
            {
                foreach (var row in selectedRows)
                {
                    if (row.Cells[0].Value != null) // Verificar si la celda no es nula
                    {
                        idPlatosSeleccionados.Add(int.Parse(row.Cells[0].Value.ToString()));
                    }
                }

                string msg = "¿Desea seguir agregando más platos?";
                var pregunta = MessageBox.Show(msg, "Guardar Platos", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (pregunta == DialogResult.No)
                {
                    this.Hide();
                }
            }
            else
            {
                // No se han seleccionado filas, mostrar un mensaje de error o realizar alguna acción adicional
                MessageBox.Show("No se ha seleccionado ningún plato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}