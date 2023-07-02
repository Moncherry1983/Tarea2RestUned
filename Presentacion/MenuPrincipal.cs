using System;
using Entidades;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MenuPrincipal : Form
    {


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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Estas saliendo del programa Rest-Uned adios...");
            Application.Exit();
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
            new ConsultaCliente().Show(); 
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
            new ConsultarCategoriaPlato().Show(); 
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new ConsultaPlato().Show(); 
            this.Hide();
        }
    }
}
