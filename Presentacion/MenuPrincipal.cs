using System;
using Entidades;
using SimpleTCP;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Build.Framework.XamlTypes;

namespace Presentacion
{
    public partial class MenuPrincipal : Form
    {
       public SimpleTcpClient tcpClient;
        readonly string nombreMaquinaCliente;

        public MenuPrincipal(Cliente cliente , string nombreMaquinaCliente)
        {                   
            InitializeComponent();
            this.nombreMaquinaCliente = nombreMaquinaCliente;
        }

        public MenuPrincipal()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            MenuRestaurante menurestaurante = new MenuRestaurante();
            menurestaurante.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new MenuPlatoRestaurante().Show();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }
       
        private void button2_Click(object sender, EventArgs e)
        {

            new MenuCategoríaPlato().Show();
            this.Hide();
        }

       
        private void button3_Click(object sender, EventArgs e)
        {            
                new MenuPlatos().Show();
                this.Hide();                      
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new MenuClientes().Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            new ConsultaPlatoRestaurante().Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            new ConsultarCliente().Show(); 
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new MenuExtra().Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new ConsultarRestaurante().Show();
            this.Hide();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            new ConsultarCategoriaPlato(nombreMaquinaCliente).Show(); 
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new ConsultaPlato().Show(); 
            this.Hide();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            new ConsultarExtra().Show();
            this.Hide();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {         
            new Login().Show();
            this.Hide();
        }

        
    }
}
