using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorDFD.Datos_Externos
{
    public class Token
    {
        public enum TipoToken
        {
            PR,//Palabra Reservada
            SS,//Signo de Separacion
            SES,//Signo Especial Separacion 
            OP,//Operador
            SCOM,//Signo de Comparacion
            SR,//Signo de Relacionador
            SP,//Signo de Puntuacion
            ID,//Identificador usado para variables
            NE,//Usado para numeros enteros
            ND,//Usado para numeros decimales
            SD,//Simbolo Desconocido
            CL,//Comentario Linea
            CP,//Comentario Parrafo
            CA,//Cadena
            CR,//Caracter
            VN,//Variable Numerica
            VC,//Variable Cadena
        }

        public int id;
        public string nombre;
        public TipoToken tipoToken;
        //Constructor de la funcion
        //Se asignan los parametros del token
        public Token(int idT, string nombreT, TipoToken tipoT)
        {
            id = idT;
            nombre = nombreT;
            tipoToken = tipoT;
        }
        public Token()
        {
        }
        //Estructura para guardar tipos de tokens que existen en el lexico definido
    }
    //Clase para manejar los datos del token
    public class TokenData {
        public string codigo; //Codigo que posee
        public Token tokenInfo;//Informacion mas detallada del token
        public TokenData tokenDataRef = null;//Referencia al token que le precede
        //Constructor de la clase para inicializar los componentes
        public TokenData(string dato,Token token) {
            codigo = dato;
            tokenInfo = token;
        }
        //Constructor vacio
        public TokenData() { }
       }
}
