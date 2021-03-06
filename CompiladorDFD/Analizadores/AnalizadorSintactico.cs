﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorDFD.Datos_Externos;
namespace CompiladorDFD.Analizadores
{
    class AnalizadorSintactico
    {   //Elemento raiz del grafo
        private ElementoDFD raiz;
        private ElementoDFD tempElemento;
        //Elemento para realizar el analisis lexico
        private AnalizadorLexico analizadorLexico = new AnalizadorLexico();
        public AnalizadorSintactico() { }    
        //Se recorre todo el arbol y se va verificando los elementos agregados a este
        public void GenerarArbol() {
            Stack<ElementoDFD> elementoIf = new Stack<ElementoDFD>();
            tempElemento = ValoresGlobales.valores().elementoRaiz;
            while(tempElemento.tipo != Elemento.fin){
                switch (tempElemento.tipo) { 
                    case Elemento.Asignacion:
                        //Se limpian las referencias del arbol
                        tempElemento.tokenDataRef.Clear();
                        //Se obtiene los parametros de las cadenas para poder proceder a realizar el analisis lexico
                        if (tempElemento.datos != null)
                        {
                            string[] cadena = tempElemento.datos.Split('\n');
                            if (cadena.Length > 1) tempElemento.tokenDataRef.Add(SintaxisAsignacion(cadena[0])); ;
                            if (cadena.Length > 2) tempElemento.tokenDataRef.Add(SintaxisAsignacion(cadena[1])); ;
                            if (cadena.Length > 3) tempElemento.tokenDataRef.Add(SintaxisAsignacion(cadena[2])); ;
                        } 
                        break;
                    case Elemento.Escritura:
                        tempElemento.tokenDataRef.Clear();
                        tempElemento.tokenDataRef.Add( SintaxisExpresionSalida(tempElemento.datos));
                        break;
                    case Elemento.Lectura:
                        tempElemento.tokenDataRef.Clear();
                        string[] cadenaLectura = tempElemento.datos.Split(',');
                        int i = 0;
                        while (i < cadenaLectura.Length) tempElemento.tokenDataRef.Add(SintaxisExpresionLectura(cadenaLectura[i++]));
                        break;
                    case Elemento.Eif:
                        tempElemento.tokenDataRef.Clear();
                        //Se agregan los datos para que el analizador trabaje
                        analizadorLexico.AgregarCodigo(tempElemento.datos);
                        analizadorLexico.tempElemento = tempElemento;
                        //Se procede a verifiar la sintaxis
                        tempElemento.tokenDataRef.Add(SintaxisExpresionIf());
                        if (analizadorLexico.returnToken != null) 
                        {//Se verifica si el token ingresado es uno de los esperados
                            switch (analizadorLexico.returnToken.tokenInfo.id)
                            {
                                case 38: // ==
                                case 39: // <=
                                case 40: // >=
                                case 41: // <
                                case 42: // >
                                case 43: // !=
                                    tempElemento.tokenDataRef.Add(analizadorLexico.returnToken);
                                    break;
                                default:
                                    Error error2 = new Error();
                                    error2.NoSeEsperaba(analizadorLexico.returnToken.tokenInfo.id, "Sintactico", tempElemento);
                                    ValoresGlobales.valores().tablaDeErrores.AgregarError(error2);
                                    break;
                            }
                            tempElemento.tokenDataRef.Add(SintaxisExpresionIf());
                            if (tempElemento.derecha != null)
                            {
                                tempElemento = tempElemento.derecha;
                                elementoIf.Push(tempElemento);
                                continue;
                            }
                            else if (tempElemento.izquierda != null)
                            {
                                tempElemento = tempElemento.izquierda;
                                elementoIf.Push(tempElemento);
                                continue;
                            }
                        } 
                        else
                        {
                            Error error = new Error();
                            error.ErrorCustom("No se han obtendido la gramatica esperada 'EXP COMPARADOR EXP' en el elemento" + tempElemento.tipo.ToString(), "Sintactico", tempElemento);
                            ValoresGlobales.valores().tablaDeErrores.AgregarError(error);
                        }
                        break;
                    case Elemento.EndIf:
                        if (elementoIf.Count > 0)
                        {
                            if (tempElemento.padre.derecha == elementoIf.Peek())
                            {
                                tempElemento = tempElemento.padre.izquierda;
                                elementoIf.Pop();
                                continue;
                            }
                        }
                        break;
                    case Elemento.EWhile:
                         tempElemento.tokenDataRef.Clear();
                        //Se agregan los datos para que el analizador trabaje
                        analizadorLexico.AgregarCodigo(tempElemento.datos);
                        analizadorLexico.tempElemento = tempElemento;
                        //Se procede a verifiar la sintaxis
                        tempElemento.tokenDataRef.Add(SintaxisExpresionIf());
                        if (analizadorLexico.returnToken != null)
                        {//Se verifica si el token ingresado es uno de los esperados
                            switch (analizadorLexico.returnToken.tokenInfo.id)
                            {
                                case 38: // ==
                                case 39: // <=
                                case 40: // >=
                                case 41: // <
                                case 42: // >
                                case 43: // !=
                                    tempElemento.tokenDataRef.Add(analizadorLexico.returnToken);
                                    break;
                                default:
                                    Error error2 = new Error();
                                    error2.NoSeEsperaba(analizadorLexico.returnToken.tokenInfo.id, "Sintactico", tempElemento);
                                    ValoresGlobales.valores().tablaDeErrores.AgregarError(error2);
                                    break;
                            }
                            tempElemento.tokenDataRef.Add(SintaxisExpresionIf());
                        }
                        else
                        {
                            Error error = new Error();
                            error.ErrorCustom("No se han obtendido la gramatica esperada 'EXP COMPARADOR EXP' en el elemento" + tempElemento.tipo.ToString(), "Sintactico", tempElemento);
                            ValoresGlobales.valores().tablaDeErrores.AgregarError(error);
                        }
                        break;
                    default:
                        break;
                }
                tempElemento = tempElemento.centro;
            }
        }

