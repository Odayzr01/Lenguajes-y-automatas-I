using System;

//Requerimiento 1: Grabar la fecha y hora en el log   --------------------------------------------------------------------------------------------      LISTO
//Requerimiento 2: Programar la produccion For -> for(Asignacion Condicion; Incremento) Bloque de instrucciones | instruccion   ------------------      LISTO
//Requerimiento 3: Programar la produccion Incremento -> Identificador ++ | -- -------------------------------------------------------------------      LISTO
//Requerimiento 4: Programar la produccion Swich -> swich(Expresion) {Lista de casos} ------------------------------------------------------------      LISTO
//Requerimiento 5: Programar la produccion ListaDeCasos -> case Expresion: listaInstruccionesCase (break;)? (ListaDeCasos)?  ---------------------      LISTO
//Requerimiento 6: Incluir en el swich un default optativo  --------------------------------------------------------------------------------------      LISTO
//Requerimiento 7: Levantar una excepcion cuando el archivo de prueba.cpp no exista --------------------------------------------------------------      LISTO
//Requerimiento 8: Si el programa a compilar es Suma.cpp debera generar un Suma.log --------------------------------------------------------------      LISTO
//Requerimiento 9: Es necesario que le error lexico o sintactico indique el numero de linea en el que ocurrio ------------------------------------      LISTO

namespace Sintaxis1
{
        public class Lenguaje : Sintaxis
        {
            public Lenguaje()
            {

            }
            public Lenguaje(string nombre) : base(nombre)
            {

            }

        //Programa	-> 	Librerias? Variables? Main
        public void Programa()
        {
            Librerias();
            Variables();
            Main();
        }

        //Librerias	->	#include<identificador(.h)?> Librerias?
        private void Librerias()
        {
            if (getContenido() == "#")
            {
                match("#");
                match("include");
                match("<");
                match(tipos.identificador);
                if (getContenido() == ".")
                {
                    match(".");
                    match("h");
                }
                match(">");
                Librerias();
            }
        }

        // Variables -> tipo_dato Lista_identificadores ; Variables?
        private void Variables(){
            if(getClasificacion() == tipos.tipo_datos)
            {
                match(tipos.tipo_datos);
                listaIdentificadores();
                match(tipos.fin_sentencia);
                Variables();
            }
        }

        // Lista_identificadores -> identificador (,Lista_identificadores)? 
        private void listaIdentificadores(){
            match(tipos.identificador);
            if(getContenido() == ",")
            {
                match(",");
                listaIdentificadores();
            }
        }

        // Bloque de instrucciones -> {Lista de instrucciones?}
        private void bloqueInstrucciones()
        {
            match("{");
            if(getContenido() != "}")
            {
                listaInstrucciones();
            }
            match("}");
        }

        //listainstrucciones -> instruccion listadeinstrucciones?
        private void listaInstrucciones()
        {
            instruccion();
            if (getContenido() != "}")
            {
                listaInstrucciones();
            }
        }

        // Instruccion -> Printf | Scanf | If | While | do while | Asignacion
        private void instruccion()
        {
            if(getContenido() == "printf")
            {
                Printf();
            }
            else if (getContenido() == "scanf")
            {
                Scanf();
            }
            else if (getContenido() == "if") 
            {
                If();
            }
            else if (getContenido() == "while")
            {
                While();
            }
            else if (getContenido() == "do")
            {
                Do();
            }
            else if (getContenido() == "for")
            {
                For();
            }
            else if (getContenido() == "switch")
            {
                Switch();
            }
            else
                Asignacion();
        }

        // Asignacion -> identificador = cadena | Expreison;
        private void Asignacion()
        {
            match(tipos.identificador);
            match("=");
            if(getClasificacion() == tipos.cadena)
            {
                match(tipos.cadena);
            }
            else 
            {
                Expresion();
            }
            match(";");
        }

        // While -> while(Condicion) bloqueInstrucciones | instruccion
        private void While()
        {
            match("while");
            match("(");
            Condicion();
            match(")");
            if(getContenido() == "{")
            {
                bloqueInstrucciones();
            }
            else
            {
                instruccion();
            }
        }

