using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompiladorDFD
{

    public partial class UCElementos : UserControl
    {
        public new event EventHandler Click;//permite recibir el comportamiento de la funcion click del control
        public new event EventHandler DoubleClick;//Permite recibir el comportamiento de la fucnion doble click del control
        //Variable para determinar la naturaleza del elemento
        private Elemento elemento;
        //Variable para poder almacenar los datos de la cadena de datos segun sea el caso
        //para despues ser procesados por el analizador lexico
        private string datos;
        //Obejeto al que apunta el elemento dentro del grafo para 
        //determinar los caminos que puede tomar dentro del grafo
        private UCElementos[] ucRefHijos = new UCElementos[3];
        private UCElementos ucRefPadre;
        //Constructores del control
        public UCElementos()
        {
            InitializeComponent();
        }
        public UCElementos(Image img, Elemento elem) {
            elemento = elem;
            Imagen.Image = img;
            InitializeComponent();
        }
        //Funciones de asignacion de Datos hacia el control
        //Agregar la imagen correspondiente
        public void ColocarImagen(Image img) {
            Imagen.Image = img;
        }
        //Agregar el elmento que representa
        public void ColocarTipoElemnto(Elemento elem) {
            elemento = elem;
        }
        //Funciones publicas para agregar datos a las propiedades del elemento
        //Imagen a colocar al control
        [Category("Imagenes")]
        [Description("Imagen a agregar al control")]
        public Image UCImagen {
            set { Imagen.Image = value; }
            get { return Imagen.Image; }
        }
        //Se utiliza para obtener la cadena que contiene el control
        [Category("Datos")]
        [Description("Cadena de datos que posee el control")]
        public string UCString {
            set { datos = value; }
            get { return datos; }
        }
        //Se utiliza para poder saber el tipo de elemento que se posee
        [Category("Datos")]
        [Description("Representa el tipo de elemento")]
        public Elemento UCTipo
        {
            set { elemento = value; }
            get { return elemento; }
        }
        //Se usa para definir la propiedad de todas las referencias hacia los hijos
        [Category("Datos")]
        [Description("Contiene todas las referencias a los demas hijos")]
        public UCElementos[] UCRefHijos
        {
            set { ucRefHijos = value; }
            get { return ucRefHijos; }
        }

        private void Imagen_Click(object sender, EventArgs e)
        {
            //redimensiona el efecto del dar click sobre el picture 
            // realizando el codigo proporcionada al control UC
            if (this.Click != null) Click(this, EventArgs.Empty);
        }
        //Se usa para definir la propiedad de todas las referencias hacia el nodo padre
        [Category("Datos")]
        [Description("Contiene todas las referencia al padre")]
        public UCElementos UCRefPadre
        {
            set { ucRefPadre = value; }
            get { return ucRefPadre; }
        }

        private void Imagen_DoubleClick(object sender, EventArgs e)
        {
            if (this.DoubleClick != null) DoubleClick(this, EventArgs.Empty);
        }

       


     }
}
