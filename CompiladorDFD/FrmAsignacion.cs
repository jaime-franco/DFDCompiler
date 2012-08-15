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
    public partial class FrmAsignacion : Form
    {

        //Variable que contiene el tipo del control
        ElementoDFD elemento;
        public FrmAsignacion()
        {
            InitializeComponent();
        }

        private void FrmAsignacion_Load(object sender, EventArgs e)
        {
            
        }
        //Funcion para obtener el elemento a utlizar
        public void PasarElemento( ElementoDFD tempElemento) {
            elemento = tempElemento;
            Actualizar();
        }
        
        public void Actualizar() {
            if (elemento.datos != null)
            {
                string[] datos = elemento.datos.Replace('=', '\n').Split('\n');
                if (datos.Length > 1)
                {
                    txtVariable1.Text = datos[0];
                    txtContenido1.Text = datos[1];
                }
                if (datos.Length > 3)
                {
                    txtVariable2.Text = datos[2];
                    txtContenido2.Text = datos[3];
                }
                if(datos.Length>5){
                txtVariable3.Text = datos[4];
                txtContenido3.Text = datos[5];
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {//Se agregan los datos modificados
            elemento.datos = "";
            if (txtVariable1.Text != string.Empty || txtContenido1.Text != string.Empty)
            {
                elemento.datos += txtVariable1.Text + "=";
                elemento.datos += txtContenido1.Text + "\n";
            }
            if (txtVariable2.Text != string.Empty || txtContenido2.Text != string.Empty)
            {
                elemento.datos += txtVariable2.Text + "=";
                elemento.datos += txtContenido2.Text + "\n";
            } if (txtVariable3.Text != string.Empty || txtContenido3.Text != string.Empty)
            {
                elemento.datos += txtVariable3.Text + "=";
                elemento.datos += txtContenido3.Text + "\n";
            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