        private TokenData SintaxisIf(string cadena) {
            TokenData returnTokenData = new TokenData();
            //Se agrega el codigo a ser analizado por el analizador lexico
            analizadorLexico.tempElemento = tempElemento;
            analizadorLexico.AgregarCodigo(cadena);
            
            return returnTokenData;
        }

        private TokenData SintaxisAsignacion(string cadena) {
            TokenData returnTokenData = new TokenData();
            //Se agrega el codigo a ser analizado por el analizador lexico
            analizadorLexico.tempElemento = tempElemento;
            analizadorLexico.AgregarCodigo(cadena);
            //Variable temporal para almacenar el tokendata
            TokenData tempTokenData;
            int[] patron = { 53, 37 };
            int posPatron = 0;
            bool    er = true;
            while (analizadorLexico.ObtenerToken()) {
                tempTokenData =analizadorLexico.returnToken;
                if (posPatron  < patron.Length)
                {
                    if (patron[posPatron] == tempTokenData.tokenInfo.id)
                    {
                        if(posPatron==0) returnTokenData = tempTokenData;
                        else returnTokenData.tokenDataRef = tempTokenData;
                        posPatron++;
                    }
                    else { 
                        //Agregando el error correspondiente
                        ValoresGlobales.valores().tablaDeErrores.errores.Add(new Error(patron[posPatron],"Sintactico",tempElemento,tempTokenData));
                    }
                }
                else {
                    returnTokenData.tokenDataRef.tokenDataRef= SintaxisExpresionesAsignacion();
                    if(returnTokenData.tokenDataRef.tokenDataRef != null) er = false;
                }
            }
            if (er == true) {
                Error error = new Error();
                error.ErrorCustom("Expresion ingresada incorrecta dentro del elemento" + tempElemento.tipo.ToString(), "Sintactico", tempElemento);
                ValoresGlobales.valores().tablaDeErrores.AgregarError(error);
            }
            return returnTokenData;
        }

        public void AgregarTokenData(ref TokenData tempRaiz, ref TokenData temp){
            if(temp==null && tempRaiz==null){
                //Se coloca el elemento raiz 
                tempRaiz = analizadorLexico.returnToken;
                temp=analizadorLexico.returnToken;
            }else{
                //Agregamos el nuevo elemento al arbol y luego nos movemos para dejar espacio
                //para el siguiete elemento
                temp.tokenDataRef=analizadorLexico.returnToken;
                temp= temp.tokenDataRef;
            }
        }
        //Nos lleva hasta el final de la lista
        private void AvanzarFinal(TokenData tempToken){
            while (tempToken.tokenDataRef != null)
                tempToken = tempToken.tokenDataRef;
        }

