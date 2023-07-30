using System;
using Entidades;
using System.Linq;
using LogicaNegocio;
using System.Collections;
using System.Windows.Forms;
using LogicaNegocio.Accesores;
using Presentacion.Miscelaneas;
using LogicaNegocio.Enumeradores;
using System.Collections.Generic;

namespace Presentacion
{
    public partial class MenuExtra : Form
    {
        //inicializacion de arrays que se van a utilizar en la ventana
        PantallaEspera pantallaEspera = new PantallaEspera();
        readonly string nombreMaquinaCliente;
        AdministradorTCP tcpClient;
        List<CategoriaPlato> listaCategoriaPlatos = new List<CategoriaPlato>();
        public MenuExtra(string nombreMaquinaCliente)
        {    //inicializa los componentes de la ventana      
            InitializeComponent();
            dgvExtra.ReadOnly = true;
            this.nombreMaquinaCliente = nombreMaquinaCliente;         
            InitializeDataGridView();
            InicializarComboBox();
        }
        //crear los datagrid para que se muestren los datos
        void InitializeDataGridView()
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

        }


        //metodo para obtener las categorias disponibles
        private void InicializarComboBox()
        {
            //llamar metodo listar Categorias desde la logica de negocio

            //cargar combox
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.DisplayMember = "Descripcion";
            cmbCategoria.ValueMember = "IdCategoria";

            //cmbPlatos.DataSource = ObtenerCategoriasDisponibles();
        }

        private void MenuExtra_Load(object sender, EventArgs e)
        {
            tcpClient = new AdministradorTCP();
            tcpClient.TcpClient.DataReceived += Client_DataReceived;
            cmbEstado.SelectedIndex = 0;
            SolicitarDatosAlServidor();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
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
                    Extra registrarExtra = new Extra(int.Parse(txtIdextra.Text), txtDescripcion.Text, int.Parse(txtPrecio.Text), cmbEstado.SelectedIndex ==0,(int)cmbCategoria.SelectedValue);
                    GuardarExtra(registrarExtra);
                    SolicitarDatosAlServidor();

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
                        e.Value = listaCategoriaPlatos
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

        private void GuardarExtra(Extra ingresarExtras)
        {
            try
            {
                if (tcpClient.ConectarTCP())
                {
                    var paquete = new Paquete<Extra>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Agregar,
                        ListaInstaciasGenericas = new ArrayList() { ingresarExtras }
                    };

                    string ExtraSerializada = AdmistradorPaquetes.SerializePackage(paquete);
                    tcpClient.TcpClient.WriteLineAndGetReply(ExtraSerializada, TimeSpan.FromSeconds(3));
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
                    Extra extra = new Extra(0,"",0,true,0);
                    CategoriaPlato categoriaPlato = new CategoriaPlato(0, "", true);
                    var paquete = new Paquete<Extra>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Listar,
                        ListaInstaciasGenericas = new ArrayList() { extra }
                    };

                    string ExtraSerializada = AdmistradorPaquetes.SerializePackage(paquete);
                    tcpClient.TcpClient.WriteLineAndGetReply(ExtraSerializada, TimeSpan.FromSeconds(3));
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
            var informacionExtra = AdmistradorPaquetes.DeserializePackage(valorRecibido);

            if (informacionExtra != null)
            {
                switch (informacionExtra.TiposAccion)
                {
                    case TiposAccion.Agregar:
                        ReiniciarPantalla();
                        break;

                    case TiposAccion.Listar:
                        List<Extra> ingresarExtras = informacionExtra.InstaciaGenerica;
                        CargarDatos(ingresarExtras);
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
            txtIdextra.Invoke((MethodInvoker)delegate ()
            {
                SolicitarDatosAlServidor();
                txtIdextra.Focus();
                cmbEstado.SelectedIndex = 1;
            });
        }

        private void CargarDatos(List<Extra> lista)
        {
            dgvExtra.Invoke((MethodInvoker)delegate ()
            {
                pantallaEspera.Hide();
                dgvExtra.DataSource = lista;
                dgvExtra.Refresh();
            });
        }



    }

}