using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Reflection;
using System.Reflection.Emit;
using CompiladorDFD.Datos_Externos;
namespace CompiladorDFD.Generacion_de_Codigo
{    
    class GenerarCodigo
    {

        //-----------------------------------------------------------------------------------------
        //                                  METODOS UTILIZADOS PARA LA GENERACION DE CODIGO
        //-----------------------------------------------------------------------------------------
        //Metodo para poder realizar leer de la consola
        MethodInfo metodoLeer = typeof(Console).GetMethod("ReadLine",new Type[0]);
        MethodInfo metodoPausa = typeof(Console).GetMethod("ReadLine", new Type[0]);
        MethodInfo metodoEscribir = typeof(Console).GetMethod("Write", new Type[]{typeof(string)});
        MethodInfo metodoEscribirNum = typeof(Console).GetMethod("Write", new Type[] { typeof(Double) });


        //------------------------------------------------------------------------------------------
        //                                  VARIABLES DE LA CLASE
        //------------------------------------------------------------------------------------------
        //Clase que permite la generacion de codigo MSIL (Microsoft Intermediate language)
        ILGenerator il = null;
        //Elemento utilizado para tener una referencia hacia el elemento actual dentro del recorrido
        ElementoDFD tempElemento;
        //Diccionario utilizado para obtener control de todas las variables utilizadas
        Dictionary<string, LocalBuilder> TablaDeVariables = new Dictionary<string, LocalBuilder>();
        public GenerarCodigo() {
           
        }

        public void GenerarEjecutable(string nombre) {
            //Se prepara el entorno para la generacion de codigo MSIL para luego ser compilado
            //Se establece el dominio del aplicativo en el cual va a ser compilado en este caso 
            //se trabaja con el mismo del programa
            AppDomain appDomain = AppDomain.CurrentDomain;
            //Esta se utiliza para dar los parametros acerca de la informacion del compilable
            //En este caso nos sirve para poner el nombre asignado al ensamblado
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = nombre;
            //Esta clase nos permite crear un ejecutable dinamicamente es decir crear un ejecutable
            //Desde el programa siempre y cuando se utilice MSLI y las clases ILGenerator para poder
            //Proporcionar el lenguaje intermedio con el cual trabaja
            AssemblyBuilder assemblyBuilder = appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);
            //Esto nos permite crear un modulo dinamico que luego sera utilizado por TypeBuilder
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("Modulo", "TestAsm.exe");
            //Esta se encarga de crear la definicion de la ruta y el metodo inicial dentro de nuestro programa
            TypeBuilder typeBuilder = moduleBuilder.DefineType("MiTipo", TypeAttributes.Public);
            //Este es el discreptor principal que engloba todos los parametros anteriores 
            //Para ser utilizado con el MSIL
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("Main", MethodAttributes.Public | MethodAttributes.Static, null, null);
            //Linea de codigo que proporciona la linea de entrada en la cual se ejecutar
            //La funcion principal en este caso el "Main"
            assemblyBuilder.SetEntryPoint(methodBuilder);
            //Se entrega a la clase ILGenerator los parametros para poder crear un ejecutable una vez terminado
            //EL ingreso de todo el codigo para crear el codigo MSLI
            il = methodBuilder.GetILGenerator();

            //Se comienza a formar el codigo apartir del Arbol que se creo en los analizadores obteniendo el elemento Raiz
            ElementoDFD raiz = ValoresGlobales.valores().elementoRaiz;
            //Ahora se comienza a recorrer todo el arbol para generar el codigo de acuerdo a la estructura planteada
            tempElemento = raiz;
            //Mientras no se llege al final del grafo
            while (tempElemento.tipo != Elemento.fin) {
                switch (tempElemento.tipo) { 
                    case Elemento.Asignacion:
                          GenerarAsignaciones();
                        break;
                    case Elemento.Escritura:
                          GenerarEscritura();
                        break;
                }
                tempElemento = tempElemento.centro;
            }
            //Codigo para hacer una pausa al final del codigo mediante una lectura de teclado
            il.Emit(OpCodes.Call, metodoLeer);
            //Hace la devolucion del CPU a la computadora
            il.Emit(OpCodes.Ret);
            //Se crea el ejecutable apartir del codigo MSLI creado anteriormente
            typeBuilder.CreateType();
            assemblyBuilder.Save(nombre+".exe");
        }

        private void GenerarAsignaciones() {
            foreach (TokenData raizTokenData in tempElemento.tokenDataRef)
            {
                TokenData tempTokenData = raizTokenData.tokenDataRef.tokenDataRef;
                //Se obtiene la variable sobre la que se guardara el dato
                LocalBuilder variable = ObtenerVariable(raizTokenData);
                

                    switch (tempTokenData.tokenInfo.id)
                    {
                        case 58://Cadena
                            //Se limpia las comillas extra que posee
                            string texto = tempTokenData.codigo.Substring(1, tempTokenData.codigo.Length - 2);
                            il.Emit(OpCodes.Ldstr, texto);
                            il.Emit(OpCodes.Stloc, variable);
                            break;
                        case 59:// Variable Numerica
                        case 55:// Numero 
                        case 23:// (
                            IngresarOperaciones(PolacaInversa(tempTokenData));
                            il.Emit(OpCodes.Stloc, variable);
                            break;
                        case 60://Variable Cadena
                            break;
                    }
                    
            }
        }//Fin GenerarAsignaciones

