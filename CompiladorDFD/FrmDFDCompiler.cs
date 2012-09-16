using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