        //Do -> do bloqueInstrucciones | instruccion while(Condicion);
        private void Do()
        {
            match("do");
            if(getContenido() == "{")
            {
                bloqueInstrucciones();
            }
            else
            {
                instruccion();
            }
            match("while");
            match("(");
            Condicion();
            match(")");
            match(";");
        }

        //For -> for(Asignacion Condicion; Incremento) Bloque de instrucciones | instruccion
        private void For()
        {
            match("for");
            match("(");
            Asignacion();
            Condicion();
            match(";");
            Incremento();
            match(")");
            if(getContenido() == "{")
            {
                bloqueInstrucciones();
            }
            else
            {
                instruccion();
            }
        }

        //Incremento -> identificador ++ | --                                                                      
        private void Incremento()
        {
            match(tipos.identificador);
            if(getContenido()=="+")
            {
                match("++");
            }
            else
            {
                match("--");
            }

        }

        //Swich -> swich(Expresion) {Lista de casos}                                                                
        private void Switch()
        {
           match("switch");
            match("(");
            Expresion();
            match(")");
            match("{");
            ListaDeCasos();
            if(getContenido() == "default")
            {
                match("default");
                match(":");
                ListaInstruccionesCase();
                if(getContenido() == "break")
                {
                    match("break");
                     match(tipos.fin_sentencia);
                }
            }
            match("}");
        }
        //Requerimiento 5: Programar la produccion listaDeCasos -> case Expresion: listaInstruccionesCase (break;)? (listaDeCasos)?           
        private void ListaDeCasos()
        {
            match("case");
            Expresion();
            match(":");
            ListaInstruccionesCase();
            if(getContenido() == "break") 
            {
                match("break");
                match(tipos.fin_sentencia);
            }      
            if(getContenido() == "case")
            {
                ListaDeCasos();
            }
        }

        private void ListaInstruccionesCase()
        {
            instruccion();
            if (getContenido() != "case" && getContenido() != "default" && getContenido() != "}" && getContenido() != "break")
            {
                ListaInstruccionesCase();
            }
        }

        //Condicion -> Expresion OperadorRelacional Expresion
        private void Condicion()
        {
            Expresion();
            match(tipos.operador_relacional);
            Expresion();
        }

        // If -> if(Condicion) Bloque de instrucciones (Else bloqueInstrucciones)?
        private void If()
        {
            match("if");
            match("(");
            Condicion();
            match(")");
            if(getContenido() == "{")
                bloqueInstrucciones();
            else
                instruccion();
            if(getContenido() == "else")
            {
                match("else");
                if(getContenido() == "{")
                {
                    bloqueInstrucciones();
                }
                else
                {
                    instruccion();
                }
            }
        }

        // Printf -> printf(cadena);
        private void Printf()
        {
            match("printf");
            match("(");
            match(tipos.cadena);
            match(")");
            match(tipos.fin_sentencia);
        }

        // Scanf -> scanf(cadena);
        private void Scanf()
        {
            match("scanf");
            match("(");
            match(tipos.cadena);
            match(")");
            match(tipos.fin_sentencia);
        }

        // Main -> void main() Bloque de instrucciones
        private void Main()
        {
            match("void");
            match("main");
            match("(");
            match(")");
            bloqueInstrucciones();
        }
        
        // Expresion -> Termino MasTermino
        private void Expresion()
        {
            Termino();
            MasTermino();
        }
        // MasTermino -> (OperadorTermino Termino)?
        private void MasTermino()
        {
            if(getClasificacion() == tipos.operador_termino)
            {
                match(tipos.operador_termino);
                Termino();
            }
        }
        // Termino -> Factor PorFactor
        private void Termino()
        {
            Factor();
            PorFactor();
        }
        // PorFactor -> (OperadorFactor Factor)?
        private void PorFactor()
        {
            if(getClasificacion() == tipos.operador_factor)
            {
                match(tipos.operador_factor);
                Factor();
            }
        }
        // Factor -> numero | identificador | (Expresion)
        private void Factor()
        {
            if(getClasificacion() == tipos.numero)
            {
                match(tipos.numero);
            }
            else if(getClasificacion() == tipos.identificador)
            {
                match(tipos.identificador);
            }
            else 
            {
                match("(");
                Expresion();
                match(")");
            }
        }
    }
}