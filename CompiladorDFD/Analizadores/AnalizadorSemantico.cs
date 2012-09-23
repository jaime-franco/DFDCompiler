using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorDFD.Datos_Externos;

namespace CompiladorDFD.Analizadores
{//Clase utilizada para realizar el analisis semant
    class AnalizadorSemantico
    {
        public AnalizadorSemantico(){}
        private ElementoDFD tempElemento;
        public void AnalizarTipos(){
              Stack<ElementoDFD> elementoIf = new Stack<ElementoDFD>();
            //Se obtien la regerencia al grafo formado para por el analizador sintactico
            tempElemento = ValoresGlobales.valores().elementoRaiz;
            //A continuacion se procede a realizar un analisis semantico el cual consistira en esta etapa 
            //En la verificacion de los tipos de datos dentro de las cadenas
            while (tempElemento.tipo != Elemento.fin)
            {//Se verificaa el tipo de elemento que se esta analizando para luego proceder a analizar la validez de las variables
                switch (tempElemento.tipo)
                {
                    case Elemento.Asignacion:
                        VerificarAsignacion();
                        break;
                    case Elemento.Escritura:
                        verificarEscritura();
                        break;
                    case Elemento.Lectura:
                        verificarLectura();
                        break;
                    case Elemento.Eif: //En este caso se procede a realizar un analisis con un stack para recorrer los elementos internos dentro del if
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
                    case Elemento.EndIf: //Se espera que el elemento sea el fin del if para indicar el fin del analisis recursivo
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
                    case Elemento.EWhile: //Para la funcion while se utiliza la funcion de verificacion del if ya que esta
                        //es identica a la del if por la forma EXP COMP EXP
                        VerificarIf();
                        break;
                    default:
                        break;
                }
                tempElemento = tempElemento.centro; //Se procede a analizar el siguiente elemento del grafo
            }
        }
        //Funcion utilizada para verificar que los id que se pasan como parametros sean iguales
        //Esto para comprobar el tipo de dato que se va a obtener al final de las comparaciones
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
                            errorIdentificador1.ErrorCustom("Las cadenas no estan permitidas en la estructura" + tempElemento.tipo.ToString(), "Semantico", tempElemento);
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
                        case 59://Variable Numerica
                            if (!VerificarID(ref id, 59))
                            {
                                Error error1 = new Error();
                                error1.ErrorCustom("Los tipos de datos no coinciden en" + tempElemento.tipo.ToString(), "Semantico", tempElemento);
                                ValoresGlobales.valores().tablaDeErrores.AgregarError(error1);
                                error = true;
                            }
                            break;
                        case 60://Variable cadena
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
                    if (tokenref.codigo != null)
                    {
                        //Se asigna el valor a la variable segun los parametros asignados 
                        ValoresGlobales.valores().tablaDeSimbolos.CambiarTipo(tokenref.codigo, id);
                        tokenref.tokenInfo = ValoresGlobales.valores().tablaDeSimbolos.ObtenerToken(tokenref.codigo);
                        id = 0; //Se restablece el id utilizado para la verificacion anteriror
                    }
                    else {
                        Error error1 = new Error();
                        error1.ErrorCustom("Los tipos de datos no coinciden en" + tempElemento.tipo.ToString(), "Semantico", tempElemento);
                        ValoresGlobales.valores().tablaDeErrores.AgregarError(error1);
                        id = 0; //Se restablece el id utilizado para la verificacion anteriror
                        error = true;
                    }
                }
            } 
        }
    }
}
