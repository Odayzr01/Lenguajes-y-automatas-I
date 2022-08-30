using System;

namespace Sintaxis1
{
    public class Lexico : Token
    {
        StreamReader archivo;
        protected StreamWriter log;
        const int F = -1;
        const int E = -2;
        protected int linea;
        string datetime = DateTime.Now.ToString("hh:mm:ss tt");
        string Date = DateTime.Now.ToString("dd/MM/yyyy");
        int[,] trand = new int[,]
        {
            //WS,EF,EL,L, D, .,	E, +, -, =,	:, ;, &, |,	!, >, <, *,	%, /, ", ?, #, ',La
            { 0, F, 0, 1, 2,33, 1,21,22, 8,10,12,13,14,15,18,19,24,24,29,26,28,33,37,40}, //estado 0
            { F, F, F, 1, 1, F, 1, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 1
            { F, F, F, F, 2, 3, 5, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 2
            { E, E, E, E, 4, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E}, //estado 3
            { F, F, F, F, 4, F, 5, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 4
            { E, E, E, E, 7, E, E, 6, 6, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E}, //estado 5
            { E, E, E, E, 7, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E}, //estado 6
            { F, F, F, F, 7, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 7
            { F, F, F, F, F, F, F, F, F, 9, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 8
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 9
            { F, F, F, F, F, F, F, F, F,11, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 10
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 11
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 12
            { F, F, F, F, F, F, F, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 13
            { F, F, F, F, F, F, F, F, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F}, //estado 14
            { F, F, F, F, F, F, F, F, F,17, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 15
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 16
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 17
            { F, F, F, F, F, F, F, F, F,20, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 18
            { F, F, F, F, F, F, F, F, F,20, F, F, F, F, F,20, F, F, F, F, F, F, F, F, F}, //estado 19
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 20
            { F, F, F, F, F, F, F,23, F,23, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 21
            { F, F, F, F, F, F, F, F,23,23, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 22
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 23
            { F, F, F, F, F, F, F, F, F,25, F, F, F, F, F, F, F, F, F, F, F, F ,F, F, F}, //estado 24
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 25
            {26, E,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,27,26,26,26,26}, //estado 26
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 27
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 28
            { F, F, F, F, F, F, F, F, F,25, F, F, F, F, F, F, F,31, F,30, F, F, F, F, F}, //estado 29
            {30, 0, 0,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30}, //estado 30
            {31, E,31,31,31,31,31,31,31,31,31,31,31,31,31,31,31,32,31,31,31,31,31,31,31}, //estado 31
            {31,31,31,31,31,31,31,31,31,31,31,31,31,31,31,31,31,32,31, 0,31,31,31,31,31}, //estado 32
            { F, F, F, F,34, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 33
            { F, F, F, F,35, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 34
            { F, F, F, F,36, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 35
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 36
            {38,38,38,38,38,38,38,38,38,38,38,38,38,38,38,38,38,38,38,38,38,38,38,38,38}, //estado 37
            { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E,39, E}, //estado 38
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 39
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}  //estado 40
            //WS,EF,EL,L, D, .,	E, +, -, =,	:, ;, &, |,	!, >, <, *,	%, /, ", ?, #, ',La
        };
        public Lexico()
        {
            linea = 1;
            log = new StreamWriter("C:\\Users\\oscar\\Desktop\\Lenguajes y Automatas\\Sintaxis1\\Prueba.log");
            log.AutoFlush = true;
            log.WriteLine("Primer constructor");
            log.WriteLine("Archivo: prueba.cpp");
            log.WriteLine("Compilado:  "+Date+" "+datetime); //Requerimiento 1
            //Investigar como checar si un archivo existe o no existe
            string path = "C:\\Users\\oscar\\Desktop\\Lenguajes y Automatas\\Sintaxis1\\Prueba.cpp";
            bool result = File.Exists(path);
            if(result == true)
            {
                archivo = new StreamReader("C:\\Users\\oscar\\Desktop\\Lenguajes y Automatas\\Sintaxis1\\Prueba.cpp");
                
            }
            else
            {
                throw new Error("Error: El archivo prueba no existe", log);
            }
        }

        public Lexico(string nombre)
        {
            string FileName = nombre;
            string result2;
            result2 = Path.ChangeExtension(FileName, ".log");
            linea = 1;
            log = new StreamWriter(result2);
            log.AutoFlush = true;
            log.WriteLine("Segundo constructor");
            log.WriteLine("Archivo: "+nombre);   
            log.WriteLine("Compilado:  "+Date+" "+datetime); //Requerimiento 1

            //Investigar como checar si un archivo existe o no existe
            string path = nombre;
            bool result = File.Exists(path);
            if(result == true)
            {
                archivo = new StreamReader(nombre);
                
            }
            else
            {
                throw new Error("Error: El archivo prueba no existe", log);
            }
        }        
        public void Cerrar()
        {
            archivo.Close();
            log.Close();
        }

        private void clasifica(int estado)
        {
            switch(estado)
            {
                case 1:
                    setClasificacion(tipos.identificador);
                    break;
                case 2:
                    setClasificacion(tipos.numero);
                    break;
                case 8:
                    setClasificacion(tipos.asignacion);
                    break;
                case 9:
                case 17:
                case 18:
                case 19:
                    setClasificacion(tipos.operador_relacional);
                    break;
                case 10:
                case 13:
                case 14:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 39:
                case 40:
                    setClasificacion(tipos.caracter);
                    break;
                case 11:
                    setClasificacion(tipos.inicializacion);
                    break;
                case 12:
                    setClasificacion(tipos.fin_sentencia);
                    break;
                case 15:
                case 16:
                    setClasificacion(tipos.operador_logico);
                    break;
                case 21:
                case 22:
                    setClasificacion(tipos.operador_termino);
                    break;
                case 23:
                    setClasificacion(tipos.incremento_termino);
                    break;   
                case 24:
                case 29:
                    setClasificacion(tipos.operador_factor);
                    break;
                case 25:
                    setClasificacion(tipos.incremento_factor);
                    break;
                case 26:
                    setClasificacion(tipos.cadena);
                    break;
                case 28:
                    setClasificacion(tipos.operador_ternario);
                    break;
            }
        }
        private int columna(char c)
        {
            //WS,EF,EL,L, D, .,	E, +, -, =,	:, ;, &, |,	!, >, <, *,	%, /, ", ?,La
            if(FinArchivo())
                return 1;
            else if(c == '\n')
                return 2;
            else if(char.IsWhiteSpace(c))
                return 0;
            else if(char.ToUpper(c) == 'E')
                return 6;
            else if(char.IsLetter(c))
                return 3;
            else if(char.IsDigit(c))
                return 4;
            else if(c == '.')
                return 5;
            else if(c == '+')
                return 7;
            else if(c == '-')
                return 8;
            else if(c == '=')
                return 9;
            else if(c == ':')
                return 10;
            else if(c == ';')
                return 11;
            else if(c == '&')
                return 12;
            else if(c == '|')
                return 13;
            else if(c == '!')
                return 14;
            else if(c == '>')
                return 15;
            else if(c == '<')
                return 16;
            else if(c == '*')
                return 17;
            else if(c == '%')
                return 18;
            else if(c == '/')
                return 19;
            else if(c == '"')
                return 20;
            else if(c == '?')
                return 21;
            else if(c == '#')
                return 22;
            else if(c == '\'')
                return 23;
            else
                return 24;
        }
        //WS,EF,EL,L, D, .,	E, +, -, =,	:, ;, &, |,	!, >, <, *,	%, /, ", ?,#,',La
        public void NextToken() 
        {
            string buffer = "";           
            char c;      
            int estado = 0;
            while(estado >= 0)
            {
                c = (char)archivo.Peek(); //Funcion de transicion
                estado = trand[estado,columna(c)];
                clasifica(estado);
                if (estado >= 0)
                {

                    archivo.Read();
                    if(c=='\n')
                    {
                        linea++;
                    }
                    if (estado >0)
                    {
                        buffer += c;
                    }
                    else
                    {
                        buffer = "";
                    }
                }
            }
            setContenido(buffer); 
            switch(buffer)
            {
                case "char":
                case "int":
                case "float":
                        setClasificacion(tipos.tipo_datos);
                        break;
                case "private":
                case "protected":
                case "public":
                        setClasificacion(tipos.zona);
                        break;
                case "if":
                case "else":
                case "switch":
                        setClasificacion(tipos.condicion);
                        break;
                case "while":
                case "for":
                case "do":
                        setClasificacion(tipos.ciclo);
                        break;
            }
            if(estado == E)
            {
                // Requerimiento 9: Agregar el numero de linea en el error
                if(getContenido()[0] == '"')
                {
                    throw new Error("Error Lexico: No se cerro la cadena con \".    Numero de Linea: "+linea, log);
                }
                else if(getContenido()[0] == '\'')
                {
                    throw new Error("Error Lexico: No se cerro el caracter con comilla simple.  Numero de Linea: "+linea, log);
                }
                else if(getClasificacion() == tipos.numero)
                {
                    throw new Error("Error Lexico: Se espera un digito.     Numero de Linea: "+linea, log);
                }
                else
                {
                    throw new Error("Error Lexico: No definido.     Numero de linea: "+linea, log);
     
                }
            }
            else if (!FinArchivo())
            {
                log.WriteLine(getContenido() + " | " + getClasificacion());        
            }
        }
        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}