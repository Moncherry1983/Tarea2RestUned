using System;
using Entidades;
using System.Linq;
using LogicaNegocio;
using System.Windows.Forms;
using Presentacion.Miscelaneas;
using LogicaNegocio.Enumeradores;
using System.Collections.Generic;

namespace Presentacion
{
    public partial class ConsultarExtra : Form
    {
        readonly string nombreMaquinaCliente;
        PantallaEspera pantallaEspera = new PantallaEspera();
        AdministradorTCP tcpClient;
        List<CategoriaPlato> listaCategoriaPlatos = new List<CategoriaPlato>();

        public ConsultarExtra( string nombreMaquinaCliente)
        {
            InitializeComponent();
            this.nombreMaquinaCliente = nombreMaquinaCliente;
            dvgConsultaExtra.ReadOnly = true;
            InicializarDataGridView();
 
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


        }



        private void button1_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }


        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = dvgConsultaExtra.Columns[e.ColumnIndex];

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


        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valorRecibido = e.MessageString.TrimEnd('\u0013');
            Paquete<List<Extra>> informacionExtra = AdmistradorPaquetes.DeserializePackage(valorRecibido);

            if (informacionExtra != null)
            {
                List<Extra> listaExtra = (List<Extra>)informacionExtra.ListaInstaciasGenericas[0];
                listaCategoriaPlatos = (List<CategoriaPlato>)informacionExtra.ListaInstaciasGenericas[1];

                CargarDatos(listaExtra);
            }
            else
            {
                MessageBox.Show("La extra no existe");
            }
        }

        private void SolicitarDatosAlServidor()
        {
            try
            {
                if (tcpClient.ConectarTCP())
                {
                    pantallaEspera.Show();
                    Extra extra = new Extra(0, "",0,true, 0);
                    CategoriaPlato categoriaPlato = new CategoriaPlato(0, "", true);
                    var paquete = new Paquete<Extra>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Listar,
                        ListaInstaciasGenericas = new System.Collections.ArrayList() { extra, categoriaPlato }
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
        private void CargarDatos(List<Extra> lista)
        {
            dvgConsultaExtra.Invoke((MethodInvoker)delegate ()
            {
                dvgConsultaExtra.DataSource = lista;
                dvgConsultaExtra.Refresh();
                pantallaEspera.Hide();
            });
        }

        private void ConsultarExtra_Load(object sender, EventArgs e)
        {
            tcpClient = new AdministradorTCP();
            tcpClient.TcpClient.DataReceived += Client_DataReceived;
            SolicitarDatosAlServidor();
        }
    }
}