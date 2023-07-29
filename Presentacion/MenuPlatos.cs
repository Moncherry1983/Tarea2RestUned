using Entidades;
using LogicaNegocio;
using LogicaNegocio.Accesores;
using LogicaNegocio.Enumeradores;
using Presentacion.Miscelaneas;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MenuPlatos : Form
    {
        readonly string nombreMaquinaCliente;
        PantallaEspera pantallaEspera = new PantallaEspera();
        AdministradorTCP tcpClient;
        List<CategoriaPlato> listaCategoriaPlatos= new List<CategoriaPlato>();

        public MenuPlatos(string nombreMaquinaCliente)
        {
            InitializeComponent();
            this.nombreMaquinaCliente = nombreMaquinaCliente;
            dgvPlato.ReadOnly = true;
            InicializarDataGridView();
            InicializarComboBox();
        }

        void InicializarDataGridView()
        {
            dgvPlato.ReadOnly = true;
            dgvPlato.AutoGenerateColumns = false;

            dgvPlato.Columns.Add("IdPlato", "IdPlato");
            dgvPlato.Columns.Add("IdCategoria", "Categoria Plato");
            dgvPlato.Columns.Add("NombrePlato", "Nombre Plato");
            dgvPlato.Columns.Add("Precio", "Precio");

            dgvPlato.Columns["IdPlato"].DataPropertyName = "IdPlato";
            dgvPlato.Columns["IdPlato"].Width = 50;

            dgvPlato.Columns["IdCategoria"].DataPropertyName = "IdCategoria";
            dgvPlato.Columns["IdCategoria"].Width = 120;

            dgvPlato.Columns["NombrePlato"].DataPropertyName = "NombrePlato";
            dgvPlato.Columns["NombrePlato"].Width = 120;

            dgvPlato.Columns["Precio"].DataPropertyName = "Precio";
            dgvPlato.Columns["Precio"].Width = 60;

        }
        
        private void InicializarComboBox()
        {
            //llamar metodo listar Categorias desde la logica de negocio

            //cargar combox
            cmbPlatos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlatos.DisplayMember = "Descripcion";
            cmbPlatos.ValueMember = "IdCategoria";

            //cmbPlatos.DataSource = ObtenerCategoriasDisponibles();
        }       
        
        private void MenuPlatos_Load(object sender, EventArgs e)
        {
            tcpClient = new AdministradorTCP();
            tcpClient.TcpClient.DataReceived += Client_DataReceived;
            SolicitarDatosAlServidor();
        }
        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idPlato = int.Parse(txtIdPlato.Text);
                string nombrePlato = txtNombrePlato.Text;
                int precio = int.Parse(txtPrecioPlato.Text);

                if (String.IsNullOrEmpty(txtIdPlato.Text) || String.IsNullOrEmpty(txtNombrePlato.Text) || String.IsNullOrEmpty(txtPrecioPlato.Text))
                {
                    MessageBox.Show("No se permite dejar campos vacios ...");
                }
                else if (cmbPlatos.SelectedIndex == -1)
                {
                    MessageBox.Show("No se permite dejar campos vacios al escojer la categoria...");
                }
                else
                {
                    int idCategoria = (int)cmbPlatos.SelectedValue;
                    PlatoLN platoLn = new PlatoLN();
                    CategoriaPlato categoriaPlato = new CategoriaPlato((int)cmbPlatos.SelectedValue, cmbPlatos.DisplayMember, true);
                    Plato plato = new Plato(int.Parse(txtIdPlato.Text), txtNombrePlato.Text, int.Parse(txtPrecioPlato.Text), categoriaPlato);
                    GuardarCambios(plato);
                    SolicitarDatosAlServidor();
                }

                txtIdPlato.Text = " ";
                txtNombrePlato.Text = " ";
                cmbPlatos.SelectedValue = -1;
                txtPrecioPlato.Text = " ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "\n\tHa sucedido un error y no se registrar el plato\n", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        private void txtIdPlato_TextChanged(object sender, EventArgs e)
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

        private void txtPrecioPlato_TextChanged(object sender, EventArgs e)
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

        private void txtIdValidar_TextChanged(object sender, EventArgs e)
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

        private void cmbPlatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlatos.SelectedIndex != -1)
            {
                // El usuario ha seleccionado un elemento del combobox
            }
            else
            {
                // El usuario no ha seleccionado ningún elemento del combobox
            }
        }

        private void dgvPlato_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dgvPlato.Columns[e.ColumnIndex];

            try
            {
                
                if (col != null && col.Name == "IdCategoria" && e.Value != null)
                {
                    var categoria = listaCategoriaPlatos // ObtenerCategoriasDisponibles()
                        .FirstOrDefault(cp => cp.IdCategoria == (int)e.Value);

                    if (categoria != null)
                        e.Value = categoria.Descripcion;
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Se ha producido un error al dar formato a la celda Categoría","Error al cargar datos", MessageBoxButtons.OKCancel, MessageBoxIcon.Error );
            }
        }

        private void GuardarCambios(Plato Plato)
        {
            try
            {
                if (tcpClient.ConectarTCP())
                {
                    var paquete = new Paquete<Plato>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Agregar,
                        ListaInstaciasGenericas = new ArrayList() { Plato }
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
                    Plato plato = new Plato(0, "", 0,categoriaPlato);
                    var paquete = new Paquete<Plato>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Listar,
                        ListaInstaciasGenericas = new ArrayList() { categoriaPlato, plato }
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
                        listaCategoriaPlatos = informacionCategoriaPlatos.ListaInstaciasGenericas[0];
                        List<Plato> listaPlatos = informacionCategoriaPlatos.ListaInstaciasGenericas[1];
                        CargarDatos(listaCategoriaPlatos, listaPlatos);
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
            txtIdPlato.Invoke((MethodInvoker)delegate ()
            {
                SolicitarDatosAlServidor();
                txtIdPlato.Focus();
                cmbPlatos.SelectedIndex = 0;
            });
        }

        private void CargarDatos(List<CategoriaPlato> listaCategoriaPlatos, List<Plato> listaPlatos)
        {
            cmbPlatos.Invoke((MethodInvoker)delegate ()
            {

                if(listaCategoriaPlatos.Count > 0)
                {
                    cmbPlatos.DataSource = listaCategoriaPlatos;                    
                    cmbPlatos.SelectedIndex = 0;                    
                    cmbPlatos.Refresh();
                }
            });

            dgvPlato.Invoke((MethodInvoker)delegate ()
            {
                dgvPlato.DataSource = listaPlatos;                
                dgvPlato.Refresh();

                pantallaEspera.Hide();
            });
        }

    }
}