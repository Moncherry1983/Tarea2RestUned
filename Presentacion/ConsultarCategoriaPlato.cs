using System;
using Entidades;
using SimpleTCP;
using LogicaNegocio;
using System.Collections;
using System.Windows.Forms;
using Presentacion.Miscelaneas;
using LogicaNegocio.Enumeradores;
using System.Collections.Generic;

namespace Presentacion
{
    public partial class ConsultarCategoriaPlato : Form
    {
        readonly string nombreMaquinaCliente;
        PantallaEspera pantallaEspera = new PantallaEspera();
        AdministradorTCP tcpClient;

        public ConsultarCategoriaPlato(string nombreMaquinaCliente)
        {
            InitializeComponent();
            this.nombreMaquinaCliente= nombreMaquinaCliente;
            dvgConsultaCategoriaPlato.ReadOnly = true;            
            InicializarDataGridView();            
        }

        void InicializarDataGridView()
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
        }
        private void ConsultarCategoriaPlato_Load(object sender, EventArgs e)
        {
            tcpClient = new AdministradorTCP();
            tcpClient.TcpClient.DataReceived += Client_DataReceived;
            SolicitarDatosAlServidor();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }        

        private void dvgConsultaCategoriaPlato_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
            catch (Exception)
            {
                e.Value = "desconocido";
            }
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valorRecibido = e.MessageString.TrimEnd('\u0013');
            Paquete<List<CategoriaPlato>> informacionCategoriaPlatos = AdmistradorPaquetes.DeserializePackage(valorRecibido);

            if (informacionCategoriaPlatos != null)
            {
                List<CategoriaPlato> listaCategoriaPlatos = (List<CategoriaPlato>)informacionCategoriaPlatos.ListaInstaciasGenericas[0];
                CargarDatos(listaCategoriaPlatos);
            }
            else
            {
               MessageBox.Show("La categoría de plato no existe");
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
        private void CargarDatos(List<CategoriaPlato> lista)
        {
            dvgConsultaCategoriaPlato.Invoke((MethodInvoker)delegate () 
            {
                dvgConsultaCategoriaPlato.DataSource = lista;
                dvgConsultaCategoriaPlato.Refresh();
                pantallaEspera.Hide();
            });
        }
    
    }
}