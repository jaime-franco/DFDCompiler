namespace CompiladorDFD
{
    partial class FrmDFDCompiler
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
            this.btnCompilar = new System.Windows.Forms.Button();
            this.btnLectura = new System.Windows.Forms.Button();
            this.ucdfd1 = new CompiladorDFD.UCDFD();
            this.btnEscritura = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 379);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Asignacion";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCompilar
            // 
            this.btnCompilar.Location = new System.Drawing.Point(107, 379);
            this.btnCompilar.Name = "btnCompilar";
            this.btnCompilar.Size = new System.Drawing.Size(75, 23);
            this.btnCompilar.TabIndex = 2;
            this.btnCompilar.Text = "Compilar";
            this.btnCompilar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCompilar.UseVisualStyleBackColor = true;
            this.btnCompilar.Click += new System.EventHandler(this.btnCompilar_Click);
            // 
            // btnLectura
            // 
            this.btnLectura.Location = new System.Drawing.Point(188, 379);
            this.btnLectura.Name = "btnLectura";
            this.btnLectura.Size = new System.Drawing.Size(75, 23);
            this.btnLectura.TabIndex = 3;
            this.btnLectura.Text = "Lectura";
            this.btnLectura.UseVisualStyleBackColor = true;
            this.btnLectura.Click += new System.EventHandler(this.btnLectura_Click);
            // 
            // ucdfd1
            // 
            this.ucdfd1.AutoScroll = true;
            this.ucdfd1.AutoScrollMinSize = new System.Drawing.Size(3000, 3000);
            this.ucdfd1.BackColor = System.Drawing.Color.White;
            this.ucdfd1.Color = System.Drawing.Color.Black;
            this.ucdfd1.ColorFondo = System.Drawing.Color.White;
            this.ucdfd1.DimensionAsignacion = new System.Drawing.Size(120, 60);
            this.ucdfd1.DimensionIf = new System.Drawing.Size(90, 60);
            this.ucdfd1.Grosor = 1;
            this.ucdfd1.Location = new System.Drawing.Point(12, 49);
            this.ucdfd1.Name = "ucdfd1";
            this.ucdfd1.Size = new System.Drawing.Size(543, 323);
            this.ucdfd1.TabIndex = 0;
            this.ucdfd1.Load += new System.EventHandler(this.ucdfd1_Load);
            // 
            // btnEscritura
            // 
            this.btnEscritura.Location = new System.Drawing.Point(269, 379);
            this.btnEscritura.Name = "btnEscritura";
            this.btnEscritura.Size = new System.Drawing.Size(75, 23);
            this.btnEscritura.TabIndex = 4;
            this.btnEscritura.Text = "Escritura";
            this.btnEscritura.UseVisualStyleBackColor = true;
            this.btnEscritura.Click += new System.EventHandler(this.btnEscritura_Click);
            // 
            // FrmDFDCompiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 414);
            this.Controls.Add(this.btnEscritura);
            this.Controls.Add(this.btnLectura);
            this.Controls.Add(this.btnCompilar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ucdfd1);
            this.Name = "FrmDFDCompiler";
            this.Text = "FrmDFDCompiler";
            this.Load += new System.EventHandler(this.FrmDFDCompiler_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UCDFD ucdfd1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCompilar;
        private System.Windows.Forms.Button btnLectura;
        private System.Windows.Forms.Button btnEscritura;
    }
}