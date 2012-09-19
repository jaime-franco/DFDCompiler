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
    public partial class FrmLectura : Form
    {
        ElementoDFD elemento;
        public FrmLectura()
        {
            InitializeComponent();
        }
        public void pasarElemento(ElementoDFD elem)
        {
            elemento = elem;
            if (elemento.datos != null) txtEntrada.Text = elemento.datos;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            elemento.datos = txtEntrada.Text;
            this.Close();
        }
    }
}
