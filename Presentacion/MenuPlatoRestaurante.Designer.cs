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
            this.dgvListaplatos = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIdAsociacionRestaurantes = new System.Windows.Forms.TextBox();
            this.txtIdAsociacionPlatos = new System.Windows.Forms.TextBox();
            this.dgvAsociacionesCompletadas = new System.Windows.Forms.DataGridView();
            this.dtpFechaAfiliacion = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaplatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsociacionesCompletadas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Restaurantes disponibles";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(481, 9);
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
            this.cmbRestaurantesDisponibles.Size = new System.Drawing.Size(435, 21);
            this.cmbRestaurantesDisponibles.TabIndex = 6;
            // 
            // dgvListaplatos
            // 
            this.dgvListaplatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaplatos.Location = new System.Drawing.Point(473, 23);
            this.dgvListaplatos.Name = "dgvListaplatos";
            this.dgvListaplatos.Size = new System.Drawing.Size(240, 269);
            this.dgvListaplatos.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(719, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "ID de la asociacion del plato al restaurante:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "ID de la asociacion restaurante:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtIdAsociacionRestaurantes
            // 
            this.txtIdAsociacionRestaurantes.Location = new System.Drawing.Point(21, 25);
            this.txtIdAsociacionRestaurantes.Name = "txtIdAsociacionRestaurantes";
            this.txtIdAsociacionRestaurantes.Size = new System.Drawing.Size(113, 20);
            this.txtIdAsociacionRestaurantes.TabIndex = 10;
            // 
            // txtIdAsociacionPlatos
            // 
            this.txtIdAsociacionPlatos.Location = new System.Drawing.Point(742, 28);
            this.txtIdAsociacionPlatos.Name = "txtIdAsociacionPlatos";
            this.txtIdAsociacionPlatos.Size = new System.Drawing.Size(130, 20);
            this.txtIdAsociacionPlatos.TabIndex = 11;
            // 
            // dgvAsociacionesCompletadas
            // 
            this.dgvAsociacionesCompletadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAsociacionesCompletadas.Location = new System.Drawing.Point(144, 349);
            this.dgvAsociacionesCompletadas.Name = "dgvAsociacionesCompletadas";
            this.dgvAsociacionesCompletadas.Size = new System.Drawing.Size(627, 291);
            this.dgvAsociacionesCompletadas.TabIndex = 12;
            // 
            // dtpFechaAfiliacion
            // 
            this.dtpFechaAfiliacion.Location = new System.Drawing.Point(220, 25);
            this.dtpFechaAfiliacion.Name = "dtpFechaAfiliacion";
            this.dtpFechaAfiliacion.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaAfiliacion.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Fecha de la asociacion plato restaurante";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 66);
            this.button1.TabIndex = 15;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 467);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 66);
            this.button2.TabIndex = 16;
            this.button2.Text = "Regresar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MenuPlatoRestaurante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 652);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpFechaAfiliacion);
            this.Controls.Add(this.dgvAsociacionesCompletadas);
            this.Controls.Add(this.txtIdAsociacionPlatos);
            this.Controls.Add(this.txtIdAsociacionRestaurantes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvListaplatos);
            this.Controls.Add(this.cmbRestaurantesDisponibles);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Name = "MenuPlatoRestaurante";
            this.Text = "MenuPlatoRestaurante";
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaplatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsociacionesCompletadas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbRestaurantesDisponibles;
        private System.Windows.Forms.DataGridView dgvListaplatos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIdAsociacionRestaurantes;
        private System.Windows.Forms.TextBox txtIdAsociacionPlatos;
        private System.Windows.Forms.DataGridView dgvAsociacionesCompletadas;
        private System.Windows.Forms.DateTimePicker dtpFechaAfiliacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}