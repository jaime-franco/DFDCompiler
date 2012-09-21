using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CompiladorDFD.Datos_Externos;
namespace CompiladorDFD.Generacion_de_Codigo
{
    class GuardarCargarDFD
    {
        FileStream fileStream;
        StreamWriter streamWriter;
        public GuardarCargarDFD()
        {
            
        }
        //Se Guarda el archivo en un formato de texto linea por linea recorriendo el grafo actual
        public void GuardarArchivo(string ruta)
        {
            fileStream = new FileStream(ruta, FileMode.Create);
            streamWriter = new StreamWriter(fileStream);
            ElementoDFD tempElemento = ValoresGlobales.valores().elementoRaiz;
            Recorrer(tempElemento);
            streamWriter.Close();   
        }
        //Se regresa una lista sobre la cual estan todos los elementos recuperados por 
        //el archivo de texto y que seran luego pasados al control de usuario para crear
        //lo que es el DFD
        public List<string> Leer(string ruta) {
            List<string> retCad = new List<string>();
            fileStream = new FileStream(ruta, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            while (!streamReader.EndOfStream) {
                string temporal = streamReader.ReadLine();
                if (temporal != "")
                {
                    retCad.Add(temporal);
                }
            }
            return retCad;
            streamReader.Close();
        }
        //Funcion recursiva que se utiliza para generar las inserciones dentro del archivo de texto
        //Segun la conformacion del grafo
        private void Recorrer(ElementoDFD elemento) {
            if (elemento == null) return;
            switch (elemento.tipo) {
                case Elemento.Eif:
                    streamWriter.WriteLine(elemento.tipo.ToString());
                    streamWriter.WriteLine(elemento.datos);
                   
                    streamWriter.WriteLine("derecha");
                    Recorrer(elemento.derecha);
                    streamWriter.WriteLine("izquierda");
                    Recorrer(elemento.izquierda);
                    elemento = elemento.centro;
                    streamWriter.WriteLine(elemento.tipo.ToString());
                    Recorrer(elemento.centro);
                    break;
                case Elemento.EndIf:
                    return;
                    break;
                case Elemento.inicio:
                    streamWriter.WriteLine(elemento.tipo.ToString());
                    Recorrer(elemento.centro);
                    break;
                case Elemento.fin:
                    streamWriter.WriteLine(elemento.tipo.ToString());
                    return;
                    break;
                case Elemento.EndWhile:
                case Elemento.Endfor:
                    streamWriter.WriteLine(elemento.tipo.ToString());
                    Recorrer(elemento.centro);
                    break;
                default:
                    streamWriter.WriteLine(elemento.tipo.ToString());
                    streamWriter.WriteLine(elemento.datos);
                    Recorrer(elemento.centro);
                    break;
            }
        }
    }
}
