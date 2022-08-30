using System;

namespace Sintaxis1
{
    public class Sintaxis : Lexico
    {
        public Sintaxis()
        {
            NextToken();
        }

        public Sintaxis(string nombre) : base(nombre)
        {
            NextToken();
        }

        public void match(String espera)
        {
            if(espera == getContenido())
            {
                NextToken();
            }
            else
            {
                // Requerimiento 9: Agregar el numero de linea en el error
                throw new Error("Error de sintaxis: Se espera un " +espera+".    Numero de linea: "+linea, log);
            }
        }

        public void match(tipos espera)
        {
            if(espera == getClasificacion())
            {
                NextToken();
            }
            else
            {
                // Requerimiento 9: Agregar el numero de linea en el error
                throw new Error("Error de sintaxis: Se espera un " +espera+".   Numero de Linea: "+linea, log);
            }
        }
    }
}
