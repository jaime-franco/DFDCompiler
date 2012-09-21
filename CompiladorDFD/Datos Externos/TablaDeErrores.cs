using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CompiladorDFD.Datos_Externos
{//Clase utilizada para manejar lo que son las tablas de los errores generados a lo largo de los analisis
    public class TablaDeErrores
    {//Lista de errores que se generaron
        public List<Error> errores = new List<Error>();
        //Funcion para agregar los errores dentro de la tabla de errores
        public void AgregarError(Error error){
            errores.Add(error);
        }
        //Funcion para verificar si existe o no errores dentro de la tabla de errores
           public bool Existen() {
               if (errores.Count > 0) return true;
           return false;
       }
    }
    //Clase que permite alamacenar los errores para poder especificar de una manera mas clara y 
    //Precisa el elemento que genero el error , en que etapa del analisis y ademas que lo causo
    public class Error{
        public string detalle;  //Detalle del error
        public ElementoDFD ElementoError;//Elemento en el que se genero el error
        public string faseAnalisis;//Fase en la que se genero el error
        public Error() { }//Constructor vacio
        //----------------------------------------------------------------------------------------------------
        // FUNCIONES PARA INGRESAR LOS DIFERENTES TIPOS DE ERRORES ENCONTRADOS DENTRO DE LA EJECUCION
        //----------------------------------------------------------------------------------------------------
        public Error(int id, string fase, ElementoDFD elemento,TokenData tempTokendata) {
            Token tempToken = ValoresGlobales.valores().tablaDeTokens.ObtenerIdToken(id);
            detalle = "Se esperaba un "+ tempToken.nombre+ " en vez de " +tempTokendata.codigo+" Dentro del elemento "+ elemento.tipo.ToString();
            ElementoError = elemento;
            faseAnalisis = fase;
        }

        public Error(int id, string fase, ElementoDFD elemento)
        {
            Token tempToken = ValoresGlobales.valores().tablaDeTokens.ObtenerIdToken(id);
            detalle = "Se esperaba un " + tempToken.nombre  + " Dentro del elemento " + elemento.tipo.ToString();
            ElementoError = elemento;
            faseAnalisis = fase;
        }

       public void NoSeEsperaba(int id,string fase, ElementoDFD elemento){
            Token tempToken = ValoresGlobales.valores().tablaDeTokens.ObtenerIdToken(id);
            detalle = "No se esperaba un " + tempToken.nombre  + " Dentro del elemento " + elemento.tipo.ToString();
            ElementoError = elemento;
            faseAnalisis = fase;
       }
       public void ErrorCustom(string mensaje, string fase, ElementoDFD elemento) {
           detalle = mensaje;
           ElementoError = elemento;
           faseAnalisis = fase;
       }
    
    }
}
