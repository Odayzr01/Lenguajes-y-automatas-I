using System;

namespace Lexico2
{
    public class Program{
        
        static void Main(string[] args){

            Lexico2 a = new Lexico2();
            
            while(!a.FinArchivo()){
                a.NextToken();
            }
            
            a.Cerrar();
        }
    }
}