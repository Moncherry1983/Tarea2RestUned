﻿using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Presentacion
{
    public partial class ListaPlatos : Form
    {
        public ListaPlatos()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MenuPlatoRestaurante().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {





        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