        private void GenerarEscritura() {
            //Se recorren todos los token dentro del token de escritura
            
            foreach (TokenData raizTokenData in tempElemento.tokenDataRef)
            {   TokenData tempTokenData = raizTokenData;
                
                while(tempTokenData != null){

                    switch (tempTokenData.tokenInfo.id) { 
                        case  55://Numero
                            break;
                        case 58://Cadena
                            //Se limpia las comillas extra que posee
                            string texto = tempTokenData.codigo.Substring(1, tempTokenData.codigo.Length - 2);
                            //Se almacena el texto
                            il.Emit(OpCodes.Ldstr, texto);
                            //Se imprime en pantalla el texto
                            il.EmitCall(OpCodes.Call, metodoEscribir,null);
                            break;
                        case 59://Variable Numerica
                            il.Emit(OpCodes.Ldloc, ObtenerVariable(tempTokenData));
                            il.EmitCall(OpCodes.Call, metodoEscribirNum, null);
                            break;
                        case 60://Variable Cadena
                            il.Emit(OpCodes.Ldloc, ObtenerVariable(tempTokenData));
                            il.EmitCall(OpCodes.Call, metodoEscribir, null);
                            break;
                    }
                    tempTokenData = tempTokenData.tokenDataRef;
                } 
            }
        }//Fin GenerarEscritura

        private LocalBuilder ObtenerVariable(TokenData tokenData){
           LocalBuilder temp;
           //Se verifica si la variable ya fue registrada
            if (!TablaDeVariables.TryGetValue(tokenData.codigo, out temp)) {
               temp=CreacionDeVariable(tokenData.tokenInfo.id); 
           //De lo contrario se crea y se registra
               TablaDeVariables.Add(tokenData.codigo, temp);
           }
           return temp;
        }
        private LocalBuilder CreacionDeVariable(int id) {
            switch (id) { 
                case 59:    //Variable Numerica
                    return il.DeclareLocal(typeof(Double));
                    break;
                case 60:    //Variable de tipo Cadena
                    return il.DeclareLocal(typeof(string));
                    break;
            }
            return null;
        }//Fin CreacionVariable
        //Funcion para insertar las operacione en el orden adecuado
        private void IngresarOperaciones(List<TokenData> tokenDataList){
            //Pila utilizada para los operadores
            Stack<TokenData> operadores = new Stack<TokenData>();
            foreach (TokenData tempTokenData in tokenDataList)
            {//Switch para identificar el parametro agregado
                switch (tempTokenData.tokenInfo.id)
                {
                    case 55://Numero
                        il.Emit(OpCodes.Ldc_R8, Convert.ToDouble(tempTokenData.codigo));
                        break;
                    case 59: //Variable
                        il.Emit(OpCodes.Ldloc,ObtenerVariable(tempTokenData));
                        break;
                    default://Operadores
                        operadores.Push(tempTokenData);
                        break;
                }
                if (operadores.Count > 0)
                {
                    switch (operadores.Pop().tokenInfo.id)
                    {
                        case 29: // +
                            il.Emit(OpCodes.Add);
                            break;
                        case 30: // -
                             il.Emit(OpCodes.Sub);
                            break;
                        case 31: // *
                            il.Emit(OpCodes.Mul);
                            break;
                        case 32: // /
                            il.Emit(OpCodes.Div);
                            break;
                    }//End  switch
                }//End if
            }//End foreach 
        }
        //Funcion para reacomodar las operaciones realizadas dentro del programa
        private List<TokenData> PolacaInversa(TokenData tokenData){
            //Pilas para realizar la polaca inversa
            Stack<TokenData> operadores = new Stack<TokenData>();
            //Lista para almacenar los resultados
            List<TokenData> salida = new List<TokenData>();
            //Si el token no es vacio
            if (tokenData != null) { 
                TokenData tempTokenData = tokenData;
                while (tempTokenData != null) {
                    switch (tempTokenData.tokenInfo.id)
                    {//En caso que sea un numero se agrega directamente a la pila
                        case 59:
                        case 55:
                            salida.Add(tempTokenData);
                            break;
                    //En caso que sea un (
                        case 23:
                            operadores.Push(tempTokenData);
                            break;
                    //En caso que sea un )
                        case 24:
                            while (operadores.Peek().tokenInfo.id != 23) {
                                salida.Add(operadores.Pop());
                            }
                            operadores.Pop();
                            break;
                    //En caso que sea + o -
                        case 29:// +
                        case 30:// -
                            if (operadores.Count == 0) operadores.Push(tempTokenData);
                            else {
                                while (operadores.Peek().tokenInfo.id == 31 || operadores.Peek().tokenInfo.id == 32) { 
                                    //Se saca el elemento que se encuentra dentro de la pila
                                    salida.Add(operadores.Pop());
                                    //Si ya no hay elementos nos salimos de la pila
                                    if (operadores.Count == 0) break;
                                }
                                //Se agrega el elemento adentro de la pila
                                operadores.Push(tempTokenData);
                            }
                            //Se verofoca si existe un operador de mayor presedencia dentro de la pila para sacarlo de la pila y agregarlo
                            break;
                    //En caso que sea * o / 
                        case 31:// *
                        case 32:// /
                            if (operadores.Count == 0) operadores.Push(tempTokenData);
                        if (operadores.Peek().tokenInfo.id == 31 || operadores.Peek().tokenInfo.id == 32) {
                            salida.Add(operadores.Pop());
                            //Si ya no hay datos dentro de la pila se sale
                            if (operadores.Count == 0) break;
                            }
                        operadores.Push(tempTokenData);
                            break;
                    }
                    //se avanza a la siguiente posicion
                    tempTokenData = tempTokenData.tokenDataRef;
                }
            }
            while (operadores.Count > 0) salida.Add(operadores.Pop());
            return salida;
        }//Fin Polaca Inversa
    }
}
