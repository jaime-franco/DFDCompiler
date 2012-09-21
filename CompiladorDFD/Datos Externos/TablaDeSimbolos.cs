using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorDFD.Datos_Externos
{//En esta tabla se almacena las varibles encontradas dentro del programa esto e utilizado tanto por el 
 //Analizador lexico, sintactico y semantico para la generacion de cambios de tipos , ingresos y reconocimiento
// de variables y tipos de datos
    public class TablaDeSimbolos
    {//Diccionario en el que se almacena el nombre de la variable y sus tokenData que es la informacion
        //acerca de ella
        Dictionary<string, TokenData> Simbolos = new Dictionary<string, TokenData>();
        //Funcion para verificar si una variable ya se encuentra dentro de la tabla de simbolos
        public bool VerificarSimbolo(string nombre) {
            TokenData tempTokenData = new TokenData();
            if(Simbolos.TryGetValue(nombre,out tempTokenData))
                return true;
            else
                return false;

        }
        //Funcion para agregar nuevos simbolos con su respectiva dataToken
        public void AgregarToken(string nombre, TokenData tokenData) {
            Simbolos.Add(nombre, tokenData);
        }
        //Funcion para obtener los datos del token apartir del nombre de la variable que se 
        //Ingreso dentro de la tabla de simbolos
        public Token ObtenerToken(string nombre){
            TokenData tempTokenData = new TokenData();
            if (Simbolos.TryGetValue(nombre, out tempTokenData))
                return tempTokenData.tokenInfo;
            else
                return null;

        }
        //Funcion utilizada por el analizador semantico para cambiar el tipo de las variables una vez evaluado
        //Todas las asignaciones determinando asi el tipo que le corresponde
        public void CambiarTipo(string variable,int id) {
            Simbolos[variable].tokenInfo = ValoresGlobales.valores().tablaDeTokens.ObtenerIdToken(id);
        }
    }


}
