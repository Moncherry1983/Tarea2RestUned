namespace Presentacion
{
    partial class MenuPlatoRestaurante
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
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbRestaurantesDisponibles = new System.Windows.Forms.ComboBox();
            this.dgvAsociacionesRestaurantes = new System.Windows.Forms.DataGridView();
            this.dtpFechaAfiliacion = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnPLatos = new System.Windows.Forms.Button();
            this.lbxPlatosSeleccionados = new System.Windows.Forms.ListBox();
            this.dgvAsociacionesPlatos = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbAsignacion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsociacionesRestaurantes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsociacionesPlatos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Restaurantes disponibles";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(217, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Platos disponibles para asignar a restaurante";
            // 
            // cmbRestaurantesDisponibles
            // 
            this.cmbRestaurantesDisponibles.FormattingEnabled = true;
            this.cmbRestaurantesDisponibles.Location = new System.Drawing.Point(3, 80);
            this.cmbRestaurantesDisponibles.Name = "cmbRestaurantesDisponibles";
            this.cmbRestaurantesDisponibles.Size = new System.Drawing.Size(307, 21);
            this.cmbRestaurantesDisponibles.TabIndex = 6;
            this.cmbRestaurantesDisponibles.SelectedIndexChanged += new System.EventHandler(this.cmbRestaurantesDisponibles_SelectedIndexChanged);
            // 
            // dgvAsociacionesRestaurantes
            // 
            this.dgvAsociacionesRestaurantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAsociacionesRestaurantes.Location = new System.Drawing.Point(383, 41);
            this.dgvAsociacionesRestaurantes.Name = "dgvAsociacionesRestaurantes";
            this.dgvAsociacionesRestaurantes.Size = new System.Drawing.Size(605, 296);
            this.dgvAsociacionesRestaurantes.TabIndex = 12;
            this.dgvAsociacionesRestaurantes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAsociacionesRestaurantes_CellFormatting);
            this.dgvAsociacionesRestaurantes.SelectionChanged += new System.EventHandler(this.dgvAsociacionesRestaurantes_SelectionChanged);
            // 
            // dtpFechaAfiliacion
            // 
            this.dtpFechaAfiliacion.Location = new System.Drawing.Point(15, 25);
            this.dtpFechaAfiliacion.Name = "dtpFechaAfiliacion";
            this.dtpFechaAfiliacion.Size = new System.Drawing.Size(163, 20);
            this.dtpFechaAfiliacion.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Fecha de la asociacion plato restaurante";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 388);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 66);
            this.button1.TabIndex = 15;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(182, 388);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 66);
            this.button2.TabIndex = 16;
            this.button2.Text = "Regresar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnPLatos
            // 
            this.btnPLatos.Location = new System.Drawing.Point(3, 168);
            this.btnPLatos.Name = "btnPLatos";
            this.btnPLatos.Size = new System.Drawing.Size(156, 86);
            this.btnPLatos.TabIndex = 17;
            this.btnPLatos.Text = "Lista de Platos";
            this.btnPLatos.UseVisualStyleBackColor = true;
            this.btnPLatos.Click += new System.EventHandler(this.btnPLatos_Click);
            // 
            // lbxPlatosSeleccionados
            // 
            this.lbxPlatosSeleccionados.FormattingEnabled = true;
            this.lbxPlatosSeleccionados.Location = new System.Drawing.Point(182, 168);
            this.lbxPlatosSeleccionados.Name = "lbxPlatosSeleccionados";
            this.lbxPlatosSeleccionados.Size = new System.Drawing.Size(163, 160);
            this.lbxPlatosSeleccionados.TabIndex = 18;
            // 
            // dgvAsociacionesPlatos
            // 
            this.dgvAsociacionesPlatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAsociacionesPlatos.Location = new System.Drawing.Point(575, 374);
            this.dgvAsociacionesPlatos.Name = "dgvAsociacionesPlatos";
            this.dgvAsociacionesPlatos.Size = new System.Drawing.Size(323, 257);
            this.dgvAsociacionesPlatos.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(604, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 18);
            this.label2.TabIndex = 20;
            this.label2.Text = "Restaurantes Asociados";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(672, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 18);
            this.label3.TabIndex = 21;
            this.label3.Text = "Platos Asociados";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // lbAsignacion
            // 
            this.lbAsignacion.AutoSize = true;
            this.lbAsignacion.Location = new System.Drawing.Point(261, 13);
            this.lbAsignacion.Name = "lbAsignacion";
            this.lbAsignacion.Size = new System.Drawing.Size(24, 13);
            this.lbAsignacion.TabIndex = 22;
            this.lbAsignacion.Text = "ID: ";
            // 
            // MenuPlatoRestaurante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 659);
            this.Controls.Add(this.lbAsignacion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvAsociacionesPlatos);
            this.Controls.Add(this.lbxPlatosSeleccionados);
            this.Controls.Add(this.btnPLatos);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpFechaAfiliacion);
            this.Controls.Add(this.dgvAsociacionesRestaurantes);
            this.Controls.Add(this.cmbRestaurantesDisponibles);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Name = "MenuPlatoRestaurante";
            this.Text = "MenuPlatoRestaurante";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsociacionesRestaurantes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsociacionesPlatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbRestaurantesDisponibles;
        private System.Windows.Forms.DataGridView dgvAsociacionesRestaurantes;
        private System.Windows.Forms.DateTimePicker dtpFechaAfiliacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnPLatos;
        private System.Windows.Forms.ListBox lbxPlatosSeleccionados;
        private System.Windows.Forms.DataGridView dgvAsociacionesPlatos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbAsignacion;
    }
}