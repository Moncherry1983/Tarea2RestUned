using Entidades;
using LogicaNegocio;
using LogicaNegocio.Enumeradores;
using Presentacion.Miscelaneas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MenuPlatoRestaurante : Form
    {
        readonly string nombreMaquinaCliente;
        readonly PantallaEspera pantallaEspera = new PantallaEspera();
        AdministradorTCP tcpClient;
        List<CategoriaPlato> listaCategoriaPlatos = new List<CategoriaPlato>();
        List<Plato> platosSeleccionados = new List<Plato>();
        List<Restaurante> listaRestaurante = new List<Restaurante>();
        List<Plato> listaPlatos = new List<Plato>();
        List<PlatoRestaurante> listaRelacionPlatosRestaurantes;

        //inicializa los componentes nesesarios para utlizar la ventana
        public MenuPlatoRestaurante(string nombreMaquinaCliente)
        {
            InitializeComponent();
            this.nombreMaquinaCliente = nombreMaquinaCliente;
            dgvAsociacionesRestaurantes.ReadOnly = true;
            dgvAsociacionesPlatos.ReadOnly = true;
            InicializarDataGridView();
            InicializarComboBox();
        }

        // se inicializa el gridview
        void InicializarDataGridView()
        {
            dgvAsociacionesRestaurantes.ReadOnly = true;
            dgvAsociacionesRestaurantes.AutoGenerateColumns = false;
            dgvAsociacionesRestaurantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvAsociacionesPlatos.ReadOnly = true;
            dgvAsociacionesPlatos.AutoGenerateColumns = false;
            dgvAsociacionesPlatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvAsociacionesRestaurantes.Columns.Add("RestauranteAsignado", "Id Restaurante");
            dgvAsociacionesRestaurantes.Columns.Add("NombreRestaurante", "Nombre del Restaurante");
            dgvAsociacionesRestaurantes.Columns.Add("Direccion", "Direccion");
            dgvAsociacionesRestaurantes.Columns.Add("FechaAfiliacion", "fecha de asociacion");
            /////////////////////////////////////////////////////////////////////////////////////////////

            dgvAsociacionesPlatos.Columns.Add("IdPlato", "Id Plato");
            dgvAsociacionesPlatos.Columns.Add("NombrePlato", "Nombre Plato");
            dgvAsociacionesPlatos.Columns.Add("Precio", "Precio");

            ////////////////////////////////////////////////////////////////////////////////////////////
            dgvAsociacionesRestaurantes.Columns["RestauranteAsignado"].DataPropertyName = "GetIdRestaurante";
            dgvAsociacionesRestaurantes.Columns["RestauranteAsignado"].Width = 98;

            dgvAsociacionesRestaurantes.Columns["NombreRestaurante"].DataPropertyName = "GetNombreRestaurante";
            dgvAsociacionesRestaurantes.Columns["NombreRestaurante"].Width = 180;

            dgvAsociacionesRestaurantes.Columns["Direccion"].DataPropertyName = "GetDireccionRestaurante";
            dgvAsociacionesRestaurantes.Columns["Direccion"].Width = 180;

            dgvAsociacionesRestaurantes.Columns["FechaAfiliacion"].DataPropertyName = "FechaAfiliacion";
            dgvAsociacionesRestaurantes.Columns["FechaAfiliacion"].Width = 85;

            ///////////////////////////////////////////////////////////////////////////////////////////
            dgvAsociacionesPlatos.Columns["IdPlato"].DataPropertyName = "IdPlato";
            dgvAsociacionesPlatos.Columns["IdPlato"].Width = 98;

            dgvAsociacionesPlatos.Columns["NombrePlato"].DataPropertyName = "NombrePlato";
            dgvAsociacionesPlatos.Columns["NombrePlato"].Width = 120;

            dgvAsociacionesPlatos.Columns["Precio"].DataPropertyName = "Precio";
            dgvAsociacionesPlatos.Columns["Precio"].Width = 60;
        }

        //Este método obtiene la información de los restaurantes disponibles y la muestra en un combo box.
        //Primero llama a otro método que lista las categorías de los restaurantes desde la lógica de negocio
        private void InicializarComboBox()
        {
            //cargar combox
            cmbRestaurantesDisponibles.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRestaurantesDisponibles.DisplayMember = "NombreRestaurante";
            cmbRestaurantesDisponibles.ValueMember = "IdRestaurante";
        }

        private void MenuPlatoRestaurante_Load(object sender, EventArgs e)
        {
            tcpClient = new AdministradorTCP();
            tcpClient.TcpClient.DataReceived += Client_DataReceived;
            SolicitarDatosAlServidor();
        }

        //Este método carga los datos de las asociaciones entre platos y restaurantes en dos tablas.
        //La primera tabla muestra todos los platos y restaurantes que existen en la base de datos.
        //La segunda tabla muestra solo los platos que el usuario ha seleccionado.El método usa una
        //clase llamada platoRestauranteLN para obtener los datos y los actualiza con el método Refresh.

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        private void btnGuardarAsociacionPlatoRest_Click(object sender, EventArgs e)
        {
            try
            {
                int idAsignacion = (int)DateTime.Now.Ticks;
                DateTime FechaAfiliacion = dtpFechaAfiliacion.Value;

                DateTime fechaActual = DateTime.Now;
                if (FechaAfiliacion > fechaActual)
                {
                    // El usuario ha seleccionado una fecha posterior a la fecha actual.
                    // Realiza acciones de validación o muestra un mensaje de error al usuario.
                    throw new Exception("\"La fecha seleccionada no tiene un orden cronologico...\"");
                }

                if (lbxPlatosSeleccionados.SelectedItem == null)
                {
                    // No se ha seleccionado ningún elemento, muestra un mensaje de error
                    throw new Exception("/tSeleccione un plato...");
                }

                //validacion para seleccionar restaurante y platos
                if (cmbRestaurantesDisponibles.SelectedIndex == -1)
                {
                    throw new Exception("/tSeleccione un Restaurante...");
                }
                if (platosSeleccionados.Count > 10)
                {
                    throw new Exception("/tSeleccione un plato...");
                }
                else
                {
                    //    //Este método toma un restaurante y una lista de platos seleccionados, y crea un objeto de tipo PlatoRestaurante que los asocia.
                    //    //Luego verifica si el restaurante ya tiene platos asociados en la base de datos, y si no, los agrega y actualiza la interfaz gráfica.
                    //    //Si el restaurante ya tiene platos asociados, lanza una excepción.
                    Restaurante infoRestaurante = listaRestaurante.Where(restaurante => restaurante.IdRestaurante == (int)cmbRestaurantesDisponibles.SelectedValue).FirstOrDefault();
                    PlatoRestaurante platoRestaurante = new PlatoRestaurante(idAsignacion, infoRestaurante, platosSeleccionados, dtpFechaAfiliacion.Value);

                    GuardarCambios(platoRestaurante);
                    SolicitarDatosAlServidor();
                }
                //terminar se vuelven a poner los valores por defecto
                cmbRestaurantesDisponibles.SelectedIndex = -1;
                dtpFechaAfiliacion.ResetText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\tHa sucedido un error y no podido registrar el restaurante\n");
            }
        }

        //Este método se ejecuta cuando se hace clic en el botón btnPLatos.Abre una ventana llamada ListaPlatos donde se puede seleccionar una lista de platos.
        //Si se cierra la ventana con el botón Cancelar, se obtiene la lista de los identificadores de los platos seleccionados.Luego, se llama a la lógica de negocios
        //para obtener la lista de platos con esos identificadores. Finalmente, se muestra la lista de platos en el control lbxPlatosSeleccionados.
        private void btnListarPlatos_Click(object sender, EventArgs e)
        {
            using (ListaPlatos platosDialog = new ListaPlatos(nombreMaquinaCliente))
            {
                DialogResult result = platosDialog.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    var listaIdsPlatosSeleccionados = platosDialog.idPlatosSeleccionados;
                    //Consultar Logica de negocios -> AccesoDatos mi lista de Ids Seleccionadas contra la lista existente, que retorne la lista de platos
                    platosSeleccionados = listaPlatos
                                         .Where(plat => plat != null && listaIdsPlatosSeleccionados.Contains(plat.IdPlato))
                                         .ToList();
                }
            }

            lbxPlatosSeleccionados.DisplayMember = "NombrePlato";
            lbxPlatosSeleccionados.ValueMember = "IdPlato";
            lbxPlatosSeleccionados.DataSource = platosSeleccionados;
        }

        //Este método se ejecuta cuando se cambia la selección de una fila en el control DataGridView que muestra las asociaciones
        //entre restaurantes y platos.Obtiene la fila seleccionada y verifica si tiene un valor en la primera celda, que corresponde
        //al identificador del restaurante. Si tiene un valor, llama al método ActualizarListaPlatos con ese valor como parámetro, para
        //mostrar los platos asociados a ese restaurante. Si no tiene un valor, llama al mismo método con -1 como parámetro, para indicar
        //que no hay ningún restaurante seleccionado y que se debe vaciar la lista de platos.
        private void dgvAsociacionesRestaurantes_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dgvAsociacionesRestaurantes.CurrentRow;
            if (selectedRow != null)
            {
                if (selectedRow.Cells[0].Value != null)
                    ActualizarListaPlatos(int.Parse(selectedRow.Cells[0].Value.ToString()));
                else
                    ActualizarListaPlatos(-1);
            }
        }

        //Este método actualiza la lista de platos de un restaurante dado su identificador.Primero,
        //obtiene los platos del restaurante usando la clase platoRestauranteLN.Luego, asigna esos platos al control
        //dgvAsociacionesPlatos, que es una tabla que muestra los datos.Si no hay platos para el restaurante, crea un arreglo vacío
        //de 10 elementos.Finalmente, refresca el control para que se vean los cambios.
        private void ActualizarListaPlatos(int idRestaurante)
        {
            PlatoRestaurante platoRes = listaRelacionPlatosRestaurantes.Where(lrpr => lrpr.GetIdRestaurante == idRestaurante).FirstOrDefault();
            dgvAsociacionesPlatos.DataSource = platoRes != null ? platoRes.ListaPlatosAsociados : new List<Plato>();
            dgvAsociacionesPlatos.Refresh();
        }

        //Este método se encarga de formatear las celdas de una columna de un DataGridView que muestra las asociaciones entre restaurantes y otros datos.
        //El método busca el valor de la celda en una lista de restaurantes disponibles y lo reemplaza por el IdRestaurante correspondiente.Si el valor de la
        //celda es nulo, no hace nada y el ultimo if es para tener un formato tipo fecha.
        private void dgvAsociacionesRestaurantes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dgvAsociacionesRestaurantes.Columns[e.ColumnIndex];
            try
            {
                if (col.Name == "FechaAfiliacion")
                {
                    if (e.Value != null && e.Value != DBNull.Value)
                    {
                        if (DateTime.TryParse(e.Value.ToString(), out DateTime dateValue))
                        {
                            e.Value = dateValue.ToString(" dd/MMMM/yyyy");
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
            }
        }

        private void cmbRestaurantesDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRestaurantesDisponibles.SelectedIndex != -1)
            {
                // El usuario ha seleccionado un elemento del combobox
            }
            else
            {
                // El usuario no ha seleccionado ningún elemento del combobox
            }
        }

        private void GuardarCambios(PlatoRestaurante platoRestaurante)
        {
            try
            {
                if (tcpClient.ConectarTCP())
                {
                    var paquete = new Paquete<PlatoRestaurante>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Agregar,
                        ListaInstaciasGenericas = new ArrayList() { platoRestaurante }
                    };

                    string paqueteSerializado = AdmistradorPaquetes.SerializePackage(paquete);
                    tcpClient.TcpClient.WriteLineAndGetReply(paqueteSerializado, TimeSpan.FromSeconds(3));
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
                    Restaurante restaurante = new Restaurante(0, "", "", true, "");
                    Plato plato = new Plato(0, "", 0, null);
                    PlatoRestaurante platoRestaurante = new PlatoRestaurante(0, null, null, new DateTime());
                    var paquete = new Paquete<PlatoRestaurante>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Listar,
                        ListaInstaciasGenericas = new ArrayList() { platoRestaurante, restaurante, plato }
                    };

                    string paqueteSerializado = AdmistradorPaquetes.SerializePackage(paquete);
                    tcpClient.TcpClient.WriteLineAndGetReply(paqueteSerializado, TimeSpan.FromSeconds(3));
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
            var informacionPaquete = AdmistradorPaquetes.DeserializePackage(valorRecibido);

            if (informacionPaquete != null)
            {
                switch (informacionPaquete.TiposAccion)
                {
                    case TiposAccion.Agregar:
                        ReiniciarPantalla();
                        break;

                    case TiposAccion.Listar:

                        listaRelacionPlatosRestaurantes = informacionPaquete.ListaInstaciasGenericas[0];
                        listaRestaurante = informacionPaquete.ListaInstaciasGenericas[1];
                        listaPlatos = informacionPaquete.ListaInstaciasGenericas[2];
                        CargarDatos(listaPlatos, listaRelacionPlatosRestaurantes);
                        break;

                    case TiposAccion.ObtenerObjetoEspecifico:

                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("La relacion Plato-Restaurante no existe");
            }
        }

        private void ReiniciarPantalla()
        {
            dtpFechaAfiliacion.Invoke((MethodInvoker)delegate ()
            {
                SolicitarDatosAlServidor();
                dtpFechaAfiliacion.Focus();
                cmbRestaurantesDisponibles.SelectedIndex = 0;
            });
        }

        private void CargarDatos(List<Plato> listaPlatos, List<PlatoRestaurante> listaRelacionPlatosRestaurantes)
        {
            cmbRestaurantesDisponibles.Invoke((MethodInvoker)delegate ()
            {
                cmbRestaurantesDisponibles.DataSource = ObtenerInformacionRestaurantesDisponibles();
                cmbRestaurantesDisponibles.Refresh();
            });

            dgvAsociacionesPlatos.Invoke((MethodInvoker)delegate ()
            {
                dgvAsociacionesPlatos.DataSource = listaPlatos;
                dgvAsociacionesPlatos.Refresh();
            });

            dgvAsociacionesRestaurantes.Invoke((MethodInvoker)delegate ()
            {
                dgvAsociacionesRestaurantes.DataSource = listaRelacionPlatosRestaurantes;
                dgvAsociacionesRestaurantes.Refresh();
                pantallaEspera.Hide();
            });
        }

        private List<Restaurante> ObtenerInformacionRestaurantesDisponibles()
        {
            return listaRestaurante.Where(restaurante => restaurante != null && restaurante.Estado == true).ToList();
        }
    }
}