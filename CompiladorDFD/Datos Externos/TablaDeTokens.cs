using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorDFD.Datos_Externos
{
    public class TablaDeTokens
    {
         Dictionary<string,Token> dToken = new Dictionary<string, Token>();
         Dictionary<int, Token> idToken = new Dictionary<int, Token>();
        //Se registran todos los tokens existentes
        public TablaDeTokens() { 
        //Tokens de palabras reservadas

        //Token de signos de separacion
        dToken.Add("(",new Token(23,"(",Token.TipoToken.SS));
        dToken.Add(")",new Token(24,")",Token.TipoToken.SS));
        //Token de Operadores
        dToken.Add("!", new Token(28, "!", Token.TipoToken.OP));
        dToken.Add("+",new Token(29,"+",Token.TipoToken.OP));
        dToken.Add("-",new Token(30,"-",Token.TipoToken.OP));
        dToken.Add("*",new Token(31,"*",Token.TipoToken.OP));
        dToken.Add("/",new Token(32,"/",Token.TipoToken.OP));
        dToken.Add("&&",new Token(33,"&&",Token.TipoToken.OP));
        dToken.Add("||",new Token(34,"||",Token.TipoToken.OP));
        dToken.Add("++", new Token(35, "++", Token.TipoToken.OP));
        dToken.Add("--", new Token(36, "--", Token.TipoToken.OP));
        dToken.Add("=", new Token(37, "=", Token.TipoToken.OP));
        //Operadores de comparacion
        dToken.Add("==", new Token(38, "==", Token.TipoToken.SCOM));
        dToken.Add("<=", new Token(39, "<=", Token.TipoToken.SCOM));
        dToken.Add(">=", new Token(40, ">=", Token.TipoToken.SCOM));
        dToken.Add("<", new Token(41, "<", Token.TipoToken.SCOM));
        dToken.Add(">", new Token(42, ">", Token.TipoToken.SCOM));
        dToken.Add("!=", new Token(43, "!=", Token.TipoToken.SCOM));
        //Signos Relacionales
       //Signos de Puntuacion
        dToken.Add(",", new Token(51, ",", Token.TipoToken.SP));
        dToken.Add(".", new Token(52, ".", Token.TipoToken.SP));
        //Identificadores para numeros y cadenas
        dToken.Add("identificador", new Token(53, "identificador", Token.TipoToken.ID));
        dToken.Add("numEntero", new Token(54, "numEntero", Token.TipoToken.NE));
        dToken.Add("numDecimal", new Token(55, "numDecimal", Token.TipoToken.ND));
        dToken.Add("", new Token(56, "", Token.TipoToken.ND));
        dToken.Add("/*", new Token(57, "//", Token.TipoToken.CL));
        dToken.Add("cadena", new Token(58, "cadena", Token.TipoToken.CA));
        dToken.Add("variableNumerica",new Token(59,"variableNumerica",Token.TipoToken.VN));
        dToken.Add("variabeCadena",new Token(60,"variableCadena",Token.TipoToken.VC));

        foreach (KeyValuePair<string,Token>  tempdToken in dToken) {
            //Se agregan todos los datos necesarios
            idToken.Add(tempdToken.Value.id, tempdToken.Value);
        }
        }
        //Funcion para obtener el tipo de token  
        public Token ObtenerToken(string cadena) { 
            Token tempToken= new Token();
            if(dToken.TryGetValue(cadena,out tempToken))
            return tempToken;
            return null;
        }

        public Token ObtenerIdToken(int id) {
            Token tempToken = new Token();
            if (idToken.TryGetValue(id, out tempToken))
                return tempToken;
            return null;
        }

    }
}
