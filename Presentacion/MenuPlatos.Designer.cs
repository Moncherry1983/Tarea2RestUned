namespace Presentacion
{
    partial class MenuPlatos
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
            this.txtNombrePlato = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdPlato = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrecioPlato = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvPlato = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPlatos = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlato)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNombrePlato
            // 
            this.txtNombrePlato.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtNombrePlato.Location = new System.Drawing.Point(152, 42);
            this.txtNombrePlato.Name = "txtNombrePlato";
            this.txtNombrePlato.Size = new System.Drawing.Size(149, 20);
            this.txtNombrePlato.TabIndex = 20;
            this.txtNombrePlato.TextChanged += new System.EventHandler(this.descripcion_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 18;
            // 
            // txtIdPlato
            // 
            this.txtIdPlato.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtIdPlato.Location = new System.Drawing.Point(125, 12);
            this.txtIdPlato.Name = "txtIdPlato";
            this.txtIdPlato.Size = new System.Drawing.Size(94, 20);
            this.txtIdPlato.TabIndex = 17;
            this.txtIdPlato.TextChanged += new System.EventHandler(this.txtIdPlato_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(175, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 15;
            // 
            // txtPrecioPlato
            // 
            this.txtPrecioPlato.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtPrecioPlato.Location = new System.Drawing.Point(146, 89);
            this.txtPrecioPlato.Name = "txtPrecioPlato";
            this.txtPrecioPlato.Size = new System.Drawing.Size(78, 20);
            this.txtPrecioPlato.TabIndex = 25;
            this.txtPrecioPlato.TextChanged += new System.EventHandler(this.txtPrecioPlato_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Ingrese el id del plato:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Ingrese el precio del plato:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(137, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Ingrese el nombre del plato:";
            // 
            // dgvPlato
            // 
            this.dgvPlato.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlato.Location = new System.Drawing.Point(239, 214);
            this.dgvPlato.Name = "dgvPlato";
            this.dgvPlato.Size = new System.Drawing.Size(416, 136);
            this.dgvPlato.TabIndex = 36;
            this.dgvPlato.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPlato_CellFormatting);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 214);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 56);
            this.button1.TabIndex = 37;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 298);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 51);
            this.button2.TabIndex = 38;
            this.button2.Text = "Regresar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Lista de categorias Disponibles";
            // 
            // cmbPlatos
            // 
            this.cmbPlatos.FormattingEnabled = true;
            this.cmbPlatos.Location = new System.Drawing.Point(178, 133);
            this.cmbPlatos.Name = "cmbPlatos";
            this.cmbPlatos.Size = new System.Drawing.Size(121, 21);
            this.cmbPlatos.TabIndex = 41;
            this.cmbPlatos.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // MenuPlatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbPlatos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvPlato);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPrecioPlato);
            this.Controls.Add(this.txtNombrePlato);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIdPlato);
            this.Controls.Add(this.label1);
            this.Name = "MenuPlatos";
            this.Text = "MenuRegistrarPlatos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlato)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtNombrePlato;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIdPlato;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrecioPlato;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvPlato;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPlatos;
    }
}