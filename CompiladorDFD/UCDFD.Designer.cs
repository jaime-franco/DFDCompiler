namespace CompiladorDFD
{
    partial class UCDFD
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TimerDibujar = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // TimerDibujar
            // 
            this.TimerDibujar.Enabled = true;
            this.TimerDibujar.Tick += new System.EventHandler(this.TimerDibujar_Tick);
            // 
            // UCDFD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(3000, 3000);
            this.Name = "UCDFD";
            this.Size = new System.Drawing.Size(353, 306);
            this.Load += new System.EventHandler(this.UCDFD_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.UCDFD_Scroll);
            this.Click += new System.EventHandler(this.UCDFD_Click);
            this.DoubleClick += new System.EventHandler(this.UCDFD_DoubleClick);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UCDFD_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.UCDFD_MouseDoubleClick);
            this.MouseEnter += new System.EventHandler(this.UCDFD_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.UCDFD_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UCDFD_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer TimerDibujar;
    }
}
