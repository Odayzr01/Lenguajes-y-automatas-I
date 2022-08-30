using System;

namespace Sintaxis1{
    public class Program{

        static void Main(string[] args){
            try
            {
                Lenguaje a = new Lenguaje("C:\\Users\\oscar\\Desktop\\Lenguajes y Automatas\\Sintaxis1\\examen.cpp");
                a.Programa();
                a.Cerrar();
            }
            catch (Exception e)
            {
                Console.WriteLine("Fin de compilacion");
            }
        }
    }
}