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
        CategoriaPlatoLN categoria;
        readonly string nombreMaquinaCliente;
        String direccion = "127.0.0.1";
        String puerto = "14100";
        SimpleTcpClient tcpClient;

        public ConsultarCategoriaPlato(string nombreMaquinaCliente)
        {
            InitializeComponent();
            this.nombreMaquinaCliente= nombreMaquinaCliente;
            dvgConsultaCategoriaPlato.ReadOnly = true;
            categoria = new CategoriaPlatoLN();
            InitializeDataGridView();            
        }



        private void SolicitarDatosAlServidor()
        {
            try
            {
                if (VerificarConexiionTCP())
                {
                    CategoriaPlato categoriaPlato = new CategoriaPlato(0, "", true);
                    var paquete = new Paquete<CategoriaPlato>()
                    {
                        ClienteId = nombreMaquinaCliente,
                        TiposAccion = TiposAccion.Listar,
                        InstaciaGenerica = categoriaPlato
                    };

                    string clienteSerializado = AdmistradorPaquetes.SerializePackage(paquete);
                    tcpClient.WriteLineAndGetReply(clienteSerializado, TimeSpan.FromSeconds(3));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con el servidor: " + ex.Message);
            }
        }

        private bool VerificarConexiionTCP()
        {
            try
            {
                tcpClient.Connect(direccion, int.Parse(puerto));
                return true;
            }
            catch (Exception)
            {

                throw;
            }
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

        private void CargarDatos(List<CategoriaPlato> lista)
        {

            dvgConsultaCategoriaPlato.Invoke((MethodInvoker)delegate () 
            {
                dvgConsultaCategoriaPlato.DataSource = lista; //categoria.ListarCategoriaPlato();
                dvgConsultaCategoriaPlato.Refresh();                
            });

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new MenuPrincipal().Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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
            catch (Exception ex)
            {
                e.Value = "Unknown";
            }
        }

        private void ConsultarCategoriaPlato_Load(object sender, EventArgs e)
        {
            
            tcpClient = new SimpleTcpClient
            {
                StringEncoder = System.Text.Encoding.UTF8,
                Delimiter = 0x13
            };
            tcpClient.DataReceived += Client_DataReceived;

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
    }
}