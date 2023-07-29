using LogicaNegocio.Accesores;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class ConsultarCliente : Form
    {
        ClienteLN cliente;

        public ConsultarCliente()
        {
            InitializeComponent();
            dgvConsultaCliente.ReadOnly = true;
            cliente = new ClienteLN();
            InicializarDataGridView();
            CargarDatos();
        }

        void InicializarDataGridView()
        {
            dgvConsultaCliente.ReadOnly = true;
            dgvConsultaCliente.AutoGenerateColumns = false;

            dgvConsultaCliente.Columns.Add("IdCedula", "Cedula");
            dgvConsultaCliente.Columns.Add("Nombre", "Nombre");
            dgvConsultaCliente.Columns.Add("PApellido", "Primer apellido");
            dgvConsultaCliente.Columns.Add("SApellido", "Segundo apellido");
            dgvConsultaCliente.Columns.Add("FNacimiento", "Fecha nacimiento");
            dgvConsultaCliente.Columns.Add("Genero", "Genero");

            dgvConsultaCliente.Columns["IdCedula"].DataPropertyName = "IdCedula";
            dgvConsultaCliente.Columns["IdCedula"].Width = 80;

            dgvConsultaCliente.Columns["Nombre"].DataPropertyName = "Nombre";
            dgvConsultaCliente.Columns["Nombre"].Width = 120;

            dgvConsultaCliente.Columns["PApellido"].DataPropertyName = "PApellido";
            dgvConsultaCliente.Columns["PApellido"].Width = 120;

            dgvConsultaCliente.Columns["SApellido"].DataPropertyName = "SApellido";
            dgvConsultaCliente.Columns["SApellido"].Width = 120;

            dgvConsultaCliente.Columns["FNacimiento"].DataPropertyName = "FNacimiento";
            dgvConsultaCliente.Columns["FNacimiento"].Width = 120;

            dgvConsultaCliente.Columns["Genero"].DataPropertyName = "Genero";
            dgvConsultaCliente.Columns["Genero"].Width = 80;

            CargarDatos();
        }

        void CargarDatos()
        {
            dgvConsultaCliente.DataSource = cliente.ListarCliente();
            dgvConsultaCliente.Refresh();
        }

        private void btnRegersar_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }        

        private void dgvConsultaCliente_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dgvConsultaCliente.Columns[e.ColumnIndex];

            try
            {
                if (col.Name == "FNacimiento")
                {
                    if (e.Value != null && e.Value != DBNull.Value)
                    {
                        if (DateTime.TryParse(e.Value.ToString(), out DateTime dateValue))
                        {
                            e.Value = dateValue.ToString(" dd / MMMM / yyyy");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "Fecha inválida";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                e.Value = "Desconocido";
            }
        }
    }
}