using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace CompiladorDFD
{
    public class ElementoDFD
    {
        //-------------------------------------------------------------------------------------------------
        //                          Propiedas necesarias para el elemento a ser mostradas
        //-------------------------------------------------------------------------------------------------
        //Para definir las dimensiones
        public int width;
        public int height;
        //para definir puntos inicales
        public int top;
        public int left;
        //para difinir tipos de datos dentro del elemento DFD
        public Elemento tipo;
        public string datos;
        //Para definir caracteristicas explicitas dentro de los elementos
        public Color color;
        public int grosor;
        public bool visible;
        //Se crean las referncias hacia los posibles hijos que pueda obtener el elemento
        public ElementoDFD izquierda;
        public ElementoDFD derecha;
        //Elementos que no tengan difuracasiones
        public ElementoDFD centro;
        //Se crea una referencia hacia el padre del objeto
        public ElementoDFD padre;
        //-------------------------------------------------------------------------------------------------
        //                         Constructores de la clase
        //-------------------------------------------------------------------------------------------------
        public ElementoDFD() { 
        //No hace nada :D
        }

         //-------------------------------------------------------------------------------------------------
        //                         Funciones necesarias para el funcionamiento
        //-------------------------------------------------------------------------------------------------
      
        public bool VerificarInteraccion(int x,int y ){
            //Se verifica si concuerda en el eje de las y
            if( top < y && height > (y - top)) 
                //Luego se veridica si concuerda con el eje de las x
                if(left< x &&  width>(x-left)) 
                    return true;
            //De lo contrario no se hizo click dentro del objeto o el contenido del objeto
            return false;
        }
        
    }
}
