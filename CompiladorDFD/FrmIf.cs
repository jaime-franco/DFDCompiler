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
    public partial class FrmIf : Form
    {
        private ElementoDFD elemento;
        public FrmIf()
        {
            InitializeComponent();
        }

        private void FrmIf_Load(object sender, EventArgs e)
        {

        }
       
        public void pasarElemento(ElementoDFD elem) {
          elemento=  elem;
          if(elemento.datos != null) txtCondicion.Text = elemento.datos;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            elemento.datos = txtCondicion.Text;
            this.Close();
        }

    }
}
