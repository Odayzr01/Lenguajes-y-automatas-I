using System;

namespace Lexico
{
    public class Program{
        
        static void Main(string[] args){

            Lexico a = new Lexico();
            
            while(!a.FinArchivo()){
                a.NextToken();
            }
            
            a.Cerrar();
        }
    }
}