        private TokenData SintaxisExpresionLectura(string cadena) {
            analizadorLexico.AgregarCodigo(cadena);
            analizadorLexico.tempElemento = tempElemento;

            TokenData raizReturn = null;
            TokenData tempReturn = null;
            Stack<int> obligatorio = new Stack<int>();
            List<int> opcional = new List<int>();
            //Valores opcionales iniciales
            opcional.Add(53);//variables
            opcional.Add(58);//Cadena
            analizadorLexico.ObtenerToken();
            do
            {
                if (opcional.Contains(analizadorLexico.returnToken.tokenInfo.id))
                {
                    switch (analizadorLexico.returnToken.tokenInfo.id)
                    {
                        case 58://cadenas
                            opcional.Clear();
                            opcional.Add(29);
                            AgregarTokenData(ref raizReturn, ref tempReturn);
                            //Se colocan los datos obligatorios dentro del stack
                            obligatorio.Push(29);
                            break;
                        case 53:// Variables
                        
                            //Se colocan los valores opcionales a continuacion
                            opcional.Clear();
                            AgregarTokenData(ref raizReturn, ref tempReturn);
                            //Se colocan los datos obligatorios dentro del stack
                            if (obligatorio.Count > 0)
                            {
                                 if (obligatorio.Peek() == 53) obligatorio.Pop();
                            }
                            break;
                        case 29:
                            //Se colocan los valores opcionales a continuacion
                            opcional.Clear();
                            opcional.Add(53);//variables
                            AgregarTokenData(ref raizReturn, ref tempReturn);
                            //Datos que son obligatorios
                            if (obligatorio.Count > 0)
                            {
                                if (obligatorio.Peek() == 29) obligatorio.Pop();
                            }
                            obligatorio.Push(53);
                            break;
                        default:
                            Error error2 = new Error();
                            error2.NoSeEsperaba(analizadorLexico.returnToken.tokenInfo.id, "Sintactico", tempElemento);
                            ValoresGlobales.valores().tablaDeErrores.AgregarError(error2);
                            break;
                    }
                }
                else
                {
                    Error error2 = new Error();
                    error2.NoSeEsperaba(analizadorLexico.returnToken.tokenInfo.id, "Sintactico", tempElemento);
                    ValoresGlobales.valores().tablaDeErrores.AgregarError(error2);
                    break;

                }
                //Mientras existan tokens
            } while (analizadorLexico.ObtenerToken());
            if (obligatorio.Count > 0)
            {
                Error error = new Error();
                error.ErrorCustom("Expresion ingresada incorrecta dentro del elemento" + tempElemento.tipo.ToString(), "Sintactico", tempElemento);
                ValoresGlobales.valores().tablaDeErrores.AgregarError(error);
            }
            return raizReturn;
        }//Fin SintaxisExpresionLectura

        private TokenData SintaxisExpresionSalida( string cadena) {
            analizadorLexico.AgregarCodigo(cadena);
            analizadorLexico.tempElemento = tempElemento;
            TokenData raizReturn = null;
            TokenData tempReturn = null;
            Stack<int> obligatorio = new Stack<int>();
            List<int> opcional = new List<int>();
            //Valores opcionales iniciales
            opcional.Add(53);//variables
            opcional.Add(55);//numero
            opcional.Add(58);//Cadena
            analizadorLexico.ObtenerToken();
            do
            {   if(opcional.Contains(analizadorLexico.returnToken.tokenInfo.id)){
                switch (analizadorLexico.returnToken.tokenInfo.id)
                {
                    case 53:// Variables
                    case 55:// Numeros
                    case 58://cadenas
                        //Se colocan los valores opcionales a continuacion
                        opcional.Clear();
                        opcional.Add(29);//variable
                        AgregarTokenData(ref raizReturn, ref tempReturn);
                        //Se colocan los datos obligatorios dentro del stack
                        if (obligatorio.Count > 0)
                        {   if(obligatorio.Peek()==58) obligatorio.Pop();
                            if (obligatorio.Peek() == 55) obligatorio.Pop();
                            if (obligatorio.Peek() == 53) obligatorio.Pop();
                        }
                        break;
                    case 29:
                        //Se colocan los valores opcionales a continuacion
                        opcional.Clear();
                        opcional.Add(53);//variables
                        opcional.Add(55);//numero
                        opcional.Add(58);//Cadena
                        AgregarTokenData(ref raizReturn, ref tempReturn);
                        //Datos que son obligatorios
                        obligatorio.Push(53);
                        obligatorio.Push(55);
                        obligatorio.Push(58);
                        break;
                    default:
                        Error error2 = new Error();
                        error2.NoSeEsperaba(analizadorLexico.returnToken.tokenInfo.id, "Sintactico", tempElemento);
                        ValoresGlobales.valores().tablaDeErrores.AgregarError(error2);
                        break;
                }
            }else{
                        Error error2 = new Error();
                        error2.NoSeEsperaba(analizadorLexico.returnToken.tokenInfo.id, "Sintactico", tempElemento);
                        ValoresGlobales.valores().tablaDeErrores.AgregarError(error2);
                        break;
        
            }
                //Mientras existan tokens
            } while (analizadorLexico.ObtenerToken());
            if (obligatorio.Count > 0)
            {
                Error error = new Error();
                error.ErrorCustom("Expresion ingresada incorrecta dentro del elemento" + tempElemento.tipo.ToString(), "Sintactico", tempElemento);
                ValoresGlobales.valores().tablaDeErrores.AgregarError(error);
            }
            return raizReturn;
        }

