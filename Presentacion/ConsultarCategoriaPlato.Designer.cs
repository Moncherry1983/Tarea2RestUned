using System;

namespace Presentacion
{
    partial class ConsultarCategoriaPlato
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.dvgConsultaCategoriaPlato = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dvgConsultaCategoriaPlato)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(627, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "Regresar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dvgConsultaCategoriaPlato
            // 
            this.dvgConsultaCategoriaPlato.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgConsultaCategoriaPlato.Location = new System.Drawing.Point(184, 61);
            this.dvgConsultaCategoriaPlato.Name = "dvgConsultaCategoriaPlato";
            this.dvgConsultaCategoriaPlato.Size = new System.Drawing.Size(358, 357);
            this.dvgConsultaCategoriaPlato.TabIndex = 4;
            this.dvgConsultaCategoriaPlato.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dvgConsultaCategoriaPlato.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(231, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Consulta de Categoria de Platos ingresados";
            // 
            // ConsultarCategoriaPlato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dvgConsultaCategoriaPlato);
            this.Controls.Add(this.label1);
            this.Name = "ConsultarCategoriaPlato";
            this.Text = "ConsultarCategoriaPlato";
            ((System.ComponentModel.ISupportInitialize)(this.dvgConsultaCategoriaPlato)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dvgConsultaCategoriaPlato;
        private System.Windows.Forms.Label label1;
    }
}