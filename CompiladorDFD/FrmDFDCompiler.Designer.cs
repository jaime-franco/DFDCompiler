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
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.BtnGuardar = new System.Windows.Forms.Button();
            this.Eliminar = new System.Windows.Forms.Button();
            this.BtnWhile = new System.Windows.Forms.Button();
            this.BtnIF = new System.Windows.Forms.Button();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucdfd1 = new CompiladorDFD.UCDFD();
            this.SaveFile = new System.Windows.Forms.SaveFileDialog();
            this.OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.BtnNuevo = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Asignacion";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCompilar
            // 
            this.btnCompilar.Location = new System.Drawing.Point(10, 365);
            this.btnCompilar.Name = "btnCompilar";
            this.btnCompilar.Size = new System.Drawing.Size(107, 23);
            this.btnCompilar.TabIndex = 2;
            this.btnCompilar.Text = "Compilar";
            this.btnCompilar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCompilar.UseVisualStyleBackColor = true;
            this.btnCompilar.Click += new System.EventHandler(this.btnCompilar_Click);
            // 
            // btnLectura
            // 
            this.btnLectura.Location = new System.Drawing.Point(3, 124);
            this.btnLectura.Name = "btnLectura";
            this.btnLectura.Size = new System.Drawing.Size(112, 23);
            this.btnLectura.TabIndex = 3;
            this.btnLectura.Text = "Lectura";
            this.btnLectura.UseVisualStyleBackColor = true;
            this.btnLectura.Click += new System.EventHandler(this.btnLectura_Click);
            // 
            // btnEscritura
            // 
            this.btnEscritura.Location = new System.Drawing.Point(3, 95);
            this.btnEscritura.Name = "btnEscritura";
            this.btnEscritura.Size = new System.Drawing.Size(112, 23);
            this.btnEscritura.TabIndex = 4;
            this.btnEscritura.Text = "Escritura";
            this.btnEscritura.UseVisualStyleBackColor = true;
            this.btnEscritura.Click += new System.EventHandler(this.btnEscritura_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnNuevo);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.BtnGuardar);
            this.groupBox1.Controls.Add(this.Eliminar);
            this.groupBox1.Controls.Add(this.BtnWhile);
            this.groupBox1.Controls.Add(this.BtnIF);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnCompilar);
            this.groupBox1.Controls.Add(this.btnLectura);
            this.groupBox1.Controls.Add(this.btnEscritura);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(698, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 425);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones DFD";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(10, 253);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNombre.TabIndex = 11;
            this.txtNombre.Text = "Ejecutable";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(10, 336);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Abrir";
            this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // BtnGuardar
            // 
            this.BtnGuardar.Location = new System.Drawing.Point(10, 307);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.Size = new System.Drawing.Size(107, 23);
            this.BtnGuardar.TabIndex = 9;
            this.BtnGuardar.Text = "Guardar";
            this.BtnGuardar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnGuardar.UseVisualStyleBackColor = true;
            this.BtnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            // 
            // Eliminar
            // 
            this.Eliminar.Location = new System.Drawing.Point(3, 207);
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Size = new System.Drawing.Size(112, 23);
            this.Eliminar.TabIndex = 8;
            this.Eliminar.Text = "Eliminar";
            this.Eliminar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Eliminar.UseVisualStyleBackColor = true;
            this.Eliminar.Click += new System.EventHandler(this.Eliminar_Click);
            // 
            // BtnWhile
            // 
            this.BtnWhile.Location = new System.Drawing.Point(3, 178);
            this.BtnWhile.Name = "BtnWhile";
            this.BtnWhile.Size = new System.Drawing.Size(112, 23);
            this.BtnWhile.TabIndex = 6;
            this.BtnWhile.Text = "While";
            this.BtnWhile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnWhile.UseVisualStyleBackColor = true;
            this.BtnWhile.Click += new System.EventHandler(this.BtnWhile_Click);
            // 
            // BtnIF
            // 
            this.BtnIF.Location = new System.Drawing.Point(3, 153);
            this.BtnIF.Name = "BtnIF";
            this.BtnIF.Size = new System.Drawing.Size(112, 23);
            this.BtnIF.TabIndex = 5;
            this.BtnIF.Text = "If";
            this.BtnIF.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnIF.UseVisualStyleBackColor = true;
            this.BtnIF.Click += new System.EventHandler(this.button2_Click);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(125, 175);
            this.ContentPanel.Load += new System.EventHandler(this.toolStripContainer1_ContentPanel_Load);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(150, 150);
            this.toolStripContainer1.Location = new System.Drawing.Point(175, 78);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(150, 175);
            this.toolStripContainer1.TabIndex = 7;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStripContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(698, 425);
            this.panel1.TabIndex = 6;
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
            this.ucdfd1.Size = new System.Drawing.Size(698, 425);
            this.ucdfd1.TabIndex = 0;
            this.ucdfd1.Load += new System.EventHandler(this.ucdfd1_Load);
            // 
            // OpenFile
            // 
            this.OpenFile.FileName = "openFileDialog1";
            // 
            // BtnNuevo
            // 
            this.BtnNuevo.Location = new System.Drawing.Point(10, 279);
            this.BtnNuevo.Name = "BtnNuevo";
            this.BtnNuevo.Size = new System.Drawing.Size(107, 23);
            this.BtnNuevo.TabIndex = 12;
            this.BtnNuevo.Text = "Nuevo DFD";
            this.BtnNuevo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnNuevo.UseVisualStyleBackColor = true;
            this.BtnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            // 
            // FrmDFDCompiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 425);
            this.Controls.Add(this.ucdfd1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmDFDCompiler";
            this.Text = "COMPILADOR DFD BETA V1.0";
            this.Load += new System.EventHandler(this.FrmDFDCompiler_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.Button BtnIF;
        private System.Windows.Forms.Button BtnWhile;
        private System.Windows.Forms.Button Eliminar;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.Button BtnGuardar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.SaveFileDialog SaveFile;
        private System.Windows.Forms.OpenFileDialog OpenFile;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button BtnNuevo;
    }
}