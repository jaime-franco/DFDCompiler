using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorDFD.Datos_Externos
{//Clase con patron Singleton, que permite instanciar solo una vez la clase  y ademas de eso
 //Nos permite tener acceso a ella desde cualquier parte del codigo sin crear ningun objeto
    public class ValoresGlobales
    {

            //Variables globales a utilizar por el compilador
        //Tabla para almacenar un registro de todas las variables que se han ingresado 
        public  TablaDeSimbolos tablaDeSimbolos = new TablaDeSimbolos();
        //Tabla de tokens que posee todos los tokens a ser utilizados por el programa
        public  TablaDeTokens tablaDeTokens = new TablaDeTokens();
        //Tabla utilizada para almacenar todos los posibles errores dentro del compilador
        public  TablaDeErrores tablaDeErrores = new TablaDeErrores();
        //Referencia hacia el elemento inicial del grafo que representa toda la estructura del DFD
        public ElementoDFD elementoRaiz;
        //Varible utilizada para tener acceso a la instancia de la clase
        private static ValoresGlobales instance = null;
        //Constructor de la clase
        private ValoresGlobales() { }
        //Funcion para limpiar las tablas una vez se inicia una nueva fase de compilacion
        //y asi evitar problemas
        public void LimpiarDatos(){
            tablaDeSimbolos = new TablaDeSimbolos();
            tablaDeErrores = new TablaDeErrores();
                
        }
        //Funcion en patron singleton para instanciar la clase y tener acceso a ella 
        //desde cualquier parte del codigo
        public static ValoresGlobales valores()
        {
            if (instance == null)
            {
                instance = new ValoresGlobales();
            }
            return instance;
        }
    }
}



