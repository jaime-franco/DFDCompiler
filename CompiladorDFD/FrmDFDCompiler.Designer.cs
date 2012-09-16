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
            this.ucdfd1 = new CompiladorDFD.UCDFD();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // FrmDFDCompiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 414);
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
    }
}