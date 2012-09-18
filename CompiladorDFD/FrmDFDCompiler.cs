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
            analisisSemantico.AnalizarTipos();
            //Se verifica si existen errores o no dentro del codigo antes de compilarlo
            if (!ValoresGlobales.valores().tablaDeErrores.Existen()) {
                generarCodigo.GenerarEjecutable("Prueba");
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
    }
}
