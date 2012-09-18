using System;
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
        private void verificarEscritura() { 
         TokenData tempTokenData;
            int id=0;
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
        }
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
