using Entidades;
using LogicaNegocio;
using LogicaNegocio.Enumeradores;
using Presentacion.Miscelaneas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MenuCategoriaPlato : Form
    {        
        readonly string nombreMaquinaCliente;
        PantallaEspera pantallaEspera = new PantallaEspera();
        AdministradorTCP tcpClient;

        public MenuCategoriaPlato(string nombreMaquinaCliente)
        {
            //inicializacion de componentes de la ventana
            InitializeComponent();
            this.nombreMaquinaCliente = nombreMaquinaCliente;
            dgvCategoriaPlato.ReadOnly = true;
            InicializarDataGridView();
        }

        //metodo para inicializar el datagridview
        void InicializarDataGridView()
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
        }

        //metodo para cargar los datos en el datagridview
        private void MenuCategoríaPlato_Load(object sender, EventArgs e)
        {
            cmbEstado.SelectedIndex = 1;
            
            tcpClient = new AdministradorTCP();
            tcpClient.TcpClient.DataReceived += Client_DataReceived;
            SolicitarDatosAlServidor();
        }

        private void btnGuardar_Click(object sender, System.EventArgs e)
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
                    CategoriaPlato categoriaPlato = new CategoriaPlato(int.Parse(txtidCategoria.Text), txtdescripcion.Text, cmbEstado.SelectedItem.ToString() == "Activo");
                    GuardarCambios(categoriaPlato);
                    SolicitarDatosAlServidor();
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

        private void btnRegresar_Click(object sender, System.EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
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
            catch (Exception)
            {
                e.Value = "Desconocido";
            }
        }

        private void GuardarCambios(CategoriaPlato categoriaPlato)
        {
            try
            {
                if (tcpClient.ConectarTCP())
                {
                    var paquete = new Paquete<CategoriaPlato>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Agregar,
                        ListaInstaciasGenericas = new ArrayList() { categoriaPlato }
                    };

                    string CategoriaPlatoSerializada = AdmistradorPaquetes.SerializePackage(paquete);
                    tcpClient.TcpClient.WriteLineAndGetReply(CategoriaPlatoSerializada, TimeSpan.FromSeconds(3));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con el servidor: " + ex.Message);
            }
        }

        private void SolicitarDatosAlServidor()
        {
            try
            {
                if (tcpClient.ConectarTCP())
                {
                    pantallaEspera.Show();
                    CategoriaPlato categoriaPlato = new CategoriaPlato(0, "", true);
                    var paquete = new Paquete<CategoriaPlato>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Listar,
                        ListaInstaciasGenericas = new ArrayList() { categoriaPlato }
                    };

                    string CategoriaPlatoSerializada = AdmistradorPaquetes.SerializePackage(paquete);
                    tcpClient.TcpClient.WriteLineAndGetReply(CategoriaPlatoSerializada, TimeSpan.FromSeconds(3));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con el servidor: " + ex.Message);
            }
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valorRecibido = e.MessageString.TrimEnd('\u0013');
            var informacionCategoriaPlatos = AdmistradorPaquetes.DeserializePackage(valorRecibido);

            if (informacionCategoriaPlatos != null)
            {
                switch (informacionCategoriaPlatos.TiposAccion)
                {
                    case TiposAccion.Agregar:
                        ReiniciarPantalla();
                        break;

                    case TiposAccion.Listar:
                        List<CategoriaPlato> listaCategoriaPlatos = informacionCategoriaPlatos.ListaInstaciasGenericas[0];
                        CargarDatos(listaCategoriaPlatos);
                        break;

                    case TiposAccion.ObtenerObjetoEspecifico:

                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("La categoría de plato no existe");
            }
        }

        private void ReiniciarPantalla()
        {
            txtidCategoria.Invoke((MethodInvoker)delegate ()
            {
                SolicitarDatosAlServidor();
                txtidCategoria.Focus();
                cmbEstado.SelectedIndex = 1;
            });
        }

        private void CargarDatos(List<CategoriaPlato> lista)
        {
            dgvCategoriaPlato.Invoke((MethodInvoker)delegate ()
            {
                pantallaEspera.Hide();
                dgvCategoriaPlato.DataSource = lista;
                dgvCategoriaPlato.Refresh();
            });
        }
    }
}