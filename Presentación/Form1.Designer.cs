namespace Presentación
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbSedes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCargar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.rbValidar = new System.Windows.Forms.RadioButton();
            this.rbSinvalidar = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSedes
            // 
            this.cbSedes.FormattingEnabled = true;
            this.cbSedes.Location = new System.Drawing.Point(208, 84);
            this.cbSedes.Name = "cbSedes";
            this.cbSedes.Size = new System.Drawing.Size(121, 21);
            this.cbSedes.TabIndex = 1;
            this.cbSedes.SelectedIndexChanged += new System.EventHandler(this.cbSedes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sedes";
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(349, 84);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(75, 23);
            this.btnCargar.TabIndex = 3;
            this.btnCargar.Text = "Cargar";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(49, 162);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(475, 236);
            this.dataGridView1.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // rbValidar
            // 
            this.rbValidar.AutoSize = true;
            this.rbValidar.Location = new System.Drawing.Point(169, 38);
            this.rbValidar.Name = "rbValidar";
            this.rbValidar.Size = new System.Drawing.Size(105, 17);
            this.rbValidar.TabIndex = 5;
            this.rbValidar.TabStop = true;
            this.rbValidar.Text = "Validando Sedes";
            this.rbValidar.UseVisualStyleBackColor = true;
            // 
            // rbSinvalidar
            // 
            this.rbSinvalidar.AutoSize = true;
            this.rbSinvalidar.Location = new System.Drawing.Point(316, 38);
            this.rbSinvalidar.Name = "rbSinvalidar";
            this.rbSinvalidar.Size = new System.Drawing.Size(108, 17);
            this.rbSinvalidar.TabIndex = 6;
            this.rbSinvalidar.TabStop = true;
            this.rbSinvalidar.Text = "Sin Validar Sedes";
            this.rbSinvalidar.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 450);
            this.Controls.Add(this.rbSinvalidar);
            this.Controls.Add(this.rbValidar);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSedes);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSedes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RadioButton rbValidar;
        private System.Windows.Forms.RadioButton rbSinvalidar;
    }
}

