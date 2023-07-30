namespace Presentacion
{
    partial class ListaPlatos
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
            this.dgvPlatosDisponibles = new System.Windows.Forms.DataGridView();
            this.btnGuardarSeleccionados = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlatosDisponibles)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPlatosDisponibles
            // 
            this.dgvPlatosDisponibles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlatosDisponibles.Location = new System.Drawing.Point(98, 50);
            this.dgvPlatosDisponibles.Name = "dgvPlatosDisponibles";
            this.dgvPlatosDisponibles.Size = new System.Drawing.Size(278, 279);
            this.dgvPlatosDisponibles.TabIndex = 0;            
            // 
            // button1
            // 
            this.btnGuardarSeleccionados.Location = new System.Drawing.Point(12, 335);
            this.btnGuardarSeleccionados.Name = "button1";
            this.btnGuardarSeleccionados.Size = new System.Drawing.Size(118, 50);
            this.btnGuardarSeleccionados.TabIndex = 1;
            this.btnGuardarSeleccionados.Text = "Guarda Plato";
            this.btnGuardarSeleccionados.UseVisualStyleBackColor = true;
            this.btnGuardarSeleccionados.Click += new System.EventHandler(this.btnGuardarSeleccionados_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(126, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "LISTA DE PLATOS DISPONIBLES";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(149, 354);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(376, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Presione Ctrl + Enter para escojer varios platos de la lista";
            // 
            // ListaPlatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 402);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGuardarSeleccionados);
            this.Controls.Add(this.dgvPlatosDisponibles);
            this.Name = "ListaPlatos";
            this.Text = "ListaPlatos";
            this.Load += new System.EventHandler(this.ListaPlatos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlatosDisponibles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPlatosDisponibles;
        private System.Windows.Forms.Button btnGuardarSeleccionados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}