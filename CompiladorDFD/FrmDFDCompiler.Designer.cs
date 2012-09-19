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
            this.btnEscritura = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnIF = new System.Windows.Forms.Button();
            this.ucdfd1 = new CompiladorDFD.UCDFD();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Asignacion";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCompilar
            // 
            this.btnCompilar.Location = new System.Drawing.Point(6, 147);
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
            this.btnLectura.Location = new System.Drawing.Point(6, 89);
            this.btnLectura.Name = "btnLectura";
            this.btnLectura.Size = new System.Drawing.Size(75, 23);
            this.btnLectura.TabIndex = 3;
            this.btnLectura.Text = "Lectura";
            this.btnLectura.UseVisualStyleBackColor = true;
            this.btnLectura.Click += new System.EventHandler(this.btnLectura_Click);
            // 
            // btnEscritura
            // 
            this.btnEscritura.Location = new System.Drawing.Point(6, 60);
            this.btnEscritura.Name = "btnEscritura";
            this.btnEscritura.Size = new System.Drawing.Size(75, 23);
            this.btnEscritura.TabIndex = 4;
            this.btnEscritura.Text = "Escritura";
            this.btnEscritura.UseVisualStyleBackColor = true;
            this.btnEscritura.Click += new System.EventHandler(this.btnEscritura_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnIF);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnCompilar);
            this.groupBox1.Controls.Add(this.btnLectura);
            this.groupBox1.Controls.Add(this.btnEscritura);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(659, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(87, 425);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(659, 425);
            this.panel1.TabIndex = 6;
            // 
            // BtnIF
            // 
            this.BtnIF.Location = new System.Drawing.Point(6, 118);
            this.BtnIF.Name = "BtnIF";
            this.BtnIF.Size = new System.Drawing.Size(75, 23);
            this.BtnIF.TabIndex = 5;
            this.BtnIF.Text = "If";
            this.BtnIF.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnIF.UseVisualStyleBackColor = true;
            this.BtnIF.Click += new System.EventHandler(this.button2_Click);
            // 
            // ucdfd1
            // 
            this.ucdfd1.AutoScroll = true;
            this.ucdfd1.AutoScrollMinSize = new System.Drawing.Size(3000, 3000);
            this.ucdfd1.AutoSize = true;
            this.ucdfd1.BackColor = System.Drawing.Color.White;
            this.ucdfd1.Color = System.Drawing.Color.Black;
            this.ucdfd1.ColorFondo = System.Drawing.Color.White;
            this.ucdfd1.DimensionAsignacion = new System.Drawing.Size(120, 60);
            this.ucdfd1.DimensionIf = new System.Drawing.Size(90, 60);
            this.ucdfd1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucdfd1.Grosor = 1;
            this.ucdfd1.Location = new System.Drawing.Point(0, 0);
            this.ucdfd1.Name = "ucdfd1";
            this.ucdfd1.Size = new System.Drawing.Size(659, 425);
            this.ucdfd1.TabIndex = 0;
            this.ucdfd1.Load += new System.EventHandler(this.ucdfd1_Load);
            // 
            // FrmDFDCompiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 425);
            this.Controls.Add(this.ucdfd1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmDFDCompiler";
            this.Text = "FrmDFDCompiler";
            this.Load += new System.EventHandler(this.FrmDFDCompiler_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCDFD ucdfd1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCompilar;
        private System.Windows.Forms.Button btnLectura;
        private System.Windows.Forms.Button btnEscritura;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnIF;
    }
}