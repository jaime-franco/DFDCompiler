using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CompiladorDFD.Datos_Externos
{
    public class TablaDeErrores
    {
        public List<Error> errores = new List<Error>();

        public void AgregarError(Error error){
            errores.Add(error);
        }
           public bool Existen() {
               if (errores.Count > 0) return true;
           return false;
       }
    }

    public class Error{
        public string detalle;
        public ElementoDFD ElementoError;
        public string faseAnalisis;
        public Error() { }

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
