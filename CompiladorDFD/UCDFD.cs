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
        //Declaracion de las imagenes para los elemetos a ser utilizados
        private Image imgInicio;
        private Image imgAsignacion;
        private Image imgFin;
        //Declaracion de variables a utilizar para controlar las lineas
        private Color colorLinea = Color.White;
        private int grosorLinea = 1;
        private Color colorFondo = Color.Black;
        //Declaracion de las variables a ser utilizadas por el control
        private UCElementos raiz;//Raiz del grafo a crear desde un nodo inicio 
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
           AgregarElemento(null, CrearElemento(Elemento.inicio, imgInicio));     
            //se agrega el final
           AgregarElemento(raiz, CrearElemento(Elemento.fin, imgFin));
            //Se ajustan los elementos
           ReajustarElementos(raiz);
        }


        //------------------------------------------------------------------
        //
        //------------------------------------------------------------------

        //------------------------------------------------------------------
        //          Funciones para crear los elementos
        //------------------------------------------------------------------

        private UCElementos CrearElemento(Elemento tipoE,Image img) {
            //Se crea dinamicamente el tipo de elemento
            UCElementos temp = new UCElementos();
            temp.Visible = true; //Se hace visible el elemento creado
            temp.UCTipo = tipoE; //Se le asigana el tipo de elemento que representara
            temp.UCImagen = img; //Se le asigana la imagen correspondiente al tipo de elemento
            temp.DoubleClick += new EventHandler(Click_Elemento);//Se le agrega el evento click_Elemento
            temp.BackColor = Color.Transparent;
            temp.Width = 60;
            temp.Height = 60;
            this.Controls.Add(temp);
            return temp;
        }

     
        //------------------------------------------------------------------
        //          Funciones para ingresar los elementos dentro del grafo
        //------------------------------------------------------------------
        private void AgregarElemento(UCElementos padre, UCElementos nuevo) {
            if (padre == null)//Si no existe el padre quiere decir que se esta ingresando la raiz
            {
                raiz = nuevo;
            }
            else {
                
                    nuevo.UCRefPadre = padre;
                    nuevo.UCRefHijos[1] = padre.UCRefHijos[1];
                    padre.UCRefHijos[1] = nuevo;
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
        void ReajustarElementos(UCElementos ucAjuste) {
            UCElementos tempElemento = ucAjuste;
            //Dezplazamientos realizados
            int espaciado= 30;
            int x = this.Width/2;           //Indica el inicio del dezplazamiento
            int y = 10; //centro de la ventana contenedora 
            while (tempElemento!= null) {
                tempElemento.Left = x;
                tempElemento.Top = y;
                y += tempElemento.Width + espaciado;
                tempElemento = tempElemento.UCRefHijos[1];
            }
        }
        //---------------------------------------------------------------------------
        //        Funciones para realizar las lineas sobre el grafo y los elementos
        //---------------------------------------------------------------------------
        private void Dibujar() {
            //Se crea el lienzo en el cual se trabajara
            GX = this.CreateGraphics();
            bg = bgc.Allocate(GX, new Rectangle(0, 0, this.Width, this.Height));
           
            //Se dibuja un rectangulo en el fondo
            SolidBrush Brectangulo = new SolidBrush(colorFondo);
            bg.Graphics.FillRectangle(Brectangulo,new Rectangle(0,0,this.Width,this.Height));
            //Se comienzan a pintar las lineas dentro del control
            UCElementos tempElemento = raiz; //Se obtiene una referencia al elemento inicial
            //se crean variables para manejar los puntos en los cuales se crearan las lineas
            //punto inicial y punto final
            Point puntoInicial;
            Point puntoFinal;
            //Se crea el lapiza a ser utilizado para dibujar las lineas
            Pen lapiz= new Pen(colorLinea,grosorLinea);
            Pen lapizFiguras = new Pen(colorLinea, grosorLinea);
            //Se recorre todo el grafo hasta llegar al elemento final del grafo que 
            //es de tipo fin indicando el fin del dibujo del grafo
            while(tempElemento.UCTipo!= Elemento.fin){
                //Se dibuja el elemento a utilizar
                DibujarElemento(ref bg, tempElemento, lapizFiguras);
                //Se dibujan elementos lineales es decir que solo tienen un camnino central
                if (tempElemento.UCRefHijos[1].UCTipo != Elemento.none)
                {   //Se agrega al lapiz al final una flecha
                    lapiz.EndCap = LineCap.ArrowAnchor;
                    //Se calculan los puntos a utilizar para redibujar la linea
                    puntoInicial = new Point(tempElemento.Width / 2 + tempElemento.Left, tempElemento.Height / 2 + tempElemento.Top);
                    //variable temporal para almacenar el elemento del punto final
                    UCElementos tempElemento2 = tempElemento.UCRefHijos[1];
                    //Se calcula el punto final a utilizar para realizar el dibujo de la linea
                    puntoFinal = new Point(tempElemento2.Width / 2 + tempElemento2.Left, tempElemento2.Top);
                    bg.Graphics.DrawLine(lapiz, puntoInicial, puntoFinal);
                }
                else { 
            
                }
                tempElemento = tempElemento.UCRefHijos[1];
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

        private void DibujarElemento(ref BufferedGraphics tempbf,UCElementos tempElemento,Pen tempPen){
            //Rectangulo utilizado por la funcion de dibujo
            Rectangle tempRectangle = new Rectangle(tempElemento.Left,tempElemento.Top,tempElemento.Width,tempElemento.Height);
            
            switch (tempElemento.UCTipo) { 
                case Elemento.inicio:
                    tempbf.Graphics.DrawEllipse(tempPen,tempRectangle);
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


        //------------------------------------------------------------------
        //                          IMAGENES 
        //------------------------------------------------------------------
        [Category("Imagenes")]
        [Description("Imagen de inicio del grafo")]
        public Image Inicio{
            set{ imgInicio=value;}
            get{return imgInicio;}
        }
        [Category("Imagenes")]
        [Description("Imagen de inicio del grafo")]
        public Image Asignacion
        {
            set { imgAsignacion = value; }
            get { return imgAsignacion; }
        }
        [Category("Imagenes")]
        [Description("Imagen de Fin del grafo")]
        public Image Fin {
            set { imgFin = value; }
            get { return imgFin; }
        }
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
    }
}
