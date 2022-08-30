using System;

namespace Automatas_I
{
    public class Program{
        
        static void Main(string[] args){

            
            archivo a = new archivo();
            //a.desplegar();
            //a.Palabras();
            
            while (!a.Finarchivo())
            {
                 a.NextToken();
            }

           
           
            a.Cerrar();
        }
    }
}