using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Miscelaneas
{
    public partial class PantallaEspera : Form
    {
        public PantallaEspera()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.Text = "Procesando la Información. Espere, por favor...";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void PantallaEspera_Load(object sender, EventArgs e)
        {
            
        }
    }
}
