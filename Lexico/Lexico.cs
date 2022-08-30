namespace Lexico
{
        public class Lexico : Token
    {
        StreamReader Archivo; 
        StreamWriter Log;
        public Lexico() 
        
        { 
            Archivo = new StreamReader("C:\\Users\\oscar\\Desktop\\Lenguajes y Automatas\\Lexico\\Prueba.cpp");
            Log     = new StreamWriter("C:\\Users\\oscar\\Desktop\\Lenguajes y Automatas\\Lexico\\Prueba.log");
            Log.AutoFlush = true;
        }

        public void Cerrar()
        {
            Archivo.Close();
            Log.Close();
        }

        public void NextToken()
        {
            char c;
            string Buffer = "";

            while(char.IsWhiteSpace(c = (char) Archivo.Read()));

            bool impClasi = true;    // Aqui se inicia la variable Bool

            if(char.IsLetter(c))
            {
                Buffer += c;
                while(char.IsLetterOrDigit(c = (char) Archivo.Peek()))
                {
                    Buffer+=c;
                    Archivo.Read();
                }
                setClasificacion(Tipos.Identicador);
            }

            else if(char.IsDigit(c))
            {
                Buffer += c;
                while(char.IsDigit(c = (char) Archivo.Peek()))
                {
                    Buffer+=c;
                    Archivo.Read();
                }
                if (c=='.')
                {
                    Buffer+=c;
                    Archivo.Read();
                    if (char.IsDigit(c = (char) Archivo.Peek())) 
                    {
                        while(char.IsDigit(c = (char) Archivo.Peek()))
                            {
                                Buffer+=c;
                                Archivo.Read();
                            }
                    }
                    else 
                    {
                        Console.WriteLine("Error lexico: Se espera un digito");
                        Log.WriteLine("Error lexico: Se espera un digito");
                    }
                }
                if (c=='E' || c=='e')
                {
                    Buffer+=c;
                    Archivo.Read();
                    if((c = (char) Archivo.Peek()) == '+')
                    {
                        Buffer+=c;
                        Archivo.Read();
                    }
                    else if((c = (char) Archivo.Peek()) == '-')
                    {
                        Buffer+=c;
                        Archivo.Read();
                    }
                }
                while(char.IsDigit(c = (char) Archivo.Peek()))
                {
                    Buffer+=c;
                    Archivo.Read();
                }
                setClasificacion(Tipos.Numero);
            }

            else if(c == ';')
            {
                Buffer+=c;
                setClasificacion(Tipos.FinSentencia);
            } 

            else if(c == '=')
            {
                Buffer+=c;
                setClasificacion(Tipos.Asignacion);
                if((c = (char) Archivo.Peek()) == '=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorRelacional);
                    Archivo.Read();
                }
            }

            else if(c == '*' || c=='%')
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorFactor);
                if((c = (char) Archivo.Peek()) == '=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.IncrementoFactor);
                    Archivo.Read();
                }
            }

            else if(c == '+' )
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorTermino);
                if((c = (char) Archivo.Peek()) == '=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorTermino);
                    Archivo.Read();
                }
                if((c = (char) Archivo.Peek()) == '+')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorTermino);
                    Archivo.Read();
                }
            }

            else if(c=='-')
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorTermino);
                if((c = (char) Archivo.Peek()) == '=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorTermino);
                    Archivo.Read();
                }
                if((c = (char) Archivo.Peek()) == '-')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorTermino);
                    Archivo.Read();
                }
            }
            
            else if(c == '&')
            {
                Buffer+=c;
                setClasificacion(Tipos.Caracter);
                if((c = (char) Archivo.Peek()) == '&')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorLogico);
                    Archivo.Read();
                }
            }

            else if(c == '|')
            {
                Buffer+=c;
                setClasificacion(Tipos.Caracter);
                if((c = (char) Archivo.Peek()) == '|')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorLogico);
                    Archivo.Read();
                }
            }

            else if(c == '!')
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorLogico);
                if((c = (char) Archivo.Peek()) == '=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorRelacional);
                    Archivo.Read();
                }
            }

            else if(c == '>')
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorRelacional);
                if((c = (char) Archivo.Peek()) == '=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorRelacional);
                    Archivo.Read();
                }
            }

            else if(c == '<')
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorRelacional);
                if((c = (char) Archivo.Peek()) == '=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.OperadorRelacional);
                    Archivo.Read();
                }
            }

            else if(c == '"')
            {
                Buffer+=c;
                setClasificacion(Tipos.cadena);
                while((c = (char) Archivo.Read())!= '"' )
                    {
                        Buffer+=c;
                                              
                    }
                Buffer+=c;
            }


            else if(c == '?')
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorTernario);
            } 

            /////////////////////////////////////////////////////////////////////////////////////////

            else if(c=='/')
            {
                Buffer+=c;
                setClasificacion(Tipos.OperadorFactor);
                if((c = (char) Archivo.Peek()) == '=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.IncrementoFactor);
                    Archivo.Read();
                }
                else if((c = (char) Archivo.Peek()) == '/')
                {
                    while((c = (char) Archivo.Read())!= (10) && FinArchivo()==false)
                    {
                        impClasi = false;  //Aqui paso a false la variable
                        Buffer = "";          
                    }      
                                
                }
            } 
            else if(c == ':' )
            {
                Buffer+=c;
                setClasificacion(Tipos.Caracter);
                if((c = (char) Archivo.Peek()) == '=')
                {
                    Buffer+=c;
                    setClasificacion(Tipos.Inicializacion);
                    Archivo.Read();
                }
                
            }

            else
            {
                Buffer+=c;
                setClasificacion(Tipos.Caracter);
            }
            
            if (impClasi==true)  //Agregas este if, ya que si la varible esta en positivo  
            {                    // nunca entro al while de si es comentario
            setContenido(Buffer);
            Log.WriteLine(getContenido() + " | " + getClasificacion());
            }
            else                // Si la variable esta en false no se manda nada a setContenido 
            {                   // no imprime nada

            }
        }

        public bool FinArchivo()
        {
            return Archivo.EndOfStream;
        }
    }
}