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

        public void GuardarArchivo(string ruta)
        {
            fileStream = new FileStream(ruta, FileMode.Create);
            streamWriter = new StreamWriter(fileStream);
            ElementoDFD tempElemento = ValoresGlobales.valores().elementoRaiz;
            Recorrer(tempElemento);
            streamWriter.Close();   
        }
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
