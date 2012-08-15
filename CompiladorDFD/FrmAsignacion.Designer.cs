namespace CompiladorDFD
{
    partial class FrmAsignacion
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
            this.txtVariable1 = new System.Windows.Forms.TextBox();
            this.txtVariable2 = new System.Windows.Forms.TextBox();
            this.txtVariable3 = new System.Windows.Forms.TextBox();
            this.txtContenido1 = new System.Windows.Forms.TextBox();
            this.txtContenido2 = new System.Windows.Forms.TextBox();
            this.txtContenido3 = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtVariable1
            // 
            this.txtVariable1.Location = new System.Drawing.Point(12, 12);
            this.txtVariable1.Name = "txtVariable1";
            this.txtVariable1.Size = new System.Drawing.Size(51, 20);
            this.txtVariable1.TabIndex = 0;
            // 
            // txtVariable2
            // 
            this.txtVariable2.Location = new System.Drawing.Point(12, 38);
            this.txtVariable2.Name = "txtVariable2";
            this.txtVariable2.Size = new System.Drawing.Size(51, 20);
            this.txtVariable2.TabIndex = 2;
            // 
            // txtVariable3
            // 
            this.txtVariable3.Location = new System.Drawing.Point(12, 64);
            this.txtVariable3.Name = "txtVariable3";
            this.txtVariable3.Size = new System.Drawing.Size(51, 20);
            this.txtVariable3.TabIndex = 4;
            // 
            // txtContenido1
            // 
            this.txtContenido1.Location = new System.Drawing.Point(85, 12);
            this.txtContenido1.Name = "txtContenido1";
            this.txtContenido1.Size = new System.Drawing.Size(89, 20);
            this.txtContenido1.TabIndex = 1;
            // 
            // txtContenido2
            // 
            this.txtContenido2.Location = new System.Drawing.Point(85, 38);
            this.txtContenido2.Name = "txtContenido2";
            this.txtContenido2.Size = new System.Drawing.Size(89, 20);
            this.txtContenido2.TabIndex = 3;
            // 
            // txtContenido3
            // 
            this.txtContenido3.Location = new System.Drawing.Point(85, 64);
            this.txtContenido3.Name = "txtContenido3";
            this.txtContenido3.Size = new System.Drawing.Size(89, 20);
            this.txtContenido3.TabIndex = 5;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(12, 90);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(96, 90);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmAsignacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(183, 120);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtContenido3);
            this.Controls.Add(this.txtContenido2);
            this.Controls.Add(this.txtContenido1);
            this.Controls.Add(this.txtVariable3);
            this.Controls.Add(this.txtVariable2);
            this.Controls.Add(this.txtVariable1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAsignacion";
            this.Text = "FrmAsignacion";
            this.Load += new System.EventHandler(this.FrmAsignacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtVariable1;
        private System.Windows.Forms.TextBox txtVariable2;
        private System.Windows.Forms.TextBox txtVariable3;
        private System.Windows.Forms.TextBox txtContenido1;
        private System.Windows.Forms.TextBox txtContenido2;
        private System.Windows.Forms.TextBox txtContenido3;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
    }
}