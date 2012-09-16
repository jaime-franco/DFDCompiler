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
    {//Enum utilizado para saber el estado del control
        private enum Estado { 
            Normal,
            Agregando,
            Eliminando,
            Moviendo,
        }
        //Posiciones del Mouse dentro del control
        private int posMouseX=0;
        private int posMouseY=0;
        //Estado dentro del control segun accion a desempeniar
        private Estado estado;
        //Variable utilizada para saber cuando se esta agregando un elemento
        private ElementoDFD elementoAgregado = null;
        //Declaracion de variables a utilizar para controlar las lineas
        private Color colorLinea = Color.Black;
        private int grosorLinea = 1;
        private int grosorFiguras = 1;
        private Color colorFondo = Color.White;
        private Color colorLetra = Color.Black;
        private Color colorEliminar = Color.Red;
        private Font fontLetra = new Font("Arial", 8);
        //Declaracion de las variables a ser utilizadas por el control
        //private UCElementos raiz;//Raiz del grafo a crear desde un nodo inicio 
        private ElementoDFD elementoRaiz;//Raiz del grafo utilizado para tener una referencia hacia todos ya que es el inicio
        //Lista para utilizarse para recorrer todos los elementos creados

        //Variables para declarar los valores de las dimensiones de los elementos
        private Size tamanioAsignacion = new Size(120, 60);
        private Size tamanioIf = new Size(90, 60);
        private Size tamanioFor = new Size(140, 30);
        private Size tamanioWhile = new Size(120, 30);
        private Size tamanioInicio = new Size(60, 60);
        private Size tamanioFin = new Size(60, 60);
        private Size tamanioLectura = new Size(120, 60);
        private Size tamanioEscritura = new Size(120, 60);
        private Size tamanioEndIF = new Size(50, 0);
        private Size tamanioEndFor = new Size(45, 40);
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
        {   //Se colocan las barras de dezplazamiento
            this.AutoScrollMinSize = new Size(3000, 3000);
            this.HorizontalScroll.Enabled = true;
            this.HorizontalScroll.Minimum = 0;
            this.HorizontalScroll.Maximum = 3000;
            //Se crea el principio del grafo que se utilizara dentro del programa
            //se agrega el inicio
            //AgregarElemento(null, CrearElemento(Elemento.inicio, imgInicio));     
            AgregarElemento(null, null, CrearElementoDFD(Elemento.inicio));
            AgregarElemento(elementoRaiz, elementoRaiz.centro, CrearElementoDFD(Elemento.fin));
            //se agrega el final
            //AgregarElemento(raiz, CrearElemento(Elemento.fin, imgFin));
            //Se ajustan los elementos
            //ReajustarElementos(raiz);
            ReajustarElementos(elementoRaiz);
            Dibujar();
        }

        public void Iniciar() {
            this.HorizontalScroll.Value = 1500;
            ReajustarElementos(elementoRaiz);
        }
        //-------------------------------------------------------------------------------------
        //                          Funciones externas para realizar acciones sobre el control
        //-------------------------------------------------------------------------------------
        //Funcion para permitir agregar un elemnto dentro del grafo
        public void AgregandoElemento(Elemento tipo) {
            elementoAgregado = new ElementoDFD();
            elementoAgregado.width = 15;
            elementoAgregado.height = 15;
            elementoAgregado.tipo = tipo;
            estado = Estado.Agregando; 
        }
        public void Eliminar() {
            estado = Estado.Eliminando;
        }

        
        private bool ConcordanciaEjeX(int posX, ElementoDFD tempElemento, ElementoDFD tempElemento2) {
            //Funcion que verifica si el elemento esta entre los dos elementos dados dentro de el eje de X
            return (posX > tempElemento.left && posX < tempElemento.left + tempElemento.width) || (posX > tempElemento2.left && posX < tempElemento2.left + tempElemento2.width);
        }
        private bool ConcordanciaEjeY(int posY, ElementoDFD tempElemento,ElementoDFD tempElemento2) {
            return posY > tempElemento.top && posY < tempElemento2.top;
        }

        private void AgregarElmentoContinuo(ElementoDFD temp) {

            switch (elementoAgregado.tipo)
            {
                case Elemento.Eif:
                    ElementoDFD elementoIF = CrearElementoDFD(elementoAgregado.tipo);
                    AgregarElemento(temp, temp.centro, elementoIF);
                    AgregarElemento(elementoIF, elementoIF.centro, CrearElementoDFD(Elemento.EndIf));
                    break;
                case Elemento.Efor:
                        ElementoDFD elementoFor = CrearElementoDFD(elementoAgregado.tipo);
                        AgregarElemento(temp, temp.centro, elementoFor);
                        AgregarElemento(elementoFor, elementoFor.centro, CrearElementoDFD(Elemento.Endfor));
                        elementoFor.fin = elementoFor.centro;
                        break;
                default:
                    AgregarElemento(temp, temp.centro, CrearElementoDFD(elementoAgregado.tipo));
                    break;
            }
        
        }
        private void AgregarElementoDentroIf(ElementoDFD padre,ref ElementoDFD difuracacion)
        {
            if (difuracacion == null)
            {
                switch (elementoAgregado.tipo)
                {
                    case Elemento.Eif:
                        difuracacion = padre.centro;
                        break;
                    default:
                         difuracacion = padre.centro;
                        break;
                }
            }
                
                switch (elementoAgregado.tipo)
                {
                    case Elemento.Eif:
                        ElementoDFD elementoIF = CrearElementoDFD(elementoAgregado.tipo);
                        AgregarElemento(padre, difuracacion, elementoIF);
                        AgregarElemento(elementoIF, elementoIF.centro, CrearElementoDFD(Elemento.EndIf));
                        break;
                    case Elemento.Efor:
                        ElementoDFD elementoFor = CrearElementoDFD(elementoAgregado.tipo);
                        AgregarElemento(padre, difuracacion, elementoFor);
                        AgregarElemento(elementoFor, elementoFor.centro, CrearElementoDFD(Elemento.Endfor));
                        elementoFor.fin = elementoFor.centro;
                        break;
                    default:
                        AgregarElemento(padre, difuracacion, CrearElementoDFD(elementoAgregado.tipo));
                        break;
                }
           
        }

        private void Agregando_Click_if(ElementoDFD tempElemento) {
             //Se calcula las posiciones en las que se desea agregar el elemento
             int posX = elementoAgregado.left + elementoAgregado.width / 2;
             int posY = elementoAgregado.top + elementoAgregado.height / 2;
              
            //Variable temporal para almacenar una referencia 
            ElementoDFD temp = tempElemento;
            //Se verifica que el elemento no sea null
            if (temp != null) { 
                //se verifica si el elemento es un if
                if (temp.tipo == Elemento.Eif)
                { //Si se encuentra entre el if
                    if (ConcordanciaEjeY(posY, temp,temp.centro))
                    {   //El click fue a la derecha
                        if (temp.left + temp.width / 2 < posX)
                        {
                            if (temp.derecha == null)
                            {
                                AgregarElementoDentroIf(temp, ref temp.derecha);
                                return;
                            }
                            else {
                                if (ConcordanciaEjeY(posY, temp,temp.derecha))
                                {
                                    if(ConcordanciaEjeX(posX,temp,temp.derecha)){
                                        AgregarElementoDentroIf(temp, ref temp.derecha);
                                    return;
                                    }
                                }
                            }
                              Agregando_Click_if(temp.derecha);
                                return;
                        }//El click fue a la izquierda
                        else {
                            if (temp.izquierda == null)
                            {
                                AgregarElementoDentroIf(temp, ref temp.izquierda);
                                return;
                            }
                            else { 
                                if(ConcordanciaEjeY(posY,temp,temp.izquierda)){
                                    if(ConcordanciaEjeX(posX,temp,temp.izquierda)){
                                    AgregarElementoDentroIf(temp,ref temp.izquierda);
                                        return;
                                    }
                                }
                            }
                        }
                        Agregando_Click_if(temp.izquierda);
                        return;
                    }//De no ser asi no se encuentra dentro del if
                   
                   
                }
                else {
                    if (ConcordanciaEjeY(posY, temp, temp.centro))
                    {
                        if (ConcordanciaEjeX(posX, temp, temp.centro))
                        {
                            AgregarElmentoContinuo(temp);
                            return;
                        }
                    }
                }
                Agregando_Click_if(temp.centro);
            }
         
        }
        private void Agregando_Click() {
            if (elementoAgregado != null) { 
                ElementoDFD tempElemento = elementoRaiz;
                int posX = elementoAgregado.left + elementoAgregado.width / 2;
                int posY = elementoAgregado.top + elementoAgregado.height / 2;
                while (tempElemento.tipo != Elemento.fin) {
                    if( posY> tempElemento.top && posY<tempElemento.centro.top)

                        if (tempElemento.tipo == Elemento.Eif) {
                            Agregando_Click_if(tempElemento);
                            ReajustarElementos(elementoRaiz);

                        }else
                        if (ConcordanciaEjeX(posX,tempElemento,tempElemento.centro)){
                            AgregarElmentoContinuo(tempElemento);
                            ReajustarElementos(elementoRaiz);
                            break;
                        }
                    tempElemento = tempElemento.centro;
                }
                //Se limpia la bandera para agregar controles
                estado = Estado.Normal;
                elementoAgregado = null;
            } 
        }

        //------------------------------------------------------------------
        //          Funciones para eliminar los elementos
        //------------------------------------------------------------------
        private bool EliminarDifuracion(ElementoDFD tempElemento,ElementoDFD paro) {
            if (tempElemento == null) return false;
            if (tempElemento != paro) {
                EliminarElementos(tempElemento);
            }
            return true;
        }
        private bool EliminarElementos(ElementoDFD tempElemento) {
            if (tempElemento != null) {
                switch (tempElemento.tipo) { 
                    case Elemento.Eif:
                        //Eliminado elementos intermedios
                        EliminarDifuracion(tempElemento.izquierda, tempElemento.centro);
                        EliminarDifuracion(tempElemento.derecha, tempElemento);
                        EliminarElementos(tempElemento.centro);
                        //Eliminado elemento if
                        EliminarElemento(tempElemento);
                        break;
                    case Elemento.Efor:
                        ElementoDFD temp = tempElemento.centro;
                        //Mientras no llege al end if
                while (temp != tempElemento.fin) {
                            ElementoDFD sig;
                            if (temp.tipo == Elemento.Eif) sig = temp.centro.centro;
                            else if (temp.tipo == Elemento.Efor) sig = temp.fin.centro;
                            else sig = temp.centro;     
                            
                            EliminarElementos(temp);
                            temp = sig;
                        }
                        EliminarElemento(tempElemento.fin);
                        EliminarElemento(tempElemento);
                        break;
                    default:
                        EliminarElemento(tempElemento);
                         break;
                
                }
                return true;
            }
            return false;
        }
        private void EliminarElemento(ElementoDFD tempElemento) {
            if (tempElemento.padre.tipo == Elemento.Eif && tempElemento.tipo != Elemento.EndIf)
            {
                if (tempElemento.centro == tempElemento.padre.centro)
                {
                    if (tempElemento.padre.derecha == tempElemento) tempElemento.padre.derecha = null;
                    else if (tempElemento.padre.izquierda == tempElemento) tempElemento.padre.izquierda = null;

                }
                else
                {
                    if (tempElemento.padre.derecha == tempElemento) tempElemento.padre.derecha = tempElemento.centro;
                    else if (tempElemento.padre.izquierda == tempElemento) tempElemento.padre.derecha = tempElemento.centro;

                }
                tempElemento.centro.padre = tempElemento.padre;
                listadoElementos.Remove(tempElemento);
                tempElemento = null;
            }
            else
            {   
                tempElemento.padre.centro = tempElemento.centro;
                tempElemento.centro.padre = tempElemento.padre;
                listadoElementos.Remove(tempElemento);
                tempElemento = null;
            }
        }
        //------------------------------------------------------------------
        //          Funciones para crear los elementos
        //------------------------------------------------------------------
        private ElementoDFD CrearElementoDFD(Elemento tipo) {
        //Se crea el elemeto a manejar
            ElementoDFD tempElemento = new ElementoDFD();

            switch (tipo) { 
                case Elemento.inicio:
                    tempElemento.Tamanio(tamanioInicio);
                    break;
                case Elemento.fin:
                    tempElemento.Tamanio(tamanioFin);
                break;
                case Elemento.Asignacion:
                    tempElemento.Tamanio(tamanioAsignacion);
                    break;
                case Elemento.Lectura:
                    tempElemento.Tamanio(tamanioLectura);
                    break;
                case Elemento.Eif:
                    tempElemento.Tamanio(tamanioIf);
                    break;
                case Elemento.EndIf:
                    tempElemento.Tamanio(tamanioEndIF);
                    break;
                case Elemento.Efor:
                    tempElemento.Tamanio(tamanioFor);
                    break;
                case Elemento.Endfor:
                    tempElemento.Tamanio(tamanioEndFor);
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

        private void UCDFD_MouseDoubleClick(object sender, MouseEventArgs e)
        {   ElementoDFD tempElemento;
            switch(estado){
                case Estado.Normal:
                    
                    if ((tempElemento = VerificarAccion(e.X, e.Y)) != null)
                    {
                        switch (tempElemento.tipo) { 
                            case Elemento.Asignacion:
                                FrmAsignacion frmAsignacion = new FrmAsignacion();
                                frmAsignacion.PasarElemento( tempElemento);
                                frmAsignacion.ShowDialog();
                                break;
                            case Elemento.Eif:
                                FrmIf frmIf = new FrmIf();
                                frmIf.pasarElemento(tempElemento);
                                frmIf.ShowDialog();
                                break;
                        }
                        
                    }  
                break;
            }

        }
        //--------------------------------------------------------------------------------------
        //              Acciones hechas para manejar a los tipos de controles
        //--------------------------------------------------------------------------------------


        //-------------------------------------------------------------------------------------
        //  Funciones a utilizar para verificar los objetos sobre los que se ejecutan acciones
        //-------------------------------------------------------------------------------------
        private ElementoDFD VerificarAccion(int x , int y)
        {
            ElementoDFD resultante = null;
            foreach (ElementoDFD temp in listadoElementos)
            {
                if (temp.VerificarInteraccion(x, y))
                {
                    resultante = temp;
                }
            }
            return resultante;
                
        }
       
        //---------------------------------------------------------------------
        //  Funciones a utilizar para reacomodar los elementos dentro del grafo
        //---------------------------------------------------------------------
        void ReajustarElementos(ElementoDFD elemento)
        {
            ElementoDFD tempElemento = elemento;
            //Dezplazamientos realizados
            int espaciado = 30;
            int x = this.Width / 2+ this.AutoScrollPosition.X + 3000/2 ;           //Indica el inicio del dezplazamiento
            int y;
            
            y = 10 + this.AutoScrollPosition.Y; //centro de la ventana contenedora 
            int yMax = 10;
            int xMax = x;//Variable a utilizar para los scrools
            while (tempElemento != null)
            {//Si el elemento es diferente de visible no se dibuja ni integra dentro del diagrama
             //Evitando que se ajuste dentro de este
                if (tempElemento.visible == true)
                {
                    switch (tempElemento.tipo)
                    {
                        case Elemento.Eif:
                                tempElemento.left = x - tempElemento.width / 2;
                                tempElemento.top = y;
                                y += tempElemento.height + espaciado;

                                ReajustarIf(tempElemento, x, ref y, espaciado);
                                tempElemento= tempElemento.centro;
                            break;
                        default:
                            tempElemento.left = x - tempElemento.width / 2;
                            tempElemento.top = y;
                            y += tempElemento.height + espaciado;
                            break;
                    }
                }
                tempElemento = tempElemento.centro;
            }
            yMax= y - this.AutoScrollPosition.Y;
            //Configuracion de los scrools a utilizar
            
        }
        private void ReajustarIf(ElementoDFD tempElemento,int centro,ref int y,int espaciado) {
            ElementoDFD temp = tempElemento.izquierda;
            int separacion= CalcularSeparacionIzquierda(tempElemento.izquierda);
            int tempY = y ;
            //Calculando linea izquierda
            if (temp != null)
            {
                while (temp != tempElemento.centro)
                {

                    temp.left = centro - separacion - temp.width / 2;
                    temp.top = tempY;
                    tempY += temp.height + espaciado;  
                    if (temp.tipo == Elemento.Eif) ReajustarIf(temp, temp.left + temp.width / 2, ref tempY, espaciado);
                    temp = temp.centro;
                }
            }
            int tempY2 = y;
            //Calculando linea derecha
            temp = tempElemento.derecha;
            if (temp != null)
            {   
                separacion = CalcularSeparacionDerecha(tempElemento.derecha);
                while (temp != tempElemento.centro)
                {

                    temp.left = centro + separacion - temp.width / 2;
                    temp.top = tempY2;
                    tempY2 += temp.height + espaciado;
                    if (temp.tipo == Elemento.Eif) ReajustarIf(temp, temp.left + temp.width / 2, ref tempY2, espaciado);
                    temp = temp.centro;
                
                    
                }
            }
            if (tempY > tempY2)
            {
                y = tempY;
            }
            else {
                y = tempY2;
            }
            
            //Confugurando el End if

            tempElemento.centro.left =  tempElemento.left+ tempElemento.width/2- tempElemento.centro.width/2;
            tempElemento.centro.top = y;
            y += tempElemento.centro.height + espaciado;
                

        }
        //Se calcualan las separaciones dentro de lo que son los if
        private int CalcularSeparacionIzquierda(ElementoDFD tempElemento) {
           int separacion = 0;
           if (tempElemento == null) return 20;
           ElementoDFD temp = tempElemento;
           while (temp != tempElemento.centro) {
               if (temp.padre.tipo == Elemento.Eif) separacion += temp.width;
               if (temp.tipo == Elemento.Eif) separacion +=  CalcularSeparacionIzquierda(temp.derecha);
               temp = temp.centro;
           }
           return separacion;
        }
        private int CalcularSeparacionDerecha(ElementoDFD tempElemento)
        {
            int separacion = 0;
            if (tempElemento == null) return 20;
            ElementoDFD temp = tempElemento;
            while (temp != tempElemento.centro )
            {
                if (temp.padre.tipo == Elemento.Eif) separacion += temp.width;
                if (temp.tipo == Elemento.Eif) separacion += CalcularSeparacionDerecha(temp.izquierda);
                temp = temp.centro;
            }
            return separacion;
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
            //Se crea el lapiza a ser utilizado para dibujar las lineas
            Pen lapiz = new Pen(colorLinea, grosorLinea);
            Pen lapizFiguras = new Pen(colorLinea, grosorFiguras);
            Pen lapizEliminar = new Pen(colorEliminar, grosorFiguras);
            //Se recorre todo el grafo hasta llegar al elemento final del grafo que 
            //es de tipo fin indicando el fin del dibujo del grafo
            while (tempElemento.tipo != Elemento.fin)
            {
                //Se dibuja el elemento a utilizar
                if (estado == Estado.Eliminando && tempElemento.tipo!= Elemento.inicio &&tempElemento.VerificarInteraccion(posMouseX,posMouseY))
                    DibujarElemento(ref bg, tempElemento, lapizEliminar);                
                else
                    DibujarElemento(ref bg, tempElemento, lapizFiguras);
                
                //Se dibujan elementos lineales es decir que solo tienen un camnino central
                if (tempElemento.centro.tipo != Elemento.none)
                {
                    DibujarLinea(ref bg, tempElemento, lapiz);
                }
                else
                {

                }
                tempElemento = tempElemento.centro;
            }
            DibujarElemento(ref bg, tempElemento, lapizFiguras);
                        //Se dibuja el control que se desea agregar si es que se posee ese evento habilitado
            if (estado==Estado.Agregando && elementoAgregado != null) DibujarElemento(ref bg, elementoAgregado, lapizFiguras);

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
        private void DibujarLinea(ref BufferedGraphics tempbf, ElementoDFD tempElemento, Pen lapiz ) {
            //punto inicial y punto final
            Point puntoInicial;
            Point puntoFinal;
            switch (tempElemento.tipo) { 
                case Elemento.Eif:
                    //Elementos hacia la izquierda
                    
                    if (tempElemento.izquierda == null)
                    {
                        lapiz.EndCap = LineCap.NoAnchor;

                        
                        Point izq = new Point(tempElemento.left,tempElemento.top+tempElemento.height/2);
                        Point izqPlus = new Point(izq.X - 10,izq.Y);
                        Point izqDown = new Point(izqPlus.X, tempElemento.centro.top);
                        Point izqFinal = new Point(tempElemento.centro.left,tempElemento.centro.top);
                        Point[] izqPoints = {izq,izqPlus,izqDown,izqFinal};
                        bg.Graphics.DrawLines(lapiz, izqPoints);
                    }
                    else {
                        lapiz.EndCap = LineCap.ArrowAnchor;
                        Point izq2 = new Point(tempElemento.left, tempElemento.top + tempElemento.height / 2);
                        Point izqPlus2 = new Point(tempElemento.izquierda.left + tempElemento.izquierda.width / 2, tempElemento.top + tempElemento.height / 2);
                        Point izqDown = new Point(izqPlus2.X, tempElemento.izquierda.top);
                        Point[] izqPoints2 = {  izq2,izqPlus2, izqDown };
                        bg.Graphics.DrawLines(lapiz, izqPoints2);
                    }


                    //Elementos hacia la derecha
                    if (tempElemento.derecha == null)
                    {
                        lapiz.EndCap = LineCap.NoAnchor;
                        Point der = new Point(tempElemento.left + tempElemento.width, tempElemento.height / 2 + tempElemento.top);
                        Point derPlus = new Point(der.X+10, der.Y);
                        Point derDown = new Point(derPlus.X,tempElemento.centro.top);
                        Point derFinal = new Point(tempElemento.centro.left,tempElemento.centro.top); 
                        Point[] derPoints = {der,derPlus,derDown,derFinal};
                        bg.Graphics.DrawLines(lapiz, derPoints);
                    }

                    else {
                        lapiz.EndCap = LineCap.ArrowAnchor;
                        Point der2 = new Point(tempElemento.left + tempElemento.width, tempElemento.height / 2 + tempElemento.top);
                        Point derPlus2 = new Point(tempElemento.derecha.left + tempElemento.derecha.width / 2, tempElemento.top + tempElemento.height / 2);
                        Point derDown2 = new Point(derPlus2.X, tempElemento.derecha.top);
                        Point[] derPoints2 = { der2,derPlus2, derDown2 };
                        bg.Graphics.DrawLines(lapiz, derPoints2);
                    }
                    break;
                default:
                    //Se agrega al lapiz al final una flecha
                    lapiz.EndCap = LineCap.ArrowAnchor;
                    //Codigo para dibujar lineas

                    //Se calculan los puntos a utilizar para redibujar la linea
                    puntoInicial = new Point(tempElemento.width / 2 + tempElemento.left, tempElemento.height + tempElemento.top);
                    //variable temporal para almacenar el elemento del punto final
                    ElementoDFD tempElemento2 = tempElemento.centro;
                    //while (tempElemento2.tipo!= Elemento.fin && tempElemento2.visible != true) tempElemento2 = tempElemento2.centro;
                    //Se calcula el punto final a utilizar para realizar el dibujo de la linea
                    if (tempElemento2.tipo == Elemento.EndIf) {
                        lapiz.EndCap = LineCap.NoAnchor;
                        Point segundo = new Point(puntoInicial.X, tempElemento2.top);
                        Point final = new Point(tempElemento2.left, tempElemento2.top);
                        Point[] tempPoins = {puntoInicial,segundo,final};
                        bg.Graphics.DrawLines(lapiz, tempPoins);
                    }
                    else
                    {
                        puntoFinal = new Point(tempElemento2.width / 2 + tempElemento2.left, tempElemento2.top);
                        bg.Graphics.DrawLine(lapiz, puntoInicial, puntoFinal);
                    }
                    break;

            }
        }

        private void DibujarElemento(ref BufferedGraphics tempbf, ElementoDFD tempElemento, Pen tempPen)
        {
            if (tempElemento.visible == false) return;//si el elemto es invisible no se dibuja y se regresa
            //Rectangulo utilizado por la funcion de dibujo
            Rectangle tempRectangle = new Rectangle(tempElemento.left, tempElemento.top, tempElemento.width, tempElemento.height);
            StringFormat formato =new StringFormat();
            formato.Alignment= StringAlignment.Center;
            SolidBrush brocha = new SolidBrush(colorLetra);

            switch (tempElemento.tipo)
            {   
                case Elemento.inicio:
                    tempbf.Graphics.DrawEllipse(tempPen, tempRectangle);
                    //Agregando Texto al control
                    tempbf.Graphics.DrawString("\n\nInicio", fontLetra, brocha, tempRectangle, formato);
                    break;
                case Elemento.Asignacion:
                    tempbf.Graphics.DrawRectangle(tempPen, tempRectangle);
                    //Agregando Texto al control
                    tempbf.Graphics.DrawString("\n"+tempElemento.datos, fontLetra, brocha, tempRectangle, formato);
                    break;
                case Elemento.Lectura:
                    Point sizq = new Point(tempElemento.left,tempElemento.top+tempElemento.height/2);
                    Point sder = new Point(tempElemento.left+tempElemento.width,tempElemento.top);
                    Point ider = new Point(tempElemento.left+tempElemento.width,tempElemento.top+tempElemento.height);
                    Point iizq = new Point(tempElemento.left,tempElemento.top+tempElemento.height);
                    Point[] lect = { sizq, sder, ider, iizq };
                    tempbf.Graphics.DrawPolygon(tempPen, lect);
                    break;
                case Elemento.Eif:
                    Point sup = new Point(tempElemento.left + tempElemento.width / 2, tempElemento.top);
                    Point izq = new Point(tempElemento.left, tempElemento.top + tempElemento.height / 2);
                    Point inf = new Point(tempElemento.left + tempElemento.width / 2, tempElemento.top + tempElemento.height);
                    Point der = new Point(tempElemento.left + tempElemento.width, tempElemento.top + tempElemento.height / 2);
                    Point[] Eif = { sup, izq, inf, der };
                    tempbf.Graphics.DrawPolygon(tempPen, Eif);
                    tempRectangle = new Rectangle(tempElemento.left+10, tempElemento.top, tempElemento.width-10, tempElemento.height);
                    tempbf.Graphics.DrawString("\n\n" + tempElemento.datos, fontLetra, brocha, tempRectangle, formato);
                    tempRectangle = new Rectangle(tempElemento.left -20, tempElemento.top, 20, 20);
                    tempbf.Graphics.DrawString("Si" , fontLetra, brocha, tempRectangle, formato);
                    tempRectangle = new Rectangle(tempElemento.left + tempElemento.width+5, tempElemento.top, 20, 20);
                    tempbf.Graphics.DrawString("No" , fontLetra, brocha, tempRectangle, formato);

                    ElementoDFD temp = tempElemento.izquierda;
                    Pen lapizFiguras = new Pen(colorLinea, grosorFiguras);
                    Pen lapiz = new Pen(colorLinea, grosorLinea);
                    Pen lapizEliminar = new Pen(colorEliminar, grosorFiguras);
                    if (temp != null) {
                        while (temp != tempElemento.centro) {
                            //Se dibuja el elemento a utilizar
                            if (estado == Estado.Eliminando && temp.tipo != Elemento.inicio && temp.VerificarInteraccion(posMouseX, posMouseY))
                                DibujarElemento(ref bg, temp, lapizEliminar);
                            else
                                DibujarElemento(ref bg, temp, lapizFiguras);
                

                            DibujarLinea(ref tempbf, temp, lapiz);
                            temp = temp.centro;
                        }
                    }
                    temp = tempElemento.derecha;
                    if (temp != null) {
                        while (temp != tempElemento.centro)
                        {

                            //Se dibuja el elemento a utilizar
                            if (estado == Estado.Eliminando && temp.tipo != Elemento.inicio && temp.VerificarInteraccion(posMouseX, posMouseY))
                                DibujarElemento(ref bg, temp, lapizEliminar);
                            else
                                DibujarElemento(ref bg, temp, lapizFiguras);
                

                            DibujarLinea(ref tempbf, temp, lapiz);
                            temp = temp.centro;
                        }
                    }
                    break;
                case Elemento.Efor:
                    Point P1 = new Point(tempElemento.left, tempElemento.height / 2 + tempElemento.top);
                    Point P2 = new Point(P1.X + tempElemento.width / 10, tempElemento.top);
                    Point P3 = new Point(P1.X + tempElemento.width - tempElemento.width / 10, tempElemento.top);
                    Point P4 = new Point(P1.X + tempElemento.width, P1.Y);
                    Point P5 = new Point(P3.X, tempElemento.top + tempElemento.height);
                    Point P6 = new Point(P2.X, P5.Y);
                    Point[] Efor = { P1, P2, P3, P4, P5, P6 };
                    tempbf.Graphics.DrawPolygon(tempPen, Efor);
                    break;
                case Elemento.Endfor:
                     tempbf.Graphics.DrawEllipse(tempPen, tempRectangle);
                    //Agregando Texto al control
                    tempbf.Graphics.DrawString("\nFin For", fontLetra, brocha, tempRectangle, formato);
                  
                    break;
                case Elemento.fin:
                    tempbf.Graphics.DrawEllipse(tempPen, tempRectangle);
                    //Agregando Texto al control
                    tempbf.Graphics.DrawString("\n\nFin", fontLetra, brocha, tempRectangle, formato);
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

        [Category("Dimensiones")]
        [Description("Dimensiones del elemento Asignacion")]
        public Size DimensionAsignacion
        {
            set { tamanioAsignacion = value; }
            get { return tamanioAsignacion; }
        }

        [Category("Dimensiones")]
        [Description("Dimensiones del elemento If")]
        public Size DimensionIf
        {
            set { tamanioIf = value; }
            get { return tamanioIf; }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        private void TimerDibujar_Tick(object sender, EventArgs e)
        {
            Dibujar();

        }

        private void UCDFD_DoubleClick(object sender, EventArgs e)
        {
         
        }

        
        //Funcion utilizada para poder reajustar los elementos dentro del contenedor cada vez que realice un movimiento dentro del scroll
        private void UCDFD_Scroll(object sender, ScrollEventArgs e)
        {
            ReajustarElementos(elementoRaiz);
        }

        private void UCDFD_MouseMove(object sender, MouseEventArgs e)
        {
            switch (estado)
            {
                case Estado.Agregando:
                    if (elementoAgregado != null)
                    {   //se calcula la posicion que tendria que tener dentro de la ventana 
                        elementoAgregado.left = e.X - elementoAgregado.width / 2;
                        elementoAgregado.top = e.Y - elementoAgregado.height / 2;
                    }
                    break;
                case Estado.Eliminando:
                    posMouseX = e.X;
                    posMouseY = e.Y;
                    break;  
            }
            
        }

        private void UCDFD_Click(object sender, EventArgs e)
        {
           
          
        }

        private void UCDFD_MouseEnter(object sender, EventArgs e)
        {
            switch (estado)
            {
                case Estado.Agregando:
                    if (elementoAgregado != null) elementoAgregado.visible = true;
                    break;
            }

                        

        }

        private void UCDFD_MouseLeave(object sender, EventArgs e)
        {

            switch (estado) { 
                case Estado.Agregando:
                    if (elementoAgregado != null) elementoAgregado.visible = false;
                    break;
            }
            
            
        }

        private void UCDFD_MouseClick(object sender, MouseEventArgs e)
        {

            ElementoDFD tempElemento;
                 switch (estado)
            {
                case Estado.Agregando:
                    Agregando_Click();
                    break;
                case Estado.Eliminando:
                         if((tempElemento= VerificarAccion(e.X,e.Y))!=null){
                             if (tempElemento.tipo != Elemento.inicio && tempElemento.tipo != Elemento.fin && tempElemento.tipo!= Elemento.EndIf)
                                 EliminarElementos(tempElemento);
                                 estado=Estado.Normal;
                                 ReajustarElementos(elementoRaiz);
                         }
                    break;
            }
         }
    }
}