        private TokenData SintaxisExpresion() {
            TokenData raizReturn = null;
            TokenData tempReturn = null;
            Stack<int> obligatorio = new Stack<int>();
            List<int> opcional = new List<int>();
            //Valores opcionales iniciales
            opcional.Add(23);
            opcional.Add(53);
            opcional.Add(55);
            opcional.Add(30);
            do
            {
                switch (analizadorLexico.returnToken.tokenInfo.id) { 
                    case 23:// (
                        //Se colocan los valores opcionales a continuacion
                        opcional.Clear();
                        opcional.Add(23);
                        opcional.Add(53);
                        opcional.Add(55);
                        opcional.Add(30);
                        AgregarTokenData(ref raizReturn, ref tempReturn);
                        //Se colocan los datos obligatorios dentro del stack
                        if (obligatorio.Count > 0)
                        {
                            if (obligatorio.Peek() == 55) obligatorio.Pop();
                            if (obligatorio.Peek() == 53) obligatorio.Pop();
                        }
                        obligatorio.Push(24);
                        break;
                    case 24:// )
                        AgregarTokenData(ref raizReturn, ref tempReturn);
                        if (obligatorio.Count > 0)
                        {
                            if (obligatorio.Peek() != 24)
                            {
                                ValoresGlobales.valores().tablaDeErrores.errores.Add(new Error(24, "Sintactico", tempElemento));
                            }
                            else
                            {
                                obligatorio.Pop();
                            }
                        }
                        else {
                            Error error = new Error();
                            error.ErrorCustom("No se espera caracter )", "Sintactico", tempElemento);
                            ValoresGlobales.valores().tablaDeErrores.errores.Add(error);
                            
                        }
                        //Datos opcionales
                        opcional.Clear();
                        opcional.Add(29);
                        opcional.Add(30);
                        opcional.Add(31);
                        opcional.Add(32);
                        break;
                    case 29:// +
                    case 30:// -
                    case 31:// *
                    case 32:// /
                        AgregarTokenData(ref raizReturn,ref tempReturn);
                        //Datos obligatorios
                        obligatorio.Push(53);
                        obligatorio.Push(55);
                        //Datos opcionales
                        opcional.Clear();
                        opcional.Add(23);
                        opcional.Add(53);
                        opcional.Add(55);
                        break;
                    case 53://Variables
                    case 55://Numeros
                        AgregarTokenData(ref raizReturn,ref tempReturn);
                        opcional.Clear();
                        opcional.Add(29);
                        opcional.Add(30);
                        opcional.Add(31);
                        opcional.Add(32);
                        if (obligatorio.Count > 0)
                        {
                            if (obligatorio.Peek() == 55) obligatorio.Pop();
                            if (obligatorio.Peek() == 53) obligatorio.Pop();
                        }
                        break;
                    default:
                        Error error2 = new Error();
                        error2.NoSeEsperaba(analizadorLexico.returnToken.tokenInfo.id,"Sintactico",tempElemento);
                        ValoresGlobales.valores().tablaDeErrores.AgregarError(error2);
                        break;
                 }
            //Mientras existan tokens
            } while (analizadorLexico.ObtenerToken());
            if (obligatorio.Count > 0) {
                Error error = new Error();
                error.ErrorCustom("Expresion ingresada incorrecta dentro del elemento" + tempElemento.tipo.ToString(), "Sintactico", tempElemento);
            }
            return raizReturn;
        }//Fin SintaxisExpresion
        private TokenData SintaxisExpresionIf()
        {
            TokenData raizReturn = null;
            TokenData tempReturn = null;
            Stack<int> obligatorio = new Stack<int>();
            List<int> opcional = new List<int>();
            //Valores opcionales iniciales
            opcional.Add(23);
            opcional.Add(53);
            opcional.Add(55);
            opcional.Add(30);
            //Se obtiene el primer token
            if (!analizadorLexico.ObtenerToken() || analizadorLexico.returnToken == null)
            {
                Error error = new Error();
                error.ErrorCustom("No se han obtendido la gramatica esperada 'EXP COMPARADOR EXP' en el elemento" + tempElemento.tipo.ToString(), "Sintactico", tempElemento);
           
            }
            else
            {
                do
                {
                    switch (analizadorLexico.returnToken.tokenInfo.id)
                    {
                        case 23:// (
                            //Se colocan los valores opcionales a continuacion
                            opcional.Clear();
                            opcional.Add(23);
                            opcional.Add(53);
                            opcional.Add(55);
                            opcional.Add(30);
                            AgregarTokenData(ref raizReturn, ref tempReturn);
                            //Se colocan los datos obligatorios dentro del stack
                            if (obligatorio.Count > 0)
                            {
                                if (obligatorio.Peek() == 55) obligatorio.Pop();
                                if (obligatorio.Peek() == 53) obligatorio.Pop();
                            }
                            obligatorio.Push(24);
                            break;
                        case 24:// )
                            AgregarTokenData(ref raizReturn, ref tempReturn);
                            if (obligatorio.Count > 0)
                            {
                                if (obligatorio.Peek() != 24)
                                {
                                    ValoresGlobales.valores().tablaDeErrores.errores.Add(new Error(24, "Sintactico", tempElemento));
                                }
                                else
                                {
                                    obligatorio.Pop();
                                }
                            }
                            else
                            {
                                Error error = new Error();
                                error.ErrorCustom("No se espera caracter )", "Sintactico", tempElemento);
                                ValoresGlobales.valores().tablaDeErrores.errores.Add(error);

                            }
                            //Datos opcionales
                            opcional.Clear();
                            opcional.Add(29);
                            opcional.Add(30);
                            opcional.Add(31);
                            opcional.Add(32);
                            break;
                        case 29:// +
                        case 30:// -
                        case 31:// *
                        case 32:// /
                            AgregarTokenData(ref raizReturn, ref tempReturn);
                            //Datos obligatorios
                            obligatorio.Push(53);
                            obligatorio.Push(55);
                            //Datos opcionales
                            opcional.Clear();
                            opcional.Add(23);
                            opcional.Add(53);
                            opcional.Add(55);
                            break;
                        case 53://Variables
                        case 55://Numeros
                            AgregarTokenData(ref raizReturn, ref tempReturn);
                            opcional.Clear();
                            opcional.Add(29);
                            opcional.Add(30);
                            opcional.Add(31);
                            opcional.Add(32);
                            if (obligatorio.Count > 0)
                            {
                                if (obligatorio.Peek() == 55) obligatorio.Pop();
                                if (obligatorio.Peek() == 53) obligatorio.Pop();
                            }
                            break;
                        case 38: // ==
                        case 39: // <=
                        case 40: // >=
                        case 41: // <
                        case 42: // >
                        case 43: // !=
                            goto cont;//Se sale del analisis
                            break;
                        default:
                            Error error2 = new Error();
                            error2.NoSeEsperaba(analizadorLexico.returnToken.tokenInfo.id, "Sintactico", tempElemento);
                            ValoresGlobales.valores().tablaDeErrores.AgregarError(error2);
                            break;
                    }
                    //Mientras existan tokens
                } while (analizadorLexico.ObtenerToken());
            }
        cont:
            if (obligatorio.Count > 0)
            {
                Error error = new Error();
                error.ErrorCustom("Expresion ingresada incorrecta dentro del elemento" + tempElemento.tipo.ToString(), "Sintactico", tempElemento);
            }
            return raizReturn;
        }//Fin de SintaxisExpresionIf()

        private TokenData SintaxisExpresionesAsignacion() {

            if (analizadorLexico.returnToken == null)
            {
                Error error = new Error();
                error.ErrorCustom("Expresion ingresada No cumple la sintaxis en el elemento" + tempElemento.tipo.ToString(), "Sintactico", tempElemento);
                ValoresGlobales.valores().tablaDeErrores.AgregarError(error);
                return null; 
            }
                switch(analizadorLexico.returnToken.tokenInfo.id){
                    //Caso en el que inicie con un parentesis ( ,variable o un numero
                    case 23:
                    case 53:
                    case 55:
                    case 30:
                        return SintaxisExpresion();
                    break;
                    //En caso sea una cadena
                    case 58:
                         TokenData temp = analizadorLexico.returnToken;
                         if(analizadorLexico.ObtenerToken()){
                            Error error = new Error();
                            error.ErrorCustom("Expresion ingreada no cumple requisitos de ser solamente una cadena","Sintactico",tempElemento);
                            ValoresGlobales.valores().tablaDeErrores.AgregarError(error);
                         }
                         return temp;
                        break;
                    break;
                }
            return null;
            }
        }
    }

