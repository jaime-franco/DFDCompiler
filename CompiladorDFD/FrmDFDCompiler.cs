using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CompiladorDFD.Analizadores;
using CompiladorDFD.Datos_Externos;
using CompiladorDFD.Generacion_de_Codigo;
namespace CompiladorDFD
{
    public partial class FrmDFDCompiler : Form
    {
        public FrmDFDCompiler()
        {
            InitializeComponent();
        }

        private void FrmDFDCompiler_Load(object sender, EventArgs e)
        {
            ucdfd1.Iniciar();
        }

        private void ucdfd1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ucdfd1.AgregandoElemento(Elemento.Asignacion);
        }

        private void btnCompilar_Click(object sender, EventArgs e)
        {
            ValoresGlobales.valores().LimpiarDatos();
            ValoresGlobales.valores().elementoRaiz = ucdfd1.ObtenerRaiz();

            AnalizadorSintactico analisSintactico = new AnalizadorSintactico();
            AnalizadorSemantico analisisSemantico = new AnalizadorSemantico();
            GenerarCodigo generarCodigo = new GenerarCodigo();

            analisSintactico.GenerarArbol();
            if (!ValoresGlobales.valores().tablaDeErrores.Existen()) analisisSemantico.AnalizarTipos();
            //Se verifica si existen errores o no dentro del codigo antes de compilarlo
            if (!ValoresGlobales.valores().tablaDeErrores.Existen())
            {
                if (txtNombre.Text == "") txtNombre.Text = "Ejecutable";
                generarCodigo.GenerarEjecutable(txtNombre.Text);
                System.Diagnostics.Process.Start(txtNombre.Text + ".exe");
                
            }
            else {
                FrmTablaDeErrores frmTablaDeErrores = new FrmTablaDeErrores();
                frmTablaDeErrores.Show();
            }
        }

        private void btnLectura_Click(object sender, EventArgs e)
        {
            ucdfd1.AgregandoElemento(Elemento.Lectura);
        }

        private void btnEscritura_Click(object sender, EventArgs e)
        {
            ucdfd1.AgregandoElemento(Elemento.Escritura);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ucdfd1.AgregandoElemento(Elemento.Eif);
        }

        private void BtnWhile_Click(object sender, EventArgs e)
        {
            ucdfd1.AgregandoElemento(Elemento.EWhile);
        }

        private void BtnFor_Click(object sender, EventArgs e)
        {
            ucdfd1.AgregandoElemento(Elemento.Efor);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {   
            SaveFile = new SaveFileDialog();
            SaveFile.Filter = "txt files (*.txt)|*.txt";
            SaveFile.DefaultExt = "txt";
            SaveFile.Title = "Guardar Archivo DFD";
            SaveFile.ShowDialog();

   // If the file name is not an empty string open it for saving.
            if (SaveFile.FileName != "")
            {
                ValoresGlobales.valores().elementoRaiz = ucdfd1.ObtenerRaiz();
                GuardarCargarDFD guardar = new GuardarCargarDFD();
                guardar.GuardarArchivo(SaveFile.FileName);
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            ucdfd1.Eliminar();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {

            OpenFile = new OpenFileDialog();


            OpenFile.Filter = "txt files (*.txt)|*.txt";
            OpenFile.FilterIndex = 2;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ValoresGlobales.valores().elementoRaiz = ucdfd1.ObtenerRaiz();
                    GuardarCargarDFD guardar = new GuardarCargarDFD();
                    ucdfd1.CargarDFD(guardar.Leer(OpenFile.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error No se logro abrir el archivo deseado probablemente el formato no es el adecuado");
                }
            }


           
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            ucdfd1.Nuevo();
        }
    }
}
