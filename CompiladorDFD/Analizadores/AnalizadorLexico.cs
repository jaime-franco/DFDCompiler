using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorDFD.Datos_Externos;
namespace CompiladorDFD.Analizadores
{//Clase utilizada para realizar el Analisis Lexico sobre el Grafo (DFD)
    class AnalizadorLexico
    {
        int pos = 0;  //Posicion dentro de la cadena a analizar
        char[] codigo;
        string union = "";//String utilizado para unir las cadenas 
        public ElementoDFD tempElemento= null;
        public TokenData returnToken = new TokenData();
        //Funcion para agregar el codigo Fuente
        public void AgregarCodigo(string cod)
        {
            //Se convierte el string pasado a una coleccion de 
            //Caracteres para poder poder recorrer el arreglo.
            codigo = cod.ToCharArray();
            //Se hace un reset a la posicion de lectura
            pos = 0;
        }

        //Funcion para comenzar el analisis

        public bool ObtenerToken() {
            //Se verifica si ya se llego al final de los datos a analizar
            if (pos == codigo.Length) return false;
            else {
                //Mientras se encuentren caracters que leer
                while (pos < codigo.Length) {
                    if (IsNextToken())
                    {   //Se incrementa la posicion
                        pos++;
                    }
                    else { 
                        //Se verifica si es una letra o _
                        if (char.IsLetter(codigo[pos]) || codigo[pos] == '_') {
                            if(!verificarVariables())
                            {   Error error = new Error();
                               error.ErrorCustom("La variable "+ union + "No es valida","Lexico",tempElemento);
                               ValoresGlobales.valores().tablaDeErrores.AgregarError(error);
                            }
                            return true;
                        }else if(char.IsNumber(codigo[pos])){//Se verifica si es un numero
                            if (!verificarNumero()) {
                                Error error = new Error();
                                error.ErrorCustom("El siguiente parametro no es valido " + union +" en el elemento"+ tempElemento.tipo.ToString(), "Lexico", tempElemento);
                                ValoresGlobales.valores().tablaDeErrores.AgregarError(error);
                          
                            }
                            return true;
                        }
                        else if (codigo[pos] == '"')//Se verifica si es una cadena
                        {
                            if (!verificarCadena())
                            {
                                Error error = new Error();
                                error.ErrorCustom("La variable " + union + "No es valida", "Lexico", tempElemento);
                                ValoresGlobales.valores().tablaDeErrores.AgregarError(error);
                            }
                            return true;
                        }
                        else if (codigo[pos] == '(') {//Se ingresa como token "("
                            union = "(";
                            returnToken = new TokenData(union, ValoresGlobales.valores().tablaDeTokens.ObtenerToken(union));
                            pos++;
                            return true;
                        }
                        else if (codigo[pos] == ')')
                        {//Se ingresa como token ")"
                            union = ")";
                            returnToken = new TokenData(union, ValoresGlobales.valores().tablaDeTokens.ObtenerToken(union));
                            pos++;
                            return true;
                        }
                        else if (codigo[pos] == ',')
                        {//Se ingresa como token ","
                            union = ",";
                            returnToken = new TokenData(union, ValoresGlobales.valores().tablaDeTokens.ObtenerToken(union));
                            pos++;
                            return true;
                        }
                        else//De no ser ninguno de los anteriores se busca una concordancia con los operadores
                        {//Para ello se verifica si esta dentro de los operadores
                            if (!VerificarOperador())
                            {
                                Error error = new Error();
                                error.ErrorCustom("El caractrer " + union + "No es valida no se encuentra registrado", "Lexico", tempElemento);
                                ValoresGlobales.valores().tablaDeErrores.AgregarError(error);
                            }
                            else
                            {
                                return true;
                            }
                        }
                        }
                    }
                }
                return false;//Si no existe un token que retornar
            }

        private bool VerificarOperador()
        {//Verificacion de operadores Matematicos y Logicos
            returnToken = null;
            union = "";
            switch (codigo[pos])
            {
                case '-':
                    union += codigo[pos];
                    //if (codigo[pos + 1] == '-') union += codigo[pos];
                    break;
                case '+':
                    union += codigo[pos];
                    //if (codigo[pos + 1] == '+') union += codigo[pos];
                    break;
                case '/':
                    union += codigo[pos];
                    break;
                case '*':
                    union += codigo[pos];
                    break;
                case '>':
                    union += codigo[pos];
                    //if (codigo[pos + 1] == '>') union += codigo[pos];
                    if (codigo.Length > pos + 1)
                        if (codigo[pos + 1] == '=') union += codigo[++pos];
                    break;
                case '<':
                    union += codigo[pos];
                    //if (codigo[pos + 1] == '<') union += codigo[pos];
                    if (codigo.Length > pos + 1)
                        if (codigo[pos + 1] == '=') union += codigo[++pos];
                    break;
                case '=':
                    union += codigo[pos];
                    if (codigo.Length > pos+1)
                        if (codigo[pos + 1] == '=') union += codigo[++pos];
                    break;
                case '|':
                    union += codigo[pos];
                    if (codigo.Length > pos + 1)
                        if (codigo[pos + 1] == '|') union += codigo[++pos];
                    break;
                case '&':
                    union += codigo[pos];
                    if (codigo.Length > pos + 1)
                        if (codigo[pos + 1] == '&') union += codigo[++pos];
                    break;
                case '!':
                    union += codigo[pos];
                    if (codigo.Length > pos + 1)
                        if (codigo[pos + 1] == '=') union += codigo[++pos];
                    break;
                default:
                    pos++;
                    return false;
                    break;
            }
            //Se genera el token a regresar
            returnToken = new TokenData(union, ValoresGlobales.valores().tablaDeTokens.ObtenerToken(union));
            pos++;
            return true;

        }

