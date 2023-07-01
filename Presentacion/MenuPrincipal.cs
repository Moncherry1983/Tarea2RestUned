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
            


        }

        private void button5_Click(object sender, EventArgs e)
        {
           

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
           new MenuCategoríaPlato().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {       
            new MenuPlatos().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {           
            new MenuClientes().Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
    }
}
