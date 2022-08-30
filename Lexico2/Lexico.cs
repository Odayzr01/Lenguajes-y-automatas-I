namespace Lexico2
{
    public class Lexico2 : Token
    {
        StreamReader Archivo;
        StreamWriter Log;
        public Lexico2()

        {
            Archivo = new StreamReader("C:\\Users\\oscar\\Desktop\\Lenguajes y Automatas\\Lexico2\\Prueba.cpp");
            Log = new StreamWriter("C:\\Users\\oscar\\Desktop\\Lenguajes y Automatas\\Lexico2\\Prueba.log");
            Log.AutoFlush = true;
        }

        public void Cerrar()
        {
            Archivo.Close();
            Log.Close();
        }

        private void clasifica(int estado)
        {
            switch (estado)
            {
                case 1:
                    setClasificacion(Tipos.Identicador);
                    break;
                case 2:
                    setClasificacion(Tipos.Numero);
                    break;
                case 8:
                    setClasificacion(Tipos.Asignacion);
                    break;
                case 9:
                case 17:
                case 18:
                case 19:
                    setClasificacion(Tipos.OperadorRelacional);
                    break;
                case 10:
                case 13:
                case 14:
                    setClasificacion(Tipos.Caracter);
                    break;
                case 11:
                    setClasificacion(Tipos.Inicializacion);
                    break;
                case 12:
                    setClasificacion(Tipos.FinSentencia);
                    break;    
                case 15:
                case 16:
                    setClasificacion(Tipos.OperadorLogico);
                    break;
                case 21:
                case 22:
                    setClasificacion(Tipos.OperadorTermino);
                    break;
                case 23:
                    setClasificacion(Tipos.IncrementoTermino);
                    break;
                case 24:
                case 29:
                    setClasificacion(Tipos.OperadorFactor);
                    break;
                case 25:
                    setClasificacion(Tipos.IncrementoFactor);
                    break;
                case 26:
                    setClasificacion(Tipos.cadena);
                    break;
                case 28:
                    setClasificacion(Tipos.OperadorTernario);
                    break;
                case 33:
                    setClasificacion(Tipos.Caracter);
                    break;

                
            }
        }

        public void NextToken()
        {
            string Buffer = "";
            char c;
            int estado = 0;
            const int f = -1, e = -2;

            while (estado >= 0)
            {
                c = (char)Archivo.Peek();  //FUNCION DE TRANCISION
                estado = Automata(estado, c, f, e);
                clasifica(estado);
                if (estado >= 0)
                {
                    Archivo.Read();
                    if (estado > 0)
                    {
                        Buffer += c;
                    }
                    else
                    {
                        Buffer="";
                    }
                }

            }
            setContenido(Buffer);

            switch(Buffer)
            {
                case "char":
                case "int":
                case "float":
                    setClasificacion(Tipos.TipoDatos);
                break;
                case "private":
                case "protected":
                case "public":
                    setClasificacion(Tipos.Zona);
                break;
                case "if":
                case "else":
                case "switch":
                    setClasificacion(Tipos.Condicion);
                    break;
                case "while":
                case "for":
                case "do":
                    setClasificacion(Tipos.Ciclo);
                break;
                
            }




            Log.WriteLine(getContenido() + " | " + getClasificacion());

        }


        private int Automata(int estado, char t, int f, int e)
        {
            switch (estado)
            {
                case 0:
                    
                    if (char.IsLetter(t))
                    {
                        estado = 1;
                    }
                    else if(char.IsDigit(t))
                    {
                        estado=2;
                    }
                    else if(t=='=')
                    {
                        estado=8;
                    }
                    else if(t==':')
                    {
                        estado=10;
                    }
                    else if(t==';')
                    {
                        estado=12;
                    }
                    else if(t=='&')
                    {
                        estado=13;
                    }
                    else if(t=='|')
                    {
                        estado=14;
                    }
                    else if(t=='!')
                    {
                        estado=15;
                    }
                    else if(t=='>')
                    {
                        estado=18;
                    }
                    else if(t=='<')
                    {
                        estado=19;
                    }
                    else if(t=='+')
                    {
                        estado=21;
                    }
                    else if(t=='-')
                    {
                        estado=22;
                    }
                    else if(t=='%'|| t=='*')
                    {
                        estado=24;
                    }
                    else if(t=='"')
                    {
                        estado=26;
                    }
                    else if(t=='?')
                    {
                        estado=28;
                    }
                    else if(t=='/')
                    {
                        estado=29;
                    }
                    else if (!char.IsWhiteSpace(t))
                    {
                        estado=33;
                    }
                    break;
                    
                case 1:
                    if (!char.IsLetter(t))
                    {
                        estado = f;
                    }

                    break;
                case 2:
                    if(t=='.')
                    {
                        estado=3;
                    }
                    else if(t=='e' || t=='E')
                    {
                        estado=5;
                    }
                    else if (!char.IsDigit(t))
                    {
                        estado = f;
                    }
                    break;
                case 3:
                    if (char.IsDigit(t))
                    {
                        estado = 4;
                    }
                    else
                    {
                        estado=e;
                    }
                    break;
                case 4:
                    if(t=='e' || t=='E')
                    {
                        estado=5;
                    }
                    else if (!char.IsDigit(t))
                    {
                        estado=f;
                    }

                    break;
                case 5:
                    if(t=='+' || t=='-')
                    {
                        estado=6;
                    }
                    else if (char.IsDigit(t))
                    {
                        estado=7;
                    }
                    else
                    {
                        estado=e;
                    }
                    break;
                case 6:
                    if (char.IsDigit(t))
                    {
                        estado=7;
                    }
                    else
                    {
                        estado=e;
                    }

                    break;
                case 7:
                    if (!char.IsDigit(t))
                    {
                        estado=f;
                    }
                    break;
                case 8:
                    if(t=='=')
                    {
                        estado=9;
                    }
                    else
                    {
                        estado=f;
                    }
                    break;
                case 9:
                case 11:
                case 12:
                case 16:
                case 17:
                case 20:
                case 23:
                case 25:
                case 27:
                case 28:
                case 33:
                    estado=f;
                    break;
                case 10:
                    if(t=='=')
                    {
                        estado=11;
                    }
                    else 
                    {
                        estado=f;
                    }
                    break;
                case 13:
                    if (t=='&')
                    {
                        estado=16;
                    }
                    else{
                        estado=f;
                    }
                    break;
                case 14:
                if (t=='|')
                    {
                        estado=16;
                    }
                    else{
                        estado=f;
                    }
                    break;
                case 15:
                if (t=='=')
                    {
                        estado=17;
                    }
                else
                {
                    estado=f;
                }
                    break;
                case 18:
                if (t=='=')
                {
                    estado=20;
                }
                else
                {
                    estado=f;
                }
                    break;
                case 19:
                if (t=='=' || t=='>')
                {
                    estado=20;
                }
                else
                {
                    estado=f;
                }
                    break;
                
                case 21:
                if (t=='=' || t=='+')
                {
                    estado=23;
                }
                else
                {
                    estado=f;
                }
                    break;
                case 22:
                if (t=='=' || t=='-')
                {
                    estado=23;
                }
                else
                {
                    estado=f;
                }
                    break;

                case 24:
                if (t=='=')
                {
                    estado=25;
                }
                else
                {
                    estado=f;
                }
                    break;
                
                case 26:
                if (t==10)
                {
                    estado= e;
                }
                else if(t=='"')
                {
                    estado = 27;
                }
                else
                {
                    estado=26;
                }
                    break;
        
                case 29:
                    if (t == '=')
                    {
                        estado = 25;
                    }
                    else if (t == '*')
                    {
                        estado = 31;
                    }
                    else if (t == '/')
                    {
                        estado = 30;
                    }
                    else
                    {
                        estado = f;
                    }
                    break;
                case 30:
                    if (t == (10) || FinArchivo() == true)
                    {
                        estado = 0;
                    }
                    else
                    {
                        estado = 30;
                    }
                    break;
                case 31:
                    if (t == '*')
                    {
                        estado = 32;
                    }
                    else if (FinArchivo() == true)
                    {
                        estado = e;
                    }
                    else
                    {
                        estado = 31;
                    }
                    break;
                case 32:
                    if (t == '/')
                    {
                        estado = 0;
                    }
                    else if (t == '*')
                    {
                        estado = 32;
                    }
                    else if (FinArchivo() == true)
                    {
                        estado = e;
                    }
                    break;
            }
            return estado;
        }


        public bool FinArchivo()
        {
            return Archivo.EndOfStream;
        }
    }
}