using Entidades;
using LogicaNegocio;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class ConsultarCategoriaPlato : Form
    {        
        readonly string nombreMaquinaCliente;        
        AdministradorTCP tcpClient;

        public ConsultarCategoriaPlato(string nombreMaquinaCliente)
        {
            InitializeComponent();
            this.nombreMaquinaCliente= nombreMaquinaCliente;
            dvgConsultaCategoriaPlato.ReadOnly = true;            
            InitializeDataGridView();            
        }

        void InitializeDataGridView()
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

        private void button1_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }        

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
                e.Value = "Unknown";
            }
        }

        private void ConsultarCategoriaPlato_Load(object sender, EventArgs e)
        {
            tcpClient = new AdministradorTCP();
            tcpClient.TcpClient.DataReceived += Client_DataReceived;
            SolicitarDatosAlServidor();
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valorRecibido = e.MessageString.TrimEnd('\u0013');
            Paquete<List<CategoriaPlato>> informacionCategoriaPlatos = AdmistradorPaquetes.DeserializePackage(valorRecibido);

            if (informacionCategoriaPlatos != null)
            {
                List<CategoriaPlato> listaCategoriaPlatos = informacionCategoriaPlatos.InstaciaGenerica;
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
                        CategoriaPlato categoriaPlato = new CategoriaPlato(0, "", true);
                        var paquete = new Paquete<CategoriaPlato>()
                        {
                            ClienteId = nombreMaquinaCliente,
                            TiposAccion = TiposAccion.Listar,
                            InstaciaGenerica = categoriaPlato
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
                dvgConsultaCategoriaPlato.DataSource = lista; //categoria.ListarCategoriaPlato();
                dvgConsultaCategoriaPlato.Refresh();                
            });

        }
    
    }
}