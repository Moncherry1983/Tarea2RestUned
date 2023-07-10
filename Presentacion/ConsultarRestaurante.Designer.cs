namespace Presentacion
{
    partial class ConsultarRestaurante
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
            this.dgvConsultaRestaurante = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsultaRestaurante)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvConsultaRestaurante
            // 
            this.dgvConsultaRestaurante.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConsultaRestaurante.Location = new System.Drawing.Point(53, 81);
            this.dgvConsultaRestaurante.Name = "dgvConsultaRestaurante";
            this.dgvConsultaRestaurante.Size = new System.Drawing.Size(580, 333);
            this.dgvConsultaRestaurante.TabIndex = 0;
            this.dgvConsultaRestaurante.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(661, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 60);
            this.button2.TabIndex = 2;
            this.button2.Text = "Regresar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Consulta de Restaurantes ingresados";
            // 
            // ConsultarRestaurante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dgvConsultaRestaurante);
            this.Name = "ConsultarRestaurante";
            this.Text = "ConsultarRestaurante";
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsultaRestaurante)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvConsultaRestaurante;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
    }
}