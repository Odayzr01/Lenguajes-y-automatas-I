using System; 
using System.IO; 

namespace Sintaxis1
{
    public class Error : Exception
    {
        public Error(string msg, StreamWriter log)
        {
            Console.WriteLine(msg);
            log.WriteLine(msg);
        }
    }
}