        public bool verificarNumero() {
            returnToken = null;
            int punto = 0;
            union = "";
            union += codigo[pos]; //Se comienza a formar la palabras
            pos++;//Se pasa a la siguiente posicion
            if (pos < codigo.Length )
            {//Se verifica que no sea el final de la cadena
                //Ahora como comenzamos a verificar que sea una palabra 
                //Verificamos si es un numero  o "."
                while (char.IsDigit(codigo[pos]) || codigo[pos] == '.')
                {
                    if (codigo[pos] == '.') punto++;
                    //Procegimos a concatenar y verificar el siguiente caracter
                    union += codigo[pos];
                    pos++;
                    if (pos == codigo.Length) break; // si llegamos al final salimos de ella

                }
            }
            if (punto > 1) return false;
            returnToken = new TokenData(union, ValoresGlobales.valores().tablaDeTokens.ObtenerToken("numDecimal"));
            return true;
        }
        //Se verifica si se ha llegado al siguiente token o no
        public bool IsNextToken(){
            return codigo[pos] == ' ' || codigo[pos] == '\n' || codigo[pos] == '\t';
        }

        public bool verificarCadena() {
            returnToken = null;
            union = "";
            union += codigo[pos];//Se comienza a formar el codigo de la palabra
            //Se incremente la posicion de la palabra
            pos++;
            if (pos+1 > codigo.Length) return false;
            //Se recorre la cadena para verificar que sea un dato
            while (codigo[pos] != '"' && pos < codigo.Length)
            {

                if (pos < codigo.Length - 1)
                {
                    if (codigo[pos] == '\\' && codigo[pos + 1] == 'n')
                    {
                        union += '\n';
                        pos += 2;
                    }else
                    union += codigo[pos++];
                }
                else {
                    break;
                }
                
            }
            union+=codigo[pos];
            if (codigo[pos] != '"') return false;
            pos++;
            returnToken = new TokenData(union, ValoresGlobales.valores().tablaDeTokens.ObtenerToken("cadena"));
            return true;
        }
        public bool verificarVariables()
        {
            union = "";
            union += codigo[pos];//Se comienza a formar el codigo de la palabra
            //Se incremente la posicion de la palabra
            pos++;
            //Se recorre la cadena para verificar que sea un dato
            if (!(pos < codigo.Length)) goto continuar;
            while (codigo[pos] != ' ')
            {
                if (!IsNextToken())
                {
                    if (char.IsLetterOrDigit(codigo[pos]) || codigo[pos] == '_')
                    {//Se continua agregando datos 
                        union += codigo[pos++];
                        if (!(pos < codigo.Length)) break;
                    }
                    else
                    {   //Codigos de escape al encontrar otro caracter a la par
                        if (codigo[pos] == '=') break;
                        if (codigo[pos] == '+') break;
                        if (codigo[pos] == '-') break;
                        if (codigo[pos] == '*') break;
                        if (codigo[pos] == '/') break;
                        if (codigo[pos] == '(') break;
                        if (codigo[pos] == ')') break;
                        if (codigo[pos] == ',') break;
                        if (codigo[pos] == '>') break;
                        if (codigo[pos] == '<') break;
                        if (codigo[pos] == '!') break;
                        
                        else return false;
                    }
                }
                else
                {
                    break;
                }
            }
  continuar:
            //Se procede a verificar si las variables seran registradas o no en la tabla de simbolos
            if (ValoresGlobales.valores().tablaDeSimbolos.VerificarSimbolo(union))
            {//Se obtiene la informacion del token si esque se encontro dentro de la tabla de simbolos
                returnToken = new TokenData(union, ValoresGlobales.valores().tablaDeSimbolos.ObtenerToken(union));
            }else
            {//De no estar en la tabla de simbolos se procede a ingresarlo dentro de ella con los parametros 
             //De un identificador para poder darle seguimiento a dicha variable
                TokenData newToken = new TokenData(union, ValoresGlobales.valores().tablaDeTokens.ObtenerToken("identificador"));
                ValoresGlobales.valores().tablaDeSimbolos.AgregarToken(union,newToken);
                returnToken = new TokenData(union, newToken.tokenInfo);
            }
            return true;
        }
    }
}
