﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorDFD.Datos_Externos;

namespace CompiladorDFD.Analizadores
{
    class AnalizadorSemantico
    {
        public AnalizadorSemantico(){}
        private ElementoDFD tempElemento;
        public void AnalizarTipos(){
              Stack<ElementoDFD> elementoIf = new Stack<ElementoDFD>();
            //Se obtien la regerencia al grafo formado para por el analizador sintactico
            tempElemento = ValoresGlobales.valores().elementoRaiz;

            while (tempElemento.tipo != Elemento.fin)
            {
                switch (tempElemento.tipo)
                {
                    case Elemento.Asignacion:
                        //Se obtiene los parametros de las cadenas para poder proceder a realizar el analisis lexico
                        VerificarAsignacion();
                        break;
                    case Elemento.Escritura:
                        verificarEscritura();
                        break;
                    case Elemento.Lectura:
                        verificarLectura();
                        break;
                    case Elemento.Eif:
                        VerificarIf();
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
                    default:
                        break;
                }
                tempElemento = tempElemento.centro;
            }
        }

        private bool VerificarID(ref int id,int idObtenido) {
            if (id == 0) id = idObtenido;
            else {
                if (id != idObtenido)
                    return false;
            }
                    return true;
        }
        private void verificarLectura() {
            TokenData tempTokenData;
            foreach (TokenData tokenref in tempElemento.tokenDataRef)
            {
                tempTokenData = tokenref;
                //Mientras no se llege al final 
                while (tempTokenData != null)
                {
                    if (tempTokenData.tokenInfo.id == 53) tempTokenData.tokenInfo = ValoresGlobales.valores().tablaDeSimbolos.ObtenerToken(tempTokenData.codigo);
                    switch (tempTokenData.tokenInfo.id)
                    {
                        case 58://cadena
                        case 29:// +
                        case 59://Variable numeros
                        case 60://Variable caracteres
                            break;
                        case 53://Identificador
                            //Se verifica el tipo de dato para ver si coincide o no 
                            Error errorIdentificador = new Error();
                            errorIdentificador.ErrorCustom("La variable : '" + tempTokenData.codigo + "' No se ha declarado", "Semantico", tempElemento);
                            ValoresGlobales.valores().tablaDeErrores.AgregarError(errorIdentificador);
                            break;
                    }
                    tempTokenData = tempTokenData.tokenDataRef;
                }
            } 
        }
        private void VerificarIf() {
            TokenData tempTokenData;
            foreach (TokenData tokenref in tempElemento.tokenDataRef)
            {
                tempTokenData = tokenref;
                //Mientras no se llege al final 
                while (tempTokenData != null)
                {
                    if (tempTokenData.tokenInfo.id == 53) tempTokenData.tokenInfo = ValoresGlobales.valores().tablaDeSimbolos.ObtenerToken(tempTokenData.codigo);
                    switch (tempTokenData.tokenInfo.id)
                    {
                       
                        case 55://Numeros
                        case 59://Variable numeros  
                            break;
                        case 60://Variable caracteres
                        case 58://Cadena
                            //Se verifica el tipo de dato para ver si coincide o no 
                            Error errorIdentificador1 = new Error();
                            errorIdentificador1.ErrorCustom("Las cadenas no estan permitidas en la estructura if", "Semantico", tempElemento);
                            ValoresGlobales.valores().tablaDeErrores.AgregarError(errorIdentificador1);
                            break;
                            break;
                        case 53://Identificador
                            //Se verifica el tipo de dato para ver si coincide o no 
                            Error errorIdentificador = new Error();
                            errorIdentificador.ErrorCustom("La variable : '" + tempTokenData.codigo + "' No se ha declarado", "Semantico", tempElemento);
                            ValoresGlobales.valores().tablaDeErrores.AgregarError(errorIdentificador);
                            break;
                    }
                    tempTokenData = tempTokenData.tokenDataRef;
                }
            } 
             
        }
        private void verificarEscritura() { 
         TokenData tempTokenData;
            foreach (TokenData tokenref in tempElemento.tokenDataRef) {
                tempTokenData = tokenref;
                //Mientras no se llege al final 
                while (tempTokenData != null) {
                    if( tempTokenData.tokenInfo.id==53) tempTokenData.tokenInfo = ValoresGlobales.valores().tablaDeSimbolos.ObtenerToken(tempTokenData.codigo);
                    switch (tempTokenData.tokenInfo.id) { 
                        case 58://cadena
                        case 55://Numeros
                        case 59://Variable numeros
                        case 60://Variable caracteres
                            break;
                        case 53://Identificador
                            //Se verifica el tipo de dato para ver si coincide o no 
                            Error errorIdentificador = new Error();
                            errorIdentificador.ErrorCustom("La variable : '" + tempTokenData.codigo + "' No se ha declarado", "Semantico", tempElemento);
                            ValoresGlobales.valores().tablaDeErrores.AgregarError(errorIdentificador);
                            break;
                    }
                    tempTokenData = tempTokenData.tokenDataRef;
                }
            } 
        }//Fin VerificarEscritura()
        private void VerificarAsignacion() { 
            TokenData tempTokenData;
            bool error = false;
            int id=0;
            foreach (TokenData tokenref in tempElemento.tokenDataRef) {
                error = false;
                tempTokenData = tokenref.tokenDataRef;
                //Mientras no se llege al final 
                while (tempTokenData != null) {
                    if( tempTokenData.tokenInfo.id==53) tempTokenData.tokenInfo = ValoresGlobales.valores().tablaDeSimbolos.ObtenerToken(tempTokenData.codigo);
                    switch (tempTokenData.tokenInfo.id) {
                        case 53://Identificador
                            //Se verifica el tipo de dato para ver si coincide o no 
                            Error errorIdentificador = new Error();
                            errorIdentificador.ErrorCustom("La variable : '" + tempTokenData.codigo + "' No se ha declarado", "Semantico", tempElemento);
                            ValoresGlobales.valores().tablaDeErrores.AgregarError(errorIdentificador);
                            error = true;
                            break;
                        case 55://Numero  
                            if(!VerificarID(ref id,59)){
                            Error error1 = new Error();
                            error1.ErrorCustom("Los tipos de datos no coinciden en"+tempElemento.tipo.ToString(), "Semantico", tempElemento);
                            ValoresGlobales.valores().tablaDeErrores.AgregarError(error1);
                            error = true;
                            }
                            break;
                        case 58://Cadena
                            if (!VerificarID(ref id, 60))
                            {
                                Error error1 = new Error();
                                error1.ErrorCustom("Los tipos de datos no coinciden en" + tempElemento.tipo.ToString(), "Semantico", tempElemento);
                                ValoresGlobales.valores().tablaDeErrores.AgregarError(error1);
                                error = true;
                            }
                            break;
                        case 59:
                            if (!VerificarID(ref id, 59))
                            {
                                Error error1 = new Error();
                                error1.ErrorCustom("Los tipos de datos no coinciden en" + tempElemento.tipo.ToString(), "Semantico", tempElemento);
                                ValoresGlobales.valores().tablaDeErrores.AgregarError(error1);
                                error = true;
                            }
                            break;
                        case 60:
                            if (!VerificarID(ref id, 60))
                            {
                                Error error1 = new Error();
                                error1.ErrorCustom("Los tipos de datos no coinciden en" + tempElemento.tipo.ToString(), "Semantico", tempElemento);
                                ValoresGlobales.valores().tablaDeErrores.AgregarError(error1);
                                error = true;
                            }
                        break;
                    }
                    tempTokenData = tempTokenData.tokenDataRef;
                }

                if (!error) {
                    //Se asigna el valor a la variable segun los parametros asignados 
                    ValoresGlobales.valores().tablaDeSimbolos.CambiarTipo(tokenref.codigo, id);
                    tokenref.tokenInfo= ValoresGlobales.valores().tablaDeSimbolos.ObtenerToken(tokenref.codigo);
                }
            } 
        }
    }
}
