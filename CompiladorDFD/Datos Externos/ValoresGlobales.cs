using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorDFD.Datos_Externos
{
    public class ValoresGlobales
    {

                //Variables globales a utilizar por el compilador
            public  TablaDeSimbolos tablaDeSimbolos = new TablaDeSimbolos();
            public  TablaDeTokens tablaDeTokens = new TablaDeTokens();
            public  TablaDeErrores tablaDeErrores = new TablaDeErrores();
            public ElementoDFD elementoRaiz;
            private static ValoresGlobales instance = null;

            private ValoresGlobales() { }
            public void LimpiarDatos(){
                tablaDeSimbolos = new TablaDeSimbolos();
                tablaDeErrores = new TablaDeErrores();
                
            }
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



