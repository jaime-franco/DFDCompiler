using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace CompiladorDFD
{
    public partial class UCDFD : UserControl
    {
        //Declaracion de variables a utilizar para controlar las lineas
        private Color colorLinea = Color.White;
        private int grosorLinea = 1;
        private Color colorFondo = Color.Black;
        //Declaracion de las variables a ser utilizadas por el control
        //private UCElementos raiz;//Raiz del grafo a crear desde un nodo inicio 
        private ElementoDFD elementoRaiz;//Raiz del grafo utilizado para tener una referencia hacia todos ya que es el inicio
        //Lista para utilizarse para recorrer todos los elementos creados
        private List<ElementoDFD> listadoElementos = new List<ElementoDFD>();
        //---------------------------------------------------------------
        //                      Elementos para utilizar el doble buffer
        //---------------------------------------------------------------
        Graphics GX;
        BufferedGraphicsContext bgc = new BufferedGraphicsContext();
        BufferedGraphics bg;
        //---------------------------------------------------------------

        public UCDFD()
        {
            InitializeComponent();
            //Utilizado para permitir el doble buffer este optimizado
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //Se crean los elementos iniales dentro del grafo
            this.BackColor = colorFondo;
        }

        private void UCDFD_Load(object sender, EventArgs e)
        {  //Se crea el principio del grafo que se utilizara dentro del programa
            //se agrega el inicio
           //AgregarElemento(null, CrearElemento(Elemento.inicio, imgInicio));     
            AgregarElemento(null,null,CrearElementoDFD(Elemento.inicio));
            AgregarElemento(elementoRaiz,elementoRaiz.centro,CrearElementoDFD(Elemento.fin));

            AgregarElemento(elementoRaiz, elementoRaiz.centro, CrearElementoDFD(Elemento.Asignacion));
            //se agrega el final
           //AgregarElemento(raiz, CrearElemento(Elemento.fin, imgFin));
            //Se ajustan los elementos
           //ReajustarElementos(raiz);
            ReajustarElementos(elementoRaiz);
        }


        //------------------------------------------------------------------
        //
        //------------------------------------------------------------------

        //------------------------------------------------------------------
        //          Funciones para crear los elementos
        //------------------------------------------------------------------

        private ElementoDFD CrearElementoDFD(Elemento tipo) {
        //Se crea el elemeto a manejar
            ElementoDFD tempElemento = new ElementoDFD();

            switch (tipo) { 
                case Elemento.inicio:
                case Elemento.fin:
                     tempElemento.width = 60;
                     tempElemento.height = 60;
                break;
                case Elemento.Asignacion:
                     tempElemento.width = 120;
                     tempElemento.height = 60;
                    break;
            }   
            tempElemento.visible = true;
            tempElemento.tipo = tipo;
            listadoElementos.Add(tempElemento);
        //Se retorna el elemento
            return tempElemento;
        }
     
        //------------------------------------------------------------------
        //          Funciones para ingresar los elementos dentro del grafo
        //------------------------------------------------------------------

        //Se pasan los elementos para poder ser asignados correctamente deltro del grafo
        private void AgregarElemento(ElementoDFD padre,ElementoDFD hijo,ElementoDFD nuevo) {
            //Si no existe un padre se coloca como raiz del grafo entero
            if (padre == null)
            {
                elementoRaiz = nuevo;
            }
            else
            {//Se verifica si existe un hijo
                if (hijo != null)
                {//Se comienza a verificar que hijo es de los 3 posibles que existen
                    if (padre.izquierda == hijo) padre.izquierda = nuevo;
                    else if (padre.derecha == hijo) padre.derecha = nuevo;
                    else if (padre.centro == hijo) padre.centro = nuevo;
                    //Se pasa al hijo como referencia
                    nuevo.centro = hijo;
                }
                else
                {
                    padre.centro = nuevo;
                }
                //Se le asigna la referencia la padre
                nuevo.padre = padre;
            }
        }
        //------------------------------------------------------------------
        // Eventos creados para manejar las funciones hechas sobre los controles
        //------------------------------------------------------------------
       
        //Evento Click
        void Click_Elemento(object sender, EventArgs e)
        {   //Se hace una transformacion para poder trabajar con el elemento sobre el que se dio click
            UCElementos tempElementos = sender as UCElementos;
            MessageBox.Show("Mi tipo es :" + tempElementos.UCTipo.ToString());
        }


        //---------------------------------------------------------------------
        //  Funciones a utilizar para reacomodar los elementos dentro del grafo
        //---------------------------------------------------------------------
        void ReajustarElementos(ElementoDFD elemento)
        {
            ElementoDFD tempElemento = elemento;
            //Dezplazamientos realizados
            int espaciado = 30;
            int x = this.Width / 2;           //Indica el inicio del dezplazamiento
            int y = 10 + this.AutoScrollPosition.Y; //centro de la ventana contenedora 
            int yMax = 10;
            int xMax = x;//Variable a utilizar para los scrools
            while (tempElemento != null)
            {
                tempElemento.left = x - tempElemento.width/2;
                tempElemento.top = y;
                y += tempElemento.height + espaciado;
                yMax += espaciado + tempElemento.height;
                tempElemento = tempElemento.centro;

            }
            //Configuracion de los scrools a utilizar
            this.AutoScrollMinSize = new Size(xMax, yMax);
        }

        //---------------------------------------------------------------------------
        //        Funciones para realizar las lineas sobre el grafo y los elementos
        //---------------------------------------------------------------------------
        private void Dibujar()
        {
            //Se crea el lienzo en el cual se trabajara
            GX = this.CreateGraphics();
            bg = bgc.Allocate(GX, new Rectangle(0, 0, this.Width, this.Height));

            //Se dibuja un rectangulo en el fondo
            SolidBrush Brectangulo = new SolidBrush(colorFondo);
            bg.Graphics.FillRectangle(Brectangulo, new Rectangle(0, 0, this.Width, this.Height));
            //Se comienzan a pintar las lineas dentro del control
            ElementoDFD tempElemento = elementoRaiz; //Se obtiene una referencia al elemento inicial
            //se crean variables para manejar los puntos en los cuales se crearan las lineas
            //punto inicial y punto final
            Point puntoInicial;
            Point puntoFinal;
            //Se crea el lapiza a ser utilizado para dibujar las lineas
            Pen lapiz = new Pen(colorLinea, grosorLinea);
            Pen lapizFiguras = new Pen(colorLinea, grosorLinea);
            //Se recorre todo el grafo hasta llegar al elemento final del grafo que 
            //es de tipo fin indicando el fin del dibujo del grafo
            while (tempElemento.tipo != Elemento.fin)
            {
                //Se dibuja el elemento a utilizar
                DibujarElemento(ref bg, tempElemento, lapizFiguras);
                //Se dibujan elementos lineales es decir que solo tienen un camnino central
                if (tempElemento.centro.tipo != Elemento.none)
                {   //Se agrega al lapiz al final una flecha
                    lapiz.EndCap = LineCap.ArrowAnchor;
                    //Se calculan los puntos a utilizar para redibujar la linea
                    puntoInicial = new Point(tempElemento.width / 2 + tempElemento.left, tempElemento.height  + tempElemento.top);
                    //variable temporal para almacenar el elemento del punto final
                    ElementoDFD tempElemento2 = tempElemento.centro;
                    //Se calcula el punto final a utilizar para realizar el dibujo de la linea
                    puntoFinal = new Point(tempElemento2.width / 2 + tempElemento2.left, tempElemento2.top);
                    bg.Graphics.DrawLine(lapiz, puntoInicial, puntoFinal);
                }
                else
                {

                }
                tempElemento = tempElemento.centro;
            }
            DibujarElemento(ref bg, tempElemento, lapizFiguras);
            //Se realiza el render de las lineas para poder ser visualizadas todas a la vez
            bg.Render();
            //Se procede a realizar un dispose sobre todos los elementos que se dejaron de usar para
            //poder asi liberar los recursos utilizados por estos
            lapiz.Dispose();
            bg.Dispose();
            GX.Dispose();
            Brectangulo.Dispose();
        }
        //------------------------------------------------------------------
        //                  Funciones para dibujar los elementos a crear
        //------------------------------------------------------------------

        private void DibujarElemento(ref BufferedGraphics tempbf, ElementoDFD tempElemento, Pen tempPen)
        {
            //Rectangulo utilizado por la funcion de dibujo
            Rectangle tempRectangle = new Rectangle(tempElemento.left, tempElemento.top, tempElemento.width, tempElemento.height);

            switch (tempElemento.tipo)
            {
                case Elemento.inicio:
                    tempbf.Graphics.DrawEllipse(tempPen, tempRectangle);
                    break;
                case Elemento.Asignacion:
                    tempbf.Graphics.DrawRectangle(tempPen, tempRectangle);
                    break;
                case Elemento.fin:
                    tempbf.Graphics.DrawEllipse(tempPen, tempRectangle);
                    break;
            }
        }

        //------------------------------------------------------------------
        //Declaracion de propiedades publicas para utilizar con los controles
        //------------------------------------------------------------------
        [Category("Lineas")]
        [Description("Color de las lineas del grafo")]
        public Color Color
        {
            set { colorLinea = value; }
            get { return colorLinea; }
        }
        [Category("Lineas")]
        [Description("Color del fondo")]
        public Color ColorFondo
        {
            set { colorFondo = value;
            this.BackColor = value;
            }
            get { return colorFondo; }
        }
        [Category("Lineas")]
        [Description("Grosor de las lineas del grafo")]
        public int Grosor
        {
            set { grosorLinea = value; }
            get { return grosorLinea; }
        }

        private void TimerDibujar_Tick(object sender, EventArgs e)
        {
            Dibujar();
        }

        private void UCDFD_DoubleClick(object sender, EventArgs e)
        {
         
        }

        private void UCDFD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
             ElementoDFD resultante= null;
            foreach (ElementoDFD temp in listadoElementos) {
                if (temp.VerificarInteraccion(e.X, e.Y)) {
                    resultante = temp;
                }
            }
            if(resultante != null)
                MessageBox.Show("Mi tipo es: " + resultante.tipo.ToString());
        }

        private void UCDFD_Scroll(object sender, ScrollEventArgs e)
        {
            ReajustarElementos(elementoRaiz);
        }

    }
}
