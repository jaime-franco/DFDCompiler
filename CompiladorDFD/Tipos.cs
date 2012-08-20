using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorDFD
{
 //Elementos utilizados para declarar el tipo de elemento que repredsentara
    public enum Elemento { 
    none,//No tiene un tipo definido
    inicio,//Inicio del grafo
    Asignacion,//Asignacion de variables
    Eif,//Estructura If
    EndIf,//Fin del if
    Efor,//Estructura for
    Endfor,//FIn de la estructura for
    Lectura,//Leer un dato de teclado
    fin
    }


    class Tipos
    {
    }
}
