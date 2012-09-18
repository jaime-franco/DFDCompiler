using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorDFD.Datos_Externos
{
    public class TablaDeSimbolos
    {
        Dictionary<string, TokenData> Simbolos = new Dictionary<string, TokenData>();

        public bool VerificarSimbolo(string nombre) {
            TokenData tempTokenData = new TokenData();
            if(Simbolos.TryGetValue(nombre,out tempTokenData))
                return true;
            else
                return false;

        }

        public void AgregarToken(string nombre, TokenData tokenData) {
            Simbolos.Add(nombre, tokenData);
        }

        public Token ObtenerToken(string nombre){
            TokenData tempTokenData = new TokenData();
            if (Simbolos.TryGetValue(nombre, out tempTokenData))
                return tempTokenData.tokenInfo;
            else
                return null;

        }
        public void CambiarTipo(string variable,int id) {
            Simbolos[variable].tokenInfo = ValoresGlobales.valores().tablaDeTokens.ObtenerIdToken(id);
        }
    }


}
