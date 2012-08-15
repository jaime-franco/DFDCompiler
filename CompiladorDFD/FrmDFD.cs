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
    public partial class FrmDFD : Form
    {
        public FrmDFD()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ucdfd1.AgregandoElemento(Elemento.Asignacion);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ucdfd1.AgregandoElemento(Elemento.inicio);
   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ucdfd1.Eliminar();
        }

    }
}
