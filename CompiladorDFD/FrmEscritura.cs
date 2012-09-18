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
    public partial class FrmEscritura : Form
    {
        ElementoDFD elemento;

        public FrmEscritura()
        {
            InitializeComponent();
        }

        public void pasarElemento(ElementoDFD elem)
        {
            elemento = elem;
            if (elemento.datos != null) txtSalida.Text = elemento.datos;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            elemento.datos = txtSalida.Text;
            this.Close();
        }
    }
}
